# Xamarin Components for OpenID AppAuth (OAuth Client)

OpenID AppAuth is a client SDK for communicating with [OAuth 2.0](https://tools.ietf.org/html/rfc6749) 
and [OpenID Connect](http://openid.net/specs/openid-connect-core-1_0.html) providers. It strives to
directly map the requests and responses of those specifications, while following
the idiomatic style of the implementation language. In addition to mapping the
raw protocol flows, convenience methods are available to assist with common
tasks like performing an action with fresh tokens.

The library follows the best practices set out in 
[OAuth 2.0 for Native Apps](https://tools.ietf.org/html/draft-ietf-oauth-native-apps)
including using
[Custom Tabs](http://developer.android.com/tools/support-library/features.html#custom-tabs)
and `SFSafariViewController` for the auth request. For this reason,
`WebView` and `UIWebView` are explicitly *not* supported due to usability and security reasons.

The library also supports the [PKCE](https://tools.ietf.org/html/rfc7636)
extension to OAuth which was created to secure authorization codes in public
clients when custom URI scheme redirects are used. The library is friendly to
other extensions (standard or otherwise) with the ability to handle additional
parameters in all protocol requests and responses.
