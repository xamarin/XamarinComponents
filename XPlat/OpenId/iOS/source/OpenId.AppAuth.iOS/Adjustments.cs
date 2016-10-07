using UIKit;

namespace OpenId.AppAuth
{
	partial class OIDAuthState
	{
		public static IOIDAuthorizationFlowSession PresentAuthorizationRequest(OIDAuthorizationRequest authorizationRequest, UIViewController presentingViewController, OIDAuthStateAuthorizationCallback callback)
		{
			var coordinator = new OIDAuthorizationUICoordinatorIOS(presentingViewController);
			return OIDAuthState.PresentAuthorizationRequest(authorizationRequest, coordinator, callback);
		}
	}

	partial class OIDAuthorizationService
	{
		public static IOIDAuthorizationFlowSession PresentAuthorizationRequest(OIDAuthorizationRequest request, UIViewController presentingViewController, OIDAuthorizationCallback callback)
		{
			var coordinator = new OIDAuthorizationUICoordinatorIOS(presentingViewController);
			return OIDAuthorizationService.PresentAuthorizationRequest(request, coordinator, callback);
		}
	}
}
