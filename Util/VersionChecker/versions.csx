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
    new Maven ("Android Things", "0.3-devpreview", "https://google.bintray.com/androidthings/com/google/android/things/androidthings/maven-metadata.xml", OWNER_CA1),
    new MavenCentral ("Android View Animations", "1.1.3", "com/daimajia/androidanimations/library", OWNER_SA1),
    new MavenCentral ("Android Volley", "1.0.19", "com/mcxiaoke/volley/library", OWNER_SA1),
    new BinTray ("Animated Circle Loading View", "1.1.2", "jlmd", "maven/com/github/jlmd/AnimatedCircleLoadingView", OWNER_SA1),
    new MavenCentral ("Auto Fit Text View", "0.2.1", "me/grantland/autofittextview", OWNER_SA1),
    new BinTray ("Azure Messaging Android", "0.4.0", "microsoftazuremobile", "SDK/com/microsoft/azure/notification-hubs-android-sdk", OWNER_CA1),
    new MavenCentral ("Better Pickers", "1.6.0", "com/doomonafireball/betterpickers/library", OWNER_CA1),
    new BinTray ("Elastic Progress Bar", "1.0.4", "michelelacorte", "maven/it/michelelacorte/elasticprogressbar/library", OWNER_SA1),
    new MavenCentral ("Floating SearchView", "1.0.2", "com/github/arimorty/floatingsearchview", OWNER_SA1),
    new MavenCentral ("GoogleGson", "2.7.0", "com/google/code/gson/gson", OWNER_SA1),
    new MavenCentral ("KenBurnsView", "1.0.7", "com/flaviofaria/kenburnsview", OWNER_SA1),
    new MavenCentral ("Minimal Json", "0.9.4", "com/eclipsesource/minimal-json/minimal-json", OWNER_SA1),
    new MavenCentral ("Nine Old Androids", "2.4.0", "com/nineoldandroids/library", OWNER_SA1),
    new MavenCentral ("Number Progress Bar", "1.2", "com/daimajia/numberprogressbar/library", OWNER_SA1),
    new MavenCentral ("PhotoView", "1.2.4", "com/github/chrisbanes/photoview/library", OWNER_SA1),
    //new BinTray ("RecyclerView Animators", "2.1.0", "wasabeef", "maven/jp/wasabeef/recyclerview-animators", OWNER_SA1),
    new MavenCentral ("RoundedImageView", "2.2.1", "com/makeramen/roundedimageview", OWNER_SA1),
    new MavenCentral ("Scissors", "1.0.1", "com/lyft/scissors", OWNER_SA1),
    //new BinTray ("Sortable TableView", "2.2.0", "ischwarz", "maven/de/codecrafters/tableview/tableview", OWNER_SA1),
    new MavenCentral ("Sticky List Headers", "2.7.0", "se/emilsjolander/stickylistheaders", OWNER_SA1),
    new MavenCentral ("Universal Image Loader", "1.9.5", "com/nostra13/universalimageloader/universal-image-loader", OWNER_SA1),
    new MavenCentral ("VectorCompat", "1.0.5", "com/wnafee/vector-compat", OWNER_SA1),

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
    new CocoaPods ("Masonry", "0.6.4", "Masonry", OWNER_SA1),
    new CocoaPods ("RZTransitions", "1.2.1", "RZTransitions", OWNER_SA1),
    new CocoaPods ("SCCatWaitingHUD", "0.1.6", "SCCatWaitingHUD", OWNER_SA1),
    new CocoaPods ("SDWebImage", "3.7.5", "SDWebImage", OWNER_SA1),
    new CocoaPods ("SlackTextViewController", "1.9.1", "SlackTextViewController", OWNER_SA1),
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
    new CocoaPods ("Facebook iOS CoreKit", "4.9.1", "FBSDKCoreKit", OWNER_MX1),

    // Google - Android
    new XPath ("Google Play Services", "46", "https://dl.google.com/android/repository/addon2-1.xml", "//*[local-name() = 'remotePackage' and @path='extras;google;m2repository']/*[local-name() = 'revision']", OWNER_CA1),
    new XPath ("Android Support Libraries", "25.3.1", "https://dl.google.com/dl/android/maven2/com/android/support/appcompat-v7/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Google Glass SDK", "11", "https://dl.google.com/android/repository/glass/addon2-1.xml", "//*[local-name() = 'remotePackage' and @path='add-ons;addon-google_gdk-google-19']/*[local-name() = 'revision']", OWNER_CA1),

    // Google - iOS
    new CocoaPods ("Google.Analytics iOS", "3.14.0", "GoogleAnalytics", OWNER_MX1),
    new CocoaPods ("Google.AppIndexing iOS", "2.0.2", "GoogleAppIndexing", OWNER_MX1),
    new CocoaPods ("Google.AppInvites iOS", "1.0.2", "AppInvites", OWNER_MX1),
    new CocoaPods ("Google.Cast iOS", "2.10.1", "google-cast-sdk", OWNER_MX1),
    new CocoaPods ("Google.Core iOS", "1.3.2", "Google", OWNER_MX1),
    new CocoaPods ("Google.GoogleCloudMessaging iOS", "1.1.2", "GoogleCloudMessaging", OWNER_MX1),
    new CocoaPods ("Google.InstanceID iOS", "1.1.4", "GGLInstanceID", OWNER_MX1),
    new CocoaPods ("Google.MobileAds iOS", "7.6.0", "GoogleMobileAds", OWNER_MX1),
    new CocoaPods ("Google.PlayGames iOS", "5.0.0", "GooglePlayGames", OWNER_MX1),
    new CocoaPods ("Google.Plus iOS", "1.7.1", "GooglePlusOpenSource", OWNER_MX1),
    new CocoaPods ("Google.TagManager iOS", "3.15.0", "GoogleTagManager", OWNER_MX1),
    new CocoaPods ("Google.Maps iOS", "1.11.1", "GoogleMaps", OWNER_MX1),
    new CocoaPods ("Google.SignIn iOS", "2.4.0", "GoogleSignIn", OWNER_MX1),

    // Mapbox
    new MavenCentral ("Mapbox Android", "4.2.2", "com/mapbox/mapboxsdk/mapbox-android-sdk", OWNER_CA1),
    new CocoaPods ("Mapbox iOS", "3.3.4", "Mapbox-iOS-SDK", OWNER_CA1),

    // Square
    new MavenCentral ("OkIO", "1.9.0", "com/squareup/okio/okio", OWNER_SA1),
    new MavenCentral ("OkHttp", "2.7.5", "com/squareup/okhttp/okhttp", OWNER_SA1),
    new MavenCentral ("OkHttp3", "3.4.1", "com/squareup/okhttp3/okhttp", OWNER_SA1),
    new MavenCentral ("OkHttp Web Sockets", "2.7.5", "com/squareup/okhttp/okhttp-ws", OWNER_SA1),
    new MavenCentral ("OkHttp3 Web Sockets", "3.4.1", "com/squareup/okhttp3/okhttp-ws", OWNER_SA1),
    new MavenCentral ("Picasso", "2.5.2", "com/squareup/picasso/picasso", OWNER_SA1),
    new MavenCentral ("AndroidTimesSquare", "1.6.5", "com/squareup/android-times-square", OWNER_SA1),
    new MavenCentral ("Seismic", "1.0.2", "com/squareup/seismic", OWNER_SA1),
    new MavenCentral ("Pollexor", "2.0.4", "com/squareup/pollexor", OWNER_SA1),
    new MavenCentral ("OkHttp UrlConection", "2.7.5", "com/squareup/okhttp/okhttp-urlconnection", OWNER_SA1),
    new MavenCentral ("Retrofit", "1.9.0", "com/squareup/retrofit", OWNER_SA1),
    new MavenCentral ("Retrofit2", "0.0.0", "com/squareup/retrofit2", OWNER_SA1),

    // Square dependencies
    new GitHubReleases ("SocketRocket", "0.4.2", "square", "SocketRocket", OWNER_SA1),
    new GitHubReleases ("Valet", "2.2.2", "square", "Valet", OWNER_SA1),
    new GitHubReleases ("Aardvark", "1.4.0", "square", "Aardvark", OWNER_SA1),
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

SlackNotifier.Notify (failures);
