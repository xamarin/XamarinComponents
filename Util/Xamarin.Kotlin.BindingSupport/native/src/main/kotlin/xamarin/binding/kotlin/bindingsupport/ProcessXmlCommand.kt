@file:Suppress("RemoveCurlyBracesFromTemplate")

package xamarin.binding.kotlin.bindingsupport

import com.github.ajalt.clikt.core.CliktCommand
import com.github.ajalt.clikt.parameters.options.*
import com.github.ajalt.clikt.parameters.types.*
import java.io.File

class ProcessXmlCommand : CliktCommand(name = "KotlinBindingSupport") {

    val xmlFile: File by option("-x", "--xml", help = "path to the generated api.xml file")
        .file(exists = true)
        .required()

    val jarFiles: List<File> by option("-j", "--jar", help = "paths to the various .jar files being bound")
        .file(exists = true)
        .multiple(required = true)

    val outputFile: File? by option("-o", "--output", help = "path to the output transform file")
        .file(fileOkay = true, folderOkay = false)

    val ignoreFile: File? by option("-i", "--ignore", help = "path to the ignore file")
        .file(fileOkay = true, folderOkay = false)

    val companions: Processor.CompanionProcessing by option("--companions", help = "indicate how to handle companions")
        .choice(
            "default" to Processor.CompanionProcessing.Default,
            "rename" to Processor.CompanionProcessing.Rename,
            "remove" to Processor.CompanionProcessing.Remove
        )
        .default(Processor.CompanionProcessing.Default)

    val verbose: Boolean by option("-v", "--verbose", help = "output verbose information")
        .flag(default = false)

    override fun run() {
        val proc = Processor(xmlFile, jarFiles, outputFile, ignoreFile)
        proc.verbose = verbose
        proc.companions = companions
        proc.process()
    }
}
