using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;

namespace Xamarin.Components.SampleBuilder
{
    public class SolutionProcessor
    {
        static readonly Regex[] IgnorePatterns = {
            new Regex (@"Thumbs\.db", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex (@"^bin$", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex (@"^obj$", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex (@"^Debug$", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex (@"^Release$", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex (@"test\-results", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex (@"TestResults", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex (@"\.userprefs$", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex (@"\.suo$", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex (@"\.user$", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex (@"\.build$", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex (@"^\.", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex (@"\.DS_Store", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex (@"\.DotSettings", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex (@"\.pidb", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex (@"^packages$", RegexOptions.IgnoreCase | RegexOptions.Compiled),
        };

        #region Public Methods


        public static string Process(string[] projectPaths, string outputPath)
        {
            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            foreach (var projectPath in projectPaths)
            {
                ValidateParameters(projectPath, outputPath);

                //start by copying the sample solution to temp.

                var newSolution = CopyToTemp(projectPath, outputPath);

            }
            
            
            //var zipOutPut = Directory.GetParent(outputPath);
            var zipPath = outputPath + ".zip";

            if (File.Exists(zipPath))
                File.Delete(zipPath);

            ZipFile.CreateFromDirectory(outputPath, zipPath);

            Directory.Delete(outputPath,true);

            return zipPath;
        }

        #endregion

        #region Private Methods

        public static string CopyToTemp(string projectPath, string outputPath)
        {
            var parentPath = Path.GetDirectoryName(projectPath);

            var slnPath = string.Empty;
            var loops = 0;

            while (string.IsNullOrWhiteSpace(slnPath))
            {
                if (loops > 1)
                    throw new Exception("Cannot find the sln file");

                var files = Directory.GetFiles(parentPath, "*.sln");

                if (files.Count() > 0)
                {
                    slnPath = parentPath;
                    break;
                }
                else
                {
                    parentPath = new DirectoryInfo(parentPath).Parent.FullName;
                }

                loops++;
            }


            var name = new DirectoryInfo(slnPath).Name;

            var tempPath = Path.Combine(outputPath, name);

            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);

            CopyDirectory(slnPath, tempPath);

            var newPath = projectPath.Replace(slnPath, tempPath);

            return newPath;
        }

        public static void CopyDirectory(string projectPath, string target)
        {
 
            if (!Directory.Exists(target))
                Directory.CreateDirectory(target);

            var fsEntries = Directory.GetFileSystemEntries(projectPath);

            foreach (string sysEntry in fsEntries)
            {
                var fileName = Path.GetFileName(sysEntry);


                var targetPath = Path.Combine(target, fileName);

                if (!IgnorePatterns.Any(x => x.IsMatch(fileName)))
                {
                    if (Directory.Exists(sysEntry))
                        CopyDirectory(sysEntry, targetPath);
                    else
                    {
                        File.Copy(sysEntry, targetPath, true);
                    }
                }
            }
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
