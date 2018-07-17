using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace NugetAuditor.Core.Processors
{
    public class ZipProcessor : IDisposable
    {
        private string filePath;

        public ZipProcessor(string filePath)
        {
            this.filePath = filePath;
        }

        public void Dispose()
        {
            this.filePath = null;
        }

        public List<string> GetSignableFilesMS(string outputFolderPath)
        {
            var results = new List<string>();

            //normalise the path
            var exportPath = Path.GetFullPath(outputFolderPath);

            using (var fStream = new FileStream(this.filePath, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(fStream))
                {
                    var its = archive.Entries.Where(x => x.FullName.EndsWith(".exe") || x.FullName.EndsWith(".dll")).ToList();

                    foreach (ZipArchiveEntry entry in its)
                    {
                        var fileName = entry.FullName;

                        //check to make sure there is no funny business going on
                        if (!fileName.EndsWith(Path.DirectorySeparatorChar.ToString()))
                        {
                            var exportFilePath = Path.Combine(exportPath, fileName);


                            if (!Directory.Exists(Path.GetDirectoryName(exportFilePath)))
                                Directory.CreateDirectory(Path.GetDirectoryName(exportFilePath));

                            var aStream = entry.Open();

                            using (var eStream = new FileStream(exportFilePath, FileMode.Create, FileAccess.ReadWrite))
                            {
                                aStream.CopyTo(eStream);
                            }


                            results.Add(exportFilePath);
                        }
                            

    
                    }
                }
            }


            return results;
        }
   }
}
