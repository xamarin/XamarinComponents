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

    private fun expressionEmptyClasses() =
        "/api/package/class[not(*)]"

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
        { x -> x.endsWith("\$default") },
        { x -> x.endsWith("\$annotations") },
        { x -> x.endsWith("\$kotlin_stdlib") },
        { x -> x.startsWith("\$EnumSwitchMapping\$") },
        { x -> x.startsWith("access\$") }
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

        // process the generated *Kt classes
        processExtensionClasses()

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

    private fun processExtensionClasses() {
        writeComment("fix Kotlin extension classes")

        val classes = xapidoc.queryElements(expressionEmptyClasses())
        for (xclass in classes) {
            val xpackage = xclass.parentElement.getAttributeValue("name")
            val xname = xclass.getAttributeValue("name")
            val xfullname = "${xpackage}.${xname}"
            val xtype = xclass.localName

            // extension classes are in the form *Kt
            if (xtype != "class" || !xname.endsWith("Kt"))
                continue

            // make sure this class is not a base class for something
            val xextends = xapidoc.queryElements(expressionClassExtenders(xfullname))
            if (xextends.isNotEmpty())
                continue

            // make sure this class is not used in any fields
            val xfields = xapidoc.queryElements(expressionClassUserFields(xfullname))
            if (xfields.isNotEmpty())
                continue

            // make sure this class is not used in any methods
            val xmethods = xapidoc.queryElements(expressionClassUserMethods(xfullname))
            if (xmethods.isNotEmpty())
                continue

            // make sure this class is not used in any generics
            val xgenerics = xapidoc.queryElements(expressionClassUserGenerics(xfullname))
            if (xgenerics.isNotEmpty())
                continue

            // this class needs to be removed for some reason
            logVerbose("Removing \"${xfullname}\" because is is a generated extensions class...")
            writeRemoveNode("/api/package[@name='${xpackage}']/${xtype}[@name='${xname}']")
        }
    }

    private fun processCompanions() {
        writeComment("fix Kotlin companion objects")

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
        // fix up any Kotlin things
        writeComment("fix Kotlin-specific elements")

        // Kotlin v1.2 names the extension method parameters to $receiver
        if (xapidoc.queryElements("/api/package/class/method[count(parameter)>0 and parameter[1][@name='\$receiver']]").any()) {
            logVerbose("Renaming the \"\$receiver\" parameter names to \"parameter\"...")
            writeAttrManagedName("/api/package/class/method[parameter[1][@name='\$receiver']]/parameter[1]", "receiver")
        }
    }

    private fun processClasses() {
        writeComment("remove Kotlin internal classes and members")

        val classes = xapidoc.queryElements(expressionClasses())
        for (xclass in classes) {
            val removeClass = shouldRemoveClass(xclass)
            if (removeClass != ProcessResult.Ignore) {
                val xpackage = xclass.parentElement.getAttributeValue("name")
                val xname = xclass.getAttributeValue("name")
                val xtype = xclass.localName

                // this class needs to be removed for some reason
                logVerbose("Removing \"${xpackage}.${xname}\" because is not meant to be bound (${removeClass})...")
                writeRemoveNode("/api/package[@name='${xpackage}']/${xtype}[@name='${xname}']")
            } else {
                processMembers(xclass)
            }
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

        processMember(xclass, "constructor") {
            shouldRemoveMember(it, jclass.declaredConstructors)
        }
        processMember(xclass, "method") {
            shouldRemoveMember(it, jclass.declaredMethods)
        }
        processMember(xclass, "field") {
            shouldRemoveMember(it, jclass.declaredFields)
        }
    }

    private fun processMember(xclass: Element, memberType: String, shouldRemove: (Element) -> ProcessResult) {
        val xpackage = xclass.parentElement.getAttributeValue("name")
        val xclassname = xclass.getAttributeValue("name")
        val xclasstype = xclass.localName

        val xmembers = xapidoc.queryElements(expressionMember(xpackage, xclasstype, xclassname, memberType))
        for (xmember in xmembers) {
            val result = shouldRemove(xmember)
            if (result != ProcessResult.Ignore) {
                val xmembername = xmember.getAttributeValue("name")
                val parameters = getMemberParameters(xmember, true)
                val params = listOf(
                    "@name='${xmembername}'",
                    "count(parameter)=${parameters.size}"
                ).union(
                    parameters.mapIndexed { idx, p -> "parameter[${idx + 1}][@type='${p}']" }
                )
                val paramsString = params.joinToString(" and ")

                val ps = parameters.joinToString(", ")
                logVerbose("Removing ${memberType} \"${xpackage}.${xclassname}.${xmembername}(${ps}))\" because is not meant to be bound (${result})...")
                writeRemoveNode("/api/package[@name='${xpackage}']/${xclasstype}[@name='${xclassname}']/${memberType}[${paramsString}]")
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
        } catch (ex: Exception) {
            if (ex is UnsupportedOperationException && ex.message != null) {
                if (ex.message!!.contains("This class is an internal synthetic class generated by the Kotlin compiler")) {
                    return ProcessResult.RemoveGenerated
                } else if (ex.message!!.contains("Packages and file facades are not yet supported in Kotlin reflection.")) {
                    if (verbose)
                        ProcessorErrors.unableToResolveKotlinClass(xtype, xfullname, ex)
                } else {
                    ProcessorErrors.errorInspectingKotlinClass(xtype, xfullname, ex)
                }
            } else {
                ProcessorErrors.errorInspectingKotlinClass(xtype, xfullname, ex)
            }
        } catch (ex: Throwable) {
            if (ex.message!!.contains("Unresolved class:")) {
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
        } catch (ex: Exception) {
            if (ex is UnsupportedOperationException && ex.message != null) {
                if (ex.message!!.contains("Packages and file facades are not yet supported in Kotlin reflection.")) {
                    if (verbose)
                        ProcessorErrors.unableToResolveKotlinMember(xtype, xfullname, ex)
                } else {
                    ProcessorErrors.errorInspectingKotlinMember(xtype, xfullname, ex)
                }
            }
        } catch (ex: Throwable) {
            if (ex.message!!.startsWith("Unknown origin of ")) {
                if (verbose)
                    ProcessorErrors.unableToResolveKotlinMember(xtype, xfullname, ex)
            } else {
                ProcessorErrors.errorInspectingKotlinMember(xtype, xfullname, ex)
            }
        }

        return ProcessResult.Ignore
    }

    private fun shouldRemoveMember(xmember: Element, jmembers: Array<Field>): ProcessResult {
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
        val jmember = jmembers.firstOrNull { jm -> jm.name == xname && jm.type.normalizedName == xtype }

        // there was a problem loading this field
        if (jmember == null) {
            ProcessorErrors.errorResolvingJavaMember("field", xfullname)
            return ProcessResult.Ignore
        }

        try {
            val kmember = jmember.kotlinProperty

            // there was a problem loading this field
            if (kmember == null) {
                ProcessorErrors.unableToResolveKotlinMember("field", xfullname)
                return ProcessResult.Ignore
            }

            // check to see if it is visible
            if (kmember.visibility != KVisibility.PUBLIC && kmember.visibility != KVisibility.PROTECTED)
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

    private fun writeComment(comment: String) {
        val xroot = xtransformsdoc.rootElement
        xroot.appendChild(Comment(comment))
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
