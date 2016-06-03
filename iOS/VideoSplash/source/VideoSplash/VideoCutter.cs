using System;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

#if __UNIFIED__
using AVFoundation;
using CoreMedia;
using Foundation;
#else
using MonoTouch.AVFoundation;
using MonoTouch.CoreMedia;
using MonoTouch.Foundation;

using CGRect = System.Drawing.RectangleF;
using CGPoint = System.Drawing.PointF;
using nfloat = System.Single;
#endif

namespace VideoSplash
{
    public static class VideoCutter
    {
        public static async Task<NSUrl> CropVideoAsync(NSUrl url, nfloat startTime, nfloat durationTime)
        {
            // get output url
            var outputURL = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (!Directory.Exists(outputURL))
            {
                Directory.CreateDirectory(outputURL);
            }
            outputURL = Path.Combine(outputURL, "output.mp4");
            try
            {
                File.Delete(outputURL);
            }
            catch
            {
                Debug.WriteLine("Export failed to remove destination file");
                return null;
            }

            // set up for export
            var asset = new AVUrlAsset(url, (AVUrlAssetOptions)null);
            var exportSession = new AVAssetExportSession(asset, AVAssetExportSession.PresetHighestQuality);
            exportSession.OutputUrl = new NSUrl(outputURL, false);
            exportSession.ShouldOptimizeForNetworkUse = true;
            exportSession.OutputFileType = AVFileType.Mpeg4;
            exportSession.TimeRange = new CMTimeRange
            {
                Start = CMTime.FromSeconds(startTime, 600),
                Duration = CMTime.FromSeconds(durationTime, 600)
            };

            // export
            await exportSession.ExportTaskAsync();
            if (exportSession.Status != AVAssetExportSessionStatus.Completed)
            {
                Debug.WriteLine("Export failed: " + exportSession.Status);
                return null;
            }

            return exportSession.OutputUrl;
        }
    }
}
