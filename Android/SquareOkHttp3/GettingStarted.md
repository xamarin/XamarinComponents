# Getting Started with OkHttp

> An HTTP+HTTP/2 client for Android and Java applications

HTTP is the way modern applications network. It’s how we exchange data &amp;
media. Doing HTTP efficiently makes your stuff load faster and saves
bandwidth.

OkHttp is an HTTP client that’s efficient by default:

  * HTTP/2 support allows all requests to the same host to share a socket.
  * Connection pooling reduces request latency (if HTTP/2 isn’t available).
  * Transparent GZIP shrinks download sizes.
  * Response caching avoids the network completely for repeat requests.

OkHttp perseveres when the network is troublesome: it will silently recover
from common connection problems. If your service has multiple IP addresses
OkHttp will attempt alternate addresses if the first connect fails. This is
necessary for IPv4+IPv6 and for services hosted in redundant data centers.
OkHttp initiates new connections with modern TLS features (SNI, ALPN), and
falls back to TLS 1.0 if the handshake fails.

Using OkHttp is easy. Its 3.0 API is designed with fluent builders and
immutability. It supports synchronous blocking calls, async calls with
callbacks and async calls using `Task` with `await`.

## Get a URL

This code downloads a URL and print its contents as a string:

    // Create a request
    OkHttpClient client = new OkHttpClient();
    Request request = new Request.Builder()
        .Url(url)
        .Build();
    
    // Synchronous blocking call
    Response response = client.NewCall(request).Execute();
    string body = response.Body().String();

## Post to a Server

This code posts data to a service:

    // Create a request
    OkHttpClient client = new OkHttpClient();
    RequestBody body = RequestBody.Create(
        MediaType.parse("application/json; charset=utf-8"), 
        "{ 'name': 'Xamarin', 'rating': 5 }");
    Request request = new Request.Builder()
        .Url(url)
        .Post(body)
        .Build();
        
    // Synchronous blocking call
    Response response = client.NewCall(request).Execute();
    string body = response.Body().String();

## Asynchronous Requests

This code executes a request on a background thread using a callback mechanism:

    OkHttpClient client = ...;
    Request request = ...;
    
    // Asynchronous callback
    client.NewCall(request).Enqueue(
        (call, response) => {
            // Response came back
            string body = response.Body().String();
        }, (call, exception) => {
            // There was an error
        });
    
This code executes a request on a background thread using `Task` and `await`:
    
    OkHttpClient client = ...;
    Request request = ...;
    
    // Asynchronous call using Task
    Response response = await client.NewCall(request).ExecuteAsync();
    string body = response.Body().String();
