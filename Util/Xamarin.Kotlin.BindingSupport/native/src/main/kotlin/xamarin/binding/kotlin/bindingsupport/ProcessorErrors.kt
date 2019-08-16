package xamarin.binding.kotlin.bindingsupport

class ProcessorErrors {
    companion object {
        fun errorResolvingJavaClass(className: String) =
            println("ERROR XKT1010: Error resolving Java class \"${className}\".")

        fun errorResolvingJavaMember(memberType: String, memberName: String) =
            println("ERROR XKT1011: Error resolving Java ${memberType} \"${memberName}\".")

        fun unableToResolveKotlinClass(className: String, exception: Throwable) =
            println("WARNING XKT1020: Unable to resolve Kotlin class \"${className}\": ${exception.localizedMessage}")

        fun errorInspectingKotlinClass(className: String, exception: Throwable) =
            println("ERROR XKT1022: Error inspecting Kotlin class \"${className}\": ${exception.localizedMessage}")

        fun unableToResolveKotlinMember(memberType: String, memberName: String) =
            println("WARNING XKT1023: Unable to resolve Kotlin ${memberType} \"${memberName}\".")

        fun errorInspectingKotlinMember(memberType: String, memberName: String, exception: Throwable) =
            println("ERROR XKT1024: Error inspecting Kotlin ${memberType} \"${memberName}\": ${exception.localizedMessage}")

        fun multipleClassesInXml(className: String) =
            println("ERROR XKT1030: Multiple classes in the XML match the full name \"${className}\".")
    }
}
