@file:Suppress("RemoveCurlyBracesFromTemplate")

package xamarin.binding.kotlin.bindingsupport

import nu.xom.*
import java.io.File
import java.lang.reflect.Constructor
import java.lang.reflect.Executable
import java.lang.reflect.Field
import java.lang.reflect.Method
import java.net.URL
import java.net.URLClassLoader
import kotlin.reflect.KFunction
import kotlin.reflect.KVisibility
import kotlin.reflect.jvm.kotlinFunction
import kotlin.reflect.jvm.kotlinProperty

class Processor(xmlFile: File, jarFiles: List<File>, outputFile: File?) {

    private fun expressionClasses() =
        "/api/package/*[local-name()='class' or local-name()='interface']"

    private fun expressionClassExtenders(xfullname: String) =
        "/api/package/*[contains(@extends,'${xfullname}') or count(implements[contains(@name,'${xfullname}')])!=0]"

    private fun expressionClassUserFields(xfullname: String) =
        "/api/package/*/field[contains(@type,'${xfullname}')]"

    private fun expressionClassUserMethods(xfullname: String) =
        "/api/package/*/*[contains(@return,'${xfullname}') or count(parameter[contains(@type,'${xfullname}')])!=0]"

    private fun expressionClassUserGenerics(xfullname: String) =
        "/api/package//genericConstraint[contains(@type,'${xfullname}')]"

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

    init {
        val urls = jarFiles.map { file -> URL("file://${file.canonicalPath}") }
        loader = URLClassLoader(urls.toTypedArray())

        val builder = Builder()
        xapidoc = builder.build(xmlFile.canonicalFile)

        xtransformsdoc = Document(Element("metadata"))
        this.outputFile = outputFile
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
            shouldRemoveMember(it, jclass.declaredConstructors)
        }
        processMembers(xclass, "method") {
            shouldRemoveMember(it, jclass.declaredMethods)
        }
        processMembers(xclass, "field") {
            shouldRemoveField(it, jclass.declaredFields)
        }
    }

    private fun processMembers(xclass: Element, xtype: String, shouldRemove: (Element) -> ProcessResult) {
        val xpackage = xclass.parentElement.getAttributeValue("name")
        val xclassname = xclass.getAttributeValue("name")
        val xclasstype = xclass.localName

        val xmembers = xapidoc.queryElements(expressionMember(xpackage, xclasstype, xclassname, xtype))
        for (xmember in xmembers) {
            val xname = xmember.getAttributeValue("name")
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
                logVerbose("Removing ${xtype} \"$friendly\" because is not meant to be bound (${result})...")
                writeRemoveNode(xpath)
            } else {
                val dashIndex = xname.indexOf("-")
                if (dashIndex > 0) {
                    // Kotlin 1.3: generated methods have a generated -* suffix
                    logVerbose("Renaming ${xtype} \"$friendly\" because is a generated method overload...")
                    val managedName = xname.substring(0, dashIndex)
                    if (managedName.length == 1)
                        writeAttrManagedName(xpath, managedName.toUpperCase())
                    else
                        writeAttrManagedName(xpath, managedName[0].toUpperCase() + managedName.substring(1))
                }
            }
        }
    }

    private fun shouldRemoveClass(xclass: Element): ProcessResult {
        // read the xml
        val xpackage = xclass.parentElement.getAttributeValue("name")
        val xname = xclass.getAttributeValue("name")
        val xtype = xclass.localName

        logVerbose("Checking the class \"${xpackage}.${xname}\"...")

        // before we check anything, make sure that the parent class is visible
        var lastPeriod = xname.lastIndexOf(".")
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
        if (invalidKeywords.any { check -> check(xname) }) {
            return ProcessResult.RemoveGenerated
        }

        // optional
        val xvisibility = xmember.getAttributeValue("visibility")

        // ignore already hidden items
        if (xvisibility != "public" && xvisibility != "protected")
            return ProcessResult.RemoveJavaInternal

        // optional
        val xextends = xclass.getAttributeValue("extends")

        // some things we know are not good to check
        if (xextends == "java.lang.Enum")
            return ProcessResult.Ignore

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
        var jmember = jmembersfiltered.firstOrNull { jm ->
            val jnames = jm.parameterTypes.map { it.normalizedName }.toTypedArray()
            xparameters contentEquals jnames
        }

        // try and replace all the generic types
        if (jmember == null) {
            val typeparams = getTypeParameters(xclass, xmember)
            if (typeparams.isNotEmpty()) {
                val xmapped = resolveGenerics(xparameters, typeparams)
                jmember = jmembersfiltered.firstOrNull { jm ->
                    val jnames = jm.parameterTypes.map { it.normalizedName }.toTypedArray()
                    xmapped contentEquals jnames
                }
            }
        }

        // there was a problem loading this member
        if (jmember == null) {
            ProcessorErrors.errorResolvingJavaMember(xtype, xfullname)
            return ProcessResult.Ignore
        }

        // Kotlin 1.3:
        if (xname.contains("-impl") && jmember.declaredAnnotations.all { it.annotationClass.qualifiedName != "kotlin.PublishedApi" }) {
            return ProcessResult.RemoveImplementation
        }

        try {
            val kmember: KFunction<*>?
            when (jmember) {
                is Constructor<*> -> kmember = jmember.kotlinFunction
                is Method -> kmember = jmember.kotlinFunction
                else -> kmember = null
            }

            // kotlin can't read this one
            if (kmember == null) {
                ProcessorErrors.unableToResolveKotlinMember(xtype, xfullname)
                return ProcessResult.Ignore
            }

            // check to see if it is visible
            if (kmember.visibility != KVisibility.PUBLIC && kmember.visibility != KVisibility.PROTECTED)
                return ProcessResult.RemoveInternal
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
            ProcessorErrors.errorInspectingKotlinMember("field", xfullname, ex)
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
    get() = this.typeName.replace("$", ".")

private val Executable.normalizedName: String
    get() = this.name.replace("$", ".")

private val Element.parentElement: Element
    get() = this.parent as Element

private fun Document.queryElements(expression: String): List<Element> =
    this.rootElement.queryElements(expression)

private fun Element.queryElements(expression: String): List<Element> =
    this.query(expression).map { it as Element }
