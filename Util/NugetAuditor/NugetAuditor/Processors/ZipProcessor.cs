using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace CheckNuGets.Processors
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

            using (ZipArchive archive = System.IO.Compression.ZipFile.OpenRead(this.filePath))
            {
                var its = archive.Entries.Where(x => x.FullName.EndsWith(".exe") || x.FullName.EndsWith(".dll")).ToList();

                foreach (ZipArchiveEntry entry in its)
                {
                    
                    var exportFilePath = Path.Combine(outputFolderPath, entry.FullName);

                    if (!Directory.Exists(Path.GetDirectoryName(exportFilePath)))
                        Directory.CreateDirectory(Path.GetDirectoryName(exportFilePath));

                    entry.ExtractToFile(exportFilePath, true);

                    results.Add(exportFilePath);
                }
            }

            return results;
        }
   }
}
