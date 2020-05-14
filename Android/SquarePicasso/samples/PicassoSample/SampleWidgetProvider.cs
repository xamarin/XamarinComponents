using System;
using Android.Appwidget;
using Android.Content;
using Android.Widget;
using Square.Picasso;
using Android.App;

namespace PicassoSample
{
    [BroadcastReceiver]
    [IntentFilter(new[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [MetaData("android.appwidget.provider", Resource = "@xml/sample_widget_info")]
    public class SampleWidgetProvider : AppWidgetProvider
    {
        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            RemoteViews updateViews = new RemoteViews(context.PackageName, Resource.Layout.sample_widget);

            // Load image for all appWidgetIds.
            Picasso picasso = Picasso.Get();
            picasso.Load(Data.Urls[(new Random()).Next(Data.Urls.Length)])
                   .Placeholder(Resource.Drawable.placeholder)
                   .Error(Resource.Drawable.error)
                   .Transform(new GrayscaleTransformation(picasso))
                   .Into(updateViews, Resource.Id.image, appWidgetIds);
        }
    }
}
