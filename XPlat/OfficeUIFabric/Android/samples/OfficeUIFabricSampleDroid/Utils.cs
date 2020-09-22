using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Microsoft.OfficeUIFabric;

namespace OfficeUIFabricSampleDroid
{
    internal class Utils
    {
        internal static IEnumerable<IPersona> CreatePersonaList(Context context)
            => context == null ? new List<IPersona>() :
            new List<IPersona> {
                CreatePersona(
                    context.GetString(Resource.String.persona_name_amanda_brady),
                    context.GetString(Resource.String.persona_subtitle_manager),
                    imageDrawable: ContextCompat.GetDrawable(context, Resource.Drawable.avatar_amanda_brady),
                    email: context.GetString(Resource.String.persona_email_amanda_brady)),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_lydia_bauer),
                    context.GetString(Resource.String.persona_subtitle_researcher),
                    email: context.GetString(Resource.String.persona_email_lydia_bauer)),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_daisy_phillips),
                    context.GetString(Resource.String.persona_subtitle_designer),
                    imageDrawable: ContextCompat.GetDrawable(context, Resource.Drawable.avatar_daisy_phillips),
                    email: context.GetString(Resource.String.persona_email_daisy_phillips)),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_allan_munger) + context.GetString(Resource.String.persona_truncation),
                    context.GetString(Resource.String.persona_subtitle_manager),
                    email: context.GetString(Resource.String.persona_email_allan_munger)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_kat_larsson),
                    context.GetString(Resource.String.persona_subtitle_designer),
                    imageBitmap: BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.avatar_kat_larsson),
                    email: context.GetString(Resource.String.persona_email_kat_larsson)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_ashley_mccarthy),
                    context.GetString(Resource.String.persona_subtitle_engineer)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_miguel_garcia),
                    context.GetString(Resource.String.persona_subtitle_researcher),
                    imageUri: GetUriFromResource(context, Resource.Drawable.avatar_miguel_garcia),
                    email: context.GetString(Resource.String.persona_email_miguel_garcia)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_carole_poland),
                    context.GetString(Resource.String.persona_subtitle_researcher),
                    email: context.GetString(Resource.String.persona_email_carole_poland)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_mona_kane),
                    context.GetString(Resource.String.persona_subtitle_designer),
                    email: context.GetString(Resource.String.persona_email_mona_kane)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_carlos_slattery),
                    context.GetString(Resource.String.persona_subtitle_engineer),
                    email: context.GetString(Resource.String.persona_email_carlos_slattery)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_wanda_howard),
                    context.GetString(Resource.String.persona_subtitle_engineer),
                    email: context.GetString(Resource.String.persona_email_wanda_howard)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_tim_deboer),
                    context.GetString(Resource.String.persona_subtitle_researcher),
                    email: context.GetString(Resource.String.persona_email_tim_deboer)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_robin_counts),
                    context.GetString(Resource.String.persona_subtitle_designer),
                    email: context.GetString(Resource.String.persona_email_robin_counts)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_elliot_woodward),
                    context.GetString(Resource.String.persona_subtitle_designer),
                    email: context.GetString(Resource.String.persona_email_elliot_woodward)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_cecil_folk),
                    context.GetString(Resource.String.persona_subtitle_manager),
                    email: context.GetString(Resource.String.persona_email_cecil_folk)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_celeste_burton),
                    context.GetString(Resource.String.persona_subtitle_researcher),
                    email: context.GetString(Resource.String.persona_email_celeste_burton)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_elvia_atkins),
                    context.GetString(Resource.String.persona_subtitle_designer),
                    imageDrawable: ContextCompat.GetDrawable(context, Resource.Drawable.avatar_elvia_atkins),
                    email: context.GetString(Resource.String.persona_email_elvia_atkins)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_colin_ballinger),
                    context.GetString(Resource.String.persona_subtitle_manager),
                    imageDrawable: ContextCompat.GetDrawable(context, Resource.Drawable.avatar_colin_ballinger),
                    email: context.GetString(Resource.String.persona_email_colin_ballinger)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_katri_ahokas),
                    context.GetString(Resource.String.persona_subtitle_designer),
                    imageBitmap: BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.avatar_katri_ahokas),
                    email: context.GetString(Resource.String.persona_email_katri_ahokas)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_henry_brill),
                    context.GetString(Resource.String.persona_subtitle_engineer),
                    email: context.GetString(Resource.String.persona_email_henry_brill)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_johnie_mcconnell),
                    context.GetString(Resource.String.persona_subtitle_researcher),
                    imageUri: GetUriFromResource(context, Resource.Drawable.avatar_johnie_mcconnell),
                    email: context.GetString(Resource.String.persona_email_johnie_mcconnell)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_kevin_sturgis),
                    context.GetString(Resource.String.persona_subtitle_researcher),
                    email: context.GetString(Resource.String.persona_email_kevin_sturgis)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_kristen_patterson),
                    context.GetString(Resource.String.persona_subtitle_designer),
                    email: context.GetString(Resource.String.persona_email_kristen_patterson)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_charlotte_waltson),
                    context.GetString(Resource.String.persona_subtitle_engineer),
                    email: context.GetString(Resource.String.persona_email_charlotte_waltson)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_erik_nason),
                    context.GetString(Resource.String.persona_subtitle_engineer),
                    email: context.GetString(Resource.String.persona_email_erik_nason)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_isaac_fielder),
                    context.GetString(Resource.String.persona_subtitle_researcher),
                    email: context.GetString(Resource.String.persona_email_isaac_fielder)
                ),
                CreatePersona(
                    context.GetString(Resource.String.persona_name_mauricio_august),
                    context.GetString(Resource.String.persona_subtitle_designer),
                    email: context.GetString(Resource.String.persona_email_mauricio_august)
                ),
                CreateCustomPersona(context)
            };

        internal static Android.Net.Uri GetUriFromResource(Context context, int avatarDrawable)
            => Android.Net.Uri.Parse(ContentResolver.SchemeAndroidResource +
                "://" + context.Resources.GetResourcePackageName(avatarDrawable) +
                "/" + context.Resources.GetResourceTypeName(avatarDrawable) +
                "/" + context.Resources.GetResourceEntryName(avatarDrawable));

        internal static IPersona CreatePersona(string name, string subtitle, int? imageResource = null, Drawable imageDrawable = null, Bitmap imageBitmap = null, Android.Net.Uri imageUri = null, string email = "")
            => new Persona(name, email)
            {
                Subtitle = subtitle,
                AvatarImageResourceId = imageResource.HasValue ? new Java.Lang.Integer(imageResource.Value) : null,
                AvatarImageDrawable = imageDrawable,
                AvatarImageBitmap = imageBitmap,
                AvatarImageUri = imageUri,
            };

        internal static CustomPersona CreateCustomPersona(Context context, string email = null)
            => new CustomPersona(context.GetString(Resource.String.persona_name_robert_tolbert), email ?? string.Empty)
            {
                AvatarImageDrawable = ContextCompat.GetDrawable(context, Resource.Drawable.avatar_robert_tolbert),
                Description = context.GetString(Resource.String.people_picker_custom_persona_description)
            };
    }

    internal class CustomPersona : Java.Lang.Object, IPersona
    {
        public CustomPersona(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Description { get; set; }

        public string Footer { get; set; }
        public string Subtitle { get; set; }
        public Bitmap AvatarImageBitmap { get; set; }
        public Drawable AvatarImageDrawable { get; set; }
        public Integer AvatarImageResourceId { get; set; }
        public Android.Net.Uri AvatarImageUri { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public Integer AvatarBackgroundColor 
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }
    }
}