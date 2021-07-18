using System;
using System.Threading;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Java.Nio.Charset;

using Square.OkHttp;
using Square.OkHttp.WS;
using Square.OkIO;

namespace OkHttpSample
{
    [Activity(Label = "OkHttpWSSample", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat")]
    public class MainActivity : AppCompatActivity
    {
        private const string Endpoint = "wss://echo.websocket.org";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Button startTest = FindViewById<Button>(Resource.Id.startTest);
            ListView listView = FindViewById<ListView>(Resource.Id.listView);

            startTest.Click += delegate
            {
                ArrayAdapter<string> adapter = new ArrayAdapter<string>(
                    this,
                    Android.Resource.Layout.SimpleListItem1,
                    Android.Resource.Id.Text1);
                listView.Adapter = adapter;

                OkHttpClient client = new OkHttpClient();

                // Create request for remote resource.
                Request request = new Request.Builder()
                    .Url(Endpoint)
                    .Build();

                // Execute the request and retrieve the response.
                WebSocketCall call = WebSocketCall.Create(client, request);
                WebSocketListener listener = call.Enqueue();

                // attach handlers to the various events
                listener.Close += (sender, e) =>
                {
                    RunOnUiThread(() => adapter.Add(string.Format("{0}: {1}", e.Code, e.Reason)));
                };
                listener.Failure += (sender, e) =>
                {
                    if (e.Exception != null)
                        RunOnUiThread(() => adapter.Add(e.Exception.Message));
                    else
                        RunOnUiThread(() => adapter.Add("Unknown Error!"));
                };
                listener.Message += (sender, e) =>
                {
                    string payload = e.Payload.String();
                    e.Payload.Close();
					RunOnUiThread(() => adapter.Add(string.Format("{0}\n{1}", payload, e.Payload.ContentType())));
                };
                listener.Open += (sender, e) =>
                {
                    RunOnUiThread(() => adapter.Add("Opened Web Socket."));

                    StartMessages(e.WebSocket);
                };
                listener.Pong += (sender, e) =>
                {
                    string payload = e.Payload.ReadString(Charset.DefaultCharset());
                    e.Payload.Close();
                    RunOnUiThread(() => adapter.Add(payload));
                };
            };
        }

        private void StartMessages(IWebSocket webSocket)
        {
            // we want to ping every 2 seconds
            new Timer(state =>
            {
                var buffer = new OkBuffer();
                buffer.WriteString("Ping!", Charset.DefaultCharset());
                webSocket.SendPing(buffer);
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));

            // we want to send a message every 5 seconds
            new Timer(state =>
            {
				var body = RequestBody.Create(WebSocket.Text, "Hello World!");
                webSocket.SendMessage(body);
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(3));
        }
    }
}
