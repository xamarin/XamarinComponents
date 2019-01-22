# Sample Packager

The Sample packager is designed to provide a zipped sample package after processing a solution containing packages that will produce Nugets.

The sample packager is compatible with:

- SDK style project (.Net Core and .Net Standard)
- Classic style projects (.Net Framework, Xamarin.iOS and Xamarin.Android)

##  Setup

The sample packager will detect any referenced project that is configured to produce a Nuget package.  It will removed  the references and re-added them as package references to the nugets that will be produced.

To do this it will detect the following properties in the csproj.

- PackOnBuild (Classic)
- GeneratePackageOnBuild (SDK)

If it detects these properties it will asssume the project is packaged for nuget.

It will then process the following properties to calculate the package Id and Package version.

- PackageId (SDK and Classic)
- Version (SDK)
- PackageVersion (Classic)


