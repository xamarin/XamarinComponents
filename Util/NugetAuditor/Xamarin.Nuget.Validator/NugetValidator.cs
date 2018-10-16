using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace Xamarin.Nuget.Validator
{
    public class NugetValidator
    {
        public static ValidationResult Validate(string path, NugetValidatorOptions options)
        {
            var result = new ValidationResult();

            ValidatePath(path);
            ValidateOptions(options);

            //need to extract the file
            var folderName = Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString());

            var nugetFilePath = ExtractNuspecFile(path, folderName);

            if (string.IsNullOrWhiteSpace(nugetFilePath))
                throw new Exception($"Could not find the nuspec file");

            var errors =  ProcessNuspecFile(nugetFilePath, options);

            if (errors.Count > 0)
            {
                result.Success = false;

                foreach (var aMsg in errors)
                    result.Errors.Add(aMsg);
            }

            return result;
        }

        private static string ExtractNuspecFile(string filePath, string folderName)
        {
            using (var aZipProc = new ZipProcessor(filePath))
            {

                return aZipProc.GetNuspecFile(folderName);

            }

        }

        private static void ValidateOptions(NugetValidatorOptions options)
        {
            if (options == null)
                throw new Exception("You must provide an instance of NugetValidatorOptions to NugetValidator.Validate");
        }

        private static void ValidatePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new Exception("You must provide a path to the nupkg file");

            if (!File.Exists(path))
                throw new Exception($"The file {path} cannot be found");

            if (!Path.GetExtension(path).Equals(".nupkg"))
                throw new Exception($"The file {path} is not a nupkg file");
        }

        private static List<string> ProcessNuspecFile(string filePath, NugetValidatorOptions options)
        {

            //var text = File.ReadAllLines(filePath);

            var xml = new XmlDocument();
            xml.Load(filePath);

            //var str = new XmlSerializer(typeof(Package));

            var nuspec = PackageDefinition.FromXml(xml);

           



            //using (var aStream = new FileStream(filePath, FileMode.Open))
            //{


            //   nuspec = (Package)str.Deserialize(aStream);
            //}

            var errors = new List<string>();

            // var metEntry = xmlcontents.FirstChild;
            if (!nuspec.Authors.Contains(options.Author.ToLower()))
                errors.Add($"{options.Author} is not an author of the package");

            if (!nuspec.Owners.Contains(options.Owner.ToLower()))
                errors.Add($"{options.Owner} is not an owner of the package");

            if (!nuspec.Copyright.Equals(options.Copyright, StringComparison.OrdinalIgnoreCase))
                errors.Add($"{options.Copyright} is not used as the copyright for the package");

            if (options.NeedsProjectUrl && string.IsNullOrWhiteSpace(nuspec.ProjectUrl))
                errors.Add("You must have a project url");

            if (options.NeedsLicenseUrl && string.IsNullOrWhiteSpace(nuspec.LicenseUrl))
                errors.Add("You must have a license url");

            if (options.ValidateRequireLicenseAcceptance && nuspec.RequireLicenseAcceptance != true)
                errors.Add("You must have RequireLicenceAcceptance set to true");

            if (options.ValidPackageNamespace != null && options.ValidPackageNamespace.Length > 0)
            {
                // matching the following patterns:
                //    'NameSpace' exactly. This is useful for the 'SkiaSharp' base package
                //    'NameSpace.*' pattern. This is useful for the 'Xamarin.Forms' and 'SkiaSharp.Views' packages.

                var match = options.ValidPackageNamespace.Any(valid =>
                    nuspec.Id.StartsWith(valid + ".", StringComparison.OrdinalIgnoreCase) ||
                    nuspec.Id.Equals(valid, StringComparison.OrdinalIgnoreCase));

                if (!match)
                    errors.Add($"The package does not start with any of the following namespaces: [ '{string.Join("', '", options.ValidPackageNamespace)}' ]");
            }

            return errors;

        }
    }
}
