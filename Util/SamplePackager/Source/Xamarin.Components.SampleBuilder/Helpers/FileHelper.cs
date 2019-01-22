using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Xamarin.Components.SampleBuilder.Helpers
{
    public static class FileHelper
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
            new Regex (@".vs", RegexOptions.IgnoreCase | RegexOptions.Compiled),
        };

        internal static void CopyDirectory(string projectPath, string target)
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
    }
}
