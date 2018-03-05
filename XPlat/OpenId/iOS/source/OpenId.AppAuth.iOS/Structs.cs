using System;
using ObjCRuntime;

namespace OpenId.AppAuth
{
	[Native]
	public enum ErrorCode : long
	{
		InvalidDiscoveryDocument = -2,
		UserCanceledAuthorizationFlow = -3,
		ProgramCanceledAuthorizationFlow = -4,
		NetworkError = -5,
		ServerError = -6,
		JSONDeserializationError = -7,
		TokenResponseConstructionError = -8,
		SafariOpenError = -9,
		BrowserOpenError = -10,
		TokenRefreshError = -11,
		RegistrationResponseConstructionError = -12,
		JSONSerializationError = -13,
	}

	[Native]
	public enum ErrorCodeOAuth : long
	{
		InvalidRequest = -2,
		UnauthorizedClient = -3,
		AccessDenied = -4,
		UnsupportedResponseType = -5,
		InvalidScope = -6,
		ServerError = -7,
		TemporarilyUnavailable = -8,
		InvalidClient = -9,
		InvalidGrant = -10,
		UnsupportedGrantType = -11,
		InvalidRedirectUri = -12,
		InvalidClientMetadata = -13,
		ClientError = -0xEFFF,
		Other = -0xF000
	}

	[Native]
	public enum ErrorCodeOAuthAuthorization : long
	{
		InvalidRequest = ErrorCodeOAuth.InvalidRequest,
		UnauthorizedClient = ErrorCodeOAuth.UnauthorizedClient,
		AccessDenied = ErrorCodeOAuth.AccessDenied,
		UnsupportedResponseType = ErrorCodeOAuth.UnsupportedResponseType,
		AuthorizationInvalidScope = ErrorCodeOAuth.InvalidScope,
		ServerError = ErrorCodeOAuth.ServerError,
		TemporarilyUnavailable = ErrorCodeOAuth.TemporarilyUnavailable,
		ClientError = ErrorCodeOAuth.ClientError,
		Other = ErrorCodeOAuth.Other
	}

	[Native]
	public enum ErrorCodeOAuthToken : long
	{
		InvalidRequest = ErrorCodeOAuth.InvalidRequest,
		InvalidClient = ErrorCodeOAuth.InvalidClient,
		InvalidGrant = ErrorCodeOAuth.InvalidGrant,
		UnauthorizedClient = ErrorCodeOAuth.UnauthorizedClient,
		UnsupportedGrantType = ErrorCodeOAuth.UnsupportedGrantType,
		InvalidScope = ErrorCodeOAuth.InvalidScope,
		ClientError = ErrorCodeOAuth.ClientError,
		Other = ErrorCodeOAuth.Other
	}

	[Native]
	public enum ErrorCodeOAuthRegistration : long
	{
		InvalidRequest = ErrorCodeOAuth.InvalidRequest,
		InvalidRedirectURI = ErrorCodeOAuth.InvalidRedirectUri,
		InvalidClientMetadata = ErrorCodeOAuth.InvalidClientMetadata,
		ClientError = ErrorCodeOAuth.ClientError,
		Other = ErrorCodeOAuth.Other
	}
}
