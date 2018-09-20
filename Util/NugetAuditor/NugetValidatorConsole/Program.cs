using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Nuget.Validator;

namespace NugetValidatorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var nugetPath = @"C:\xamarin.firebase.abt.nupkg";

            var options = new NugetValidatorOptions()
            {
                Copyright = "© Microsoft Corporation. All rights reserved.",
                Author = "Microsoft",
                Owner = "Microsoft",
                NeedsProjectUrl = true,
                NeedsLicenseUrl = true,
                ValidateRequireLicenseAcceptance = true,
                ValidPackageNamespace = "Xamarin",
            };

            var result = NugetValidator.Validate(nugetPath, options);
            
            if (result.Success == false)
            {
                Console.WriteLine($"Nuget at path: {nugetPath} failed validation" + Environment.NewLine;

                Console.Write(result.ErrorMessages);

            }
            else
            {
                Console.WriteLine("Validation Passed");
            }
            


            Console.ReadLine();

        }
    }
}
