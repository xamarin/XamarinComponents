# Sample Packager

The Sample packager is designed to provide a zipped sample package after processing a solution containing packages that will produce Nugets.

The sample packager is compatible with:

- SDK style project (.Net Core and .Net Standard)
- Classic style projects (.Net Framework, Xamarin.iOS and Xamarin.Android)

##  Setup

The sample packager will detect any referenced project that is configured to produce a Nuget package.  It will remove the references and re-add them as package references, linking to the nugets that will be produced.

To do this it will detect the following properties in the csproj.

- PackOnBuild (Classic)
- GeneratePackageOnBuild (SDK)

If it detects these properties it will asssume the project is packaged for nuget.

It will then process the following properties to calculate the package Id and Package version.

- PackageId (SDK and Classic)
- Version (SDK)
- PackageVersion (Classic)

*NOTE: The sample packager does not currently process .nuspec files*

##  API

`SolutionProcessor.Process` is processing method to convert your solution into a sample package.

It has two required parameters

 - solutionPath
   - This is the path to the .sln file containing the sample projects  
 - outputPath
   - This is the path were then temp folders and the final zip file will be created  

 Below is an example:

    using Xamarin.Components.SampleBuilder;

    var solutionPath = @"C:\ARCore\samples\HelloAR.sln";

    var outPutPath = @"C:\SamplePackagerOutput\ARCoreSamples";

    var outputfile = SolutionProcessor.Process(solutionPath, outPutPath); 


There is also an optional parameter called `nugetPackageOverrides`.  This takes a `Dictionary<string,string>` and allows you to override the version numbers of the calculated package version numbers(calculated from the csproj files).


    using Xamarin.Components.SampleBuilder;

    var solutionPath = @"C:\ARCore\samples\HelloAR.sln";

    var outPutPath = @"C:\SamplePackagerOutput\ARCoreSamples";

    var packageVersions = new Dictionary<string, string>()
    {
        {"StandardSample","1.1.0" },
        {"AndroidLibary","1.1.0" },
    };

    var outputfile = SolutionProcessor.Process(solutionPath, outPutPath, packageVersions); 

