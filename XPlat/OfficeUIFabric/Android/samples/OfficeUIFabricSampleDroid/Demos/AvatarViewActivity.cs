
using Android.App;
using Android.OS;
using Android.Widget;
using Microsoft.OfficeUIFabric;
using Square.Picasso;

namespace OfficeUIFabricSampleDroid.Demos
{
    [Activity]
    public class AvatarViewActivity : DemoActivity
    {
        protected override int ContentLayoutId => Resource.Layout.activity_avatar_view;

        AvatarView avatar_example_medium_photo;
        AvatarView avatar_example_large_photo;

        AvatarView avatar_example_xlarge_photo;
        AvatarView avatar_example_small_photo;
        AvatarView avatar_example_large_initials_square;
        TableRow avatar_circle_example_xxlarge;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            avatar_example_medium_photo = FindViewById<AvatarView>(Resource.Id.avatar_example_medium_photo);
            avatar_example_large_photo = FindViewById<AvatarView>(Resource.Id.avatar_example_large_photo);
            avatar_example_xlarge_photo = FindViewById<AvatarView>(Resource.Id.avatar_example_xlarge_photo);
            avatar_example_small_photo = FindViewById<AvatarView>(Resource.Id.avatar_example_small_photo);
            avatar_example_large_initials_square = FindViewById<AvatarView>(Resource.Id.avatar_example_large_initials_square);
            avatar_circle_example_xxlarge = FindViewById<TableRow>(Resource.Id.avatar_circle_example_xxlarge);


            // Avatar drawables with bitmap
            LoadBitmapFromPicasso(avatar_example_medium_photo);
            LoadBitmapFromPicasso2(avatar_example_large_photo);

            avatar_example_xlarge_photo.AvatarImageResourceId = new Java.Lang.Integer(Resource.Drawable.avatar_erik_nason);

            avatar_example_small_photo.Name = GetString(Resource.String.persona_name_kat_larsson);
            avatar_example_small_photo.Email = GetString(Resource.String.persona_email_kat_larsson);
            avatar_example_small_photo.AvatarImageResourceId = new Java.Lang.Integer (Resource.Drawable.avatar_kat_larsson);

            // Avatar drawable with initials
            avatar_example_large_initials_square.Name = GetString(Resource.String.persona_email_henry_brill);
            avatar_example_large_initials_square.AvatarStyle = AvatarStyle.Square;

            // Add AvatarView with code
            CreateNewAvatarFromCode();
        }

        void LoadBitmapFromPicasso(ImageView imageView)
        {
            Picasso.With(this).Load(Resource.Drawable.avatar_celeste_burton)
                .Into(imageView);
        }

        void LoadBitmapFromPicasso2(ImageView imageView)
        {
            Picasso.With(this)
                .Load(Resource.Drawable.avatar_isaac_fielder)
                .Into(imageView);
        }

        void CreateNewAvatarFromCode()
        {
            var avatarView = new AvatarView(this);
            avatarView.AvatarSize = AvatarSize.Xxlarge;
            avatarView.Name = GetString(Resource.String.persona_name_mauricio_august);
            avatarView.Email = GetString(Resource.String.persona_email_mauricio_august);

            avatar_circle_example_xxlarge.AddView(avatarView);
        }
    }
}