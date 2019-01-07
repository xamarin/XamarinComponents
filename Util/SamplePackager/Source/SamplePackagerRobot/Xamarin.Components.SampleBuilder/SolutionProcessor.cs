using System;
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

        public static string Process(string solutionPath, string outputPath)
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
            newSolutuion.UpdateSampleReferencesAndClean();


            var zipPath = outputPath + ".zip";

            if (File.Exists(zipPath))
                File.Delete(zipPath);

            ZipFile.CreateFromDirectory(outputPath, zipPath);

            Directory.Delete(outputPath, true);

            return zipPath;
        }
        #endregion

        #region Private Methods

        private static ProjectSpec CopyToTemp(string projectPath, string outputPath)
        {
            var parentPath = Path.GetDirectoryName(projectPath);

            var slnFilePath = string.Empty;
            var loops = 0;

            while (string.IsNullOrWhiteSpace(slnFilePath))
            {
                if (loops > 1)
                    throw new Exception("Cannot find the sln file");

                var files = Directory.GetFiles(parentPath, "*.sln");

                if (files.Count() > 0)
                {
                    slnFilePath = files[0];
                    break;
                }
                else
                {
                    parentPath = new DirectoryInfo(parentPath).Parent.FullName;
                }

                loops++;
            }

            var slnPath = Path.GetDirectoryName(slnFilePath);

            var name = new DirectoryInfo(slnPath).Name;

            var tempPath = Path.Combine(outputPath, name);

            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);

            FileHelper.CopyDirectory(slnPath, tempPath);

            var newPath = projectPath.Replace(slnPath, tempPath);
            var newSlPath = slnFilePath.Replace(slnPath, tempPath);

            var projectName = Path.GetFileNameWithoutExtension(projectPath);

            var aproj = new ProjectSpec()
            {
                Name = projectName,
                SourceProjectPath = projectPath,
                TempProjectPath = newPath,
                OriginalSolutionPath = slnFilePath,
                TempSolutionPath = newSlPath,
            };

            return aproj;
        }

        

        private static void ValidateParameters(string projectPath, string outputPath)
        {
            if (string.IsNullOrWhiteSpace(projectPath))
                throw new Exception("SolutionPath cannot be empty");

            if (string.IsNullOrWhiteSpace(outputPath))
                throw new Exception("OutputPath cannot be empty");

            if (Path.GetExtension(projectPath) != ".csproj")
                throw new Exception("You must provide a link to a .cproj file");

            if (!File.Exists(projectPath))
                throw new Exception($"{projectPath} is cannot be found");
            
            if (!Directory.Exists(outputPath))
                throw new Exception($"Output path {outputPath} doesn't exist");

            var fileAttrs = File.GetAttributes(outputPath);

            if ((fileAttrs & FileAttributes.Directory) != FileAttributes.Directory)
                throw new Exception($"Output path is not a directory");
        }


        #endregion
    }
}
