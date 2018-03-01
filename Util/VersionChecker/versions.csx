#load "./common/CocoaPods.csx"
#load "./common/Maven.csx"
#load "./common/GitHubReleases.csx"
#load "./common/XPath.csx"
#load "./common/SlackNotifier.csx"

var OWNER_US1 = System.Environment.GetEnvironmentVariable ("OWNER_US1");
var OWNER_CA1 = System.Environment.GetEnvironmentVariable ("OWNER_CA1");
var OWNER_MX1 = System.Environment.GetEnvironmentVariable ("OWNER_MX1");
var OWNER_SA1 = System.Environment.GetEnvironmentVariable ("OWNER_SA1");

var fetchers = new VersionFetcher[] {
    // Android
    new MavenCentral ("Android Easing Functions", "1.0.2", "com/daimajia/easing/library", OWNER_SA1),
    new MavenCentral ("Android Swipe Layout", "1.2.0", "com/daimajia/swipelayout/library", OWNER_SA1),
    new Maven ("Android Things", "0.6.1-devpreview", "https://google.bintray.com/androidthings/com/google/android/things/androidthings/maven-metadata.xml", OWNER_CA1),
    new MavenCentral ("Android View Animations", "1.1.3", "com/daimajia/androidanimations/library", OWNER_SA1),
    new MavenCentral ("Android Volley", "1.0.19", "com/mcxiaoke/volley/library", OWNER_SA1),
    new BinTray ("Animated Circle Loading View", "1.1.5", "jlmd", "maven/com/github/jlmd/AnimatedCircleLoadingView", OWNER_SA1),
    new MavenCentral ("Auto Fit Text View", "0.2.1", "me/grantland/autofittextview", OWNER_SA1),
    new BinTray ("Azure Messaging Android", "0.4.0", "microsoftazuremobile", "SDK/com/microsoft/azure/notification-hubs-android-sdk", OWNER_CA1),
    new MavenCentral ("Better Pickers", "1.6.0", "com/doomonafireball/betterpickers/library", OWNER_CA1),
    new BinTray ("Elastic Progress Bar", "1.0.4", "michelelacorte", "maven/it/michelelacorte/elasticprogressbar/library", OWNER_SA1),
    new MavenCentral ("Floating SearchView", "1.0.2", "com/github/arimorty/floatingsearchview", OWNER_SA1),
    new MavenCentral ("GoogleGson", "2.8.1", "com/google/code/gson/gson", OWNER_SA1),
    new MavenCentral ("KenBurnsView", "1.0.7", "com/flaviofaria/kenburnsview", OWNER_SA1),
    new MavenCentral ("Minimal Json", "0.9.4", "com/eclipsesource/minimal-json/minimal-json", OWNER_SA1),
    new MavenCentral ("Nine Old Androids", "2.4.0", "com/nineoldandroids/library", OWNER_SA1),
    new MavenCentral ("Number Progress Bar", "1.2", "com/daimajia/numberprogressbar/library", OWNER_SA1),
    new MavenCentral ("PhotoView", "1.2.4", "com/github/chrisbanes/photoview/library", OWNER_SA1),
    //new BinTray ("RecyclerView Animators", "2.1.0", "wasabeef", "maven/jp/wasabeef/recyclerview-animators", OWNER_SA1),
    new MavenCentral ("RoundedImageView", "2.3.0", "com/makeramen/roundedimageview", OWNER_SA1),
    new MavenCentral ("Scissors", "1.0.1", "com/lyft/scissors", OWNER_SA1),
    //new BinTray ("Sortable TableView", "2.2.0", "ischwarz", "maven/de/codecrafters/tableview/tableview", OWNER_SA1),
    new MavenCentral ("Sticky List Headers", "2.7.0", "se/emilsjolander/stickylistheaders", OWNER_SA1),
    new MavenCentral ("Timber", "4.6.0", "com/jakewharton/timber/timber", OWNER_US1),
    new MavenCentral ("Universal Image Loader", "1.9.5", "com/nostra13/universalimageloader/universal-image-loader", OWNER_SA1),
    new MavenCentral ("VectorCompat", "1.0.5", "com/wnafee/vector-compat", OWNER_SA1),
    new MavenCentral ("ViewPropertyObjectAnimator", "1.4.5", "com/bartoszlipinski/viewpropertyobjectanimator", OWNER_SA1),
    new MavenCentral ("Kotlin", "1.1.4-3", "org/jetbrains/kotlin/kotlin-stdlib", OWNER_SA1),

    // iOS
    new CocoaPods ("AMViralSwitch", "1.0.0", "AMViralSwitch", OWNER_SA1),
    new CocoaPods ("Chameleon", "2.0.6", "ChameleonFramework", OWNER_SA1),
    new GitHubReleases ("CorePlot", "1.5.1", "core-plot", "core-plot", OWNER_SA1),
    new CocoaPods ("DACircularProgress", "2.3.1", "DACircularProgress", OWNER_SA1),
    new CocoaPods ("DZNEmptyDataSet", "1.7.3", "DZNEmptyDataSet", OWNER_SA1),
    new CocoaPods ("FXBlurView", "1.6.4", "FXBlurView", OWNER_SA1),
    new CocoaPods ("GPUImage", "0.1.7", "GPUImage", OWNER_SA1),
    new CocoaPods ("InAppSettingsKit", "2.6", "InAppSettingsKit", OWNER_SA1),
    new CocoaPods ("JDStatusBarNotification", "1.5.3", "JDStatusBarNotification", OWNER_SA1),
    new GitHubReleases ("JSQMessagesViewController", "7.2.0", "jessesquires", "JSQMessagesViewController", OWNER_US1),
    new CocoaPods ("Progress HUD", "0.9.2", "MBProgressHUD", OWNER_SA1),
    new CocoaPods ("MWPhotoBrowser", "2.1.1", "MWPhotoBrowser", OWNER_SA1),
    new CocoaPods ("Masonry", "1.0.2", "Masonry", OWNER_SA1),
    new CocoaPods ("RZTransitions", "1.2.1", "RZTransitions", OWNER_SA1),
    new CocoaPods ("SCCatWaitingHUD", "0.1.6", "SCCatWaitingHUD", OWNER_SA1),
    new CocoaPods ("SDWebImage", "3.7.5", "SDWebImage", OWNER_SA1),
    new CocoaPods ("SlackTextViewController", "1.9.6", "SlackTextViewController", OWNER_SA1),
    new CocoaPods ("TPKeyboardAvoiding", "1.2.11", "TPKeyboardAvoiding", OWNER_SA1),
    new CocoaPods ("ZipArchive", "1.4.0", "ZipArchive", OWNER_SA1),
    new CocoaPods ("iCarousel", "1.8.2", "iCarousel", OWNER_SA1),
    new GitHubReleases ("iRate", "1.11.4", "nicklockwood", "iRate", OWNER_SA1),

    // Card.IO
    new GitHubReleases ("Card.IO Android", "5.5.1", "card-io", "card.io-Android-SDK", OWNER_CA1),
    new GitHubReleases ("Card.IO iOS", "5.4.1", "card-io", "card.io-iOS-SDK", OWNER_CA1),


    // Facebook
    new XPath ("Facebook Android", "4.24.0", "https://repo1.maven.org/maven2/com/facebook/android/facebook-android-sdk/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new CocoaPods ("Facebook Pop", "1.0.9", "pop", OWNER_CA1),
    new CocoaPods ("Facebook iOS CoreKit", "4.24.0", "FBSDKCoreKit", OWNER_MX1),

    // Google - Android
    new XPath ("Google Play Services", "11.6.0", "https://dl.google.com/dl/android/maven2/com/google/android/gms/play-services/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Android Support Libraries", "27.0.2", "https://dl.google.com/dl/android/maven2/com/android/support/appcompat-v7/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Google Glass SDK", "11", "https://dl.google.com/android/repository/glass/addon2-1.xml", "//*[local-name() = 'remotePackage' and @path='add-ons;addon-google_gdk-google-19']/*[local-name() = 'revision']", OWNER_CA1),
    new XPath ("Google Android ARCore", "0.91.0", "https://dl.google.com/dl/android/maven2/com/google/ar/core/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Android Wearable/Wearable", "2.2.0", "https://dl.google.com/dl/android/maven2/com/google/android/wearable/wearable/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Android Support/Wearable", "2.2.0", "https://dl.google.com/dl/android/maven2/com/google/android/support/wearable/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),

    // Google - iOS
    new CocoaPods ("Google.Analytics iOS", "3.17.0", "GoogleAnalytics", OWNER_MX1),
    new CocoaPods ("Google.AppIndexing iOS", "2.0.3", "GoogleAppIndexing", OWNER_MX1),
    // new CocoaPods ("Google.AppInvites iOS", "1.0.2", "AppInvites", OWNER_MX1), // Deprecated
    new CocoaPods ("Google.Cast iOS", "2.3.0", "google-cast-sdk", OWNER_MX1),
    new CocoaPods ("Google.Core iOS", "3.0.3", "Google", OWNER_MX1),
    // new CocoaPods ("Google.GoogleCloudMessaging iOS", "1.1.2", "GoogleCloudMessaging", OWNER_MX1), // Deprecated
    new CocoaPods ("Google.InstanceID iOS", "1.2.1", "GGLInstanceID", OWNER_MX1),
    new CocoaPods ("Google.Maps iOS", "2.1.0", "GoogleMaps", OWNER_MX1),
    // new CocoaPods ("Google.MobileAds iOS", "7.6.0", "GoogleMobileAds", OWNER_MX1), // Renamed as Firebase.AdMob
    new CocoaPods ("Google.PlayGames iOS", "5.1.1", "GooglePlayGames", OWNER_MX1),
    // new CocoaPods ("Google.Plus iOS", "1.7.1", "GooglePlusOpenSource", OWNER_MX1), // Deprecated
    new CocoaPods ("Google.SignIn iOS", "4.0.2", "GoogleSignIn", OWNER_MX1),
    new CocoaPods ("Google.TagManager iOS", "6.0.0", "GoogleTagManager", OWNER_MX1),

    // Firebase - iOS
    new CocoaPods ("Firebase iOS", "4.0.3", "Firebase", OWNER_MX1),
    new CocoaPods ("Firebase.AdMob iOS", "7.21.0", "Google-Mobile-Ads-SDK", OWNER_MX1),
    new CocoaPods ("Firebase.Analytics iOS", "4.0.2", "FirebaseAnalytics", OWNER_MX1),
    new CocoaPods ("Firebase.Auth iOS", "4.0.0", "FirebaseAuth", OWNER_MX1),
    new CocoaPods ("Firebase.CloudMessaging iOS", "2.0.0", "FirebaseMessaging", OWNER_MX1),
    new CocoaPods ("Firebase.Core iOS", "4.0.3", "FirebaseCore", OWNER_MX1),
    new CocoaPods ("Firebase.CrashReporting iOS", "2.0.0", "FirebaseCrash", OWNER_MX1),
    new CocoaPods ("Firebase.Database iOS", "4.0.0", "FirebaseDatabase", OWNER_MX1),
    new CocoaPods ("Firebase.DynamicLinks iOS", "2.0.0", "FirebaseDynamicLinks", OWNER_MX1),
    new CocoaPods ("Firebase.InstanceID iOS", "2.0.0", "FirebaseInstanceID", OWNER_MX1),
    new CocoaPods ("Firebase.Invites iOS", "2.0.0", "FirebaseInvites", OWNER_MX1),
    new CocoaPods ("Firebase.PerformanceMonitoring iOS", "0.0.0", "FirebasePerformance", OWNER_MX1),
    new CocoaPods ("Firebase.RemoteConfig iOS", "2.0.1", "FirebaseRemoteConfig", OWNER_MX1),
    new CocoaPods ("Firebase.Storage iOS", "2.0.0", "FirebaseStorage", OWNER_MX1),

    // Mapbox
    new MavenCentral ("Mapbox Android", "4.2.2", "com/mapbox/mapboxsdk/mapbox-android-sdk", OWNER_CA1),
    new CocoaPods ("Mapbox iOS", "3.5.0", "Mapbox-iOS-SDK", OWNER_CA1),

    // Socket.IO
    new MavenCentral ("Socket.IO Client", "1.0.0", "io/socket/socket.io-client", OWNER_SA1),
    new MavenCentral ("Engine.IO Client", "1.0.0", "io/socket/engine.io-client", OWNER_SA1),

    // Square
    new MavenCentral ("OkIO", "1.13.0", "com/squareup/okio/okio", OWNER_SA1),
    new MavenCentral ("OkHttp", "2.7.5", "com/squareup/okhttp/okhttp", OWNER_SA1),
    new MavenCentral ("OkHttp3", "3.8.1", "com/squareup/okhttp3/okhttp", OWNER_SA1),
    new MavenCentral ("OkHttp Web Sockets", "2.7.5", "com/squareup/okhttp/okhttp-ws", OWNER_SA1),
    new MavenCentral ("OkHttp3 Web Sockets", "3.4.2", "com/squareup/okhttp3/okhttp-ws", OWNER_SA1),
    new MavenCentral ("Picasso", "2.5.2", "com/squareup/picasso/picasso", OWNER_SA1),
    new MavenCentral ("AndroidTimesSquare", "1.7.3", "com/squareup/android-times-square", OWNER_SA1),
    new MavenCentral ("Seismic", "1.0.2", "com/squareup/seismic", OWNER_SA1),
    new MavenCentral ("Pollexor", "2.0.4", "com/squareup/pollexor", OWNER_SA1),
    new MavenCentral ("OkHttp UrlConection", "2.7.5", "com/squareup/okhttp/okhttp-urlconnection", OWNER_SA1),
    new MavenCentral ("Retrofit", "1.9.0", "com/squareup/retrofit", OWNER_SA1),
    new MavenCentral ("Retrofit2", "2.3.0", "com/squareup/retrofit2", OWNER_SA1),
    new GitHubReleases ("SocketRocket", "0.5.1", "square", "SocketRocket", OWNER_SA1),
    new GitHubReleases ("Valet", "2.4.1", "square", "Valet", OWNER_SA1),
    new GitHubReleases ("Aardvark", "1.5.0", "square", "Aardvark", OWNER_SA1),
};

var updates = new List<UpdateInfo> ();
var failures = new List<string> ();

foreach (var v in fetchers) {
    try {
        var updateInfo = v.Run ();
        if (updateInfo != null)
            updates.Add (updateInfo);
    } catch (Exception ex) {
        failures.Add (v.ComponentName);
    }
}

foreach (var grp in updates.GroupBy (up => up.Owner)) {
    var owner = string.IsNullOrEmpty (grp.Key) ? "here" : grp.Key;
    SlackNotifier.Notify (owner, grp);
}

if (failures.Any ())
    SlackNotifier.Notify (failures);
