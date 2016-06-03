using System;
using System.Linq;
using System.Reflection;
using Android.Widget;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;

using Jazzy;
using Jazzy.Effects;

namespace JazzyViewPagerSample
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat")]
    public class MainActivity : AppCompatActivity
    {
        private JazzyViewPager jazzy;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            jazzy = FindViewById<JazzyViewPager>(Resource.Id.jazzy);

            SetupJazziness(JazzyEffects.Tablet);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var effects = typeof(JazzyEffects).GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (string effect in effects.Select(e => e.Name))
            {
                menu.Add(effect);
            }
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var title = item.TitleFormatted.ToString();
            var effect = typeof(JazzyEffects).GetField(title).GetValue(null);
            SetupJazziness((IJazzyEffect)effect);
            return true;
        }

        private void SetupJazziness(IJazzyEffect effect)
        {
            jazzy.TransitionEffect = effect;
            jazzy.Adapter = new MainAdapter(jazzy);
            jazzy.PageMargin = 30;
        }

        private class MainAdapter : JazzyPagerAdapter
        {
            private readonly Random random;

            public MainAdapter(JazzyViewPager jazzy)
                : base(jazzy)
            {
                random = new Random();
            }

            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
                var text = new TextView(JazzyViewPager.Context);
                text.Gravity = GravityFlags.Center;
                text.TextSize = 30;
                text.SetTextColor(Color.White);
                text.Text = "Page #" + position;
                text.SetPadding(30, 30, 30, 30);
                text.Background = new BitmapDrawable();
                text.SetBackgroundColor(Color.Rgb(
                    (int)(random.NextDouble() * 128) + 64,
                    (int)(random.NextDouble() * 128) + 64,
                    (int)(random.NextDouble() * 128) + 64));
                container.AddView(text, ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                SetObjectForPosition(text, position);
                return text;
            }

            public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj)
            {
                container.RemoveView(FindViewFromObject(position));
            }

            public override int Count
            {
                get { return 10; }
            }
        }
    }
}
