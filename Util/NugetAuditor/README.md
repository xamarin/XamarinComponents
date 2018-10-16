# Nuget Audit Robot

The Nuget Audit Robot is designed to verify and validate the Xamarin owned nugets.

It will check the following:

 - All dlls and exes are signed
 - Licence and Project urls are using FWLinks
 - Licence, Project and Icon urls are valid and working

It also collects data about each package which can be stored in a Azure or MS Sql Database

 - Total Downloads
 - Latest Version
 - Lastest Publish Date
 - Total Versions
 

# Nuget Validator example

    var options = new NugetValidatorOptions(
    {
        Copyright = "© Microsoft Corporation. All rights reserved.",
        Author = "Microsoft",
        Owner = "Microsoft",
        NeedsProjectUrl = true,
        NeedsLicenseUrl = true,
        ValidateRequireLicenseAcceptance = true,
        ValidPackageNamespace = new [] { "Xamarin" },
    };

    var result = NugetValidator.Validate(nugetPath, options);

    if (!result.Success)
    {
        Console.WriteLine($"Nuget at path: {nugetPath} failed validation" + Environment.NewLine);
        Console.Write(result.ErrorMessages);
    }
    else
    {
        Console.WriteLine("Validation Passed");
    }
