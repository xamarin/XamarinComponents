using System;

namespace Dropbox.CoreApi.OSX
{
	public enum ErrorCode : uint
	{
		None = 0,
		GenericError = 1000,
		FileNotFound,
		InsufficientDiskSpace,
		IllegalFileType,
		InvalidResponse
	}

	public enum JsonError : uint
	{
		Unsupported = 1,
		Parsenum,
		Parse,
		Fragment,
		Ctrl,
		Unicode,
		Depth,
		Escape,
		Trailcomma,
		Trailgarbage,
		Eof,
		Input
	}


}

namespace Dropbox.CoreApi.OSX.MPOAuth
{
	public enum SignatureScheme : uint
	{
		PlainText,
		Hmacsha1,
		Rsasha1
	}

	public enum AuthenticationState : uint
	{
		Unauthenticated = 0,
		Authenticating = 1,
		Authenticated = 2
	}
}
