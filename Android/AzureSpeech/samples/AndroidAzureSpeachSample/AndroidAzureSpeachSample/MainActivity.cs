using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using CognitiveServices.Speech;
using CognitiveServices.Speech.Audio;
using Java.Util.Concurrent;

namespace AndroidAzureSpeachSample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        // Replace below with your own subscription key
        private static readonly string SpeechSubscriptionKey = "YourSubscriptionKey";
        // Replace below with your own service region (e.g., "westus").
        private static readonly string SpeechRegion = "YourServiceRegion";

        //
        // Configuration for intent recognition
        //

        // Replace below with your own Language Understanding subscription key
        // The intent recognition service calls the required key 'endpoint key'.
        private static readonly string LanguageUnderstandingSubscriptionKey = "YourLanguageUnderstandingSubscriptionKey";
        // Replace below with the deployment region of your Language Understanding application
        private static readonly string LanguageUnderstandingServiceRegion = "YourLanguageUnderstandingServiceRegion";
        // Replace below with the application ID of your Language Understanding application
        private static readonly string LanguageUnderstandingAppId = "YourLanguageUnderstandingAppId";

        private TextView recognizedTextView;
        private Button recognizeButton;
        private Button recognizeIntermediateButton;
        private Button recognizeContinuousButton;
        private Button recognizeIntentButton;

        private MicrophoneStream microphoneStream;
        private SpeechConfig speechConfig;
        private readonly SpeechRecognizer reco;
        private readonly AudioConfig audioInput;
        private event EventHandler<SpeechRecognitionResult> OnSpeechRecognized;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            recognizedTextView = (TextView)FindViewById(Resource.Id.recognizedText);

            recognizeButton = (Button)FindViewById(Resource.Id.buttonRecognize);
            recognizeIntermediateButton = (Button)FindViewById(Resource.Id.buttonRecognizeIntermediate);
            recognizeContinuousButton = (Button)FindViewById(Resource.Id.buttonRecognizeContinuous);
            recognizeIntentButton = (Button)FindViewById(Resource.Id.buttonRecognizeIntent);

            // Initialize SpeechSDK and request required permissions.
            try
            {
                // a unique number within the application to allow
                // correlating permission request responses with the request.
                int permissionRequestId = 5;

                // Request permissions needed for speech recognition
                RequestPermissions(new string[]
                {
                    Android.Manifest.Permission.RecordAudio,
                    Android.Manifest.Permission.Internet },
                    permissionRequestId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SpeechSDK: ", ex.Message);
                recognizedTextView.Text = ex.ToString();
            }

            try
            {
                speechConfig = SpeechConfig.FromSubscription(SpeechSubscriptionKey, SpeechRegion);
            }
            catch (Exception ex)
            {
                DisplayException(ex);
                return;
            }

            recognizeButton.Click += RecognizeButton_Click;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainActivity_OnSpeechRecognized(object sender, SpeechRecognitionResult e)
        {
            string s = e.Text;
            if (e.Reason != ResultReason.RecognizedSpeech)
            {
                string errorDetails = (e.Reason == ResultReason.Canceled) ? CancellationDetails.FromResult(e).ErrorDetails : string.Empty;
                _ = "Recognition failed with " + e.Reason + ". Did you enter your subscription?" + System.Environment.NewLine + errorDetails;
            }

            reco.Close();
            Console.WriteLine("Reco 1 ****** Recognizer returned: " + s);

            //setRecognizedText(s);
            //enableButtons();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecognizeButton_Click(object sender, EventArgs e)
        {
            AudioConfig audioInput = AudioConfig.FromStreamInput(CreateMicrophoneStream());
            SpeechRecognizer reco = new SpeechRecognizer(speechConfig, audioInput);

            OnSpeechRecognized += MainActivity_OnSpeechRecognized;

            //disableButtons();
            //clearTextBox();

            try
            {
                // final AudioConfig audioInput = AudioConfig.fromDefaultMicrophoneInput();
                audioInput = AudioConfig.FromStreamInput(CreateMicrophoneStream());
                reco = new SpeechRecognizer(speechConfig, audioInput);

                Task<SpeechRecognitionResult> task = (Task<SpeechRecognitionResult>) reco.RecognizeOnceAsync();
                OnSpeechRecognized?.Invoke(this, task.Result);

            }
            catch (Exception ex)
            {
            //    System.out.println(ex.getMessage());
            //    displayException(ex);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        private void DisplayException(Exception ex)
        {
            recognizedTextView.Text = ex.Message + System.Environment.NewLine + string.Join(System.Environment.NewLine, ex.StackTrace);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private MicrophoneStream CreateMicrophoneStream()
        {
            if (microphoneStream != null)
            {
                microphoneStream.Close();
                microphoneStream = null;
            }

            microphoneStream = new MicrophoneStream();
            return microphoneStream;
        }
    }
}

