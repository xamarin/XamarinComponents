# OkHttp Web Sockets Details

> A **RFC6455**-compliant web socket implementation

## Connecting

We can connect to a web socket using the `WebSocketCall` type with the usual `OkHttpClient` and `Request` types of OkHttp:

    OkHttpClient client = new OkHttpClient();

    // Create request for remote resource.
    Request request = new Request.Builder()
        .Url(Endpoint)
        .Build();

    // Execute the request and retrieve the response.
    WebSocketCall call = WebSocketCall.Create(client, request);

## Listening

We can use an event-based listener:

    WebSocketListener listener = call.Enqueue();
	listener.Open += (sender, e) => {
		// the socket was opened, so get the socket
        var socket = e.WebSocket;
	};
	
Or, we can implement the listener:

    // connect and receive
    call.Enqueue(new SocketListener());
	
	// the listener type
	class SocketListener : Java.Lang.Object, IWebSocketListener
	{
	    public void OnClose(int code, string reason)
		{
		    // the connection was closed
		}
		public void OnFailure(Java.IO.IOException exception, Response response)
		{
		    // there was an error
		}
		public void OnMessage(IBufferedSource source, WebSocketPayloadType payloadType)
		{
		    // we received a message
		}
		public void OnOpen(IWebSocket socket, Response response)
		{
		    // the socket was opened
		}
		public void OnPong(OkBuffer buffer)
		{
		    // respond to a ping
		}
	}
	
## Communicating

Using either the event-based listener or a custom implementation, we can access the open socket and start sending messages:

	public void OnOpen(IWebSocket socket, Response response)
	{
		var buffer = new OkBuffer();
		buffer.WriteString("Hello World!", Charset.DefaultCharset());
		socket.SendMessage(WebSocketPayloadType.Text, buffer);
	};

When a message comes in from a remote source, we can handle it using either the event-based listener or a custom implementation:

	listener.Message += (sender, e) => {
	    // read the contents
	    string payload = e.Payload.ReadString(Charset.DefaultCharset());
		
		// close the message
		e.Payload.Close();
	};
	
*Note: This module's API should be considered experimental and may be subject to breaking changes in future releases.*
