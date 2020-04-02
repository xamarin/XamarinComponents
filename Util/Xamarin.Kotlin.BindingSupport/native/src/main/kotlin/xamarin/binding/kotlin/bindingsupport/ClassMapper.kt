@file:Suppress("RemoveCurlyBracesFromTemplate")

package xamarin.binding.kotlin.bindingsupport

import kotlinx.metadata.KmClassifier
import kotlinx.metadata.KmType
import kotlinx.metadata.KmValueParameter
import kotlin.collections.LinkedHashMap

object ClassMapper {
    private val store: Map<String, String>

    init {
        val primitives = mapOf(
            "kotlin/Unit" to "V",
            "kotlin/Boolean" to "Z",
            "kotlin/Char" to "C",
            "kotlin/Byte" to "B",
            "kotlin/Short" to "S",
            "kotlin/Int" to "I",
            "kotlin/Float" to "F",
            "kotlin/Long" to "J",
            "kotlin/Double" to "D",
            "kotlin/BooleanArray" to "[Z",
            "kotlin/CharArray" to "[C",
            "kotlin/ByteArray" to "[B",
            "kotlin/ShortArray" to "[S",
            "kotlin/IntArray" to "[I",
            "kotlin/FloatArray" to "[F",
            "kotlin/LongArray" to "[J",
            "kotlin/DoubleArray" to "[D",
            "kotlin/UByte" to "B",
            "boolean" to "Z",
            "char" to "C",
            "byte" to "B",
            "short" to "S",
            "int" to "I",
            "float" to "F",
            "long" to "J",
            "double" to "D"
        )

        store = primitives.toMap(LinkedHashMap()).apply {
            fun add(kotlinSimpleName: String, javaInternalName: String) {
                put("kotlin/${kotlinSimpleName}", "L$javaInternalName;")
            }

            add("Any", "java/lang/Object")
            add("Nothing", "java/lang/Void")
            add("Annotation", "java/lang/annotation/Annotation")

            for (klass in listOf("String", "CharSequence", "Throwable", "Cloneable", "Number", "Comparable", "Enum")) {
                add(klass, "java/lang/${klass}")
            }

            for (klass in listOf("Iterator", "Collection", "List", "Set", "Map", "ListIterator")) {
                add("collections/${klass}", "java/util/${klass}")
                add("collections/Mutable${klass}", "java/util/${klass}")
            }

            add("collections/Iterable", "java/lang/Iterable")
            add("collections/MutableIterable", "java/lang/Iterable")
            add("collections/Map\$Entry", "java/util/Map\$Entry")
            add("collections/MutableMap\$MutableEntry", "java/util/Map\$Entry")

            add("random/Random", "java/util/Random")

            for (i in 0..22) {
                add("Function${i}", "kotlin/jvm/functions/Function${i}")
            }
        }
    }

    fun map(type: KmType): String {
        return when (val classifer = type.classifier) {
            is KmClassifier.Class -> when (classifer.name) {
                "kotlin/Array" -> "[" + map(type.arguments[0].type!!)
                else -> map(classifer.name)
            }
            else -> ""
        }
    }

    fun map(parameter: KmValueParameter): String {
        val type = parameter.varargElementType ?: parameter.type
        return map(type!!)
    }

    fun map(className: String): String {
        val internalName = className.replace(".", "/")
        return store[internalName] ?: "L$internalName;"
    }

    fun map(packageName: String, className: String): String {
        val internalName = packageName + "." + className.replace(".", "$")
        return map(internalName)
    }
}
