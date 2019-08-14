import com.github.ajalt.clikt.core.CliktCommand
import com.github.ajalt.clikt.parameters.options.*
import com.github.ajalt.clikt.parameters.types.*
import org.w3c.dom.Document
import org.w3c.dom.Element
import org.w3c.dom.Node
import org.w3c.dom.NodeList
import java.io.File
import java.lang.UnsupportedOperationException
import java.net.URL
import java.net.URLClassLoader
import javax.xml.parsers.DocumentBuilderFactory
import javax.xml.xpath.XPathConstants
import javax.xml.xpath.XPathFactory
import kotlin.reflect.KVisibility

fun main(args: Array<String>) =
    ProcessXmlCommand().main(args)

class ProcessXmlCommand : CliktCommand(name = "KotlinBindingHelper") {

    val xmlFile: File by option("-x", "--xml", help = "path to the generated api.xml file")
        .file(exists = true)
//        .required()
        .default(File("/Users/matthew/Projects/XamarinComponents/Android/Kotlin/generated/org.jetbrains.kotlin.kotlin-stdlib/obj/Release/monoandroid90/api.xml"))

    val jarFiles: List<File> by option("-j", "--jar", help = "paths to the various .jar files being bound")
        .file(exists = true)
//        .multiple(required = true)
        .multiple(default = arrayListOf(File("/Users/matthew/Projects/XamarinComponents/Android/Kotlin/externals/org.jetbrains.kotlin/kotlin-stdlib.jar")))

    val outputFile: File? by option("-o", "--output", help = "path to the output transform file")
        .file(fileOkay = false)
        .default(File("/Users/matthew/Projects/XamarinComponents/Android/Kotlin/source/Xamarin.Kotlin.StdLib/Transforms/Metadata.generated.xml"))

    val verbose: Boolean by option("-v", "--verbose", help = "output verbose information")
//        .flag(default = false)
        .flag(default = true)

    private val docBuilderFac = DocumentBuilderFactory.newInstance()
    private val docBuilder = docBuilderFac.newDocumentBuilder()

    private val xpathFac = XPathFactory.newInstance()
    private val xpath = xpathFac.newXPath()

    private val expression = "/api/package/*[(local-name()='class')or(local-name()='interface')]"
    private val expressionParent =
        "/api/package[@name='%s']/*[((local-name()='class')or(local-name()='interface'))and(@name='%s')]"
    private val expressionCompanion =
        "/api/package[@name='%s']/class[@name='%s']/field[@name='Companion' and @type='%s']"

    private val potentialRemoveNodes = arrayOf(
        "/api/package/class[starts-with(@name,'-')]",
        "/api/package/class/method[starts-with(@name,'-')]",
        "/api/package/class/method[contains(@name,'\$default')]",
        "/api/package/class/method[contains(@name,'\$annotations')]",
        "/api/package/class/method[contains(@name,'\$kotlin_stdlib')]",
        "/api/package/class/field[contains(@name,'\$EnumSwitchMapping\$')]"
    )

    private val potentialCorrections = mapOf(
        "/api/package/class/method[count(parameter)>0 and parameter[1][@name='\$receiver']]" to
                "<attr path=\"/api/package/class/method[parameter[1][@name='\$receiver']]/parameter[1]\" name=\"managedName\">receiver</attr>"
    )

    override fun run() {
        val urls = jarFiles.map { file -> URL("file://${file.canonicalPath}") }
        val loader = URLClassLoader(urls.toTypedArray())

        val xdoc = docBuilder.parse(xmlFile.canonicalFile)

        // create the output document
        val outputWriter = if (outputFile == null) null else outputFile!!.bufferedWriter()
        outputWriter?.appendln("<?xml version=\"1.0\" encoding=\"UTF-8\"?>")
        outputWriter?.appendln("<metadata>")

        // smart remove all known Kotlin cases
        outputWriter?.appendln("  <!-- remove Kotlin-specific elements -->")
        for (potential in potentialRemoveNodes) {
            if (verbose)
                println("Checking the XPath \"${potential}\"...")

            val matches = xpath.evaluate(potential, xdoc, XPathConstants.NODESET) as NodeList
            if (matches.length > 0) {
                if (verbose)
                    println("Removing the XPath \"${potential}\" because is not meant to be bound.")

                outputWriter?.appendln("  <remove-node path=\"${potential}\" />")
            }
        }

        // fix up any Kotlin things
        outputWriter?.appendln()
        outputWriter?.appendln("  <!-- fix Kotlin-specific elements -->")
        for (potential in potentialCorrections) {
            if (verbose)
                println("Checking the XPath \"${potential.key}\"...")

            val matches = xpath.evaluate(potential.key, xdoc, XPathConstants.NODESET) as NodeList
            if (matches.length > 0) {
                if (verbose)
                    println("Updating the XPath \"${potential.key}\" because is a special case.")

                outputWriter?.appendln("  ${potential.value}")
            }
        }

        // remove specific cases
        outputWriter?.appendln()
        outputWriter?.appendln("  <!-- remove Kotlin internal classes -->")
        val classes = xpath.evaluate(expression, xdoc, XPathConstants.NODESET) as NodeList
        for (i in 0 until classes.length) {
            val xclass = classes.item(i) as Element

            val xpackage = xclass.parentNode.attributes.getNamedItem("name").nodeValue
            val xname = xclass.attributes.getNamedItem("name").nodeValue
            val xtype = xclass.tagName

            val shouldRemove = shouldRemoveClass(xdoc, xclass, loader)
            if (shouldRemove != ProcessResult.Preserve && shouldRemove != ProcessResult.Ignore) {
                if (verbose)
                    println("Removing \"${xpackage}.${xname}\" because is not meant to be bound (${shouldRemove}).")

                outputWriter?.appendln("  <remove-node path=\"/api/package[@name='${xpackage}']/${xtype}[@name='${xname}']\" />")
            }

            // handle the cases of companions
            if (shouldRemove == ProcessResult.RemoveCompanion) {
                val companions = getCompanionFields(xpackage, xname, xdoc)
                val main = companions.first

                if (verbose)
                    println("Removing companion field \"${xpackage}.${main}.Companion\" because is not meant to be bound.")

                outputWriter?.appendln("  <remove-node path=\"/api/package[@name='${xpackage}']/class[@name='${main}']/field[@name='Companion' and @type='${xpackage}.${xname}']\" />")
            }
        }

        // finish up
        outputWriter?.append("</metadata>")
        outputWriter?.flush()
        outputWriter?.close()

        println("Processing complete.")
    }

    private fun shouldRemoveClass(xdoc: Document, xclass: Node, loader: URLClassLoader): ProcessResult {
        // read the xml
        val xpackage = xclass.parentNode.attributes.getNamedItem("name").nodeValue
        val xname = xclass.attributes.getNamedItem("name").nodeValue

        if (verbose)
            println("Checking the class \"${xpackage}.${xname}\"...")

        // optional
        val xextends = xclass.attributes.getNamedItem("extends")?.nodeValue
        val xvisibility = xclass.attributes.getNamedItem("visibility")?.nodeValue

        // ignore already hidden items
        if (xvisibility != "public")
            return ProcessResult.RemoveJavaInternal

        // some things we know are not good to check
        if (xextends == "java.lang.Enum")
            return ProcessResult.Ignore

        // some things we know for sure are internal or generated
        if (xname.endsWith(".Companion") && xextends == "java.lang.Object") {
            val companionFields = getCompanionFields(xpackage, xname, xdoc)
            if (companionFields.second.length == 1) {
                return ProcessResult.RemoveCompanion
            } else if (companionFields.second.length > 1) {
                if (verbose)
                    println("WARNING: Multiple companion fields found for \"${xpackage}.${xname}\".")
            }
        }

        // first try names with periods, but they may be private, so try again with dollars
        var jclass: Class<*>
        var xfullname = "${xpackage}.${xname}"
        try {
            jclass = loader.loadClass(xfullname)
        } catch (ex: Exception) {
            xfullname = "${xpackage}.${xname.replace(".", "\$")}"
            jclass = loader.loadClass(xfullname)
        }

        // get the kotlin class
        val kclass = jclass.kotlin

        // determine if this is some internal method
        try {
            if (kclass.visibility != KVisibility.PUBLIC)
                return ProcessResult.RemoveInternal
        } catch (ex: Exception) {
            if (ex is UnsupportedOperationException && ex.message != null && ex.message!!.startsWith("Packages and file facades are not yet supported in Kotlin reflection.")) {
                if (verbose)
                    println("WARNING: \"${xfullname}\" is bad: ${ex.localizedMessage}")
            } else {
                println("ERROR: \"${xfullname}\" is bad: ${ex.localizedMessage}")
            }
        }

        // check to make sure the container is also visible
        var lastPeriod = xname.lastIndexOf(".")
        while (lastPeriod != -1) {
            val xparentname = xname.substring(0, lastPeriod)
            val expression = expressionParent.format(xpackage, xparentname)
            val parentClasses = xpath.evaluate(expression, xdoc, XPathConstants.NODESET) as NodeList
            if (parentClasses.length == 1) {
                if (verbose)
                    println("Checking the parent class \"${xpackage}.${xparentname}\"...")

                val removeParent = shouldRemoveClass(xdoc, parentClasses.item(0), loader)
                if (removeParent != ProcessResult.Preserve && removeParent != ProcessResult.Ignore)
                    return ProcessResult.RemoveInternalParent
            } else if (parentClasses.length > 1) {
                println("ERROR: multiple classes match the full name \"${xpackage}.${xparentname}\"")
            } else {
                if (verbose)
                    println("INFO: \"${xpackage}.${xparentname}\" is not a real parent for \"${xpackage}.${xname}\".")
            }
            lastPeriod = xparentname.lastIndexOf(".")
        }

        return ProcessResult.Preserve
    }

    private fun getCompanionFields(xpackage: String, xname: String, xdoc: Document): Pair<String, NodeList> {
        val main = xname.substring(0, xname.length - ".Companion".length)
        val mainClassExpression = expressionCompanion.format(xpackage, main, "${xpackage}.${xname}")
        return (main to xpath.evaluate(mainClassExpression, xdoc, XPathConstants.NODESET) as NodeList)
    }

    enum class ProcessResult {
        Preserve,               // PRESERVE this class because it is meant to be used
        Ignore,                 // IGNORE this class in further processing
        RemoveJavaInternal,     // REMOVE this class because it is private in Java
        RemoveInternal,         // REMOVE this class because it is internal
        RemoveCompanion,        // REMOVE this class as it is a "generated" type
        RemoveInternalParent    // REMOVE this class as the parent is removed
    }
}

