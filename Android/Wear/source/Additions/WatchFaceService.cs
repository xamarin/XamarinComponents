using System;
using Android.Runtime;

namespace Android.Support.Wearable.Watchface
{
    public partial class WatchFaceService
    {
        // Metadata.xml XPath method reference: path="/api/package[@name='android.support.wearable.watchface']/class[@name='WatchFaceService']/method[@name='onCreateEngine' and count(parameter)=0]"
        [Register ("onCreateEngine", "()Landroid/support/wearable/watchface/WatchFaceService$Engine;", "GetOnCreateEngineHandler")]
        public override abstract Android.Service.Wallpaper.WallpaperService.Engine OnCreateEngine ();
    }

    public partial class CanvasWatchFaceService
    {
        // Metadata.xml XPath method reference: path="/api/package[@name='android.support.wearable.watchface']/class[@name='CanvasWatchFaceService']/method[@name='onCreateEngine' and count(parameter)=0]"
        [Register ("onCreateEngine", "()Landroid/support/wearable/watchface/CanvasWatchFaceService$Engine;", "GetOnCreateEngineHandler")]
        public override abstract Android.Service.Wallpaper.WallpaperService.Engine OnCreateEngine ();
    }

    public partial class Gles2WatchFaceService
    {
        // Metadata.xml XPath method reference: path="/api/package[@name='android.support.wearable.watchface']/class[@name='Gles2WatchFaceService']/method[@name='onCreateEngine' and count(parameter)=0]"
        [Register ("onCreateEngine", "()Landroid/support/wearable/watchface/Gles2WatchFaceService$Engine;", "GetOnCreateEngineHandler")]
        public override abstract Android.Service.Wallpaper.WallpaperService.Engine OnCreateEngine ();
    }
}