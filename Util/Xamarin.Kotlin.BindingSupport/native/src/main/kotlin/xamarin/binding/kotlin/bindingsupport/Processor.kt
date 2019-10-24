@file:Suppress("RemoveCurlyBracesFromTemplate")

package xamarin.binding.kotlin.bindingsupport

import kotlinx.metadata.*
import kotlinx.metadata.jvm.*
import nu.xom.*
import java.io.File
import java.lang.reflect.*
import java.net.URLClassLoader
import kotlin.reflect.KFunction
import kotlin.reflect.KVisibility
import kotlin.reflect.jvm.kotlinFunction
import kotlin.reflect.jvm.kotlinProperty

class Processor(xmlFile: File, jarFiles: List<File>, outputFile: File?, ignoreFiles: List<File>) {

    private fun expressionClasses() =
        "/api/package/*[local-name()='class' or local-name()='interface']"

    private fun expressionSpecificClass(xpackage: String, xclasstype: String, xname: String) =
        "/api/package[@name='${xpackage}']/${xclasstype}[@name='${xname}']"

    private fun expressionMember(xpackage: String, xclasstype: String, xname: String, xtype: String) =
        "/api/package[@name='${xpackage}']/${xclasstype}[@name='${xname}']/${xtype}"

    private fun expressionCompanion() =
        "/api/package/class/field[@type=concat(../../@name,'.',../@name,'.',@name)]"

    private val invalidKeywords = arrayOf<(String) -> Boolean>(
        { x -> x.endsWith("\$default") }, // Kotlin 1.2: method overloads with default parameters
        { x -> x.endsWith("\$annotations") },
        { x -> x.endsWith("\$kotlin_stdlib") },
        { x -> x.startsWith("\$EnumSwitchMapping\$") },
        { x -> x.startsWith("access\$") } // Kotlin 1.2: private methods
    )

    var companions: CompanionProcessing = CompanionProcessing.Default
    var verbose: Boolean = false

    private val loader: URLClassLoader
    private val xapidoc: Document

    private val outputFile: File?
    private val xtransformsdoc: Document

    private val ignored: List<String>

    init {
        val urls = jarFiles.map { file -> file.toURI().toURL() }
        loader = URLClassLoader(urls.toTypedArray())

        val builder = Builder()
        xapidoc = builder.build(xmlFile.canonicalFile)

        xtransformsdoc = Document(Element("metadata"))
        this.outputFile = outputFile

        ignored = ignoreFiles.flatMap { file ->
            // read all the lines from multiple files into a single list
            file.readLines().map { line ->
                // remove the BOM
                line.replace("\ufeff", "")
            }
        }
    }

    fun process() {
        // fix typical cases
        processSuggestedFixes()

        // process companion classes and related fields
        processCompanions()

        // remove specific cases
        processClasses()

        // finish up
        if (outputFile != null) {
            val serializer = Serializer(outputFile.outputStream())
            serializer.indent = 4
            serializer.write(xtransformsdoc)
            serializer.flush()
        }

        logVerbose("Processing complete.")
    }

    private fun processCompanions() {
        // find all the companion FIELDS
        val xcompanions = xapidoc.queryElements(expressionCompanion())

        for (xcompanionfield in xcompanions) {
            val xname = xcompanionfield.getAttributeValue("name")
            val xcontainer = xcompanionfield.parentElement
            val xcontainername = xcontainer.getAttributeValue("name")
            val xcontainertype = xcontainer.localName
            val xclassname = "${xcontainername}.${xname}"
            val xpackage = xcontainer.parentElement.getAttributeValue("name")

            val pair = getJavaClass(xpackage, xclassname)
            if (pair == null) {
                ProcessorErrors.errorResolvingJavaClass(xcontainertype, "${xpackage}.${xclassname}")
                continue
            }
            val jclass = pair.first

            val withoutJvmStatic = jclass.declaredMethods.filter { method ->
                method.declaredAnnotations.all { ann ->
                    ann.annotationClass.qualifiedName != "kotlin.jvm.JvmStatic"
                }
            }

            fun rename() {
                logVerbose("Renaming companion object \"${xpackage}.${xclassname}\" because it will clash with the related field...")
                writeAttrManagedName(
                    "/api/package[@name='${xpackage}']/class[@name='${xclassname}']",
                    "${xclassname}Static"
                )
            }

            fun remove() {
                logVerbose("Removing companion object \"${xpackage}.${xclassname}\" because all the methods are static...")
                writeRemoveNode("/api/package[@name='${xpackage}']/class[@name='${xclassname}']")
                writeRemoveNode("/api/package[@name='${xpackage}']/class[@name='${xcontainername}']/field[@name='${xname}']")
            }

            when (companions) {
                CompanionProcessing.Default -> {
                    if (withoutJvmStatic.isEmpty())
                        remove()
                    else
                        rename()
                }
                CompanionProcessing.Rename -> rename()
                CompanionProcessing.Remove -> remove()
            }
        }
    }

    private fun processSuggestedFixes() {
        // Kotlin v1.2 names the extension method parameters to $receiver
        if (xapidoc.queryElements("/api/package/class/method[count(parameter)>0 and parameter[1][@name='\$receiver']]").any()) {
            logVerbose("Renaming the \"\$receiver\" parameter names to \"parameter\"...")
            writeAttrManagedName("/api/package/class/method[parameter[1][@name='\$receiver']]/parameter[1]", "receiver")
        }
    }

    private fun processClasses() {
        val classes = xapidoc.queryElements(expressionClasses())
        for (xclass in classes) {
            val xpackage = xclass.parentElement.getAttributeValue("name")
            val xname = xclass.getAttributeValue("name")
            val xtype = xclass.localName
            val xfullname = "${xpackage}.${xname}"

            // make sure we haven't been told to ignore this
            if (wasIgnored(xfullname)) {
                logVerbose("Ignoring class \"${xpackage}.${xname}\" because it was found in the ignore file...")
                continue
            }

            val removeClass = shouldRemoveClass(xclass)
            if (removeClass != ProcessResult.Ignore) {
                // this class needs to be removed for some reason
                logVerbose("Removing \"${xpackage}.${xname}\" because is not meant to be bound (${removeClass})...")
                writeRemoveNode("/api/package[@name='${xpackage}']/${xtype}[@name='${xname}']")
                continue
            }

            // extension classes are in the form *Kt which extend *Kt__*
            val xextends = xclass.getAttributeValue("extends")
            if (xtype == "class" && xname.endsWith("Kt") && !xname.contains("__") && xextends.startsWith("${xpackage}.${xname}__"))
                processExtensionClass(xclass)

            // now process all the members of the class
            processMembers(xclass)
        }
    }

    private fun processExtensionClass(xclass: Element) {
        val xpackage = xclass.parentElement.getAttributeValue("name")
        val xname = xclass.getAttributeValue("name")

        var xcurrentclass: Element? = xclass
        var xextends: String? = xcurrentclass?.getAttributeValue("extends")
        while (xextends?.isNotEmpty() == true && xextends.startsWith("${xpackage}.${xname}__")) {
            val xextname = xextends.substring(xpackage.length + 1)
            logVerbose("Updating \"${xextends}\" because is the base of an extension class...")
            writeAttr("/api/package[@name='${xpackage}']/class[@name='${xextname}']", "visibility", "public")

            xcurrentclass = xapidoc.queryElements(expressionSpecificClass(xpackage, "class", xextname)).firstOrNull()
            xextends = xcurrentclass?.getAttributeValue("extends")
        }
    }

    private fun processMembers(xclass: Element) {
        val xpackage = xclass.parentElement.getAttributeValue("name")
        val xname = xclass.getAttributeValue("name")
        val xtype = xclass.localName

        val pair = getJavaClass(xpackage, xname)
        if (pair == null) {
            ProcessorErrors.errorResolvingJavaClass(xtype, "${xpackage}.${xname}")
            return
        }

        val jclass = pair.first

        // remove the members that are not meant to be used
        processMembers(xclass, "constructor") {
            shouldRemoveMember(it, jclass.declaredConstructors.union(jclass.constructors.asIterable()).toTypedArray())
        }
        processMembers(xclass, "method") {
            shouldRemoveMember(it, jclass.declaredMethods.union(jclass.methods.asIterable()).toTypedArray())
        }
        processMembers(xclass, "field") {
            shouldRemoveField(it, jclass.declaredFields.union(jclass.fields.asIterable()).toTypedArray())
        }
    }

    private fun processMembers(xclass: Element, xtype: String, shouldRemove: (Element) -> ProcessResult) {
        val xpackage = xclass.parentElement.getAttributeValue("name")
        val xclassname = xclass.getAttributeValue("name")
        val xclasstype = xclass.localName

        val xmembers = xapidoc.queryElements(expressionMember(xpackage, xclasstype, xclassname, xtype))
        for (xmember in xmembers) {
            val xname = xmember.getAttributeValue("name")
            val xfullname = "${xpackage}.${xclassname}.${xname}"

            // make sure we haven't been told to ignore this
            if (wasIgnored(xfullname)) {
                logVerbose("Ignoring ${xtype} \"${xfullname}\" because it was found in the ignore file...")
                continue
            }

            val xparams = getMemberParameters(xmember, true)
            val params = listOf(
                "@name='${xname}'",
                "count(parameter)=${xparams.size}"
            ).union(
                xparams.mapIndexed { idx, p -> "parameter[${idx + 1}][@type='${p}']" }
            )
            val paramsStr = params.joinToString(" and ")
            val friendly = "${xpackage}.${xclassname}.${xname}(${xparams.joinToString(", ")}))"
            val xpath = "/api/package[@name='${xpackage}']/${xclasstype}[@name='${xclassname}']/${xtype}[${paramsStr}]"

            val result = shouldRemove(xmember)
            if (result != ProcessResult.Ignore) {
                // this member needs to be removed for some reason
                logVerbose("Removing ${xtype} \"${friendly}\" because is not meant to be bound (${result})...")
                writeRemoveNode(xpath)
            } else {
                // Kotlin 1.3: generated methods have a generated -* suffix
                val dashIndex = xname.indexOf("-")
                if (dashIndex > 0) {
                    logVerbose("Renaming ${xtype} \"${friendly}\" because is a generated method overload (${result})...")
                    val managedName = xname.substring(0, dashIndex)
                    if (managedName.length == 1)
                        writeAttrManagedName(xpath, managedName.toUpperCase())
                    else
                        writeAttrManagedName(xpath, managedName[0].toUpperCase() + managedName.substring(1))
                }

                // Kotlin 1.3: "extension methods" have their first parameter named "$this$<method-name>"
                val firstParam = xmember.getChildElements("parameter").firstOrNull()
                if (firstParam?.getAttributeValue("name")?.startsWith("\$this\$") == true) {
                    logVerbose("Renaming parameter \"${firstParam}\" because is a generated parameter name (${result})...")
                    writeAttrManagedName("${xpath}/parameter[1]", "that")
                }
            }
        }
    }

    private fun wasIgnored(fullname: String): Boolean {
        // exact match
        if (ignored.contains(fullname)) {
            return true;
        }

        // basic wildcards
        for (ignore in ignored) {
            if (ignore.startsWith("*") && ignore.endsWith("*")) {
                if (fullname.contains(ignore.trim('*')))
                    return true;
            } else if (ignore.startsWith("*")) {
                if (fullname.endsWith(ignore.trim('*')))
                    return true;
            } else if (ignore.endsWith("*")) {
                if (fullname.startsWith(ignore.trim('*')))
                    return true;
            }
        }

        // process this
        return false;
    }

    private fun shouldRemoveClass(xclass: Element): ProcessResult {
        // read the xml
        val xpackage = xclass.parentElement.getAttributeValue("name")
        val xname = xclass.getAttributeValue("name")
        val xtype = xclass.localName

        logVerbose("Checking the class \"${xpackage}.${xname}\"...")

        var lastPeriod = xname.lastIndexOf(".")

        // make sure the class is not a generated digit for anonymous classes
        if (lastPeriod != -1) {
            val xinnername = xname.substring(lastPeriod + 1)
            if (xinnername.toIntOrNull() != null)
                return ProcessResult.RemoveGenerated
        }

        // make sure that the parent class is visible
        while (lastPeriod != -1) {
            val xparentname = xname.substring(0, lastPeriod)
            val parentClasses = xapidoc.queryElements(expressionSpecificClass(xpackage, xtype, xparentname))
            if (parentClasses.size == 1) {
                logVerbose("Checking the parent class \"${xpackage}.${xparentname}\"...")

                val removeParent = shouldRemoveClass(parentClasses.first())
                if (removeParent != ProcessResult.Ignore)
                    return ProcessResult.RemoveInternalParent
            } else if (parentClasses.size > 1) {
                ProcessorErrors.multipleClassesInXml("${xpackage}.${xparentname}")
            }
            lastPeriod = xparentname.lastIndexOf(".")
        }

        // optional
        val xextends = xclass.getAttributeValue("extends")

        // some things we know are not good to check
        if (xextends == "java.lang.Enum")
            return ProcessResult.Ignore

        // now check this type and see what we can do
        val pair = getJavaClass(xpackage, xname)

        // this class could not be loaded, so do nothing
        if (pair == null) {
            ProcessorErrors.errorResolvingJavaClass(xtype, "${xpackage}.${xname}")
            return ProcessResult.Ignore
        }
        val (jclass, xfullname) = pair

        try {
            val kclass = jclass.kotlin

            // determine if this is some internal class
            if (kclass.visibility != KVisibility.PUBLIC)
                return ProcessResult.RemoveInternal
        } catch (ex: Throwable) {
            if (ex.message!!.contains("This class is an internal synthetic class generated by the Kotlin compiler")) {
                return ProcessResult.RemoveGenerated
            } else if (ex.message!!.contains("Packages and file facades are not yet supported in Kotlin reflection")) {
                if (verbose)
                    ProcessorErrors.unableToResolveKotlinClass(xtype, xfullname, ex)
            } else if (ex.message!!.contains("Unresolved class:")) {
                if (verbose)
                    ProcessorErrors.unableToResolveKotlinClass(xtype, xfullname, ex)
            } else {
                ProcessorErrors.errorInspectingKotlinClass(xtype, xfullname, ex)
            }
        }

        // this class is public
        return ProcessResult.Ignore
    }

    private fun shouldRemoveMember(xmember: Element, jmembers: Array<out Executable>): ProcessResult {
        val xclass = xmember.parentElement
        val xclassname = xclass.getAttributeValue("name")
        val xpackage = xclass.parentElement.getAttributeValue("name")
        val xname = xmember.getAttributeValue("name")
        val xtype = xmember.localName
        val xfullname = "${xpackage}.${xclassname}.${xname}"

        // before doing any complex checks, make sure it is not an invalid member
        if (invalidKeywords.any { check -> check(xname) })
            return ProcessResult.RemoveGenerated

        // ignore already hidden items
        val xvisibility = xmember.getAttributeValue("visibility")
        if (xvisibility != "public" && xvisibility != "protected")
            return ProcessResult.RemoveJavaInternal

        // some things we know are not good to check
        val xextends = xclass.getAttributeValue("extends")
        if (xextends == "java.lang.Enum")
            return ProcessResult.Ignore

        // try and find this member in the Java object
        val jmember = resolveMember(xmember, jmembers)

        // there was a problem loading this member
        if (jmember == null) {
            ProcessorErrors.errorResolvingJavaMember(xtype, xfullname)
            return ProcessResult.Ignore
        }

        // Kotlin 1.3: methods with the -impl suffix are internal, unless they have the @PublishedApi annotation
        if (xname.contains("-impl") && jmember.declaredAnnotations.all { it.annotationClass.qualifiedName != "kotlin.PublishedApi" })
            return ProcessResult.RemoveImplementation

        // try the declared visibility in Kotlin
        try {
            val kmember: KFunction<*>?
            when (jmember) {
                is Constructor<*> -> kmember = jmember.kotlinFunction
                is Method -> kmember = jmember.kotlinFunction
                else -> kmember = null
            }
            if (kmember != null) {
                return when (kmember.visibility) {
                    KVisibility.PUBLIC, KVisibility.PROTECTED -> ProcessResult.Ignore
                    else -> ProcessResult.RemoveInternal
                }
            }
        } catch (ex: Throwable) {
            if (ex.message!!.contains("Packages and file facades are not yet supported in Kotlin reflection.")) {
                if (verbose)
                    ProcessorErrors.unableToResolveKotlinMember(xtype, xfullname, ex)
            } else if (ex.message!!.startsWith("Unknown origin of ")) {
                if (verbose)
                    ProcessorErrors.unableToResolveKotlinMember(xtype, xfullname, ex)
            } else {
                ProcessorErrors.errorInspectingKotlinMember(xtype, xfullname, ex)
            }
        }

        if (xtype == "method") {
            // it may be a generated method from a property
            val propVisibility = getJvmPropertyVisibility(jmember)
            if (propVisibility != null) {
                return when (propVisibility) {
                    KVisibility.PUBLIC, KVisibility.PROTECTED -> ProcessResult.Ignore
                    else -> ProcessResult.RemoveInternal
                }
            }
        } else if (xtype == "constructor") {
            // it may be a generated default constructor
            if (jmember.isSynthetic && jmember.parameterTypes.lastOrNull()?.normalizedName == "kotlin.jvm.internal.DefaultConstructorMarker") {
                return ProcessResult.RemoveGenerated
            }
        }

        // check the default overloads
        val memberVisibility = getJvmMethodVisibility(jmember)
        if (memberVisibility != null) {
            return when (memberVisibility) {
                KVisibility.PUBLIC, KVisibility.PROTECTED -> ProcessResult.Ignore
                else -> ProcessResult.RemoveInternal
            }
        }

        // kotlin can't read this one
        ProcessorErrors.unableToResolveKotlinMember(xtype, xfullname)
        return ProcessResult.Ignore
    }

    private fun shouldRemoveField(xmember: Element, jmembers: Array<Field>): ProcessResult {
        val xclass = xmember.parentElement
        val xclassname = xclass.getAttributeValue("name")
        val xpackage = xclass.parentElement.getAttributeValue("name")
        val xname = xmember.getAttributeValue("name")
        val xtype = xmember.getAttributeValue("type")
        val xfullname = "${xpackage}.${xclassname}.${xname}"

        // before doing any complex checks, make sure it is not an invalid member
        if (invalidKeywords.any { check -> check(xname) }) {
            return ProcessResult.RemoveGenerated
        }

        // match the field
        val jfield = jmembers.firstOrNull { jm -> jm.name == xname && jm.type.normalizedName == xtype }

        // there was a problem loading this field
        if (jfield == null) {
            ProcessorErrors.errorResolvingJavaMember("field", xfullname)
            return ProcessResult.Ignore
        }

        try {
            val kfield = jfield.kotlinProperty

            // there was a problem loading this field
            if (kfield == null) {
                ProcessorErrors.unableToResolveKotlinMember("field", xfullname)
                return ProcessResult.Ignore
            }

            // check to see if it is visible
            if (kfield.visibility != KVisibility.PUBLIC && kfield.visibility != KVisibility.PROTECTED)
                return ProcessResult.RemoveInternal
        } catch (ex: Exception) {
            ProcessorErrors.errorInspectingKotlinMember("field", xfullname, ex)
        } catch (ex: Throwable) {
            // TODO: https://youtrack.jetbrains.com/issue/KT-22923
            if (ex.message!!.contains("Unknown origin") && ex.message!!.contains("fun clone()") && ex.message!!.contains("kotlin.Cloneable")) {
                if (verbose)
                    ProcessorErrors.unableToResolveKotlinMember(xtype, xfullname, ex)
            } else {
                ProcessorErrors.errorInspectingKotlinMember("field", xfullname, ex)
            }
        }

        return ProcessResult.Ignore
    }

    private fun getMemberParameters(xmember: Element, preserve: Boolean = false): Array<String> {
        val jparameters = mutableListOf<String>()
        val xparameters = xmember.getChildElements("parameter")
        for (xparam in xparameters) {
            var xparamtype = xparam.getAttributeValue("type")

            if (preserve) {
                jparameters.add(xparamtype)
            } else {
                // remove generic
                xparamtype = removeGenerics(xparamtype)

                // remove ellipsis
                jparameters.add(xparamtype.replace("...", "[]"))
            }
        }
        return jparameters.toTypedArray()
    }

    private fun getTypeParameters(xclass: Element, xmember: Element): MutableMap<String, String> {
        val typeparams = mutableMapOf<String, String>()

        fun merge(xtypeparameters: List<Element>) {
            for (xparam in xtypeparameters) {
                val name = xparam.getAttributeValue("name")
                val xgen = xparam.queryElements("genericConstraints/genericConstraint")
                var xtype = xgen.firstOrNull()?.getAttributeValue("type")

                // remove generic
                if (xtype != null)
                    xtype = removeGenerics(xtype)

                typeparams[name] = xtype ?: "java.lang.Object"
            }
        }

        merge(xclass.queryElements("typeParameters/typeParameter"))
        merge(xmember.queryElements("typeParameters/typeParameter"))

        return typeparams
    }

    private fun resolveMember(xmember: Element, jmembers: Array<out Executable>): Executable? {
        val xclass = xmember.parentElement
        val xpackage = xclass.parentElement.getAttributeValue("name")
        val xname = xmember.getAttributeValue("name")

        // get a list of all the parameters
        val xparameters = getMemberParameters(xmember)
        val jmembersfiltered = jmembers.filter { jm ->
            when (jm) {
                is Constructor<*> -> jm.normalizedName == "${xpackage}.${xname}"
                is Method -> jm.normalizedName == xname
                else -> false
            }
        }

        // match the member signature
        var jmember = jmembersfiltered.firstOrNull { matchParameters(xparameters, it) }

        // try and replace all the generic types
        if (jmember == null) {
            val typeparams = getTypeParameters(xclass, xmember)
            if (typeparams.isNotEmpty()) {
                val xmapped = resolveGenerics(xparameters, typeparams)
                jmember = jmembersfiltered.firstOrNull { matchParameters(xmapped, it) }
            }
        }

        return jmember
    }

    private fun matchParameters(xparameters: Array<String>, jm: Executable): Boolean {
        val paramTypes = jm.parameterTypes
        val jnames = paramTypes.map { it.normalizedName }.toTypedArray()

        // drop the first argument if this is a non-static nested class
        return if (jm is Constructor<*> && jm.declaringClass.declaringClass != null && (jm.declaringClass.modifiers and Modifier.STATIC) == 0) {
            jm.declaringClass.declaringClass == paramTypes[0] && xparameters contentEquals jnames.drop(1).toTypedArray()
        } else {
            xparameters contentEquals jnames
        }
    }

    private fun removeGenerics(xparamtype: String): String {
        val idxStart = xparamtype.indexOf("<")
        val idxEnd = xparamtype.lastIndexOf(">")
        if (idxStart == -1 || idxEnd == -1)
            return xparamtype
        return xparamtype.removeRange(idxStart, idxEnd + 1)
    }

    private fun resolveGenerics(parameters: Array<String>, typeparams: MutableMap<String, String>): Array<String> {
        fun resolve(type: String, suffix: String): String? {
            if (!typeparams.containsKey(type))
                return null

            var resolvedType = type
            while (typeparams.containsKey(resolvedType))
                resolvedType = typeparams[resolvedType]!!
            return resolvedType + suffix
        }

        val mapped = parameters.map { xp ->
            val type: String
            val suffix: String
            val idx = xp.indexOf("[")
            if (idx != -1) {
                type = xp.substring(0, idx)
                suffix = xp.substring(idx)
            } else {
                type = xp
                suffix = ""
            }
            resolve(type, suffix) ?: xp
        }

        return mapped.toTypedArray()
    }

    private fun getJavaClass(xpackage: String?, xname: String): Pair<Class<*>, String>? {
        // try exact match
        var xfullname = "${xpackage}.${xname}"
        var jclass = getJavaClass(xfullname)
        if (jclass != null)
            return Pair(jclass, xfullname)

        // try an alternative
        xfullname = "${xpackage}.${xname.replace(".", "\$")}"
        jclass = getJavaClass(xfullname)
        if (jclass != null)
            return Pair(jclass, xfullname)

        // we can't do anything
        return null
    }

    private fun getJavaClass(xfullname: String): Class<*>? {
        return try {
            loader.loadClass(xfullname)
        } catch (ex: Exception) {
            null
        }
    }

    private fun logVerbose(message: String) {
        if (verbose)
            println(message)
    }

    private fun writeAttrManagedName(path: String, value: String) =
        writeAttr(path, "managedName", value)

    private fun writeAttr(path: String, name: String, value: String) {
        val xroot = xtransformsdoc.rootElement
        val xele = Element("attr")
        xele.addAttribute(Attribute("path", path))
        xele.addAttribute(Attribute("name", name))
        xele.appendChild(value)
        xroot.appendChild(xele)
    }

    private fun writeRemoveNode(path: String) {
        val xroot = xtransformsdoc.rootElement
        val xele = Element("remove-node")
        xele.addAttribute(Attribute("path", path))
        xroot.appendChild(xele)
    }

    private fun getJvmPropertyVisibility(jmember: Executable): KVisibility? {
        val properties = when (val md = jmember.declaringClass.getMetadata()) {
            is KotlinClassMetadata.Class -> md.toKmClass().properties
            is KotlinClassMetadata.FileFacade -> md.toKmPackage().properties
            is KotlinClassMetadata.MultiFileClassPart -> md.toKmPackage().properties
            else -> null
        }
        if (properties != null) {
            val p = jmember.parameterTypes.joinToString { ClassMapper.map(it.name) }
            val filtered = properties.filter {
                (it.getterSignature?.name == jmember.name && it.getterSignature?.parameters == p) ||
                        (it.setterSignature?.name == jmember.name && it.setterSignature?.parameters == p)
            }
            if (filtered.size > 1) {
                ProcessorErrors.errorInProcessor("More than 1 matching property found for ${jmember}")
            } else if (filtered.size == 1) {
                val prop = filtered[0]
                if (prop.getterSignature?.name == jmember.name && prop.setterSignature?.name == jmember.name) {
                    ProcessorErrors.errorInProcessor("Matched both the getter and the setter for ${jmember}")
                } else {
                    return if (prop.getterSignature?.name == jmember.name)
                        prop.getterVisibility
                    else
                        prop.setterVisibility
                }
            } else {
                ProcessorErrors.unableToMatchKotlinMember("property", jmember)
            }
        }
        return null
    }

    private fun getJvmMethodVisibility(jmember: Executable): KVisibility? {
        val methods = when (val md = jmember.declaringClass.getMetadata()) {
            is KotlinClassMetadata.Class -> when (jmember) {
                is Constructor<*> -> md.toKmClass().constructorOverloads
                is Method -> md.toKmClass().functionOverloads.filter {
                    it.first.name == jmember.name || it.first.signature?.name == jmember.name
                }
                else -> null
            }
            is KotlinClassMetadata.FileFacade -> when (jmember) {
                is Constructor<*> -> null
                is Method -> md.toKmPackage().functionOverloads.filter {
                    it.first.name == jmember.name || it.first.signature?.name == jmember.name
                }
                else -> null
            }
            is KotlinClassMetadata.MultiFileClassPart -> when (jmember) {
                is Constructor<*> -> null
                is Method -> md.toKmPackage().functionOverloads.filter {
                    it.first.name == jmember.name || it.first.signature?.name == jmember.name
                }
                else -> null
            }
            else -> null
        }
        if (methods != null) {
            val filtered = methods.filter { params ->
                val types = params.second.map { ClassMapper.map(it) }.toTypedArray()
                val jtypes = jmember.parameterTypes.map { ClassMapper.map(it.name) }.toTypedArray()
                types contentEquals jtypes
            }
            if (filtered.size > 1) {
                ProcessorErrors.errorInProcessor("More than 1 matching method found for ${jmember}")
            } else if (filtered.size == 1) {
                return when (val m = filtered[0].first) {
                    is KmConstructor -> m.visibility
                    is KmFunction -> m.visibility
                    else -> null
                }
            } else {
                ProcessorErrors.unableToMatchKotlinMember("method", jmember)
            }
        }
        return null
    }

    enum class ProcessResult {
        Ignore,                 // IGNORE this class in further processing
        RemoveJavaInternal,     // REMOVE this class because it is private in Java
        RemoveInternal,         // REMOVE this class because it is internal
        RemoveImplementation,   // REMOVE this class because it is an implementation method
        RemoveGenerated,        // REMOVE this class as it is generated by the Kotlin compiler for internal use
        RemoveInternalParent    // REMOVE this class as the parent is removed
    }

    enum class CompanionProcessing {
        Default,
        Rename,
        Remove,
    }
}

private val <T> Class<T>.normalizedName: String
    get() = this.typeName.replace("$", ".").replace("/", ".")

private val Executable.normalizedName: String
    get() = this.name.replace("$", ".").replace("/", ".")

private val JvmMethodSignature.parameters: String
    get() = this.desc.substring(this.desc.indexOf("(") + 1, this.desc.indexOf(")"))

private fun Class<*>.getMetadata(): KotlinClassMetadata? {
    val metadataAnnotation = this.declaredAnnotations.firstOrNull {
        it.annotationClass.qualifiedName == "kotlin.Metadata"
    } as Metadata?
    if (metadataAnnotation == null)
        return null
    val header = KotlinClassHeader(
        metadataAnnotation.kind,
        metadataAnnotation.metadataVersion,
        metadataAnnotation.bytecodeVersion,
        metadataAnnotation.data1,
        metadataAnnotation.data2,
        metadataAnnotation.extraString,
        metadataAnnotation.packageName,
        metadataAnnotation.extraInt
    )
    return KotlinClassMetadata.read(header)
}

private val KmProperty.getterVisibility: KVisibility
    get() = when {
        Flag.IS_PUBLIC(this.getterFlags) -> KVisibility.PUBLIC
        Flag.IS_PROTECTED(this.getterFlags) -> KVisibility.PROTECTED
        Flag.IS_INTERNAL(this.getterFlags) -> KVisibility.INTERNAL
        else -> KVisibility.PRIVATE
    }

private val KmProperty.setterVisibility: KVisibility
    get() = when {
        Flag.IS_PUBLIC(this.setterFlags) -> KVisibility.PUBLIC
        Flag.IS_PROTECTED(this.setterFlags) -> KVisibility.PROTECTED
        Flag.IS_INTERNAL(this.setterFlags) -> KVisibility.INTERNAL
        else -> KVisibility.PRIVATE
    }

private val KmConstructor.visibility: KVisibility
    get() = when {
        Flag.IS_PUBLIC(this.flags) -> KVisibility.PUBLIC
        Flag.IS_PROTECTED(this.flags) -> KVisibility.PROTECTED
        Flag.IS_INTERNAL(this.flags) -> KVisibility.INTERNAL
        else -> KVisibility.PRIVATE
    }

private val KmFunction.visibility: KVisibility
    get() = when {
        Flag.IS_PUBLIC(this.flags) -> KVisibility.PUBLIC
        Flag.IS_PROTECTED(this.flags) -> KVisibility.PROTECTED
        Flag.IS_INTERNAL(this.flags) -> KVisibility.INTERNAL
        else -> KVisibility.PRIVATE
    }

private val Element.parentElement: Element
    get() = this.parent as Element

private val KmPackage.functionOverloads: List<Pair<KmFunction, List<KmValueParameter>>>
    get() = this.functions.map {
        it to it.valueParameters.getOverloads(it.receiverParameterType)
    }.flatMap { overload ->
        overload.second.map { params -> overload.first to params }
    }

private val KmClass.functionOverloads: List<Pair<KmFunction, List<KmValueParameter>>>
    get() = this.functions.map {
        it to it.valueParameters.getOverloads(it.receiverParameterType)
    }.flatMap { overload ->
        overload.second.map { params -> overload.first to params }
    }

private val KmClass.constructorOverloads: List<Pair<KmConstructor, List<KmValueParameter>>>
    get() = this.constructors.map {
        it to it.valueParameters.getOverloads()
    }.flatMap { overload ->
        overload.second.map { params -> overload.first to params }
    }

private fun List<KmValueParameter>.getOverloads(receiverParameterType: KmType? = null): List<List<KmValueParameter>> {
    val overloads = mutableListOf<List<KmValueParameter>>()
    if (receiverParameterType == null) {
        overloads.add(this)
    } else {
        val t = KmValueParameter(0, "this")
        t.type = receiverParameterType
        overloads.add(listOf(t) + this)
    }
    while (Flag.ValueParameter.DECLARES_DEFAULT_VALUE(overloads.last().lastOrNull()?.flags ?: 0)) {
        val l = overloads.last()
        overloads.add(l.take(l.size - 1))
    }
    return overloads
}

private fun Document.queryElements(expression: String): List<Element> =
    this.rootElement.queryElements(expression)

private fun Element.queryElements(expression: String): List<Element> =
    this.query(expression).map { it as Element }
