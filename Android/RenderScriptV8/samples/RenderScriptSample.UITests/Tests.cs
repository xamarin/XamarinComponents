using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace RenderScriptSample.UITests
{
    [TestFixture]
    public class Tests
    {
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest ()
        {
            app = ConfigureApp.Android
                .ApkFile ("app.apk")
                .PreferIdeSettings ()
                .StartApp ();
        }

        //[Test]
        public void Repl ()
        {
            app.Repl ();
        }

        [Test]
        public void AppLaunches ()
        {
            app.Screenshot ("Launch");
        }

        [Test]
        public void BlurImage ()
        {
            app.Screenshot ("Launch");
            app.WaitForElement (q => q.Id ("seekBar1"));
            app.Query (q => q.Id ("seekBar1").Invoke ("setProgress", 15));
            app.Screenshot ("Blurred Image");
        }
    }
}

