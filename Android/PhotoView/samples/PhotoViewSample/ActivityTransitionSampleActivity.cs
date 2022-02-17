using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using AndroidX.RecyclerView.Widget;

namespace PhotoViewSample
{
    [Activity(Label = "Activity Transition Sample")]
    public class ActivityTransitionSampleActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_transition);

            var list = FindViewById<RecyclerView>(Resource.Id.list);
            list.SetLayoutManager(new GridLayoutManager(this, 2));

            var imageAdapter = new ImageAdapter();
            imageAdapter.ImageClicked += view => Transition(view);
            list.SetAdapter(imageAdapter);
        }

        private void Transition(View view)
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
            {
                Toast.MakeText(this, "21+ only, keep out", ToastLength.Short).Show();
            }
            else
            {
                var intent = new Intent(this, typeof(ActivityTransitionToSampleActivity));
                var options = ActivityOptionsCompat.MakeSceneTransitionAnimation(this, view, GetString(Resource.String.transition_test));
                StartActivity(intent, options.ToBundle());
            }
        }

        private class ImageAdapter : RecyclerView.Adapter
        {
            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                var holder = ImageViewHolder.Create(parent);
                holder.ItemView.Click += (sender, e) => ImageClicked?.Invoke(holder.ItemView);
                return holder;
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) { }

            public override int ItemCount => 20;

            public event Action<View> ImageClicked;
        }

        private class ImageViewHolder : RecyclerView.ViewHolder
        {
            public static ImageViewHolder Create(ViewGroup parent)
            {
                var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_image, parent, false);
                return new ImageViewHolder(view);
            }

            public ImageViewHolder(View view) : base(view) { }
        }
    }
}