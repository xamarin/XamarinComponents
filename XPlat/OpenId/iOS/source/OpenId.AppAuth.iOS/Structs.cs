using System;
using ObjCRuntime;

namespace OpenId.AppAuth
{
	[Native]
	public enum OIDErrorCode : long
	{
		InvalidDiscoveryDocument = -2,
		UserCanceledAuthorizationFlow = -3,
		ProgramCanceledAuthorizationFlow = -4,
		NetworkError = -5,
		ServerError = -6,
		JSONDeserializationError = -7,
		TokenResponseConstructionError = -8,
		SafariOpenError = -9,
		BrowserOpenError = -10
	}

	[Native]
	public enum OIDErrorCodeOAuth : long
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
		ClientError = -0xEFFF,
		Other = -0xF000
	}

	[Native]
	public enum OIDErrorCodeOAuthAuthorization : long
	{
		InvalidRequest = OIDErrorCodeOAuth.InvalidRequest,
		UnauthorizedClient = OIDErrorCodeOAuth.UnauthorizedClient,
		AccessDenied = OIDErrorCodeOAuth.AccessDenied,
		UnsupportedResponseType = OIDErrorCodeOAuth.UnsupportedResponseType,
		AuthorizationInvalidScope = OIDErrorCodeOAuth.InvalidScope,
		ServerError = OIDErrorCodeOAuth.ServerError,
		TemporarilyUnavailable = OIDErrorCodeOAuth.TemporarilyUnavailable,
		ClientError = OIDErrorCodeOAuth.ClientError,
		Other = OIDErrorCodeOAuth.Other
	}

	[Native]
	public enum OIDErrorCodeOAuthToken : long
	{
		InvalidRequest = OIDErrorCodeOAuth.InvalidRequest,
		InvalidClient = OIDErrorCodeOAuth.InvalidClient,
		InvalidGrant = OIDErrorCodeOAuth.InvalidGrant,
		UnauthorizedClient = OIDErrorCodeOAuth.UnauthorizedClient,
		UnsupportedGrantType = OIDErrorCodeOAuth.UnsupportedGrantType,
		InvalidScope = OIDErrorCodeOAuth.InvalidScope,
		ClientError = OIDErrorCodeOAuth.ClientError,
		Other = OIDErrorCodeOAuth.Other
	}
}
