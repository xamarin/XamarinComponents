using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

using Square.Picasso;

namespace PicassoSample
{
    public class PicassoSampleAdapter : BaseAdapter<PicassoSampleAdapter.Sample>
    {
        private const int NotificationId = 666;

        private readonly LayoutInflater inflater;

        public PicassoSampleAdapter(Context context)
        {
            inflater = LayoutInflater.From(context);
        }

        public override int Count
        {
            get { return Sample.Values.Length; }
        }

        public override Sample this[int position]
        {
            get { return Sample.Values[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView view = (TextView)convertView;
            if (view == null)
            {
                view = (TextView)inflater.Inflate(Resource.Layout.picasso_sample_activity_item, parent, false);
            }

            view.Text = this[position].Name;

            return view;
        }

        public class Sample
        {
            static Sample()
            {
                Values = new[]
                {
                    new Sample("Image Grid View", typeof(SampleGridViewActivity)),
                    new Sample("Load from Gallery", typeof(SampleGalleryActivity)),
                    new Sample("List / Detail View", typeof(SampleListDetailActivity)),
                    new Sample("Sample Notification", activity => 
                    {
                        RemoteViews remoteViews = new RemoteViews(activity.PackageName, Resource.Layout.notification_view);
                        Intent intent = new Intent(activity, typeof(SampleGridViewActivity));
                        Notification notification = new NotificationCompat.Builder(activity)
                            .SetSmallIcon(Resource.Drawable.icon)
                            .SetContentIntent(PendingIntent.GetActivity(activity, -1, intent, 0))
                            .SetContent(remoteViews)
                            .Build();
                        
                        // Bug in NotificationCompat that does not set the content.
                        if ((int)Build.VERSION.SdkInt <= (int)BuildVersionCodes.GingerbreadMr1)
                        {
                            notification.ContentView = remoteViews;
                        }
                        
                        NotificationManager notificationManager = NotificationManager.FromContext(activity);
                        notificationManager.Notify(NotificationId, notification);
                        
                        // Now load an image for this notification.
                        Picasso.Get()
                               .Load(Data.Urls[(new Random()).Next(Data.Urls.Length)])
                               .ResizeDimen(
                                   Resource.Dimension.notification_icon_width_height, 
                                   Resource.Dimension.notification_icon_width_height)
                               .Into(remoteViews, Resource.Id.photo, NotificationId, notification);
                    })
                };
            }

            public static readonly Sample[] Values;

            private readonly Type activityClass;
            private readonly Action<Activity> onLaunch;

            private Sample(string name, Type activityClass)
            {
                this.Name = name;
                this.activityClass = activityClass;
            }

            private Sample(string name, Action<Activity> onLaunch)
            {
                this.Name = name;
                this.onLaunch = onLaunch;
            }
            public string Name { get; private set; }

            public void Launch(Activity activity)
            {
                if (onLaunch == null)
                {
                    activity.StartActivity(new Intent(activity, activityClass));
                    activity.Finish();
                }
                else
                {
                    onLaunch(activity);
                }
            }
        }
    }
}
