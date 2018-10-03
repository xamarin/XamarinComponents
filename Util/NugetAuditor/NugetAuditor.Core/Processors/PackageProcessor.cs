using Newtonsoft.Json;
using NugetAuditor.Core.Helpers;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Nuget.Validator;

namespace NugetAuditor.Core.Processors
{
    public class PackageProcessor : IDisposable
    {
        private const string FWLinkString = "go.microsoft.com/fwlink/";
        private const string MSCopyright = "© Microsoft Corporation. All rights reserved.";

        private PackageData package;
        private PackageSearchData searchData;
        private WebClient webClient;

        public PackageProcessor(PackageData package, PackageSearchData searchData)
        {
            this.package = package;
            this.searchData = searchData;

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
                DatePublished = searchData.Published.Date,
            };

            result.IsSigned = await VerifySignedAsync();

            await VerifyUrlsAsync(result);

            return result;
        }

        private async Task<RegistrationLeafResponse> GetLeafResponseAsync()
        {
            var regLeafResponse = await webClient.DownloadStringTaskAsync(LatestVersion.RegistrationLeafUrl);
            var regLeafResult = JsonConvert.DeserializeObject<RegistrationLeafResponse>(regLeafResponse, JsonDeserializeSettings.Default);

            return regLeafResult;
        }

        private async Task VerifyUrlsAsync(ProcessResult result)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(package.ProjectUrl))
                {
                    result.ProjectUrlIsFWLink = package.ProjectUrl.Contains(FWLinkString);

                    result.ProjectUrlPointsToComponentsStore = package.ProjectUrl.ToLower().Contains("components.xamarin.com");

                    result.ProjectUrlIsValid = await ValidateUrlAsync(package.ProjectUrl);
                }

                if (!string.IsNullOrWhiteSpace(package.LicenceUrl))
                {
                    result.LicenceUrlIsFWLink = package.LicenceUrl.Contains(FWLinkString);

                    result.LicenceUrlPointsToComponentsStore = package.LicenceUrl.ToLower().Contains("components.xamarin.com");

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
            var leaf = await GetLeafResponseAsync();

            if (leaf.Published < SettingsHelper.CutOffDateTime)
                return false;

            var folderName = Path.Combine(System.IO.Path.GetTempPath(), $"{package.PackageId}-{LatestVersion.VersionString}");
            var nugetPackageName = Path.Combine(folderName, $"{package.PackageId}.{LatestVersion.VersionString}.nupkg");

            try
            {
                Directory.CreateDirectory(folderName);
                await webClient.DownloadFileTaskAsync(leaf.PackageContentUrl, nugetPackageName);

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
                DatePublished = searchData.Published.Date,
                Copyright = searchData.Copyright,
            };

            if (!string.IsNullOrWhiteSpace(result.Copyright))
                result.Copyright = result.Copyright.Replace("Â", string.Empty); 

            result.IsValidCopyright = CheckCopyright(result.Copyright);

            result.Owners = string.Join(",", searchData.PackageRegistration.Owners);
            result.HasMicrosoftOwner = result.Owners.ToLower().Contains("microsoft");

            result.IsSigned = VerifySigned();

            VerifyUrls(result);

            return result;
        }

        private bool CheckCopyright(string copyright)
        {
            if (string.IsNullOrWhiteSpace(copyright))
                return false;

            return MSCopyright.Equals(copyright);
        }

        private RegistrationLeafResponse GetLeafResponse()
        {
            var regLeafResponse = webClient.DownloadString(LatestVersion.RegistrationLeafUrl);
            var regLeafResult = JsonConvert.DeserializeObject<RegistrationLeafResponse>(regLeafResponse, JsonDeserializeSettings.Default);

            return regLeafResult;
        }

        private void VerifyUrls(ProcessResult result)
        {
        
            try
            {
                if (!string.IsNullOrWhiteSpace(package.ProjectUrl))
                {
                    result.ProjectUrlIsFWLink = package.ProjectUrl.Contains(FWLinkString);

                    result.ProjectUrlPointsToComponentsStore = package.ProjectUrl.ToLower().Contains("components.xamarin.com");

                    result.ProjectUrlIsValid = ValidateUrl(package.ProjectUrl);
                }

                if (!string.IsNullOrWhiteSpace(package.LicenceUrl))
                {
                    result.LicenceUrlIsFWLink = package.LicenceUrl.Contains(FWLinkString);

                    result.LicenceUrlPointsToComponentsStore = package.LicenceUrl.ToLower().Contains("components.xamarin.com");

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
            var leaf = GetLeafResponse();

            if (leaf.Published < SettingsHelper.CutOffDateTime)
                return false;

            var folderName = Path.Combine(Path.GetTempPath(), $"{package.PackageId}-{LatestVersion.VersionString}");
            var nugetPackageName = Path.Combine(folderName, $"{package.PackageId}.{LatestVersion.VersionString}.nupkg");

            try
            {
                Directory.CreateDirectory(folderName);
                webClient.DownloadFile(leaf.PackageContentUrl, nugetPackageName);

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
            catch (Exception)
            {

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

