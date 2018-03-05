using NugetAuditor.Data;
using NugetAuditor.Data.Results;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace NugetAuditor.Processors
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

        #region Async

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

            result.IsSigned = await VerifySignedAsync();

            
            await VerifyUrlsAsync(result);

            return result;
        }

        private async Task VerifyUrlsAsync(ProcessResult result)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(package.ProjectUrl))
                {
                    result.ProjectUrlIsFWLink = package.ProjectUrl.Contains(FWLinkString);

                    result.ProjectUrlIsValid = await ValidateUrlAsync(package.ProjectUrl);
                }

                if (!string.IsNullOrWhiteSpace(package.LicenceUrl))
                {
                    result.LicenceUrlIsFWLink = package.LicenceUrl.Contains(FWLinkString);

                    result.LicenceUrlIsValid = await ValidateUrlAsync(package.LicenceUrl);
                }

                if (!string.IsNullOrWhiteSpace(package.IconUrl))
                {
                    result.IconUrlIsValid = await ValidateUrlAsync(package.IconUrl);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private async Task<bool> VerifySignedAsync()
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

        private async Task<bool> ValidateUrlAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            try
            {
                var result = await webClient.DownloadStringTaskAsync(url);

                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }


        #endregion

        #region NonAsync

        public ProcessResult Process()
        {
            var result = new ProcessResult()
            {
                PackageId = package.PackageId,
                PackageTitle = package.Title,
                CurrentVersion = package.CurrentVersion,
                TotalVersions = package.Versions.Count,
                TotalDownloads = package.TotalDownloads,
            };

            result.IsSigned = VerifySigned();

            result.UrlResult = VerifyUrls();

            return result;
        }

        private void VerifyUrls(ProcessResult result)
        {
        
            try
            {
                if (!string.IsNullOrWhiteSpace(package.ProjectUrl))
                {
                    result.ProjectUrlIsFWLink = package.ProjectUrl.Contains(FWLinkString);

                    result.ProjectUrlIsValid = ValidateUrl(package.ProjectUrl);
                }

                if (!string.IsNullOrWhiteSpace(package.LicenceUrl))
                {
                    result.LicenceUrlIsFWLink = package.LicenceUrl.Contains(FWLinkString);

                    result.LicenceUrlIsValid = ValidateUrl(package.LicenceUrl);
                }

                if (!string.IsNullOrWhiteSpace(package.IconUrl))
                {
                    result.IconUrlIsValid = ValidateUrl(package.IconUrl);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private bool VerifySigned()
        {
            var regLeafResponse = webClient.DownloadString(LatestVersion.RegistrationLeafUrl);
            var regLeafResult = JsonConvert.DeserializeObject<RegistrationLeafResponse>(regLeafResponse, JsonDeserializeSettings.Default);

            if (regLeafResult.Published < startTime)
                return false;

            var catalogEntryResponse = webClient.DownloadString(regLeafResult.CatalogEntryUrl);
            var catalogEntryResult = JsonConvert.DeserializeObject<CatalogEntryResponse>(catalogEntryResponse, JsonDeserializeSettings.Default);

            var folderName = Path.Combine(Directory.GetCurrentDirectory(), $"{package.PackageId}-{LatestVersion.VersionString}");
            var nugetPackageName = Path.Combine(folderName, $"{package.PackageId}.{LatestVersion.VersionString}.nupkg");

            try
            {
                Directory.CreateDirectory(folderName);
                webClient.DownloadFile(regLeafResult.PackageContentUrl, nugetPackageName);

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

        private bool ValidateUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            try
            {
                var result = webClient.DownloadString(url);

                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }

        #endregion
    }
}

