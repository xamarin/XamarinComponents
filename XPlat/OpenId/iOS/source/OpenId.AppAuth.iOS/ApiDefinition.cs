using System;

using UIKit;
using Foundation;
using ObjCRuntime;
using SafariServices;

namespace OpenId.AppAuth
{
	// typedef void (^OIDAuthStateAction)(NSString * _Nullable, NSString * _Nullable, NSError * _Nullable);
	delegate void AuthStateAction([NullAllowed] string accessToken, [NullAllowed] string idToken, [NullAllowed] NSError error);

	// typedef void (^OIDAuthStateAuthorizationCallback)(OIDAuthState * _Nullable, NSError * _Nullable);
	delegate void AuthStateAuthorizationCallback([NullAllowed] AuthState authState, [NullAllowed] NSError error);

	// @interface OIDAuthState : NSObject <NSSecureCoding>
	[BaseType(typeof(NSObject), Name = "OIDAuthState")]
	[DisableDefaultCtor]
	interface AuthState : INSSecureCoding
	{
		// @property (readonly, nonatomic) NSString * _Nullable refreshToken;
		[NullAllowed, Export("refreshToken")]
		string RefreshToken { get; }

		// @property (readonly, nonatomic) NSString * _Nullable scope;
		[NullAllowed, Export("scope")]
		string Scope { get; }

		// @property (readonly, nonatomic) OIDAuthorizationResponse * _Nonnull lastAuthorizationResponse;
		[Export("lastAuthorizationResponse")]
		AuthorizationResponse LastAuthorizationResponse { get; }

		// @property (readonly, nonatomic) OIDTokenResponse * _Nullable lastTokenResponse;
		[NullAllowed, Export("lastTokenResponse")]
		TokenResponse LastTokenResponse { get; }

		// @property (readonly, nonatomic) OIDRegistrationResponse * _Nullable lastRegistrationResponse;
		[NullAllowed, Export("lastRegistrationResponse")]
		RegistrationResponse LastRegistrationResponse { get; }

		// @property (readonly, nonatomic) NSError * _Nullable authorizationError;
		[NullAllowed, Export("authorizationError")]
		NSError AuthorizationError { get; }

		// @property (readonly, nonatomic) BOOL isAuthorized;
		[Export("isAuthorized")]
		bool IsAuthorized { get; }

		// @property (nonatomic, weak) id<OIDAuthStateChangeDelegate> _Nullable stateChangeDelegate;
		[NullAllowed, Export("stateChangeDelegate", ArgumentSemantic.Weak)]
		IAuthStateChangeDelegate StateChangeDelegate { get; set; }

		// @property (nonatomic, weak) id<OIDAuthStateErrorDelegate> _Nullable errorDelegate;
		[NullAllowed, Export("errorDelegate", ArgumentSemantic.Weak)]
		IAuthStateErrorDelegate ErrorDelegate { get; set; }

		// +(id<OIDAuthorizationFlowSession> _Nonnull)authStateByPresentingAuthorizationRequest:(OIDAuthorizationRequest * _Nonnull)authorizationRequest UICoordinator:(id<OIDAuthorizationUICoordinator> _Nonnull)UICoordinator callback:(OIDAuthStateAuthorizationCallback _Nonnull)callback;
		[Static]
		[Export("authStateByPresentingAuthorizationRequest:UICoordinator:callback:")]
		IAuthorizationFlowSession PresentAuthorizationRequest(AuthorizationRequest authorizationRequest, IAuthorizationUICoordinator UICoordinator, AuthStateAuthorizationCallback callback);

		// -(instancetype _Nonnull)initWithAuthorizationResponse:(OIDAuthorizationResponse * _Nonnull)authorizationResponse;
		[Export("initWithAuthorizationResponse:")]
		IntPtr Constructor(AuthorizationResponse authorizationResponse);

		// -(instancetype _Nonnull)initWithAuthorizationResponse:(OIDAuthorizationResponse * _Nonnull)authorizationResponse tokenResponse:(OIDTokenResponse * _Nullable)tokenResponse;
		[Export("initWithAuthorizationResponse:tokenResponse:")]
		IntPtr Constructor(AuthorizationResponse authorizationResponse, [NullAllowed] TokenResponse tokenResponse);

		// -(instancetype _Nonnull)initWithRegistrationResponse:(OIDRegistrationResponse * _Nonnull)registrationResponse;
		[Export("initWithRegistrationResponse:")]
		IntPtr Constructor(RegistrationResponse registrationResponse);

		// -(instancetype _Nonnull)initWithAuthorizationResponse:(OIDAuthorizationResponse * _Nullable)authorizationResponse tokenResponse:(OIDTokenResponse * _Nullable)tokenResponse registrationResponse:(OIDRegistrationResponse * _Nullable)registrationResponse __attribute__((objc_designated_initializer));
		[Export("initWithAuthorizationResponse:tokenResponse:registrationResponse:")]
		[DesignatedInitializer]
		IntPtr Constructor([NullAllowed] AuthorizationResponse authorizationResponse, [NullAllowed] TokenResponse tokenResponse, [NullAllowed] RegistrationResponse registrationResponse);

		// -(void)updateWithAuthorizationResponse:(OIDAuthorizationResponse * _Nullable)authorizationResponse error:(NSError * _Nullable)error;
		[Export("updateWithAuthorizationResponse:error:")]
		void Update([NullAllowed] AuthorizationResponse authorizationResponse, [NullAllowed] NSError error);

		// -(void)updateWithTokenResponse:(OIDTokenResponse * _Nullable)tokenResponse error:(NSError * _Nullable)error;
		[Export("updateWithTokenResponse:error:")]
		void Update([NullAllowed] TokenResponse tokenResponse, [NullAllowed] NSError error);

		// -(void)updateWithRegistrationResponse:(OIDRegistrationResponse * _Nullable)registrationResponse;
		[Export("updateWithRegistrationResponse:")]
		void UpdateWithRegistrationResponse([NullAllowed] RegistrationResponse registrationResponse);

		// -(void)updateWithAuthorizationError:(NSError * _Nonnull)authorizationError;
		[Export("updateWithAuthorizationError:")]
		void Update(NSError authorizationError);

		// -(void)performActionWithFreshTokens:(OIDAuthStateAction _Nonnull)action;
		[Export("performActionWithFreshTokens:")]
		void PerformWithFreshTokens(AuthStateAction action);

		// - (void)performActionWithFreshTokens:(OIDAuthStateAction)action additionalRefreshParameters:(nullable NSDictionary<NSString *, NSString *> *)additionalParameters;
		[Export("performActionWithFreshTokens:additionalRefreshParameters:")]
		void PerformWithFreshTokens(AuthStateAction action, [NullAllowed] NSDictionary<NSString, NSString> additionalParameters);

		// -(void)setNeedsTokenRefresh;
		[Export("setNeedsTokenRefresh")]
		void SetNeedsTokenRefresh();

		// -(OIDTokenRequest * _Nullable)tokenRefreshRequest;
		[NullAllowed, Export("tokenRefreshRequest")]
		TokenRequest TokenRefreshRequest();

		// -(OIDTokenRequest * _Nullable)tokenRefreshRequestWithAdditionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters;
		[Export("tokenRefreshRequestWithAdditionalParameters:")]
		[return: NullAllowed]
		TokenRequest TokenRefreshRequest([NullAllowed] NSDictionary<NSString, NSString> additionalParameters);
	}

	//// @interface IOS (OIDAuthState)
	//[Category]
	//[BaseType(typeof(OIDAuthState), Name="OIDAuthStateExtensions")]
	//interface AuthStateExtensions
	//{
	//	// +(id<OIDAuthorizationFlowSession> _Nonnull)authStateByPresentingAuthorizationRequest:(OIDAuthorizationRequest * _Nonnull)authorizationRequest presentingViewController:(UIViewController * _Nonnull)presentingViewController callback:(OIDAuthStateAuthorizationCallback _Nonnull)callback;
	//	[Static]
	//	[Export("authStateByPresentingAuthorizationRequest:presentingViewController:callback:")]
	//	IAuthorizationFlowSession PresentAuthorizationRequest(AuthorizationRequest authorizationRequest, UIViewController presentingViewController, AuthStateAuthorizationCallback callback);
	//}

	interface IAuthStateChangeDelegate { }

	// @protocol OIDAuthStateChangeDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "OIDAuthStateChangeDelegate")]
	interface AuthStateChangeDelegate
	{
		// @required -(void)didChangeState:(OIDAuthState * _Nonnull)state;
		[Abstract]
		[Export("didChangeState:")]
		void DidChangeState(AuthState state);
	}

	interface IAuthStateErrorDelegate { }

	// @protocol OIDAuthStateErrorDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "OIDAuthStateErrorDelegate")]
	interface AuthStateErrorDelegate
	{
		// @required -(void)authState:(OIDAuthState * _Nonnull)state didEncounterAuthorizationError:(NSError * _Nonnull)error;
		[Abstract]
		[Export("authState:didEncounterAuthorizationError:")]
		void DidEncounterAuthorizationError(AuthState state, NSError error);

		// @optional -(void)authState:(OIDAuthState * _Nonnull)state didEncounterTransientError:(NSError * _Nonnull)error;
		[Export("authState:didEncounterTransientError:")]
		void DidEncounterTransientError(AuthState state, NSError error);
	}

	[Static]
	partial interface ResponseType
	{
		// extern NSString *const OIDResponseTypeCode;
		[Field("OIDResponseTypeCode", "__Internal")]
		NSString Code { get; }

		// extern NSString *const OIDResponseTypeToken;
		[Field("OIDResponseTypeToken", "__Internal")]
		NSString Token { get; }

		// extern NSString *const OIDResponseTypeIDToken;
		[Field("OIDResponseTypeIDToken", "__Internal")]
		NSString IdToken { get; }
	}

	[Static]
	partial interface Scope
	{
		// extern NSString *const OIDScopeOpenID;
		[Field("OIDScopeOpenID", "__Internal")]
		NSString OpenId { get; }

		// extern NSString *const OIDScopeProfile;
		[Field("OIDScopeProfile", "__Internal")]
		NSString Profile { get; }

		// extern NSString *const OIDScopeEmail;
		[Field("OIDScopeEmail", "__Internal")]
		NSString Email { get; }

		// extern NSString *const OIDScopeAddress;
		[Field("OIDScopeAddress", "__Internal")]
		NSString Address { get; }

		// extern NSString *const OIDScopePhone;
		[Field("OIDScopePhone", "__Internal")]
		NSString Phone { get; }
	}

	// @interface OIDAuthorizationRequest : NSObject <NSCopying, NSSecureCoding>
	[BaseType(typeof(NSObject), Name = "OIDAuthorizationRequest")]
	[DisableDefaultCtor]
	interface AuthorizationRequest : INSCopying, INSSecureCoding
	{
		// extern NSString *const _Nonnull OIDOAuthorizationRequestCodeChallengeMethodS256;
		[Static]
		[Field("OIDOAuthorizationRequestCodeChallengeMethodS256", "__Internal")]
		NSString CodeChallengeMethodS256 { get; }

		// @property (readonly, nonatomic) OIDServiceConfiguration * _Nonnull configuration;
		[Export("configuration")]
		ServiceConfiguration Configuration { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull responseType;
		[Export("responseType")]
		string ResponseType { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull clientID;
		[Export("clientID")]
		string ClientId { get; }

		// @property (readonly, nonatomic) NSString * _Nullable clientSecret;
		[NullAllowed, Export("clientSecret")]
		string ClientSecret { get; }

		// @property (readonly, nonatomic) NSString * _Nullable scope;
		[NullAllowed, Export("scope")]
		string Scope { get; }

		// @property (readonly, nonatomic, nullable) NSURL * _Nullable redirectURL;
		[NullAllowed, Export("redirectURL")]
		NSUrl RedirectUrl { get; }

		// @property (readonly, nonatomic) NSString * _Nullable state;
		[NullAllowed, Export("state")]
		string State { get; }

		// @property (readonly, nonatomic) NSString * _Nullable codeVerifier;
		[NullAllowed, Export("codeVerifier")]
		string CodeVerifier { get; }

		// @property (readonly, nonatomic) NSString * _Nullable codeChallenge;
		[NullAllowed, Export("codeChallenge")]
		string CodeChallenge { get; }

		// @property (readonly, nonatomic) NSString * _Nullable codeChallengeMethod;
		[NullAllowed, Export("codeChallengeMethod")]
		string CodeChallengeMethod { get; }

		// @property (readonly, nonatomic) NSDictionary<NSString *,NSString *> * _Nullable additionalParameters;
		[NullAllowed, Export("additionalParameters")]
		NSDictionary<NSString, NSString> AdditionalParameters { get; }

		// -(instancetype _Nonnull)initWithConfiguration:(OIDServiceConfiguration * _Nonnull)configuration clientId:(NSString * _Nonnull)clientID scopes:(NSArray<NSString *> * _Nullable)scopes redirectURL:(NSURL * _Nonnull)redirectURL responseType:(NSString * _Nonnull)responseType additionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters;
		[Export("initWithConfiguration:clientId:scopes:redirectURL:responseType:additionalParameters:")]
		IntPtr Constructor(ServiceConfiguration configuration, string clientID, [NullAllowed] string[] scopes, NSUrl redirectURL, string responseType, [NullAllowed] NSDictionary<NSString, NSString> additionalParameters);

		// -(instancetype _Nonnull)initWithConfiguration:(OIDServiceConfiguration * _Nonnull)configuration clientId:(NSString * _Nonnull)clientID clientSecret:(NSString * _Nullable)clientSecret scopes:(NSArray<NSString *> * _Nullable)scopes redirectURL:(NSURL * _Nonnull)redirectURL responseType:(NSString * _Nonnull)responseType additionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters;
		[Export("initWithConfiguration:clientId:clientSecret:scopes:redirectURL:responseType:additionalParameters:")]
		IntPtr Constructor(ServiceConfiguration configuration, string clientID, [NullAllowed] string clientSecret, [NullAllowed] string[] scopes, NSUrl redirectURL, string responseType, [NullAllowed] NSDictionary<NSString, NSString> additionalParameters);

		// -(instancetype _Nonnull)initWithConfiguration:(OIDServiceConfiguration * _Nonnull)configuration clientId:(NSString * _Nonnull)clientID clientSecret:(NSString * _Nullable)clientSecret scope:(NSString * _Nullable)scope redirectURL:(NSURL * _Nonnull)redirectURL responseType:(NSString * _Nonnull)responseType state:(NSString * _Nullable)state codeVerifier:(NSString * _Nullable)codeVerifier codeChallenge:(NSString * _Nullable)codeChallenge codeChallengeMethod:(NSString * _Nullable)codeChallengeMethod additionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters __attribute__((objc_designated_initializer));
		[Export("initWithConfiguration:clientId:clientSecret:scope:redirectURL:responseType:state:codeVerifier:codeChallenge:codeChallengeMethod:additionalParameters:")]
		[DesignatedInitializer]
		IntPtr Constructor(ServiceConfiguration configuration, string clientID, [NullAllowed] string clientSecret, [NullAllowed] string scope, [NullAllowed] NSUrl redirectURL, string responseType, [NullAllowed] string state, [NullAllowed] string codeVerifier, [NullAllowed] string codeChallenge, [NullAllowed] string codeChallengeMethod, [NullAllowed] NSDictionary<NSString, NSString> additionalParameters);

		// -(NSURL * _Nonnull)authorizationRequestURL;
		[Export("authorizationRequestURL")]
		NSUrl AuthorizationRequestUrl { get; }

		// +(NSString * _Nullable)generateState;
		[Static]
		[NullAllowed, Export("generateState")]
		string GenerateState();

		// +(NSString * _Nullable)generateCodeVerifier;
		[Static]
		[NullAllowed, Export("generateCodeVerifier")]
		string GenerateCodeVerifier();

		// +(NSString * _Nullable)codeChallengeS256ForVerifier:(NSString * _Nullable)codeVerifier;
		[Static]
		[Export("codeChallengeS256ForVerifier:")]
		[return: NullAllowed]
		string CodeChallengeS256([NullAllowed] string codeVerifier);
	}

	// @interface OIDAuthorizationResponse : NSObject <NSCopying, NSSecureCoding>
	[BaseType(typeof(NSObject), Name = "OIDAuthorizationResponse")]
	[DisableDefaultCtor]
	interface AuthorizationResponse : INSCopying, INSSecureCoding
	{
		// @property (readonly, nonatomic) OIDAuthorizationRequest * _Nonnull request;
		[Export("request")]
		AuthorizationRequest Request { get; }

		// @property (readonly, nonatomic) NSString * _Nullable authorizationCode;
		[NullAllowed, Export("authorizationCode")]
		string AuthorizationCode { get; }

		// @property (readonly, nonatomic) NSString * _Nullable state;
		[NullAllowed, Export("state")]
		string State { get; }

		// @property (readonly, nonatomic) NSString * _Nullable accessToken;
		[NullAllowed, Export("accessToken")]
		string AccessToken { get; }

		// @property (readonly, nonatomic) NSDate * _Nullable accessTokenExpirationDate;
		[NullAllowed, Export("accessTokenExpirationDate")]
		NSDate AccessTokenExpirationDate { get; }

		// @property (readonly, nonatomic) NSString * _Nullable tokenType;
		[NullAllowed, Export("tokenType")]
		string TokenType { get; }

		// @property (readonly, nonatomic) NSString * _Nullable idToken;
		[NullAllowed, Export("idToken")]
		string IdToken { get; }

		// @property (readonly, nonatomic) NSString * _Nullable scope;
		[NullAllowed, Export("scope")]
		string Scope { get; }

		// @property (readonly, nonatomic) NSDictionary<NSString *,NSObject<NSCopying> *> * _Nullable additionalParameters;
		[NullAllowed, Export("additionalParameters")]
		NSDictionary<NSString, NSCopying> AdditionalParameters { get; }

		// -(instancetype _Nonnull)initWithRequest:(OIDAuthorizationRequest * _Nonnull)request parameters:(NSDictionary<NSString *,NSObject<NSCopying> *> * _Nonnull)parameters __attribute__((objc_designated_initializer));
		[Export("initWithRequest:parameters:")]
		[DesignatedInitializer]
		IntPtr Constructor(AuthorizationRequest request, NSDictionary<NSString, NSCopying> parameters);

		// -(OIDTokenRequest * _Nullable)tokenExchangeRequest;
		[NullAllowed, Export("tokenExchangeRequest")]
		TokenRequest CreateTokenExchangeRequest();

		// -(OIDTokenRequest * _Nullable)tokenExchangeRequestWithAdditionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters;
		[Export("tokenExchangeRequestWithAdditionalParameters:")]
		[return: NullAllowed]
		TokenRequest CreateTokenExchangeRequest([NullAllowed] NSDictionary<NSString, NSString> additionalParameters);
	}

	// typedef void (^OIDDiscoveryCallback)(OIDServiceConfiguration * _Nullable, NSError * _Nullable);
	delegate void DiscoveryCallback([NullAllowed] ServiceConfiguration configuration, [NullAllowed] NSError error);

	// typedef void (^OIDAuthorizationCallback)(OIDAuthorizationResponse * _Nullable, NSError * _Nullable);
	delegate void AuthorizationCallback([NullAllowed] AuthorizationResponse authorizationResponse, [NullAllowed] NSError error);

	// typedef void (^OIDTokenCallback)(OIDTokenResponse * _Nullable, NSError * _Nullable);
	delegate void TokenCallback([NullAllowed] TokenResponse tokenResponse, [NullAllowed] NSError error);

	// typedef void (^OIDRegistrationCompletion)(OIDRegistrationResponse * _Nullable, NSError * _Nullable);
	delegate void RegistrationCompletion([NullAllowed] RegistrationResponse registrationResponse, [NullAllowed] NSError error);

	// @interface OIDAuthorizationService : NSObject
	[BaseType(typeof(NSObject), Name = "OIDAuthorizationService")]
	[DisableDefaultCtor]
	interface AuthorizationService
	{
		// @property (readonly, nonatomic) OIDServiceConfiguration * _Nonnull configuration;
		[Export("configuration")]
		ServiceConfiguration Configuration { get; }

		// +(void)discoverServiceConfigurationForIssuer:(NSURL * _Nonnull)issuerURL completion:(OIDDiscoveryCallback _Nonnull)completion;
		[Static]
		[Async]
		[Export("discoverServiceConfigurationForIssuer:completion:")]
		void DiscoverServiceConfigurationForIssuer(NSUrl issuerUrl, DiscoveryCallback completion);

		// +(void)discoverServiceConfigurationForDiscoveryURL:(NSURL * _Nonnull)discoveryURL completion:(OIDDiscoveryCallback _Nonnull)completion;
		[Static]
		[Async]
		[Export("discoverServiceConfigurationForDiscoveryURL:completion:")]
		void DiscoverServiceConfigurationForDiscovery(NSUrl discoveryUrl, DiscoveryCallback completion);

		// +(id<OIDAuthorizationFlowSession> _Nonnull)presentAuthorizationRequest:(OIDAuthorizationRequest * _Nonnull)request UICoordinator:(id<OIDAuthorizationUICoordinator> _Nonnull)UICoordinator callback:(OIDAuthorizationCallback _Nonnull)callback;
		[Static]
		[Export("presentAuthorizationRequest:UICoordinator:callback:")]
		IAuthorizationFlowSession PresentAuthorizationRequest(AuthorizationRequest request, IAuthorizationUICoordinator UICoordinator, AuthorizationCallback callback);

		// +(void)performTokenRequest:(OIDTokenRequest * _Nonnull)request callback:(OIDTokenCallback _Nonnull)callback;
		[Static]
		[Async]
		[Export("performTokenRequest:callback:")]
		void PerformTokenRequest(TokenRequest request, TokenCallback callback);

		// +(void)performRegistrationRequest:(OIDRegistrationRequest * _Nonnull)request completion:(OIDRegistrationCompletion _Nonnull)completion;
		[Static]
		[Export("performRegistrationRequest:completion:")]
		void PerformRegistrationRequest(RegistrationRequest request, RegistrationCompletion completion);
	}

	//// @interface IOS (OIDAuthorizationService)
	//[Category]
	//[BaseType(typeof(OIDAuthorizationService))]
	//interface AuthorizationServiceExtensions
	//{
	//	// +(id<OIDAuthorizationFlowSession> _Nonnull)presentAuthorizationRequest:(OIDAuthorizationRequest * _Nonnull)request presentingViewController:(UIViewController * _Nonnull)presentingViewController callback:(OIDAuthorizationCallback _Nonnull)callback;
	//	[Static]
	//	[Export("presentAuthorizationRequest:presentingViewController:callback:")]
	//	IAuthorizationFlowSession PresentAuthorizationRequest(AuthorizationRequest request, UIViewController presentingViewController, AuthorizationCallback callback);
	//}

	interface IAuthorizationFlowSession { }

	// @protocol OIDAuthorizationFlowSession <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "OIDAuthorizationFlowSession")]
	interface AuthorizationFlowSession
	{
		// @required -(void)cancel;
		[Abstract]
		[Export("cancel")]
		void Cancel();

		// @required -(BOOL)resumeAuthorizationFlowWithURL:(NSURL * _Nonnull)URL;
		[Abstract]
		[Export("resumeAuthorizationFlowWithURL:")]
		bool ResumeAuthorizationFlow(NSUrl url);

		// @required -(void)failAuthorizationFlowWithError:(NSError * _Nonnull)error;
		[Abstract]
		[Export("failAuthorizationFlowWithError:")]
		void FailAuthorizationFlow(NSError error);
	}

	[Static]
	partial interface Error
	{
		// extern NSString *const _Nonnull OIDGeneralErrorDomain;
		[Field("OIDGeneralErrorDomain", "__Internal")]
		NSString GeneralErrorDomain { get; }

		// extern NSString *const _Nonnull OIDOAuthAuthorizationErrorDomain;
		[Field("OIDOAuthAuthorizationErrorDomain", "__Internal")]
		NSString OAuthAuthorizationErrorDomain { get; }

		// extern NSString *const _Nonnull OIDOAuthTokenErrorDomain;
		[Field("OIDOAuthTokenErrorDomain", "__Internal")]
		NSString OAuthTokenErrorDomain { get; }

		// extern NSString *const _Nonnull OIDOAuthRegistrationErrorDomain;
		[Field("OIDOAuthRegistrationErrorDomain", "__Internal")]
		NSString OAuthRegistrationErrorDomain { get; }

		// extern NSString *const _Nonnull OIDResourceServerAuthorizationErrorDomain;
		[Field("OIDResourceServerAuthorizationErrorDomain", "__Internal")]
		NSString ResourceServerAuthorizationErrorDomain { get; }

		// extern NSString *const _Nonnull OIDHTTPErrorDomain;
		[Field("OIDHTTPErrorDomain", "__Internal")]
		NSString HttpErrorDomain { get; }

		// extern NSString *const _Nonnull OIDOAuthErrorResponseErrorKey;
		[Field("OIDOAuthErrorResponseErrorKey", "__Internal")]
		NSString OAuthErrorResponseErrorKey { get; }

		// extern NSString *const _Nonnull OIDOAuthErrorFieldError;
		[Field("OIDOAuthErrorFieldError", "__Internal")]
		NSString OAuthErrorFieldError { get; }

		// extern NSString *const _Nonnull OIDOAuthErrorFieldErrorDescription;
		[Field("OIDOAuthErrorFieldErrorDescription", "__Internal")]
		NSString OAuthErrorFieldErrorDescription { get; }

		// extern NSString *const _Nonnull OIDOAuthErrorFieldErrorURI;
		[Field("OIDOAuthErrorFieldErrorURI", "__Internal")]
		NSString OAuthErrorFieldErrorUri { get; }

		// extern NSString *const _Nonnull OIDOAuthExceptionInvalidAuthorizationFlow;
		[Field("OIDOAuthExceptionInvalidAuthorizationFlow", "__Internal")]
		NSString OAuthExceptionInvalidAuthorizationFlow { get; }
	}

	// @interface OIDErrorUtilities : NSObject
	[BaseType(typeof(NSObject), Name = "OIDErrorUtilities")]
	interface ErrorUtilities
	{
		// +(NSError * _Nonnull)errorWithCode:(OIDErrorCode)code underlyingError:(NSError * _Nullable)underlyingError description:(NSString * _Nullable)description;
		[Static]
		[Export("errorWithCode:underlyingError:description:")]
		NSError CreateError(ErrorCode code, [NullAllowed] NSError underlyingError, [NullAllowed] string description);

		// +(NSError * _Nonnull)OAuthErrorWithDomain:(NSString * _Nonnull)OAuthErrorDomain OAuthResponse:(NSDictionary * _Nonnull)errorResponse underlyingError:(NSError * _Nullable)underlyingError;
		[Static]
		[Export("OAuthErrorWithDomain:OAuthResponse:underlyingError:")]
		NSError CreateError(string OAuthErrorDomain, NSDictionary errorResponse, [NullAllowed] NSError underlyingError);

		// +(NSError * _Nonnull)resourceServerAuthorizationErrorWithCode:(NSInteger)code errorResponse:(NSDictionary * _Nullable)errorResponse underlyingError:(NSError * _Nullable)underlyingError;
		[Static]
		[Export("resourceServerAuthorizationErrorWithCode:errorResponse:underlyingError:")]
		NSError CreateResourceServerAuthorizationError(nint code, [NullAllowed] NSDictionary errorResponse, [NullAllowed] NSError underlyingError);

		// +(NSError * _Nonnull)HTTPErrorWithHTTPResponse:(NSHTTPURLResponse * _Nonnull)HTTPURLResponse data:(NSData * _Nullable)data;
		[Static]
		[Export("HTTPErrorWithHTTPResponse:data:")]
		NSError CreateHttpError(NSHttpUrlResponse HttpUrlResponse, [NullAllowed] NSData data);

		// +(void)raiseException:(NSString * _Nonnull)name;
		[Static]
		[Export("raiseException:")]
		void RaiseException(string name);

		// +(void)raiseException:(NSString * _Nonnull)name message:(NSString * _Nonnull)message;
		[Static]
		[Export("raiseException:message:")]
		void RaiseException(string name, string message);

		// +(OIDErrorCodeOAuth)OAuthErrorCodeFromString:(NSString * _Nonnull)errorCode;
		[Static]
		[Export("OAuthErrorCodeFromString:")]
		ErrorCodeOAuth OAuthErrorCodeFromString(string errorCode);

		// +(BOOL)isOAuthErrorDomain:(NSString * _Nonnull)errorDomain;
		[Static]
		[Export("isOAuthErrorDomain:")]
		bool IsOAuthErrorDomain(string errorDomain);
	}

	[Static]
	partial interface GrantType
	{
		// extern NSString *const OIDGrantTypeAuthorizationCode;
		[Field("OIDGrantTypeAuthorizationCode", "__Internal")]
		NSString AuthorizationCode { get; }

		// extern NSString *const OIDGrantTypeRefreshToken;
		[Field("OIDGrantTypeRefreshToken", "__Internal")]
		NSString RefreshToken { get; }

		// extern NSString *const OIDGrantTypePassword;
		[Field("OIDGrantTypePassword", "__Internal")]
		NSString Password { get; }

		// extern NSString *const OIDGrantTypeClientCredentials;
		[Field("OIDGrantTypeClientCredentials", "__Internal")]
		NSString ClientCredentials { get; }
	}

	// @interface OIDRegistrationRequest : NSObject <NSCopying, NSSecureCoding>
	[BaseType(typeof(NSObject), Name = "OIDRegistrationRequest")]
	[DisableDefaultCtor]
	interface RegistrationRequest : INSCopying, INSSecureCoding
	{
		// @property (readonly, nonatomic) OIDServiceConfiguration * _Nonnull configuration;
		[Export("configuration")]
		ServiceConfiguration Configuration { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull applicationType;
		[Export("applicationType")]
		string ApplicationType { get; }

		// @property (readonly, nonatomic) NSArray<NSURL *> * _Nonnull redirectURIs;
		[Export("redirectURIs")]
		NSUrl[] RedirectUris { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable responseTypes;
		[NullAllowed, Export("responseTypes")]
		string[] ResponseTypes { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable grantTypes;
		[NullAllowed, Export("grantTypes")]
		string[] GrantTypes { get; }

		// @property (readonly, nonatomic) NSString * _Nullable subjectType;
		[NullAllowed, Export("subjectType")]
		string SubjectType { get; }

		// @property (readonly, nonatomic) NSString * _Nullable tokenEndpointAuthenticationMethod;
		[NullAllowed, Export("tokenEndpointAuthenticationMethod")]
		string TokenEndpointAuthenticationMethod { get; }

		// @property (readonly, nonatomic) NSDictionary<NSString *,NSString *> * _Nullable additionalParameters;
		[NullAllowed, Export("additionalParameters")]
		NSDictionary<NSString, NSString> AdditionalParameters { get; }

		// -(instancetype _Nonnull)initWithConfiguration:(OIDServiceConfiguration * _Nonnull)configuration redirectURIs:(NSArray<NSURL *> * _Nonnull)redirectURIs responseTypes:(NSArray<NSString *> * _Nullable)responseTypes grantTypes:(NSArray<NSString *> * _Nullable)grantTypes subjectType:(NSString * _Nullable)subjectType tokenEndpointAuthMethod:(NSString * _Nullable)tokenEndpointAuthMethod additionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters __attribute__((objc_designated_initializer));
		[Export("initWithConfiguration:redirectURIs:responseTypes:grantTypes:subjectType:tokenEndpointAuthMethod:additionalParameters:")]
		[DesignatedInitializer]
		IntPtr Constructor(ServiceConfiguration configuration, NSUrl[] redirectUris, [NullAllowed] string[] responseTypes, [NullAllowed] string[] grantTypes, [NullAllowed] string subjectType, [NullAllowed] string tokenEndpointAuthMethod, [NullAllowed] NSDictionary<NSString, NSString> additionalParameters);

		// -(NSURLRequest * _Nonnull)URLRequest;
		[Export("URLRequest")]
		NSUrlRequest UrlRequest { get; }
	}

	[Static]
	partial interface RegistrationResponseParameters
	{
		// extern NSString *const _Nonnull OIDClientIDParam;
		[Field("OIDClientIDParam", "__Internal")]
		NSString ClientId { get; }

		// extern NSString *const _Nonnull OIDClientIDIssuedAtParam;
		[Field("OIDClientIDIssuedAtParam", "__Internal")]
		NSString ClientIdIssuedAt { get; }

		// extern NSString *const _Nonnull OIDClientSecretParam;
		[Field("OIDClientSecretParam", "__Internal")]
		NSString ClientSecret { get; }

		// extern NSString *const _Nonnull OIDClientSecretExpirestAtParam;
		[Field("OIDClientSecretExpirestAtParam", "__Internal")]
		NSString ClientSecretExpirestAt { get; }

		// extern NSString *const _Nonnull OIDRegistrationAccessTokenParam;
		[Field("OIDRegistrationAccessTokenParam", "__Internal")]
		NSString RegistrationAccessToken { get; }

		// extern NSString *const _Nonnull OIDRegistrationClientURIParam;
		[Field("OIDRegistrationClientURIParam", "__Internal")]
		NSString RegistrationClientUri { get; }
	}

	// @interface OIDRegistrationResponse : NSObject <NSCopying, NSSecureCoding>
	[BaseType(typeof(NSObject), Name = "OIDRegistrationResponse")]
	[DisableDefaultCtor]
	interface RegistrationResponse : INSCopying, INSSecureCoding
	{
		// @property (readonly, nonatomic) OIDRegistrationRequest * _Nonnull request;
		[Export("request")]
		RegistrationRequest Request { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull clientID;
		[Export("clientID")]
		string ClientId { get; }

		// @property (readonly, nonatomic) NSDate * _Nullable clientIDIssuedAt;
		[NullAllowed, Export("clientIDIssuedAt")]
		NSDate ClientIdIssuedAt { get; }

		// @property (readonly, nonatomic) NSString * _Nullable clientSecret;
		[NullAllowed, Export("clientSecret")]
		string ClientSecret { get; }

		// @property (readonly, nonatomic) NSDate * _Nullable clientSecretExpiresAt;
		[NullAllowed, Export("clientSecretExpiresAt")]
		NSDate ClientSecretExpiresAt { get; }

		// @property (readonly, nonatomic) NSString * _Nullable registrationAccessToken;
		[NullAllowed, Export("registrationAccessToken")]
		string RegistrationAccessToken { get; }

		// @property (readonly, nonatomic) NSURL * _Nullable registrationClientURI;
		[NullAllowed, Export("registrationClientURI")]
		NSUrl RegistrationClientUri { get; }

		// @property (readonly, nonatomic) NSString * _Nullable tokenEndpointAuthenticationMethod;
		[NullAllowed, Export("tokenEndpointAuthenticationMethod")]
		string TokenEndpointAuthenticationMethod { get; }

		// @property (readonly, nonatomic) NSDictionary<NSString *,NSObject<NSCopying> *> * _Nullable additionalParameters;
		[NullAllowed, Export("additionalParameters")]
		NSDictionary<NSString, NSCopying> AdditionalParameters { get; }

		// -(instancetype _Nonnull)initWithRequest:(OIDRegistrationRequest * _Nonnull)request parameters:(NSDictionary<NSString *,NSObject<NSCopying> *> * _Nonnull)parameters __attribute__((objc_designated_initializer));
		[Export("initWithRequest:parameters:")]
		[DesignatedInitializer]
		IntPtr Constructor(RegistrationRequest request, NSDictionary<NSString, NSCopying> parameters);
	}

	// typedef void (^OIDServiceConfigurationCreated)(OIDServiceConfiguration * _Nullable, NSError * _Nullable);
	delegate void ServiceConfigurationCreated([NullAllowed] ServiceConfiguration serviceConfiguration, [NullAllowed] NSError error);

	// @interface OIDServiceConfiguration : NSObject <NSCopying, NSSecureCoding>
	[BaseType(typeof(NSObject), Name = "OIDServiceConfiguration")]
	[DisableDefaultCtor]
	interface ServiceConfiguration : INSCopying, INSSecureCoding
	{
		// @property (readonly, nonatomic) NSURL * _Nonnull authorizationEndpoint;
		[Export("authorizationEndpoint")]
		NSUrl AuthorizationEndpoint { get; }

		// @property (readonly, nonatomic) NSURL * _Nonnull tokenEndpoint;
		[Export("tokenEndpoint")]
		NSUrl TokenEndpoint { get; }

		// @property (readonly, nonatomic) NSURL * _Nullable registrationEndpoint;
		[NullAllowed, Export("registrationEndpoint")]
		NSUrl RegistrationEndpoint { get; }

		// @property (readonly, nonatomic) OIDServiceDiscovery * _Nullable discoveryDocument;
		[NullAllowed, Export("discoveryDocument")]
		ServiceDiscovery DiscoveryDocument { get; }

		// -(instancetype _Nonnull)initWithAuthorizationEndpoint:(NSURL * _Nonnull)authorizationEndpoint tokenEndpoint:(NSURL * _Nonnull)tokenEndpoint;
		[Export("initWithAuthorizationEndpoint:tokenEndpoint:")]
		IntPtr Constructor(NSUrl authorizationEndpoint, NSUrl tokenEndpoint);

		// -(instancetype _Nonnull)initWithAuthorizationEndpoint:(NSURL * _Nonnull)authorizationEndpoint tokenEndpoint:(NSURL * _Nonnull)tokenEndpoint registrationEndpoint:(NSURL * _Nullable)registrationEndpoint;
		[Export("initWithAuthorizationEndpoint:tokenEndpoint:registrationEndpoint:")]
		IntPtr Constructor(NSUrl authorizationEndpoint, NSUrl tokenEndpoint, [NullAllowed] NSUrl registrationEndpoint);

		// -(instancetype _Nonnull)initWithDiscoveryDocument:(OIDServiceDiscovery * _Nonnull)discoveryDocument;
		[Export("initWithDiscoveryDocument:")]
		IntPtr Constructor(ServiceDiscovery discoveryDocument);
	}

	// @interface OIDServiceDiscovery : NSObject <NSCopying, NSSecureCoding>
	[BaseType(typeof(NSObject), Name = "OIDServiceDiscovery")]
	[DisableDefaultCtor]
	interface ServiceDiscovery : INSCopying, INSSecureCoding
	{
		// @property (readonly, nonatomic) NSDictionary<NSString *,id> * _Nonnull discoveryDictionary;
		[Export("discoveryDictionary")]
		NSDictionary<NSString, NSObject> DiscoveryDictionary { get; }

		// @property (readonly, nonatomic) NSURL * _Nonnull issuer;
		[Export("issuer")]
		NSUrl Issuer { get; }

		// @property (readonly, nonatomic) NSURL * _Nonnull authorizationEndpoint;
		[Export("authorizationEndpoint")]
		NSUrl AuthorizationEndpoint { get; }

		// @property (readonly, nonatomic) NSURL * _Nonnull tokenEndpoint;
		[Export("tokenEndpoint")]
		NSUrl TokenEndpoint { get; }

		// @property (readonly, nonatomic) NSURL * _Nullable userinfoEndpoint;
		[NullAllowed, Export("userinfoEndpoint")]
		NSUrl UserinfoEndpoint { get; }

		// @property (readonly, nonatomic) NSURL * _Nonnull jwksURL;
		[Export("jwksURL")]
		NSUrl JwksUrl { get; }

		// @property (readonly, nonatomic) NSURL * _Nullable registrationEndpoint;
		[NullAllowed, Export("registrationEndpoint")]
		NSUrl RegistrationEndpoint { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable scopesSupported;
		[NullAllowed, Export("scopesSupported")]
		string[] ScopesSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nonnull responseTypesSupported;
		[Export("responseTypesSupported")]
		string[] ResponseTypesSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable responseModesSupported;
		[NullAllowed, Export("responseModesSupported")]
		string[] ResponseModesSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable grantTypesSupported;
		[NullAllowed, Export("grantTypesSupported")]
		string[] GrantTypesSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable acrValuesSupported;
		[NullAllowed, Export("acrValuesSupported")]
		string[] AcrValuesSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nonnull subjectTypesSupported;
		[Export("subjectTypesSupported")]
		string[] SubjectTypesSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nonnull IDTokenSigningAlgorithmValuesSupported;
		[Export("IDTokenSigningAlgorithmValuesSupported")]
		string[] IdTokenSigningAlgorithmValuesSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable IDTokenEncryptionAlgorithmValuesSupported;
		[NullAllowed, Export("IDTokenEncryptionAlgorithmValuesSupported")]
		string[] IdTokenEncryptionAlgorithmValuesSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable IDTokenEncryptionEncodingValuesSupported;
		[NullAllowed, Export("IDTokenEncryptionEncodingValuesSupported")]
		string[] IdTokenEncryptionEncodingValuesSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable userinfoSigningAlgorithmValuesSupported;
		[NullAllowed, Export("userinfoSigningAlgorithmValuesSupported")]
		string[] UserinfoSigningAlgorithmValuesSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable userinfoEncryptionAlgorithmValuesSupported;
		[NullAllowed, Export("userinfoEncryptionAlgorithmValuesSupported")]
		string[] UserinfoEncryptionAlgorithmValuesSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable userinfoEncryptionEncodingValuesSupported;
		[NullAllowed, Export("userinfoEncryptionEncodingValuesSupported")]
		string[] UserinfoEncryptionEncodingValuesSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable requestObjectSigningAlgorithmValuesSupported;
		[NullAllowed, Export("requestObjectSigningAlgorithmValuesSupported")]
		string[] RequestObjectSigningAlgorithmValuesSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable requestObjectEncryptionAlgorithmValuesSupported;
		[NullAllowed, Export("requestObjectEncryptionAlgorithmValuesSupported")]
		string[] RequestObjectEncryptionAlgorithmValuesSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable requestObjectEncryptionEncodingValuesSupported;
		[NullAllowed, Export("requestObjectEncryptionEncodingValuesSupported")]
		string[] RequestObjectEncryptionEncodingValuesSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable tokenEndpointAuthMethodsSupported;
		[NullAllowed, Export("tokenEndpointAuthMethodsSupported")]
		string[] TokenEndpointAuthMethodsSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable tokenEndpointAuthSigningAlgorithmValuesSupported;
		[NullAllowed, Export("tokenEndpointAuthSigningAlgorithmValuesSupported")]
		string[] TokenEndpointAuthSigningAlgorithmValuesSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable displayValuesSupported;
		[NullAllowed, Export("displayValuesSupported")]
		string[] DisplayValuesSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable claimTypesSupported;
		[NullAllowed, Export("claimTypesSupported")]
		string[] ClaimTypesSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable claimsSupported;
		[NullAllowed, Export("claimsSupported")]
		string[] ClaimsSupported { get; }

		// @property (readonly, nonatomic) NSURL * _Nullable serviceDocumentation;
		[NullAllowed, Export("serviceDocumentation")]
		NSUrl ServiceDocumentation { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable claimsLocalesSupported;
		[NullAllowed, Export("claimsLocalesSupported")]
		string[] ClaimsLocalesSupported { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nullable UILocalesSupported;
		[NullAllowed, Export("UILocalesSupported")]
		string[] UILocalesSupported { get; }

		// @property (readonly, nonatomic) BOOL claimsParameterSupported;
		[Export("claimsParameterSupported")]
		bool ClaimsParameterSupported { get; }

		// @property (readonly, nonatomic) BOOL requestParameterSupported;
		[Export("requestParameterSupported")]
		bool RequestParameterSupported { get; }

		// @property (readonly, nonatomic) BOOL requestURIParameterSupported;
		[Export("requestURIParameterSupported")]
		bool RequestUriParameterSupported { get; }

		// @property (readonly, nonatomic) BOOL requireRequestURIRegistration;
		[Export("requireRequestURIRegistration")]
		bool RequireRequestUriRegistration { get; }

		// @property (readonly, nonatomic) NSURL * _Nullable OPPolicyURI;
		[NullAllowed, Export("OPPolicyURI")]
		NSUrl OPPolicyUri { get; }

		// @property (readonly, nonatomic) NSURL * _Nullable OPTosURI;
		[NullAllowed, Export("OPTosURI")]
		NSUrl OPTosUri { get; }

		// -(instancetype _Nullable)initWithJSON:(NSString * _Nonnull)serviceDiscoveryJSON error:(NSError * _Nullable * _Nullable)error;
		[Export("initWithJSON:error:")]
		IntPtr Constructor(string serviceDiscoveryJson, [NullAllowed] out NSError error);

		// -(instancetype _Nullable)initWithJSONData:(NSData * _Nonnull)serviceDiscoveryJSONData error:(NSError * _Nullable * _Nullable)error;
		[Export("initWithJSONData:error:")]
		IntPtr Constructor(NSData serviceDiscoveryJsonData, [NullAllowed] out NSError error);

		// -(instancetype _Nullable)initWithDictionary:(NSDictionary * _Nonnull)serviceDiscoveryDictionary error:(NSError * _Nullable * _Nullable)error __attribute__((objc_designated_initializer));
		[Export("initWithDictionary:error:")]
		[DesignatedInitializer]
		IntPtr Constructor(NSDictionary serviceDiscoveryDictionary, [NullAllowed] out NSError error);
	}

	// @interface OIDTokenRequest : NSObject <NSCopying, NSSecureCoding>
	[BaseType(typeof(NSObject), Name = "OIDTokenRequest")]
	[DisableDefaultCtor]
	interface TokenRequest : INSCopying, INSSecureCoding
	{
		// @property (readonly, nonatomic) OIDServiceConfiguration * _Nonnull configuration;
		[Export("configuration")]
		ServiceConfiguration Configuration { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull grantType;
		[Export("grantType")]
		string GrantType { get; }

		// @property (readonly, nonatomic) NSString * _Nullable authorizationCode;
		[NullAllowed, Export("authorizationCode")]
		string AuthorizationCode { get; }

		// @property (readonly, nonatomic) NSURL * _Nonnull redirectURL;
		[Export("redirectURL")]
		NSUrl RedirectUrl { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull clientID;
		[Export("clientID")]
		string ClientId { get; }

		// @property (readonly, nonatomic) NSString * _Nullable clientSecret;
		[NullAllowed, Export("clientSecret")]
		string ClientSecret { get; }

		// @property (readonly, nonatomic) NSString * _Nullable scope;
		[NullAllowed, Export("scope")]
		string Scope { get; }

		// @property (readonly, nonatomic) NSString * _Nullable refreshToken;
		[NullAllowed, Export("refreshToken")]
		string RefreshToken { get; }

		// @property (readonly, nonatomic) NSString * _Nullable codeVerifier;
		[NullAllowed, Export("codeVerifier")]
		string CodeVerifier { get; }

		// @property (readonly, nonatomic) NSDictionary<NSString *,NSString *> * _Nullable additionalParameters;
		[NullAllowed, Export("additionalParameters")]
		NSDictionary<NSString, NSString> AdditionalParameters { get; }

		// -(instancetype _Nonnull)initWithConfiguration:(OIDServiceConfiguration * _Nonnull)configuration grantType:(NSString * _Nonnull)grantType authorizationCode:(NSString * _Nullable)code redirectURL:(NSURL * _Nonnull)redirectURL clientID:(NSString * _Nonnull)clientID clientSecret:(NSString * _Nullable)clientSecret scopes:(NSArray<NSString *> * _Nullable)scopes refreshToken:(NSString * _Nullable)refreshToken codeVerifier:(NSString * _Nullable)codeVerifier additionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters;
		[Export("initWithConfiguration:grantType:authorizationCode:redirectURL:clientID:clientSecret:scopes:refreshToken:codeVerifier:additionalParameters:")]
		IntPtr Constructor(ServiceConfiguration configuration, string grantType, [NullAllowed] string code, NSUrl redirectURL, string clientID, [NullAllowed] string clientSecret, [NullAllowed] string[] scopes, [NullAllowed] string refreshToken, [NullAllowed] string codeVerifier, [NullAllowed] NSDictionary<NSString, NSString> additionalParameters);

		// -(instancetype _Nonnull)initWithConfiguration:(OIDServiceConfiguration * _Nonnull)configuration grantType:(NSString * _Nonnull)grantType authorizationCode:(NSString * _Nullable)code redirectURL:(NSURL * _Nonnull)redirectURL clientID:(NSString * _Nonnull)clientID clientSecret:(NSString * _Nullable)clientSecret scope:(NSString * _Nullable)scope refreshToken:(NSString * _Nullable)refreshToken codeVerifier:(NSString * _Nullable)codeVerifier additionalParameters:(NSDictionary<NSString *,NSString *> * _Nullable)additionalParameters __attribute__((objc_designated_initializer));
		[Export("initWithConfiguration:grantType:authorizationCode:redirectURL:clientID:clientSecret:scope:refreshToken:codeVerifier:additionalParameters:")]
		[DesignatedInitializer]
		IntPtr Constructor(ServiceConfiguration configuration, string grantType, [NullAllowed] string code, NSUrl redirectURL, string clientID, [NullAllowed] string clientSecret, [NullAllowed] string scope, [NullAllowed] string refreshToken, [NullAllowed] string codeVerifier, [NullAllowed] NSDictionary<NSString, NSString> additionalParameters);

		// -(NSURLRequest * _Nonnull)URLRequest;
		[Export("URLRequest")]
		NSUrlRequest CreateUrlRequest();
	}

	// @interface OIDTokenResponse : NSObject <NSCopying, NSSecureCoding>
	[BaseType(typeof(NSObject), Name = "OIDTokenResponse")]
	[DisableDefaultCtor]
	interface TokenResponse : INSCopying, INSSecureCoding
	{
		// @property (readonly, nonatomic) OIDTokenRequest * _Nonnull request;
		[Export("request")]
		TokenRequest Request { get; }

		// @property (readonly, nonatomic) NSString * _Nullable accessToken;
		[NullAllowed, Export("accessToken")]
		string AccessToken { get; }

		// @property (readonly, nonatomic) NSDate * _Nullable accessTokenExpirationDate;
		[NullAllowed, Export("accessTokenExpirationDate")]
		NSDate AccessTokenExpirationDate { get; }

		// @property (readonly, nonatomic) NSString * _Nullable tokenType;
		[NullAllowed, Export("tokenType")]
		string TokenType { get; }

		// @property (readonly, nonatomic) NSString * _Nullable idToken;
		[NullAllowed, Export("idToken")]
		string IdToken { get; }

		// @property (readonly, nonatomic) NSString * _Nullable refreshToken;
		[NullAllowed, Export("refreshToken")]
		string RefreshToken { get; }

		// @property (readonly, nonatomic) NSString * _Nullable scope;
		[NullAllowed, Export("scope")]
		string Scope { get; }

		// @property (readonly, nonatomic) NSDictionary<NSString *,NSObject<NSCopying> *> * _Nullable additionalParameters;
		[NullAllowed, Export("additionalParameters")]
		NSDictionary<NSString, INSCopying> AdditionalParameters { get; }

		// -(instancetype _Nonnull)initWithRequest:(OIDTokenRequest * _Nonnull)request parameters:(NSDictionary<NSString *,NSObject<NSCopying> *> * _Nonnull)parameters __attribute__((objc_designated_initializer));
		[Export("initWithRequest:parameters:")]
		[DesignatedInitializer]
		IntPtr Constructor(TokenRequest request, NSDictionary<NSString, INSCopying> parameters);
	}

	interface IAuthorizationUICoordinator { }

	// @protocol OIDAuthorizationUICoordinator <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "OIDAuthorizationUICoordinator")]
	interface AuthorizationUICoordinator
	{
		// @required -(BOOL)presentAuthorizationRequest:(OIDAuthorizationRequest * _Nonnull)request session:(id<OIDAuthorizationFlowSession> _Nonnull)session;
		[Abstract]
		[Export("presentAuthorizationRequest:session:")]
		bool PresentAuthorizationRequest(AuthorizationRequest request, AuthorizationFlowSession session);

		// @required -(void)dismissAuthorizationAnimated:(BOOL)animated completion:(void (^ _Nonnull)(void))completion;
		[Abstract]
		[Export("dismissAuthorizationAnimated:completion:")]
		void DismissAuthorization(bool animated, Action completion);
	}

	// @protocol OIDSafariViewControllerFactory
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "OIDSafariViewControllerFactory")]
	interface SafariViewControllerFactory
	{
		// @required -(SFSafariViewController * _Nonnull)safariViewControllerWithURL:(NSURL * _Nonnull)URL;
		[Abstract]
		[Export("safariViewControllerWithURL:")]
		SFSafariViewController CreateSafariViewController(NSUrl url);
	}

	// @interface OIDAuthorizationUICoordinatorIOS : NSObject <OIDAuthorizationUICoordinator>
	[BaseType(typeof(NSObject), Name = "OIDAuthorizationUICoordinatorIOS")]
	[DisableDefaultCtor]
	interface AuthorizationUICoordinatorIOS : AuthorizationUICoordinator
	{
		// +(void)setSafariViewControllerFactory:(id<OIDSafariViewControllerFactory> _Nonnull)factory;
		[Static]
		[Export("setSafariViewControllerFactory:")]
		void SetSafariViewControllerFactory(SafariViewControllerFactory factory);

		// -(instancetype _Nullable)initWithPresentingViewController:(UIViewController * _Nonnull)presentingViewController __attribute__((objc_designated_initializer));
		[Export("initWithPresentingViewController:")]
		[DesignatedInitializer]
		IntPtr Constructor(UIViewController presentingViewController);
	}

	[Static]
	partial interface ClientMetadataParameters
	{
		// extern NSString *const _Nonnull OIDTokenEndpointAuthenticationMethodParam;
		[Field("OIDTokenEndpointAuthenticationMethodParam", "__Internal")]
		NSString TokenEndpointAuthenticationMethod { get; }

		// extern NSString *const _Nonnull OIDApplicationTypeParam;
		[Field("OIDApplicationTypeParam", "__Internal")]
		NSString ApplicationType { get; }

		// extern NSString *const _Nonnull OIDRedirectURIsParam;
		[Field("OIDRedirectURIsParam", "__Internal")]
		NSString RedirectUris { get; }

		// extern NSString *const _Nonnull OIDResponseTypesParam;
		[Field("OIDResponseTypesParam", "__Internal")]
		NSString ResponseTypes { get; }

		// extern NSString *const _Nonnull OIDGrantTypesParam;
		[Field("OIDGrantTypesParam", "__Internal")]
		NSString GrantTypes { get; }

		// extern NSString *const _Nonnull OIDSubjectTypeParam;
		[Field("OIDSubjectTypeParam", "__Internal")]
		NSString SubjectType { get; }

		// extern NSString *const _Nonnull OIDApplicationTypeNative;
		[Field("OIDApplicationTypeNative", "__Internal")]
		NSString ApplicationTypeNative { get; }
	}

	// typedef id _Nullable (^OIDFieldMappingConversionFunction)(NSObject * _Nullable);
	delegate NSObject FieldMappingConversionFunction([NullAllowed] NSObject value);

	// @interface OIDFieldMapping : NSObject
	[BaseType(typeof(NSObject), Name = "OIDFieldMapping")]
	[DisableDefaultCtor]
	interface FieldMapping : INativeObject
	{
		// @property (readonly, nonatomic) NSString * _Nonnull name;
		[Export("name")]
		string Name { get; }

		// @property (readonly, nonatomic) Class _Nonnull expectedType;
		[Export("expectedType")]
		Class ExpectedType { get; }

		// @property (readonly, nonatomic) OIDFieldMappingConversionFunction _Nullable conversion;
		[NullAllowed, Export("conversion")]
		FieldMappingConversionFunction Conversion { get; }

		// -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)name type:(Class _Nonnull)type conversion:(OIDFieldMappingConversionFunction _Nullable)conversion __attribute__((objc_designated_initializer));
		[Export("initWithName:type:conversion:")]
		[DesignatedInitializer]
		IntPtr Constructor(string name, Class type, [NullAllowed] FieldMappingConversionFunction conversion);

		// -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)name type:(Class _Nonnull)type;
		[Export("initWithName:type:")]
		IntPtr Constructor(string name, Class type);

		// +(NSDictionary<NSString *,NSObject<NSCopying> *> * _Nonnull)remainingParametersWithMap:(NSDictionary<NSString *,OIDFieldMapping *> * _Nonnull)map parameters:(NSDictionary<NSString *,NSObject<NSCopying> *> * _Nonnull)parameters instance:(id _Nonnull)instance;
		[Static]
		[Export("remainingParametersWithMap:parameters:instance:")]
		NSDictionary<NSString, NSCopying> GetRemainingParameters(NSDictionary<NSString, FieldMapping> map, NSDictionary<NSString, NSCopying> parameters, NSObject instance);

		// +(void)encodeWithCoder:(NSCoder * _Nonnull)aCoder map:(NSDictionary<NSString *,OIDFieldMapping *> * _Nonnull)map instance:(id _Nonnull)instance;
		[Static]
		[Export("encodeWithCoder:map:instance:")]
		void Encode(NSCoder aCoder, NSDictionary<NSString, FieldMapping> map, NSObject instance);

		// +(void)decodeWithCoder:(NSCoder * _Nonnull)aCoder map:(NSDictionary<NSString *,OIDFieldMapping *> * _Nonnull)map instance:(id _Nonnull)instance;
		[Static]
		[Export("decodeWithCoder:map:instance:")]
		void Decode(NSCoder aCoder, NSDictionary<NSString, FieldMapping> map, NSObject instance);

		// +(NSSet * _Nonnull)JSONTypes;
		[Static]
		[Export("JSONTypes")]
		NSSet JsonTypes { get; }

		// +(OIDFieldMappingConversionFunction _Nonnull)URLConversion;
		[Static]
		[Export("URLConversion")]
		FieldMappingConversionFunction UrlConversion { get; }

		// +(OIDFieldMappingConversionFunction _Nonnull)dateSinceNowConversion;
		[Static]
		[Export("dateSinceNowConversion")]
		FieldMappingConversionFunction DateSinceNowConversion { get; }

		// +(OIDFieldMappingConversionFunction _Nonnull)dateEpochConversion;
		[Static]
		[Export("dateEpochConversion")]
		FieldMappingConversionFunction DateEpochConversion { get; }
	}

	// @interface OIDScopeUtilities : NSObject
	[BaseType(typeof(NSObject), Name = "OIDScopeUtilities")]
	[DisableDefaultCtor]
	interface ScopeUtilities
	{
		// +(NSString * _Nonnull)scopesWithArray:(NSArray<NSString *> * _Nonnull)scopes;
		[Static]
		[Export("scopesWithArray:")]
		string GetScopes(string[] scopes);

		// +(NSArray<NSString *> * _Nonnull)scopesArrayWithString:(NSString * _Nonnull)scopes;
		[Static]
		[Export("scopesArrayWithString:")]
		string[] GetScopes(string scopes);
	}

	// @interface OIDTokenUtilities : NSObject
	[BaseType(typeof(NSObject), Name = "OIDTokenUtilities")]
	[DisableDefaultCtor]
	interface TokenUtilities
	{
		// +(NSString * _Nonnull)encodeBase64urlNoPadding:(NSData * _Nonnull)data;
		[Static]
		[Export("encodeBase64urlNoPadding:")]
		string EncodeBase64UrlNoPadding(NSData data);

		// +(NSString * _Nullable)randomURLSafeStringWithSize:(NSUInteger)size;
		[Static]
		[Export("randomURLSafeStringWithSize:")]
		[return: NullAllowed]
		string GetRandomUrlSafeString(nuint size);

		// +(NSData * _Nonnull)sha265:(NSString * _Nonnull)inputString;
		[Static]
		[Export("sha265:")]
		NSData GetSha265(string inputString);
	}

	// @interface OIDURLSessionProvider : NSObject
	[BaseType(typeof(NSObject), Name = "OIDURLSessionProvider")]
	interface UrlSessionProvider
	{
		[Static]
		[Export("session")]
		NSUrlSession Session { get; set; }
	}

	// @interface OIDURLQueryComponent : NSObject
	[BaseType(typeof(NSObject), Name = "OIDURLQueryComponent")]
	interface UrlQueryComponent
	{
		//// extern BOOL gOIDURLQueryComponentForceIOS7Handling;
		//[Static]
		//[Field("gOIDURLQueryComponentForceIOS7Handling", "__Internal")]
		//bool ForceiOS7Handling { get; }

		// @property (readonly, nonatomic) NSArray<NSString *> * _Nonnull parameters;
		[Export("parameters")]
		string[] Parameters { get; }

		// @property (readonly, nonatomic) NSDictionary<NSString *,NSObject<NSCopying> *> * _Nonnull dictionaryValue;
		[Export("dictionaryValue")]
		NSDictionary<NSString, NSCopying> DictionaryValue { get; }

		// -(instancetype _Nullable)initWithURL:(NSURL * _Nonnull)URL;
		[Export("initWithURL:")]
		IntPtr Constructor(NSUrl url);

		// -(NSArray<NSString *> * _Nonnull)valuesForParameter:(NSString * _Nonnull)parameter;
		[Export("valuesForParameter:")]
		string[] GetValues(string parameter);

		// -(void)addParameter:(NSString * _Nonnull)parameter value:(NSString * _Nonnull)value;
		[Export("addParameter:value:")]
		void AddParameter(string parameter, string value);

		// -(void)addParameters:(NSDictionary<NSString *,NSString *> * _Nonnull)parameters;
		[Export("addParameters:")]
		void AddParameters(NSDictionary<NSString, NSString> parameters);

		// -(NSURL * _Nonnull)URLByReplacingQueryInURL:(NSURL * _Nonnull)URL;
		[Export("URLByReplacingQueryInURL:")]
		NSUrl UrlByReplacingQueryInUrl(NSUrl url);

		// -(NSString * _Nonnull)URLEncodedParameters;
		[Export("URLEncodedParameters")]
		string GetUrlEncodedParameters();
	}
}
