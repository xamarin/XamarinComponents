using CheckNuGets.Data;
using CheckNuGets.Data.Results;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace CheckNuGets.Processors
{
    public class PackageProcessor : IDisposable
    {
        private const string FWLinkString = "go.microsoft.com/fwlink/";

        DateTime startTime = new DateTime(2018, 01, 15);
        private PackageData package;
        private WebClient webClient;

        public PackageProcessor(PackageData package)
        {
            this.package = package;

            webClient = new WebClient();
        }

        public void Dispose()
        {
            this.package = null;

            webClient = null;
        }

        public PackageVersion LatestVersion
        {
            get
            {
                var lastVesionIndex = package.Versions.Count - 1;

                return package.Versions[lastVesionIndex];
            }
        }
        public async Task<ProcessResult> ProcessAsync()
        {
            var result = new ProcessResult()
            {
                PackageId = package.PackageId,
                PackageTitle = package.Title,
                CurrentVersion = package.CurrentVersion,
                TotalVersions = package.Versions.Count,
                TotalDownloads = package.TotalDownloads,
            };

            result.IsSigned = await VerifySigned();

            result.UrlResult = await VerifyUrls();

            return result;
        }

        private async Task<UrlResults> VerifyUrls()
        {
            var result = new UrlResults();

            try
            {
                if (!string.IsNullOrWhiteSpace(package.ProjectUrl))
                {
                    result.ProjectUrlIsFWLink = package.ProjectUrl.Contains(FWLinkString);

                    result.ProjectUrlIsValid = await IsURLValid(package.ProjectUrl);
                }

                if (!string.IsNullOrWhiteSpace(package.LicenceUrl))
                {
                    result.LicenceUrlIsFWLink = package.LicenceUrl.Contains(FWLinkString);

                    result.LicenceUrlIsValid = await IsURLValid(package.LicenceUrl);
                }

                if (!string.IsNullOrWhiteSpace(package.IconUrl))
                {
                    result.IconUrlIsValid = await IsURLValid(package.IconUrl);
                }
            }
            catch (Exception ex)
            {

                throw;
            }


            return result;
        }
        private async Task<bool> VerifySigned()
        {
            var regLeafResponse = await webClient.DownloadStringTaskAsync(LatestVersion.RegistrationLeafUrl);
            var regLeafResult = JsonConvert.DeserializeObject<RegistrationLeafResponse>(regLeafResponse, JsonDeserializeSettings.Default);

            if (regLeafResult.Published < startTime)
                return false;

            var catalogEntryResponse = await webClient.DownloadStringTaskAsync(regLeafResult.CatalogEntryUrl);
            var catalogEntryResult = JsonConvert.DeserializeObject<CatalogEntryResponse>(catalogEntryResponse, JsonDeserializeSettings.Default);

            var folderName = Path.Combine(Directory.GetCurrentDirectory(), $"{package.PackageId}-{LatestVersion.VersionString}");
            var nugetPackageName = Path.Combine(folderName, $"{package.PackageId}.{LatestVersion.VersionString}.nupkg");

            try
            {
                Directory.CreateDirectory(folderName);
                await webClient.DownloadFileTaskAsync(regLeafResult.PackageContentUrl, nugetPackageName);

                using (var aZipProc = new ZipProcessor(nugetPackageName))
                {
                    var filesToProcess = aZipProc.GetSignableFilesMS(folderName);

                    foreach (var aFile in filesToProcess)
                    {
                        if (!SigningProcessor.IsTrusted(aFile))
                            return false;
                    }

                }

            }
            finally
            {

                Directory.Delete(folderName, true);
            }

            return true;
        }

        private async Task<bool> IsURLValid(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            try
            {
                var result = await webClient.DownloadStringTaskAsync(url);

                return true;
            }
            catch (WebException ex)
            {
                return false;
            }
        }
    }
}

