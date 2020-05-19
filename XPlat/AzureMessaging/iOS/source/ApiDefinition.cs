using System;
using ObjCRuntime;
using Foundation;
using UIKit;

namespace WindowsAzure.Messaging
{
	public delegate void ErrorCallback(NSError error);

	[BaseType (typeof (NSObject))]
	public partial interface SBConnectionString 
	{
		[Static, Export ("stringWithEndpoint:issuer:issuerSecret:")]
		string CreateByIssuer (NSUrl endpoint, string issuer, string secret);

		[Static, Export ("stringWithEndpoint:fullAccessSecret:")]
		string CreateFullAccess (NSUrl endpoint, string fullAccessSecret);

		[Static, Export ("stringWithEndpoint:listenAccessSecret:")]
		string CreateListenAccess (NSUrl endpoint, string listenAccessSecret);

		[Static, Export ("stringWithEndpoint:sharedAccessKeyName:accessSecret:")]
		string CreateByKeyName (NSUrl endpoint, string keyName, string secret);
	}

	[BaseType (typeof (NSObject))]
	public partial interface SBLocalStorage 
	{
		[Export ("deviceToken")]
		string DeviceToken { get;set; }

		[Export ("isRefreshNeeded")]
		bool IsRefreshNeeded { get; set; }

		[Export ("initWithNotificationHubPath:")]
		IntPtr Constructor (string notificationHubPath);

		[Export ("refreshFinishedWithDeviceToken:")]
		void RefreshFinished (string newDeviceToken);

		[Export ("getStoredRegistrationEntryWithRegistrationName:")]
		StoredRegistrationEntry GetStoredRegistrationEntry (string registrationName);

		[Export ("updateWithRegistrationName:registration:")]
		void Update (string registrationName, SBRegistration registration);

		[Export ("updateWithRegistrationName:registrationId:eTag:deviceToken:")]
		void Update (string registrationName, string registrationId, string eTag, string deviceToken);

		[Export ("updateWithRegistrationName:")]
		void Update (string registrationName);

		[Export ("updateWithRegistration:")]
		void Update (SBRegistration registration);

		[Export ("deleteWithRegistrationName:")]
		void Delete (string registrationName);

		[Export ("deleteAllRegistrations")]
		void DeleteAllRegistrations ();
	}

	[BaseType (typeof (NSObject))]
	public partial interface SBNotificationHub 
	{
		[Static, Export ("version")]
		string Version { get; }

		[Export ("initWithConnectionString:notificationHubPath:")]
		IntPtr Constructor (string connectionString, string notificationHubPath);

		[Export ("registerNativeWithDeviceToken:tags:completion:"), Async]
		void RegisterNative (NSData deviceToken, [NullAllowed]NSSet tags, ErrorCallback errorCallback);

		[Export ("registerTemplateWithDeviceToken:name:jsonBodyTemplate:expiryTemplate:tags:completion:"), Async]
		void RegisterTemplate (NSData deviceToken, string name, string jsonBodyTemplate, string expiryTemplate, NSSet tags, ErrorCallback errorCallback);

		[Export ("unregisterNativeWithCompletion:"), Async]
		void UnregisterNative (ErrorCallback errorCallback);

		[Export ("unregisterTemplateWithName:completion:"), Async]
		void UnregisterTemplate (string name, ErrorCallback errorCallback);

		[Export ("unregisterAllWithDeviceToken:completion:"), Async]
		void UnregisterAll (NSData deviceToken, ErrorCallback errorCallback);

		[Export ("registerNativeWithDeviceToken:tags:error:")]
		bool RegisterNative (NSData deviceToken, [NullAllowed]NSSet tags, out NSError error);

		[Export ("registerTemplateWithDeviceToken:name:jsonBodyTemplate:expiryTemplate:tags:error:")]
		bool RegisterTemplate (NSData deviceToken, string templateName, string jsonBodyTemplate, string expiryTemplate, [NullAllowed]NSSet tags, out NSError error);

		[Export ("unregisterNativeWithError:")]
		bool UnregisterNative (out NSError error);

		[Export ("unregisterTemplateWithName:error:")]
		bool UnregisterTemplate (string name, out NSError error);

		[Export ("unregisterAllWithDeviceToken:error:")]
		bool UnregisterAll (NSData deviceToken, out NSError error);
	}

	[BaseType (typeof (NSObject))]
	public partial interface SBRegistration
	{
		[Export ("ETag")]
		string ETag { get;set; }

		[Export ("expiresAt")]
		NSDate ExpiresAt { get;set; }

		[Export ("tags")]
		NSSet Tags { get;set; }

		[Export ("registrationId")]
		string RegistrationId { get;set; }

		[Export ("deviceToken")]
		string DeviceToken { get;set; }

		[Static, Export ("Name")]
		string Name ();

		[Static, Export ("payloadWithDeviceToken:tags:")]
		string Payload (string deviceToken, NSSet tags);
	}

	[BaseType (typeof (NSObject))]
	public partial interface StoredRegistrationEntry
	{
		[Export ("RegistrationName")]
		string RegistrationName { get;set; }

		[Export ("RegistrationId")]
		string RegistrationId { get;set; }

		[Export ("ETag")]
		string ETag { get;set; }

		[Export ("initWithString:")]
		IntPtr Constructor (string str);

		[Export ("toString")]
		string AsString ();
	}

	[BaseType (typeof (NSObject))]
	public partial interface SBTokenProvider
	{
		[Export ("timeToExpireinMins")]
		int TimeToExpireInMin { get;set; }

		[Export ("setTokenWithRequest:completion:")]
		void SetToken (NSMutableUrlRequest request, ErrorCallback errorCallback);

		[Export ("setTokenWithRequest:error:")]
		bool SetToken (NSMutableUrlRequest request, out NSError error);
	}
}

namespace WindowsAzure.Messaging.NotificationHubs
{
	[BaseType(typeof(NSObject))]
	public partial interface MSNotificationHub
	{
		[Static, Export("initWithConnectionString:hubName:")]
		void Init(string connectionString, string hubName);

		[Static, Export("didRegisterForRemoteNotificationsWithDeviceToken:")]
		void DidRegisterForRemoteNotifications(NSData deviceToken);

		[Static, Export("didFailToRegisterForRemoteNotificationsWithError:")]
		void DidFailToRegisterForRemoteNotifications(NSError error);

		[Static, Export("didReceiveRemoteNotification:")]
		bool DidReceiveRemoteNotification(NSDictionary userInfo);

		[Static, Export("setDelegate:")]
		void SetDelegate([NullAllowed] IMSNotificationHubDelegate nubDelegate);

		[Static, Export("isEnabled")]
		bool IsEnabled();

		[Static, Export("setEnabled:")]
		bool SetEnabled(bool isEnabled);

		[Static, Export("getPushChannel")]
		string GetPushChannel();

		[Static, Export("getInstallationId")]
		string GetInstallationId();

		[Static, Export("addTag:")]
		bool AddTag(string tag);

		[Static, Export("addTags:")]
		bool AddTags(NSArray<NSString> tags);

		[Static, Export("removeTag:")]
		bool RemoveTag(string tag);

		[Static, Export("removeTags:")]
		bool RemoveTags(NSArray<NSString> tags);

		[Static, Export("getTags")]
		NSArray<NSString> GetTags();

		[Static, Export("clearTags")]
		void ClearTags();
	}

	public delegate void CompletionHandler(UIBackgroundFetchResult fetchResult);

	[BaseType(typeof(NSObject))]
	[Model, Protocol]
	public interface MSNotificationHubDelegate
	{
		[Abstract, Export("notificationHub:didReceivePushNotification:fetchCompletionHandler:")]
		void DidReceivePushNotification(MSNotificationHub notificationHub, MSNotificationHubMessage message, CompletionHandler completionHandler);
	}

	public interface IMSNotificationHubDelegate
	{ }

	[BaseType(typeof(NSObject))]
	public partial interface MSNotificationHubMessage
	{
		[Export("initWithUserInfo:")]
		IntPtr Constructor(NSDictionary userInfo);

		[Export("title")]
		string Title { get; }

		[Export("body")]
		string Body { get; }

		[Export("userInfo")]
		NSDictionary<NSString, NSString> UserInfo { get; }
	}
}
