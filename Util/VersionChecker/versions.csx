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
    new Maven ("Android Things", "1.0", "https://google.bintray.com/androidthings/com/google/android/things/androidthings/maven-metadata.xml", OWNER_CA1),
    new MavenCentral ("Android View Animations", "1.1.3", "com/daimajia/androidanimations/library", OWNER_SA1),
    new MavenCentral ("Android Volley", "1.0.19", "com/mcxiaoke/volley/library", OWNER_SA1),
    new BinTray ("Animated Circle Loading View", "1.1.5", "jlmd", "maven/com/github/jlmd/AnimatedCircleLoadingView", OWNER_SA1),
    new MavenCentral ("Auto Fit Text View", "0.2.1", "me/grantland/autofittextview", OWNER_SA1),
    new BinTray ("Azure Messaging Android", "0.4.0", "microsoftazuremobile", "SDK/com/microsoft/azure/notification-hubs-android-sdk", OWNER_CA1),
    new MavenCentral ("Better Pickers", "1.6.0", "com/doomonafireball/betterpickers/library", OWNER_CA1),
    new BinTray ("Elastic Progress Bar", "1.0.5", "michelelacorte", "maven/it/michelelacorte/elasticprogressbar/library", OWNER_SA1),
    new MavenCentral ("Floating SearchView", "2.1.1", "com/github/arimorty/floatingsearchview", OWNER_SA1),
    new MavenCentral ("GoogleGson", "2.8.1", "com/google/code/gson/gson", OWNER_SA1),
    new MavenCentral ("KenBurnsView", "1.0.7", "com/flaviofaria/kenburnsview", OWNER_SA1),
    new MavenCentral ("Minimal Json", "0.9.4", "com/eclipsesource/minimal-json/minimal-json", OWNER_SA1),
    new MavenCentral ("Nine Old Androids", "2.4.0", "com/nineoldandroids/library", OWNER_SA1),
    new MavenCentral ("Number Progress Bar", "1.2", "com/daimajia/numberprogressbar/library", OWNER_SA1),
    new MavenCentral ("PhotoView", "2.1.3", "com/github/chrisbanes/photoview/library", OWNER_SA1),
    //new BinTray ("RecyclerView Animators", "2.1.0", "wasabeef", "maven/jp/wasabeef/recyclerview-animators", OWNER_SA1),
    new MavenCentral ("RoundedImageView", "2.3.0", "com/makeramen/roundedimageview", OWNER_SA1),
    new MavenCentral ("Scissors", "1.1.1", "com/lyft/scissors", OWNER_SA1),
    //new BinTray ("Sortable TableView", "2.2.0", "ischwarz", "maven/de/codecrafters/tableview/tableview", OWNER_SA1),
    new MavenCentral ("Sticky List Headers", "2.7.0", "se/emilsjolander/stickylistheaders", OWNER_SA1),
    new MavenCentral ("Timber", "4.6.0", "com/jakewharton/timber/timber", OWNER_US1),
    new MavenCentral ("Universal Image Loader", "1.9.5", "com/nostra13/universalimageloader/universal-image-loader", OWNER_SA1),
    new MavenCentral ("VectorCompat", "1.0.5", "com/wnafee/vector-compat", OWNER_SA1),
    new MavenCentral ("ViewPropertyObjectAnimator", "1.4.5", "com/bartoszlipinski/viewpropertyobjectanimator", OWNER_SA1),
    new MavenCentral ("Kotlin", "1.1.4-3", "org/jetbrains/kotlin/kotlin-stdlib", OWNER_SA1),        
    new Maven ("Blurring", "1.0.0", "https://github.com/500px/500px-android-blur/blob/master/releases/com/fivehundredpx/blurringview/maven-metadata.xml", OWNER_CA1),
    new MavenCentral ("DeviceYearClass", "1.0.0", "com.facebook.device.yearclass", OWNER_SA1),
    new MavenCentral ("Glide", "4.0.0", "com.github.bumptech.glide", OWNER_SA1),
    new MavenCentral ("GoogleZXing", "3.3.0", "com.google.zxing", OWNER_SA1),
    new MavenCentral ("Guava", "23.2-android", "com.google.guava", OWNER_SA1),
    new MavenCentral ("Jackson.Core", "2.7.4", "com.fasterxml.jackson.core", OWNER_SA1),
    new MavenCentral ("MinimalJson", "0.9.4", "com.eclipsesource.minimal-json", OWNER_SA1),
    new MavenCentral ("RecyclerViewAnimators", "2.1.0", "jp.wasabeef", OWNER_SA1),
    new MavenCentral ("ShimmerLayout", "0.5.0", "io.supercharge", OWNER_SA1),
    new MavenCentral ("UrlImageViewHelper", "1.0.4", "com.koushikdutta.urlimageviewhelper", OWNER_SA1),
    new MavenCentral ("Bolts", "1.4.0", "com.parse.bolts", OWNER_SA1),

    // iOS
    new CocoaPods ("AMViralSwitch", "1.0.0", "AMViralSwitch", OWNER_SA1),
    new CocoaPods ("Chameleon", "2.1", "ChameleonFramework", OWNER_SA1),
    new GitHubReleases ("CorePlot", "1.5.1", "core-plot", "core-plot", OWNER_SA1),
    new CocoaPods ("DACircularProgress", "2.3.1", "DACircularProgress", OWNER_SA1),
    new CocoaPods ("DZNEmptyDataSet", "1.7.3", "DZNEmptyDataSet", OWNER_SA1),
    new CocoaPods ("FXBlurView", "1.6.4", "FXBlurView", OWNER_SA1),
    new CocoaPods ("GPUImage", "0.1.7", "GPUImage", OWNER_SA1),
    new CocoaPods ("InAppSettingsKit", "2.6", "InAppSettingsKit", OWNER_SA1),
    new CocoaPods ("JDStatusBarNotification", "1.5.3", "JDStatusBarNotification", OWNER_SA1),
    new GitHubReleases ("JSQMessagesViewController", "7.3.5", "jessesquires", "JSQMessagesViewController", OWNER_US1),
    new CocoaPods ("Progress HUD", "0.9.2.0", "MBProgressHUD", OWNER_SA1),
    new CocoaPods ("MWPhotoBrowser", "2.1.1", "MWPhotoBrowser", OWNER_SA1),
    new CocoaPods ("Masonry", "1.0.2", "Masonry", OWNER_SA1),
    new CocoaPods ("RZTransitions", "1.2.1", "RZTransitions", OWNER_SA1),
    new CocoaPods ("SCCatWaitingHUD", "0.1.6", "SCCatWaitingHUD", OWNER_SA1),
    new CocoaPods ("SDWebImage", "3.7.5", "SDWebImage", OWNER_SA1),
    new CocoaPods ("SlackTextViewController", "1.9.6", "SlackTextViewController", OWNER_SA1),
    new CocoaPods ("TPKeyboardAvoiding", "1.3.2", "TPKeyboardAvoiding", OWNER_SA1),
    new CocoaPods ("ZipArchive", "1.4.0", "ZipArchive", OWNER_SA1),
    new CocoaPods ("iCarousel", "1.8.3", "iCarousel", OWNER_SA1),
    new GitHubReleases ("iRate", "1.11.7", "nicklockwood", "iRate", OWNER_SA1),
    new CocoaPods ("AMScrollingNavbar", "4.1.0", "AMScrollingNavbar", OWNER_SA1),
    new CocoaPods ("IQAudioRecorderController", "1.1", "IQAudioRecorderController", OWNER_SA1),
    new CocoaPods ("REFrostedViewController", "1.1", "REFrostedViewController", OWNER_SA1),
    new CocoaPods ("TwitterImagePipeline", "2.2.2", "TwitterImagePipeline", OWNER_SA1),
    new CocoaPods ("LiquidFloatingActionButton", "2.0.0", "LiquidFloatingActionButton", OWNER_SA1),
    new CocoaPods ("JVMenuPopover", "1.7", "JVMenuPopover", OWNER_SA1),
    new CocoaPods ("JZMultiChoicesCircleButton", "1.1", "JZMultiChoicesCircleButton", OWNER_SA1),
    new CocoaPods ("PickerCells", "1.2", "PickerCells", OWNER_SA1),
    new CocoaPods ("SDSegmentedControl", "2.1.5", "SDSegmentedControl", OWNER_SA1),

    // Card.IO
    new GitHubReleases ("Card.IO Android", "5.5.1", "card-io", "card.io-Android-SDK", OWNER_CA1),
    new GitHubReleases ("Card.IO iOS", "5.4.1", "card-io", "card.io-iOS-SDK", OWNER_CA1),


    // Facebook
    new XPath ("Facebook Android - SDK", "4.33.0", "https://repo1.maven.org/maven2/com/facebook/android/facebook-android-sdk/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Facebook Android - AppLinks", "4.33.0", "https://repo1.maven.org/maven2/com/facebook/android/facebook-applinks/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Facebook Android - Common", "4.33.0", "https://repo1.maven.org/maven2/com/facebook/android/facebook-common/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Facebook Android - Core", "4.33.0", "https://repo1.maven.org/maven2/com/facebook/android/facebook-core/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Facebook Android - Login", "4.33.0", "https://repo1.maven.org/maven2/com/facebook/android/facebook-login/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Facebook Android - Messenger", "4.33.0", "https://repo1.maven.org/maven2/com/facebook/android/facebook-messenger/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Facebook Android - Places", "4.33.0", "https://repo1.maven.org/maven2/com/facebook/android/facebook-places/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Facebook Android - Share", "4.33.0", "https://repo1.maven.org/maven2/com/facebook/android/facebook-share/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Facebook Android - Account Kit SDK", "4.28.0", "https://repo1.maven.org/maven2/com/facebook/android/facebook-common/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Facebook Android - Audience Network SDK", "4.28.1", "https://repo1.maven.org/maven2/com/facebook/android/facebook-common/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Facebook Android - Notifications", "1.0.2", "https://repo1.maven.org/maven2/com/facebook/android/facebook-common/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    

    new CocoaPods ("Facebook Pop", "1.0.9", "pop", OWNER_CA1),
    new CocoaPods ("Facebook iOS - CoreKit", "4.33.0", "FBSDKCoreKit", OWNER_MX1),

    // Google - Android
    new XPath ("Android Support Libraries", "27.0.2", "https://dl.google.com/dl/android/maven2/com/android/support/appcompat-v7/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Google Glass SDK", "11", "https://dl.google.com/android/repository/glass/addon2-1.xml", "//*[local-name() = 'remotePackage' and @path='add-ons;addon-google_gdk-google-19']/*[local-name() = 'revision']", OWNER_CA1),
    new XPath ("Google Android ARCore", "1.0.0", "https://dl.google.com/dl/android/maven2/com/google/ar/core/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Android Wearable/Wearable", "2.2.0", "https://dl.google.com/dl/android/maven2/com/google/android/wearable/wearable/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Android Support/Wearable", "2.2.0", "https://dl.google.com/dl/android/maven2/com/google/android/support/wearable/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Android Support Constraint Layout", "1.1.0-beta5", "https://dl.google.com/dl/android/maven2/com/android/support/constraint/constraint-layout/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),

    // Google - iOS
    new CocoaPods ("Google.Analytics iOS", "3.17.0", "GoogleAnalytics", OWNER_MX1),
    new CocoaPods ("Google.AppIndexing iOS", "2.0.3", "GoogleAppIndexing", OWNER_MX1),
    // new CocoaPods ("Google.AppInvites iOS", "1.0.2", "AppInvites", OWNER_MX1), // Deprecated
    new CocoaPods ("Google.Cast iOS", "4.1.0", "google-cast-sdk", OWNER_MX1),
    new CocoaPods ("Google.Core iOS", "3.1.0", "Google", OWNER_MX1),
    // new CocoaPods ("Google.GoogleCloudMessaging iOS", "1.2.0", "GoogleCloudMessaging", OWNER_MX1), // Deprecated
    new CocoaPods ("Google.InstanceID iOS", "1.2.1", "GGLInstanceID", OWNER_MX1),
    new CocoaPods ("Google.Maps iOS", "2.5.0", "GoogleMaps", OWNER_MX1),
    // new CocoaPods ("Google.MobileAds iOS", "7.27.0", "GoogleMobileAds", OWNER_MX1), // Renamed as Firebase.AdMob
    new CocoaPods ("Google.PlayGames iOS", "5.1.1", "GooglePlayGames", OWNER_MX1),
    // new CocoaPods ("Google.Plus iOS", "1.7.1", "GooglePlusOpenSource", OWNER_MX1), // Deprecated
    new CocoaPods ("Google.SignIn iOS", "4.8.0", "GoogleSignIn", OWNER_MX1),
    new CocoaPods ("Google.TagManager iOS", "6.0.0", "GoogleTagManager", OWNER_MX1),

    //Firebase - Android
    new XPath ("Firebase - Ads", "15.0.1", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-ads/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Analytics", "16.0.1", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-analytics/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Analytics Impl", "16.1.1", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-analytics-impl/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Appindexing", "16.0.1", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-appindexing/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Auth", "16.0.2", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-auth/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Common", "16.0.0", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-common/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Config", "16.0.0", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-config/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Core", "16.0.1", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-core/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Crash", "16.0.1", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-crash/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Database", "16.0.1", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-database/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Database Connection", "16.0.1", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-database-connection/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Dynamic Links", "16.0.1", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-dynamic-links/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Firestore", "17.0.2", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-firestore/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Iid", "16.2.0", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-iid/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Invites", "16.0.1", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-invites/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Messaging", "17.1.0", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-messaging/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Perf", "16.0.0", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-perf/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Storage", "16.0.1", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-storage/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Storage Common", "16.0.1", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-storage-common/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    
    new XPath ("Firebase - Abt", "16.0.0", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-abt/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Ads Lite", "15.0.1", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-ads-lite/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Auth Interop", "16.0.0", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-auth-interop/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Database Collection", "15.0.1", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-database-collection/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Functions", "16.0.1", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-functions/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Iid Interop", "16.0.0", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-iid-interop/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Measurement Connector", "17.0.0", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-measurement-connector/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Measurement Connector Impl", "16.0.1", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-measurement-connector-impl/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Ml Common", "16.0.0", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-ml-common/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Ml Model Interpreter", "16.0.0", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-ml-model-interpreter/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Ml Vision", "16.0.0", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-ml-vision/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),
    new XPath ("Firebase - Ml Vision Image Label Model", "15.0.0", "https://dl.google.com/dl/android/maven2/com/google/firebase/firebase-ml-vision-image-label-model/maven-metadata.xml", "/metadata/versioning/release", OWNER_CA1),


    // Firebase - iOS
    new CocoaPods ("Firebase iOS", "4.0.3", "Firebase", OWNER_MX1),
    new CocoaPods ("Firebase.AdMob iOS", "7.27.0", "Google-Mobile-Ads-SDK", OWNER_MX1),
    new CocoaPods ("Firebase.Analytics iOS", "4.0.5", "FirebaseAnalytics", OWNER_MX1),
    new CocoaPods ("Firebase.Auth iOS", "4.0.0", "FirebaseAuth", OWNER_MX1),
    new CocoaPods ("Firebase.CloudMessaging iOS", "4.8.0", "FirebaseMessaging", OWNER_MX1),
    new CocoaPods ("Firebase.Core iOS", "4.0.13", "FirebaseCore", OWNER_MX1),
    new CocoaPods ("Firebase.CrashReporting iOS", "2.0.0", "FirebaseCrash", OWNER_MX1),
    new CocoaPods ("Firebase.Database iOS", "4.1.3", "FirebaseDatabase", OWNER_MX1),
    new CocoaPods ("Firebase.DynamicLinks iOS", "4.8.0", "FirebaseDynamicLinks", OWNER_MX1),
    new CocoaPods ("Firebase.InstanceID iOS", "2.0.8", "FirebaseInstanceID", OWNER_MX1),
    new CocoaPods ("Firebase.Invites iOS", "4.8.0", "FirebaseInvites", OWNER_MX1),
    new CocoaPods ("Firebase.PerformanceMonitoring iOS", "1.1.0", "FirebasePerformance", OWNER_MX1),
    new CocoaPods ("Firebase.RemoteConfig iOS", "2.0.3", "FirebaseRemoteConfig", OWNER_MX1),
    new CocoaPods ("Firebase.Storage iOS", "2.0.2", "FirebaseStorage", OWNER_MX1),

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
