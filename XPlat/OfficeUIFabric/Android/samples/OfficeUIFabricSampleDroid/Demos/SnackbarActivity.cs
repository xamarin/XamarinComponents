using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V4.Graphics.Drawable;
using Android.Views;
using Android.Widget;
using Microsoft.OfficeUIFabric;
using Button = Microsoft.OfficeUIFabric.Button;

namespace OfficeUIFabricSampleDroid.Demos
{
    [Activity]
    public class SnackbarActivity : DemoActivity, View.IOnClickListener
    {
        protected override int ContentLayoutId => Resource.Layout.activity_snackbar;

        Button btn_snackbar_single_line;
        Button btn_snackbar_single_line_custom_view;
        Button btn_snackbar_single_line_action;
        Button btn_snackbar_single_line_action_custom_view;

        Button btn_snackbar_multiline;
        Button btn_snackbar_multiline_custom_view;
        Button btn_snackbar_multiline_action;
        Button btn_snackbar_multiline_action_custom_view;
        Button btn_snackbar_multiline_long_action;

        Button btn_snackbar_announcement;

        ViewGroup root_view;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            root_view = FindViewById<ViewGroup>(Resource.Id.root_view);

            btn_snackbar_single_line = FindViewById<Button>(Resource.Id.btn_snackbar_single_line);
            btn_snackbar_single_line_custom_view = FindViewById<Button>(Resource.Id.btn_snackbar_single_line_custom_view);
            btn_snackbar_single_line_action = FindViewById<Button>(Resource.Id.btn_snackbar_single_line_action);
            btn_snackbar_single_line_action_custom_view = FindViewById<Button>(Resource.Id.btn_snackbar_single_line_action_custom_view);

            btn_snackbar_multiline = FindViewById<Button>(Resource.Id.btn_snackbar_multiline);
            btn_snackbar_multiline_custom_view = FindViewById<Button>(Resource.Id.btn_snackbar_multiline_custom_view);
            btn_snackbar_multiline_action = FindViewById<Button>(Resource.Id.btn_snackbar_multiline_action);
            btn_snackbar_multiline_action_custom_view = FindViewById<Button>(Resource.Id.btn_snackbar_multiline_action_custom_view);
            btn_snackbar_multiline_long_action = FindViewById<Button>(Resource.Id.btn_snackbar_multiline_long_action);

            btn_snackbar_announcement = FindViewById<Button>(Resource.Id.btn_snackbar_announcement);

            btn_snackbar_single_line.SetOnClickListener(this);
            btn_snackbar_single_line_custom_view.SetOnClickListener(this);
            btn_snackbar_single_line_action.SetOnClickListener(this);
            btn_snackbar_single_line_action_custom_view.SetOnClickListener(this);

            btn_snackbar_multiline.SetOnClickListener(this);
            btn_snackbar_multiline_custom_view.SetOnClickListener(this);
            btn_snackbar_multiline_action.SetOnClickListener(this);
            btn_snackbar_multiline_action_custom_view.SetOnClickListener(this);
            btn_snackbar_multiline_long_action.SetOnClickListener(this);

            btn_snackbar_announcement.SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            var avatarView = new AvatarView(this);

            avatarView.AvatarSize = AvatarSize.Medium;
            avatarView.Name = Resources.GetString(Resource.String.persona_name_johnie_mcconnell);

            var thumbnailImageView = new ImageView(this);
            var thumbnailBitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.thumbnail_example_32);
            var roundedCornerThumbnailDrawable = RoundedBitmapDrawableFactory.Create(Resources, thumbnailBitmap);
            roundedCornerThumbnailDrawable.CornerRadius = Resources.GetDimension(Resource.Dimension.uifabric_snackbar_background_corner_radius);
            thumbnailImageView.SetImageDrawable(roundedCornerThumbnailDrawable);

            switch(v.Id) {
                // Single line
                case Resource.Id.btn_snackbar_single_line:
                    Snackbar.Make(root_view, GetString(Resource.String.snackbar_single_line), Snackbar.LengthShort, Snackbar.Style.Regular).Show();
                    break;

                case Resource.Id.btn_snackbar_single_line_custom_view:
                    var circularProgress = (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop) ?
                        new Microsoft.OfficeUIFabric.ProgressBar(this, null, 0, Resource.Style.Widget_UIFabric_CircularProgress_Small)
                        : LayoutInflater.Inflate(Resource.Layout.view_snackbar_circular_progress, null, false) as Microsoft.OfficeUIFabric.ProgressBar;

                    circularProgress.IndeterminateDrawable.SetColorFilter(
                        new Color (ContextCompat.GetColor(this, Resource.Color.snackbar_circular_progress_drawable)), PorterDuff.Mode.SrcIn);

                    Snackbar.Make(root_view, GetString(Resource.String.snackbar_single_line), Snackbar.LengthLong, Snackbar.Style.Regular)                    
                        .SetCustomView(circularProgress, Snackbar.CustomViewSize.Medium)
                        .Show();
                    break;
                case Resource.Id.btn_snackbar_single_line_action:
                    Snackbar.Make(root_view, GetString(Resource.String.snackbar_single_line))
                        .SetAction(GetString(Resource.String.snackbar_action), () => { }).Show();
                    break;

                case Resource.Id.btn_snackbar_single_line_action_custom_view:
                    Snackbar.Make(root_view, GetString(Resource.String.snackbar_single_line))
                    .SetCustomView(avatarView, Snackbar.CustomViewSize.Medium)
                    .SetAction(GetString(Resource.String.snackbar_action), () => { })
                    .Show();
                    break;

            // Multiline
                case Resource.Id.btn_snackbar_multiline:
                    Snackbar.Make(root_view, GetString(Resource.String.snackbar_multiline), Snackbar.LengthLong).Show();
                    break;
                case Resource.Id.btn_snackbar_multiline_custom_view:
                    var doneIconImageView = new ImageView(this);
                    doneIconImageView.SetImageDrawable(ContextCompat.GetDrawable(this, Resource.Drawable.ic_done_white));

                    Snackbar.Make(root_view, GetString(Resource.String.snackbar_multiline), Snackbar.LengthLong)
                        .SetCustomView(doneIconImageView, Snackbar.CustomViewSize.Small)
                        .Show();
                    break;

                case Resource.Id.btn_snackbar_multiline_action:
                    var snackbar = Snackbar.Make(root_view, GetString(Resource.String.snackbar_multiline), Snackbar.LengthIndefinite)
                        .SetAction(GetString(Resource.String.snackbar_action), () => { });
                    snackbar.Show();

                    System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(2))
                        .ContinueWith(t =>
                        {
                            snackbar.View.Post(() => snackbar.SetText(GetString(Resource.String.snackbar_description_updated)));
                        });
                    break;

                case Resource.Id.btn_snackbar_multiline_action_custom_view:
                    Snackbar.Make(root_view, GetString(Resource.String.snackbar_multiline))
                        .SetCustomView(thumbnailImageView, Snackbar.CustomViewSize.Medium)
                        .SetAction(GetString(Resource.String.snackbar_action), () => {
                            // handle click here
                        }).Show();
                    break;

                case Resource.Id.btn_snackbar_multiline_long_action:
                    Snackbar.Make(root_view, GetString(Resource.String.snackbar_multiline))
                        .SetAction(GetString(Resource.String.snackbar_action_long), () => {
                            // handle click here
                        }).Show();
                    break;

                // Announcement style
                case Resource.Id.btn_snackbar_announcement:
                    var announcementIconImageView = new ImageView(this);
                    announcementIconImageView.SetImageDrawable(ContextCompat.GetDrawable(this, Resource.Drawable.ic_birthday));

                    Snackbar.Make(root_view, GetString(Resource.String.snackbar_announcement), Snackbar.LengthShort, Snackbar.Style.Announcement)
                        .SetCustomView(announcementIconImageView, Snackbar.CustomViewSize.Medium)
                        .SetAction(GetString(Resource.String.snackbar_action), () =>
                        {
                            // handle click here
                        }).Show();
                    break;
            }
        }
    }
}