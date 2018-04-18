using System;

using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;

namespace GigyaSDK
{
	// @interface GSObject : NSObject
	[BaseType(typeof(NSObject), Name = "GSObject")]
	interface GSObject
	{
		// @property (copy, nonatomic) NSString * source;
		[Export("source")]
		string Source { get; set; }

		// -(id)objectForKeyedSubscript:(NSString *)key;
		[Export("objectForKeyedSubscript:")]
		NSObject GetKeyedSubscriptObject(string key);

		// -(void)setObject:(id)obj forKeyedSubscript:(NSString *)key;
		[Export("setObject:forKeyedSubscript:")]
		void SetKeyedSubscriptObject(NSObject obj, string key);

		// -(id)objectForKey:(NSString *)key;
		[Export("objectForKey:")]
		NSObject GetObject(string key);

		// -(void)setObject:(id)obj forKey:(NSString *)key;
		[Export("setObject:forKey:")]
		void SetObject(NSObject obj, string key);

		// -(void)removeObjectForKey:(NSString *)key;
		[Export("removeObjectForKey:")]
		void RemoveObject(string key);

		// -(NSArray *)allKeys;
		[Export("allKeys")]
		string[] AllKeys { get; }

		// -(NSString *)JSONString;
		[Export("JSONString")]
		string JsonString { get; }

		[Internal]
		[Export ("dictionary")]
		NSMutableDictionary InternalDictionary { get; set; }
	}

	// typedef void (^ _Nullable)(GSResponse * _Nullable, NSError * _Nullable) GSResponseHandler;
	delegate void GSResponseHandler([NullAllowed] GSResponse response, [NullAllowed] NSError error);

	// @interface GSRequest : NSObject
	[BaseType(typeof(NSObject), Name = "GSRequest")]
	interface GSRequest
	{
		// +(GSRequest *)requestForMethod:(NSString * _Nonnull)method;
		[Static]
		[Export("requestForMethod:")]
		GSRequest Create(string method);

		// +(GSRequest *)requestForMethod:(NSString * _Nonnull)method parameters:(NSDictionary * _Nullable)parameters;
		[Static]
		[Export("requestForMethod:parameters:")]
		GSRequest Create(string method, [NullAllowed] NSDictionary parameters);

		// @property (copy, nonatomic) NSString * _Nonnull method;
		[Export("method")]
		string Method { get; set; }

		// @property (nonatomic, strong) NSMutableDictionary * _Nullable parameters;
		[NullAllowed, Export("parameters", ArgumentSemantic.Strong)]
		NSMutableDictionary Parameters { get; set; }

		// @property (nonatomic) BOOL useHTTPS;
		[Export("useHTTPS")]
		bool UseHttps { get; set; }

		// @property (nonatomic) NSTimeInterval requestTimeout;
		[Export("requestTimeout")]
		double RequestTimeout { get; set; }

		// -(void)sendWithResponseHandler:(GSResponseHandler)handler;
		[Export("sendWithResponseHandler:")]
		[Async]
		void Send([NullAllowed] GSResponseHandler handler);

		// -(void)cancel;
		[Export("cancel")]
		void Cancel();

		// @property (nonatomic, strong) GSSession * session;
		[NullAllowed]
		[Export("session", ArgumentSemantic.Strong)]
		GSSession Session { get; set; }

		// @property (readonly, nonatomic, strong) NSString * requestID;
		[Export("requestID", ArgumentSemantic.Strong)]
		string RequestId { get; }

		// @property (nonatomic) BOOL includeAuthInfo;
		[Export("includeAuthInfo")]
		bool IncludeAuthInfo { get; set; }

		// @property (copy, nonatomic) NSString * source;
		[NullAllowed]
		[Export("source")]
		string Source { get; set; }
	}

	// @interface GSResponse : GSObject
	[BaseType(typeof(GSObject), Name = "GSResponse")]
	interface GSResponse
	{
		// +(void)responseForMethod:(NSString *)method data:(NSData *)data completionHandler:(GSResponseHandler)handler;
		[Static]
		[Export("responseForMethod:data:completionHandler:")]
		[Async]
		void Create(string method, NSData data, [NullAllowed] GSResponseHandler handler);

		// +(GSResponse *)responseWithError:(NSError *)error;
		[Static]
		[Export("responseWithError:")]
		GSResponse Create(NSError error);

		// @property (readonly, weak) NSString * method;
		[Export("method", ArgumentSemantic.Weak)]
		string Method { get; }

		// @property (readonly) int errorCode;
		[Export("errorCode")]
		int ErrorCode { get; }

		// @property (readonly, weak) NSString * callId;
		[Export("callId", ArgumentSemantic.Weak)]
		string CallId { get; }

		// -(NSArray *)allKeys;
		[Export("allKeys")]
		string[] AllKeys { get; }

		// -(id)objectForKey:(NSString *)key;
		[Export("objectForKey:")]
		NSObject GetObject(string key);

		// -(id)objectForKeyedSubscript:(NSString *)key;
		[Export("objectForKeyedSubscript:")]
		NSObject GetKeyedSubscriptObject(string key);

		// -(NSString *)JSONString;
		[Export("JSONString")]
		string JsonString { get; }
	}

	// @interface GSAccount : GSResponse
	[BaseType(typeof(GSResponse), Name = "GSAccount")]
	interface GSAccount
	{
		// @property (readonly, nonatomic, weak) NSString * UID;
		[Export("UID", ArgumentSemantic.Weak)]
		string UID { get; }

		// @property (readonly, nonatomic, weak) NSDictionary * profile;
		[Export("profile", ArgumentSemantic.Weak)]
		NSDictionary Profile { get; }

		// @property (readonly, nonatomic, weak) NSDictionary * data;
		[Export("data", ArgumentSemantic.Weak)]
		NSDictionary Data { get; }

		// @property (readonly, nonatomic, weak) NSString * nickname;
		[Export("nickname", ArgumentSemantic.Weak)]
		string Nickname { get; }

		// @property (readonly, nonatomic, weak) NSString * firstName;
		[Export("firstName", ArgumentSemantic.Weak)]
		string FirstName { get; }

		// @property (readonly, nonatomic, weak) NSString * lastName;
		[Export("lastName", ArgumentSemantic.Weak)]
		string LastName { get; }

		// @property (readonly, nonatomic, weak) NSString * email;
		[Export("email", ArgumentSemantic.Weak)]
		string Email { get; }

		// @property (readonly, nonatomic, weak) NSURL * photoURL;
		[Export("photoURL", ArgumentSemantic.Weak)]
		NSUrl PhotoUrl { get; }

		// @property (readonly, nonatomic, weak) NSURL * thumbnailURL;
		[Export("thumbnailURL", ArgumentSemantic.Weak)]
		NSUrl ThumbnailUrl { get; }

		// -(NSArray *)allKeys;
		[Export("allKeys")]
		string[] AllKeys { get; }

		// -(id)objectForKey:(NSString *)key;
		[Export("objectForKey:")]
		NSObject GetObject(string key);

		// -(id)objectForKeyedSubscript:(NSString *)key;
		[Export("objectForKeyedSubscript:")]
		NSObject GetKeyedSubscriptObject(string key);

		// -(NSString *)JSONString;
		[Export("JSONString")]
		string JsonString { get; }
	}

	interface IGSAccountsDelegate { }

	// @protocol GSAccountsDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "GSAccountsDelegate")]
	interface GSAccountsDelegate
	{
		// The managed type of GSResponse is intentional.
		// Internally the instance type of the parameter is
		// GSResponse not GSAccount.We need to respond with the
		// correct type in C#.  A ToAccount extension has been
		// to help the developer get to the type they expect.
		// @optional -(void)accountDidLogin:(GSAccount *)account;
		[Export("accountDidLogin:")]
		void AccountDidLogin(GSResponse accountResponse);

		// @optional -(void)accountDidLogout;
		[Export("accountDidLogout")]
		void AccountDidLogout();
	}

	// @interface GSSessionInfo : NSObject <NSCoding>
	[BaseType(typeof(NSObject), Name = "GSSessionInfo")]
	interface GSSessionInfo : INSCoding
	{
		// @property (copy, nonatomic) NSDate * expiration;
		[Export("expiration", ArgumentSemantic.Copy)]
		NSDate Expiration { get; set; }

		// @property (copy, nonatomic) NSString * APIKey;
		[Export("APIKey")]
		string ApiKey { get; set; }

		// -(GSSessionInfo *)initWithAPIKey:(NSString *)apikey expiration:(NSDate *)expiration;
		[Export("initWithAPIKey:expiration:")]
		IntPtr Constructor(string apikey, NSDate expiration);

		// -(BOOL)isValid;
		[Export("isValid")]
		bool IsValid { get; }
	}

	// @interface GSSession : NSObject <NSCoding>
	[BaseType(typeof(NSObject), Name = "GSSession")]
	interface GSSession : INSCoding
	{
		// @property (copy, nonatomic) NSString * token;
		[Export("token")]
		string Token { get; set; }

		// @property (copy, nonatomic) NSString * secret;
		[Export("secret")]
		string Secret { get; set; }

		// @property (retain, nonatomic) GSSessionInfo * info;
		[Export("info", ArgumentSemantic.Retain)]
		GSSessionInfo Info { get; set; }

		// @property (copy, nonatomic) NSString * lastLoginProvider;
		[Export("lastLoginProvider")]
		string LastLoginProvider { get; set; }

		// -(GSSession *)initWithSessionToken:(NSString *)token secret:(NSString *)secret;
		[Export("initWithSessionToken:secret:")]
		IntPtr Constructor(string token, string secret);

		// -(GSSession *)initWithSessionToken:(NSString *)token secret:(NSString *)secret expiration:(NSDate *)expiration;
		[Export("initWithSessionToken:secret:expiration:")]
		IntPtr Constructor(string token, string secret, NSDate expiration);

		// -(GSSession *)initWithSessionToken:(NSString *)token secret:(NSString *)secret expiresIn:(NSString *)expiresIn;
		[Export("initWithSessionToken:secret:expiresIn:")]
		IntPtr Constructor(string token, string secret, string expiresIn);

		// -(BOOL)isValid;
		[Export("isValid")]
		bool IsValid { get; }
	}

	// @interface GSUser : GSResponse
	[BaseType(typeof(GSResponse), Name = "GSUser")]
	interface GSUser
	{
		// @property (readonly, nonatomic, weak) NSString * UID;
		[Export("UID", ArgumentSemantic.Weak)]
		string UID { get; }

		// @property (readonly, nonatomic, weak) NSString * loginProvider;
		[Export("loginProvider", ArgumentSemantic.Weak)]
		string LoginProvider { get; }

		// @property (readonly, nonatomic, weak) NSString * nickname;
		[Export("nickname", ArgumentSemantic.Weak)]
		string Nickname { get; }

		// @property (readonly, nonatomic, weak) NSString * firstName;
		[Export("firstName", ArgumentSemantic.Weak)]
		string FirstName { get; }

		// @property (readonly, nonatomic, weak) NSString * lastName;
		[Export("lastName", ArgumentSemantic.Weak)]
		string LastName { get; }

		// @property (readonly, nonatomic, weak) NSString * email;
		[Export("email", ArgumentSemantic.Weak)]
		string Email { get; }

		// @property (readonly, nonatomic, weak) NSArray * identities;
		[Export("identities", ArgumentSemantic.Weak)]
		NSDictionary[] Identities { get; }

		// @property (readonly, nonatomic, weak) NSURL * photoURL;
		[Export("photoURL", ArgumentSemantic.Weak)]
		NSUrl PhotoUrl { get; }

		// @property (readonly, nonatomic, weak) NSURL * thumbnailURL;
		[Export("thumbnailURL", ArgumentSemantic.Weak)]
		NSUrl ThumbnailUrl { get; }

		// -(NSArray *)allKeys;
		[Export("allKeys")]
		string[] AllKeys { get; }

		// -(id)objectForKey:(NSString *)key;
		[Export("objectForKey:")]
		NSObject GetObject(string key);

		// -(id)objectForKeyedSubscript:(NSString *)key;
		[Export("objectForKeyedSubscript:")]
		NSObject GetKeyedSubscriptObject(string key);

		// -(NSString *)JSONString;
		[Export("JSONString")]
		string JsonString { get; }
	}

	interface IGSSessionDelegate { }

	// @protocol GSSessionDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "GSSessionDelegate")]
	interface GSSessionDelegate
	{
		// @optional -(void)userDidLogin:(GSUser *)user;
		[Export("userDidLogin:")]
		void UserDidLogin(GSUser user);

		// @optional -(void)userDidLogout;
		[Export("userDidLogout")]
		void UserDidLogout();

		// @optional -(void)userInfoDidChange:(GSUser *)user;
		[Export("userInfoDidChange:")]
		void UserInfoDidChange(GSUser user);
	}

	interface IGSSocializeDelegate { }

	// @protocol GSSocializeDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "GSSocializeDelegate")]
	interface GSSocializeDelegate
	{
		// @optional -(void)userDidLogin:(GSUser *)user;
		[Export("userDidLogin:")]
		void UserDidLogin(GSUser user);

		// @optional -(void)userDidLogout;
		[Export("userDidLogout")]
		void UserDidLogout();

		// @optional -(void)userInfoDidChange:(GSUser *)user;
		[Export("userInfoDidChange:")]
		void UserInfoDidChange(GSUser user);
	}

	interface IGSWebBridgeDelegate { }

	// @protocol GSWebBridgeDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "GSWebBridgeDelegate")]
	interface GSWebBridgeDelegate
	{
		// @optional -(void)webView:(id)webView startedLoginForMethod:(NSString *)method parameters:(NSDictionary *)parameters;
		[Export("webView:startedLoginForMethod:parameters:")]
		void StartedLogin(NSObject webView, string method, NSDictionary parameters);

		// @optional -(void)webView:(id)webView finishedLoginWithResponse:(GSResponse *)response;
		[Export("webView:finishedLoginWithResponse:")]
		void FinishedLogin(NSObject webView, GSResponse response);

		// @optional -(void)webView:(id)webView receivedPluginEvent:(NSDictionary *)event fromPluginInContainer:(NSString *)containerID;
		[Export("webView:receivedPluginEvent:fromPluginInContainer:")]
		void ReceivedPluginEvent(NSObject webView, NSDictionary @event, string containerID);

		// @optional -(void)webView:(id)webView receivedJsLog:(NSString *)logType logInfo:(NSDictionary *)logInfo;
		[Export("webView:receivedJsLog:logInfo:")]
		void ReceivedJsLog(NSObject webView, string logType, NSDictionary logInfo);
	}

	// @interface GSWebBridge : NSObject
	[BaseType(typeof(NSObject), Name = "GSWebBridge")]
	interface GSWebBridge
	{
		// +(void)registerWebView:(id)webView delegate:(id<GSWebBridgeDelegate>)delegate;
		[Static]
		[Export("registerWebView:delegate:")]
		void RegisterWebView(NSObject webView, IGSWebBridgeDelegate del);

		// +(void)registerWebView:(id)webView delegate:(id<GSWebBridgeDelegate>)delegate settings:(NSDictionary *)settings;
		[Static]
		[Export("registerWebView:delegate:settings:")]
		void RegisterWebView(NSObject webView, IGSWebBridgeDelegate del, NSDictionary settings);

		// +(void)unregisterWebView:(id)webView;
		[Static]
		[Export("unregisterWebView:")]
		void UnregisterWebView(NSObject webView);

		// +(void)webViewDidStartLoad:(id)webView;
		[Static]
		[Export("webViewDidStartLoad:")]
		void WebViewDidStartLoad(NSObject webView);

		// +(BOOL)handleRequest:(NSURLRequest *)request webView:(id)webView;
		[Static]
		[Export("handleRequest:webView:")]
		bool HandleRequest(NSUrlRequest request, NSObject webView);
	}

	// typedef void (^ _Nullable)(GSUser * _Nullable, NSError * _Nullable) GSUserInfoHandler;
	delegate void GSUserInfoHandler([NullAllowed] GSUser user, [NullAllowed] NSError error);

	// typedef void (^ _Nullable)(BOOL, NSError * _Nullable, NSArray * _Nullable) GSPermissionRequestResultHandler;
	delegate void GSPermissionRequestResultHandler(bool granted, [NullAllowed] NSError error, [NullAllowed] NSObject[] declinedPermissions);

	// typedef void (^GSPluginCompletionHandler)(BOOL, NSError * _Nullable);
	delegate void GSPluginCompletionHandler(bool closedByUser, [NullAllowed] NSError error);

	// typedef void (^GSGetSessionCompletionHandler)(GSSession * _Nullable);
	delegate void GSGetSessionCompletionHandler([NullAllowed] GSSession session);

	// @interface Gigya : NSObject
	[BaseType(typeof(NSObject), Name = "Gigya")]
	interface Gigya
	{
		// +(void)initWithAPIKey:(NSString *)apiKey application:(UIApplication *)application launchOptions:(NSDictionary *)launchOptions;
		[Static]
		[Export("initWithAPIKey:application:launchOptions:")]
		void Init(string apiKey, UIApplication application, [NullAllowed] NSDictionary launchOptions);

		// +(void)initWithAPIKey:(NSString *)apiKey application:(UIApplication *)application launchOptions:(NSDictionary *)launchOptions APIDomain:(NSString *)apiDomain;
		[Static]
		[Export("initWithAPIKey:application:launchOptions:APIDomain:")]
		void Init(string apiKey, UIApplication application, [NullAllowed] NSDictionary launchOptions, [NullAllowed] string apiDomain);

		// +(NSString *)APIKey;
		[Static]
		[Export("APIKey")]
		string ApiKey { get; }

		// +(NSString *)APIDomain;
		[Static]
		[Export("APIDomain")]
		string ApiDomain { get; }

		// +(void)getSessionWithCompletionHandler:(GSGetSessionCompletionHandler _Nonnull)handler;
		[Static]
		[Export("getSessionWithCompletionHandler:")]
		[Async]
		void GetSession(GSGetSessionCompletionHandler handler);

		// +(BOOL)isSessionValid;
		[Static]
		[Export("isSessionValid")]
		bool IsSessionValid { get; }

		// +(void)setSession:(GSSession * _Nullable)session;
		[Static]
		[Export("setSession:")]
		void SetSession([NullAllowed] GSSession session);

		// +(id<GSSessionDelegate>)sessionDelegate;
		// +(void)setSessionDelegate:(id<GSSessionDelegate>)delegate __attribute__((deprecated("Use [Gigya setSocializeDelegate:] with a GSSocializeDelegate instead")));
		[Static]
		[NullAllowed]
		[Export("sessionDelegate")]
		[Obsolete("Use SocializeDelegate { get; set; } instead.")]
		IGSSessionDelegate SessionDelegate { get; set; }

		// +(id<GSSocializeDelegate> _Nullable)socializeDelegate;
		[Static]
		[NullAllowed]
		[Export("socializeDelegate")]
		IGSSocializeDelegate SocializeDelegate { get; set; }

		//// +(void)setSocializeDelegate:(id<GSSocializeDelegate> _Nullable)socializeDelegate;
		//[Static]
		//[Export("setSocializeDelegate:")]
		//void SetSocializeDelegate([NullAllowed] IGSSocializeDelegate socializeDelegate);

		// +(id<GSAccountsDelegate> _Nullable)accountsDelegate;
		[Static]
		[NullAllowed]
		[Export("accountsDelegate")]
		IGSAccountsDelegate AccountsDelegate { get; set; }

		//[Static]
		//[Export("setAccountsDelegate:")]
		//// +(void)setAccountsDelegate:(id<GSAccountsDelegate> _Nullable)accountsDelegate;
		//void SetAccountsDelegate( IGSAccountsDelegate accountsDelegate);

		// +(void)loginToProvider:(NSString * _Nonnull)provider;
		[Static]
		[Export("loginToProvider:")]
		void Login(string provider);

		// +(void)showLoginDialogOver:(UIViewController *)viewController provider:(NSString *)provider __attribute__((deprecated("Use loginToProvider: instead")));
		[Static]
		[Export("showLoginDialogOver:provider:")]
		[Obsolete("Use Login(string) instead.")]
		void ShowLoginDialog(UIViewController viewController, string provider);

		// +(void)loginToProvider:(NSString * _Nonnull)provider parameters:(NSDictionary * _Nullable)parameters completionHandler:(GSUserInfoHandler _Nullable)handler;
		[Static]
		[Export("loginToProvider:parameters:completionHandler:")]
		[Async]
		void Login(string provider, [NullAllowed] NSDictionary parameters, [NullAllowed] GSUserInfoHandler handler);

		// +(void)showLoginDialogOver:(UIViewController *)viewController provider:(NSString *)provider parameters:(NSDictionary *)parameters completionHandler:(GSUserInfoHandler)handler __attribute__((deprecated("Use loginToProvider:parameters:completionHandler: instead")));
		[Static]
		[Export("showLoginDialogOver:provider:parameters:completionHandler:")]
		[Obsolete("Use Login(string, NSDictionary, GSUserInfoHandler) instead.")]
		[Async]
		void ShowLoginDialog(UIViewController viewController, string provider, [NullAllowed] NSDictionary parameters, [NullAllowed] GSUserInfoHandler handler);

		// +(void)loginToProvider:(NSString * _Nonnull)provider parameters:(NSDictionary * _Nullable)parameters over:(UIViewController * _Nullable)viewController completionHandler:(GSUserInfoHandler _Nullable)handler;
		[Static]
		[Export("loginToProvider:parameters:over:completionHandler:")]
		[Async]
		void Login(string provider, [NullAllowed] NSDictionary parameters, [NullAllowed] UIViewController viewController, [NullAllowed] GSUserInfoHandler handler);

		// +(void)showLoginProvidersDialogOver:(UIViewController *)viewController;
		[Static]
		[Export("showLoginProvidersDialogOver:")]
		void ShowLoginProvidersDialog(UIViewController viewController);

		// +(void)showLoginProvidersPopoverFrom:(UIView *)view;
		[Static]
		[Export("showLoginProvidersPopoverFrom:")]
		void ShowLoginProvidersPopover(UIView view);

		// +(void)showLoginProvidersDialogOver:(UIViewController *)viewController providers:(NSArray *)providers parameters:(NSDictionary *)parameters completionHandler:(GSUserInfoHandler)handler;
		[Static]
		[Export("showLoginProvidersDialogOver:providers:parameters:completionHandler:")]
		[Async]
		void ShowLoginProvidersDialog(UIViewController viewController, string[] providers, [NullAllowed] NSDictionary parameters, [NullAllowed] GSUserInfoHandler handler);

		// +(void)showLoginProvidersPopoverFrom:(UIView *)view providers:(NSArray *)providers parameters:(NSDictionary *)parameters completionHandler:(GSUserInfoHandler)handler;
		[Static]
		[Export("showLoginProvidersPopoverFrom:providers:parameters:completionHandler:")]
		[Async]
		void ShowLoginProvidersPopover(UIView view, string[] providers, [NullAllowed] NSDictionary parameters, [NullAllowed] GSUserInfoHandler handler);

		// +(void)logout;
		[Static]
		[Export("logout")]
		void Logout();

		// +(void)logoutWithCompletionHandler:(GSResponseHandler)handler;
		[Static]
		[Export("logoutWithCompletionHandler:")]
		[Async]
		void Logout([NullAllowed] GSResponseHandler handler);

		// +(void)addConnectionToProvider:(NSString *)provider;
		[Static]
		[Export("addConnectionToProvider:")]
		void AddConnection(string provider);

		// +(void)showAddConnectionDialogOver:(UIViewController *)viewController provider:(NSString *)provider __attribute__((deprecated("Use addConnectionToProvider: instead")));
		[Static]
		[Export("showAddConnectionDialogOver:provider:")]
		[Obsolete("Use AddConnectionToProvider(string) instead.")]
		void ShowAddConnectionDialog(UIViewController viewController, string provider);

		// +(void)addConnectionToProvider:(NSString *)provider parameters:(NSDictionary *)parameters completionHandler:(GSUserInfoHandler)handler;
		[Static]
		[Export("addConnectionToProvider:parameters:completionHandler:")]
		[Async]
		void AddConnection(string provider, [NullAllowed] NSDictionary parameters, [NullAllowed] GSUserInfoHandler handler);

		// +(void)showAddConnectionDialogOver:(UIViewController *)viewController provider:(NSString *)provider parameters:(NSDictionary *)parameters completionHandler:(GSUserInfoHandler)handler __attribute__((deprecated("Use addConnectionToProvider:parameters:completionHandler: instead")));
		[Static]
		[Export("showAddConnectionDialogOver:provider:parameters:completionHandler:")]
		[Obsolete("Use AddConnectionToProvider(string, NSDictionary, GSUserInfoHandler) instead.")]
		[Async]
		void ShowAddConnectionDialog(UIViewController viewController, string provider, [NullAllowed] NSDictionary parameters, [NullAllowed] GSUserInfoHandler handler);

		// +(void)addConnectionToProvider:(NSString *)provider parameters:(NSDictionary *)parameters over:(UIViewController *)viewController completionHandler:(GSUserInfoHandler)handler;
		[Static]
		[Export("addConnectionToProvider:parameters:over:completionHandler:")]
		[Async]
		void AddConnection(string provider, [NullAllowed] NSDictionary parameters, UIViewController viewController, [NullAllowed] GSUserInfoHandler handler);

		// +(void)showAddConnectionProvidersDialogOver:(UIViewController *)viewController;
		[Static]
		[Export("showAddConnectionProvidersDialogOver:")]
		void ShowAddConnectionProvidersDialog(UIViewController viewController);

		// +(void)showAddConnectionProvidersPopoverFrom:(UIView *)view;
		[Static]
		[Export("showAddConnectionProvidersPopoverFrom:")]
		void ShowAddConnectionProvidersPopover(UIView view);

		// +(void)showAddConnectionProvidersDialogOver:(UIViewController *)viewController providers:(NSArray *)providers parameters:(NSDictionary *)parameters completionHandler:(GSUserInfoHandler)handler;
		[Static]
		[Export("showAddConnectionProvidersDialogOver:providers:parameters:completionHandler:")]
		[Async]
		void ShowAddConnectionProvidersDialog(UIViewController viewController, string[] providers, [NullAllowed] NSDictionary parameters, [NullAllowed] GSUserInfoHandler handler);

		// +(void)showAddConnectionProvidersPopoverFrom:(UIView *)view providers:(NSArray *)providers parameters:(NSDictionary *)parameters completionHandler:(GSUserInfoHandler)handler;
		[Static]
		[Export("showAddConnectionProvidersPopoverFrom:providers:parameters:completionHandler:")]
		[Async]
		void ShowAddConnectionProvidersPopover(UIView view, string[] providers, [NullAllowed] NSDictionary parameters, [NullAllowed] GSUserInfoHandler handler);

		// +(void)removeConnectionToProvider:(NSString *)provider;
		[Static]
		[Export("removeConnectionToProvider:")]
		void RemoveConnection(string provider);

		// +(void)removeConnectionToProvider:(NSString *)provider completionHandler:(GSUserInfoHandler)handler;
		[Static]
		[Export("removeConnectionToProvider:completionHandler:")]
		[Async]
		void RemoveConnection(string provider, [NullAllowed] GSUserInfoHandler handler);

		// +(void)showPluginDialogOver:(UIViewController *)viewController plugin:(NSString *)plugin parameters:(NSDictionary *)parameters;
		[Static]
		[Export("showPluginDialogOver:plugin:parameters:")]
		void ShowPluginDialog(UIViewController viewController, string plugin, [NullAllowed] NSDictionary parameters);

		// +(void)showPluginDialogOver:(UIViewController *)viewController plugin:(NSString *)plugin parameters:(NSDictionary *)parameters completionHandler:(GSPluginCompletionHandler)handler;
		[Static]
		[Export("showPluginDialogOver:plugin:parameters:completionHandler:")]
		[Async]
		void ShowPluginDialog(UIViewController viewController, string plugin, [NullAllowed] NSDictionary parameters, GSPluginCompletionHandler handler);

		// +(void)showPluginDialogOver:(UIViewController *)viewController plugin:(NSString *)plugin parameters:(NSDictionary *)parameters completionHandler:(GSPluginCompletionHandler)handler delegate:(id<GSPluginViewDelegate>)delegate;
		[Static]
		[Export("showPluginDialogOver:plugin:parameters:completionHandler:delegate:")]
		void ShowPluginDialog(UIViewController viewController, string plugin, [NullAllowed] NSDictionary parameters, GSPluginCompletionHandler handler, IGSPluginViewDelegate del);

		// +(void)requestNewFacebookPublishPermissions:(NSString *)permissions viewController:(UIViewController * _Nullable)viewController responseHandler:(GSPermissionRequestResultHandler)handler;
		[Static]
		[Export("requestNewFacebookPublishPermissions:viewController:responseHandler:")]
		void RequestNewFacebookPublishPermissions(string permissions, [NullAllowed] UIViewController viewController, [NullAllowed] GSPermissionRequestResultHandler handler);

		// +(void)requestNewFacebookReadPermissions:(NSString *)permissions viewController:(UIViewController * _Nullable)viewController responseHandler:(GSPermissionRequestResultHandler)handler;
		[Static]
		[Export("requestNewFacebookReadPermissions:viewController:responseHandler:")]
		void RequestNewFacebookReadPermissions(string permissions, [NullAllowed] UIViewController viewController, [NullAllowed] GSPermissionRequestResultHandler handler);

		// +(BOOL)handleOpenURL:(NSURL *)url app:(UIApplication *)app options:(NSDictionary<NSString *,id> *)options;
		[Static]
		[Export("handleOpenURL:app:options:")]
		bool HandleOpenUrl(NSUrl url, UIApplication app, [NullAllowed] NSDictionary options);
		//bool HandleOpenUrl(NSUrl url, UIApplication app, NSDictionary<NSString, NSObject> options);

		// +(BOOL)handleOpenURL:(NSURL *)url application:(UIApplication *)application sourceApplication:(NSString *)sourceApplication annotation:(id)annotation;
		[Static]
		[Export("handleOpenURL:application:sourceApplication:annotation:")]
		bool HandleOpenUrl(NSUrl url, UIApplication application, string sourceApplication, NSObject annotation);

		// +(void)handleDidBecomeActive;
		[Static]
		[Export("handleDidBecomeActive")]
		void HandleOnActivated();

		// +(BOOL)useHTTPS;
		[Static]
		[Export("useHTTPS")]
		bool UseHttps { get; set; }

		//// +(void)setUseHTTPS:(BOOL)useHTTPS;
		//[Static]
		//[Export("setUseHTTPS:")]
		//void SetUseHttps(bool useHTTPS);

		// +(BOOL)networkActivityIndicatorEnabled;
		[Static]
		[Export("networkActivityIndicatorEnabled")]
		bool NetworkActivityIndicatorEnabled { get; set; }

		//// +(void)setNetworkActivityIndicatorEnabled:(BOOL)networkActivityIndicatorEnabled;
		//[Static]
		//[Export("setNetworkActivityIndicatorEnabled:")]
		//void SetNetworkActivityIndicatorEnabled(bool networkActivityIndicatorEnabled);

		// +(NSTimeInterval)requestTimeout;
		[Static]
		[Export("requestTimeout")]
		double RequestTimeout { get; set; }

		//// +(void)setRequestTimeout:(NSTimeInterval)requestTimeout;
		//[Static]
		//[Export("setRequestTimeout:")]
		//void SetRequestTimeout(double requestTimeout);

		// +(BOOL)dontLeaveApp;
		[Static]
		[Export("dontLeaveApp")]
		bool DoNotLeaveApp { get; set; }

		//// +(void)setDontLeaveApp:(BOOL)dontLeaveApp;
		//[Static]
		//[Export("setDontLeaveApp:")]
		//void SetDontLeaveApp(bool dontLeaveApp);

		// +(BOOL)__debugOptionEnableTestNetworks;
		[Static]
		[Export("__debugOptionEnableTestNetworks")]
		bool EnableTestNetworksDebugOption { get; set; }

		//// +(void)__setDebugOptionEnableTestNetworks:(BOOL)debugOptionEnableTestNetworks;
		//[Static]
		//[Export("__setDebugOptionEnableTestNetworks:")]
		//void __setDebugOptionEnableTestNetworks(bool debugOptionEnableTestNetworks);
	}

	interface IGSPluginViewDelegate { }

	// @protocol GSPluginViewDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "GSPluginViewDelegate")]
	interface GSPluginViewDelegate
	{
		// @optional -(void)pluginView:(GSPluginView *)pluginView finishedLoadingPluginWithEvent:(NSDictionary *)event;
		[Export("pluginView:finishedLoadingPluginWithEvent:")]
		void FinishedLoadingPlugin(GSPluginView pluginView, NSDictionary evt);

		// @optional -(void)pluginView:(GSPluginView *)pluginView firedEvent:(NSDictionary *)event;
		[Export("pluginView:firedEvent:")]
		void FiredEvent(GSPluginView pluginView, NSDictionary evt);

		// @optional -(void)pluginView:(GSPluginView *)pluginView didFailWithError:(NSError *)error;
		[Export("pluginView:didFailWithError:")]
		void DidFail(GSPluginView pluginView, NSError error);
	}

	// @interface GSPluginView : UIView
	[BaseType(typeof(UIView), Name = "GSPluginView")]
	interface GSPluginView
	{
		[Wrap("WeakDelegate")]
		GSPluginViewDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<GSPluginViewDelegate> delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(void)loadPlugin:(NSString *)plugin;
		[Export("loadPlugin:")]
		void LoadPlugin(string plugin);

		// -(void)loadPlugin:(NSString *)plugin parameters:(NSDictionary *)parameters;
		[Export("loadPlugin:parameters:")]
		void LoadPlugin(string plugin, NSDictionary parameters);

		// @property (readonly, nonatomic) NSString * plugin;
		[Export("plugin")]
		string Plugin { get; }

		// @property (nonatomic) BOOL showLoginProgress;
		[Export("showLoginProgress")]
		bool ShowLoginProgress { get; set; }

		// @property (copy, nonatomic) NSString * loginProgressText;
		[Export("loginProgressText")]
		string LoginProgressText { get; set; }

		// @property (nonatomic) BOOL showLoadingProgress;
		[Export("showLoadingProgress")]
		bool ShowLoadingProgress { get; set; }

		// @property (copy, nonatomic) NSString * loadingProgressText;
		[Export("loadingProgressText")]
		string LoadingProgressText { get; set; }

		// @property (nonatomic) NSInteger javascriptLoadingTimeout;
		[Export("javascriptLoadingTimeout")]
		nint JavascriptLoadingTimeout { get; set; }
	}
}
