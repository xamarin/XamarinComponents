using System;
using ObjCRuntime;
using Foundation;

#if !__MACOS__
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
#endif

namespace WindowsAzure.Messaging.NotificationHubs
{
	[BaseType(typeof(NSObject))]
	public partial interface MSNotificationHub
	{
		[Static, Export("startWithConnectionString:hubName:")]
		void Start(string connectionString, string hubName);

		[Static, Export("startWithInstallationManagement:")]
		void Start(MSInstallationManagementDelegate managementDelegate);

		[Static, Export("didRegisterForRemoteNotificationsWithDeviceToken:")]
		void DidRegisterForRemoteNotifications(NSData deviceToken);

		[Static, Export("didFailToRegisterForRemoteNotificationsWithError:")]
		void DidFailToRegisterForRemoteNotifications(NSError error);

		[Static, Export("didReceiveRemoteNotification:")]
		void DidReceiveRemoteNotification(NSDictionary userInfo);

		[Static, Export("setDelegate:")]
		void SetDelegate([NullAllowed] MSNotificationHubDelegate hubDelegate);

		[Static, Export("setEnrichmentDelegate:")]
		void SetEnrichmentDelegate([NullAllowed] MSInstallationEnrichmentDelegate enrichmentDelegate);

		[Static, Export("setLifecycleDelegate:")]
		void SetLifecycleDelegate([NullAllowed] MSInstallationLifecycleDelegate lifecycleDelegate);

		[Static, Export("isEnabled")]
		bool IsEnabled();

		[Static, Export("setEnabled:")]
		bool SetEnabled(bool isEnabled);

		[Static, Export("getPushChannel")]
		string GetPushChannel();

		[Static, Export("getInstallationId")]
		string GetInstallationId();

		[Static, Export("getUserId")]
		string GetUserId();

		[Static, Export("setUserId:")]
		void SetUserId(string userId);

		[Static, Export("willSaveInstallation")]
		void WillSaveInstallation();

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

		[Static, Export("setTemplate:forKey:")]
		bool SetTemplate(MSInstallationTemplate template, string key);

		[Static, Export("removeTemplateForKey:")]
		bool RemoveTemplate(string key);

		[Static, Export("getTemplateForKey:")]
		MSInstallationTemplate GetTemplate(string key);
	}

	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	public interface MSNotificationHubDelegate
	{
		[Abstract, Export("notificationHub:didReceivePushNotification:")]
		void DidReceivePushNotification(MSNotificationHub notificationHub, MSNotificationHubMessage message);
	}

	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	public interface MSInstallationEnrichmentDelegate
    {
		[Abstract, Export("notificationHub:willEnrichInstallation:")]
		void WillEnrichInstallation(MSNotificationHub notificationHub, MSInstallation installation);
    }

	public delegate void NullableCompletionHandler([NullAllowed] NSError error);

	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	public interface MSInstallationManagementDelegate
    {
		[Abstract, Export("notificationHub:willUpsertInstallation:completionHandler:")]
		void WillUpsertInstallation(MSNotificationHub notificationHub, MSInstallation installation, NullableCompletionHandler completionHandler);
	}

	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	public interface MSInstallationLifecycleDelegate
    {
		[Abstract, Export("notificationHub:didSaveInstallation:")]
		void DidSaveInstallation(MSNotificationHub notificationHub, MSInstallation installation);

		[Abstract, Export("notificationHub:didFailToSaveInstallation:withError:")]
		void DidFailToSaveInstallation(MSNotificationHub notificationHub, MSInstallation installation, NSError error);
	}

	[BaseType(typeof(NSObject))]
	public partial interface MSNotificationHubMessage
	{
		[Export("title")]
		string Title { get; }

		[Export("body")]
		string Body { get; }

		[Export("userInfo")]
		NSDictionary UserInfo { get; }
	}

	[Protocol]
	public interface MSChangeTracking
	{
		[Abstract, Export("isDirty")]
		bool IsDirty();
	}

	[Protocol]
	public interface MSTaggable
	{
		[Abstract, Export("tags")]
		NSSet<NSString> Tags { get; }

		[Abstract, Export("addTag:")]
		bool AddTag(string tag);

		[Abstract, Export("addTags:")]
		bool AddTags(NSArray<NSString> tags);

		[Abstract, Export("removeTag:")]
		bool RemoveTag(string tag);

		[Abstract, Export("removeTags:")]
		bool RemoveTags(NSArray<NSString> tags);

		[Abstract, Export("clearTags")]
		void ClearTags();
	}

	[BaseType(typeof(NSObject))]
	public partial interface MSInstallation : MSTaggable, MSChangeTracking
    {
		[Export("installationId")]
		string InstallationId { get; set; }

		[Export("pushChannel")]
		string PushChannel { get; set; }

		[Export("expirationTime")]
		NSDate ExpirationTime { get; set; }

		[Export("userId")]
		string UserId { get; set; }

		[Export("templates")]
		NSDictionary<NSString, MSInstallationTemplate> Templates { get; }

		[Export("setTemplate:forKey:")]
		bool SetTemplate(MSInstallationTemplate template, string key);

		[Export("removeTemplateForKey:")]
		bool RemoveTemplate(string key);

		[Export("getTemplateForKey:")]
		MSInstallationTemplate GetTemplate(string key);

		[Export("toJsonData")]
		NSData ToJsonData();
	}

	[BaseType(typeof(NSObject))]
	public partial interface MSInstallationTemplate : INativeObject, MSTaggable, MSChangeTracking
	{
		[Export("body")]
		string Body { get; set; }

		[Export("headers")]
		NSDictionary<NSString, NSString> Headers { get; }

		[Export("setHeaderValue:forKey:")]
		void SetHeader(string value, string key);

		[Export("removeHeaderValueForKey:")]
		void RemoveHeader(string key);

		[Export("getHeaderValueForKey:")]
		string GetHeader(string key);
	}
}