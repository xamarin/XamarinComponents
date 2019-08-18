package xamarin.binding.kotlin.bindingsupport

class ProcessorErrors {
    companion object {
        fun errorResolvingJavaClass(classType: String, className: String, exception: Throwable? = null) =
            println("ERROR XKT1010: Error resolving Java ${classType} \"${className}\"" + getEnd(exception))

        fun errorResolvingJavaMember(memberType: String, memberName: String, exception: Throwable? = null) =
            println("ERROR XKT1011: Error resolving Java ${memberType} \"${memberName}\"" + getEnd(exception))

        fun unableToResolveKotlinClass(classType: String, className: String, exception: Throwable? = null) =
            println("WARNING XKT1020: Unable to resolve Kotlin ${classType} \"${className}\"" + getEnd(exception))

        fun errorInspectingKotlinClass(classType: String, className: String, exception: Throwable? = null) =
            println("ERROR XKT1021: Error inspecting Kotlin ${classType} \"${className}\"" + getEnd(exception))

        fun unableToResolveKotlinMember(memberType: String, memberName: String, exception: Throwable? = null) =
            println("WARNING XKT1022: Unable to resolve Kotlin ${memberType} \"${memberName}\"" + getEnd(exception))

        fun errorInspectingKotlinMember(memberType: String, memberName: String, exception: Throwable? = null) =
            println("ERROR XKT1023: Error inspecting Kotlin ${memberType} \"${memberName}\"" + getEnd(exception))

        fun multipleClassesInXml(className: String) =
            println("ERROR XKT1030: Multiple classes in the XML match the full name \"${className}\".")

        fun errorInProcessor(message: String, exception: Throwable? = null) =
            println("ERROR XKT1031: Error occurred during processing: \"${message}\"" + getEnd(exception))

        private fun getEnd(exception: Throwable?): String {
            if (exception == null)
                return "."
            else
                return ": ${exception.localizedMessage}"
        }
    }
}
