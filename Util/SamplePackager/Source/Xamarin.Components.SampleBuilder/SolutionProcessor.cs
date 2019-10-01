using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Components.SampleBuilder.Helpers;
using Xamarin.Components.SampleBuilder.Models;

namespace Xamarin.Components.SampleBuilder
{
    public class SolutionProcessor
    {


        #region Public Methods

        public static string Process(string solutionPath, string outputPath, Dictionary<string, string> nugetPackageOverrides = null)
        {
            if (string.IsNullOrWhiteSpace(solutionPath))
                throw new Exception("SolutionPath cannot be empty");

            if (!File.Exists(solutionPath))
                throw new Exception("SolutionPath cannot be found");

            //get the soltion and process it
            var sourceSolution = new SolutionSpec(solutionPath);
            sourceSolution.Build();

            //copy to a new solution and build again
            var newSolutuion = sourceSolution.CopyTo(outputPath);
            newSolutuion.Build();

            //clean the temp solution and update the samples
            newSolutuion.UpdateSampleReferencesAndClean(nugetPackageOverrides);


            var zipPath = outputPath + ".zip";

            if (File.Exists(zipPath))
                File.Delete(zipPath);

            ZipFile.CreateFromDirectory(outputPath, zipPath);

            Directory.Delete(outputPath, true);

            return zipPath;
        }
        #endregion

    }
}
