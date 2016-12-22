using UIKit;

namespace OpenId.AppAuth
{
	partial class AuthState
	{
		public static IAuthorizationFlowSession PresentAuthorizationRequest(AuthorizationRequest authorizationRequest, UIViewController presentingViewController, AuthStateAuthorizationCallback callback)
		{
			var coordinator = new AuthorizationUICoordinatorIOS(presentingViewController);
			return AuthState.PresentAuthorizationRequest(authorizationRequest, coordinator, callback);
		}
	}

	partial class AuthorizationService
	{
		public static IAuthorizationFlowSession PresentAuthorizationRequest(AuthorizationRequest request, UIViewController presentingViewController, AuthorizationCallback callback)
		{
			var coordinator = new AuthorizationUICoordinatorIOS(presentingViewController);
			return AuthorizationService.PresentAuthorizationRequest(request, coordinator, callback);
		}
	}
}
