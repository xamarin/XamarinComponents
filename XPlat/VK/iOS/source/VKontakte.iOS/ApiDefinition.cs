using System;
using System.Runtime.InteropServices;
using CoreAnimation;
using CoreFoundation;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

using VKontakte;
using VKontakte.API;
using VKontakte.API.Methods;
using VKontakte.API.Models;
using VKontakte.API.Upload;
using VKontakte.Core;
using VKontakte.Image;
using VKontakte.Utils;
using VKontakte.Views;

namespace VKontakte.API.Models
{
	// @interface VKObject : NSObject
	[BaseType (typeof(NSObject))]
	interface VKObject
	{
	}
}

namespace VKontakte.API.Methods
{
	[Static]
	partial interface VKApiConst
	{
		// extern const VKDisplayType VK_DISPLAY_IOS;
		[Field ("VK_DISPLAY_IOS", "__Internal")]
		NSString DisplayiOS { get; }

		// extern const VKDisplayType VK_DISPLAY_MOBILE;
		[Field ("VK_DISPLAY_MOBILE", "__Internal")]
		NSString DisplayMobile { get; }

		// Commons

		// extern NSString *const VK_ORIGINAL_CLIENT_BUNDLE;
		[Field ("VK_ORIGINAL_CLIENT_BUNDLE", "__Internal")]
		NSString OriginalClientBundle { get; }

		// extern NSString *const VK_ORIGINAL_HD_CLIENT_BUNDLE;
		[Field ("VK_ORIGINAL_HD_CLIENT_BUNDLE", "__Internal")]
		NSString OriginalHdClientBundle { get; }

		// extern NSString *const VK_DEBUG_CLIENT_BUNDLE;
		[Field ("VK_DEBUG_CLIENT_BUNDLE", "__Internal")]
		NSString DebugClientBundle { get; }

		// extern NSString *const VK_API_USER_ID;
		[Field ("VK_API_USER_ID", "__Internal")]
		NSString UserId { get; }

		// extern NSString *const VK_API_USER_IDS;
		[Field ("VK_API_USER_IDS", "__Internal")]
		NSString UserIds { get; }

		// extern NSString *const VK_API_FIELDS;
		[Field ("VK_API_FIELDS", "__Internal")]
		NSString Fields { get; }

		// extern NSString *const VK_API_SORT;
		[Field ("VK_API_SORT", "__Internal")]
		NSString Sort { get; }

		// extern NSString *const VK_API_OFFSET;
		[Field ("VK_API_OFFSET", "__Internal")]
		NSString Offset { get; }

		// extern NSString *const VK_API_COUNT;
		[Field ("VK_API_COUNT", "__Internal")]
		NSString Count { get; }

		// extern NSString *const VK_API_OWNER_ID;
		[Field ("VK_API_OWNER_ID", "__Internal")]
		NSString OwnerId { get; }

		// Auth

		// extern NSString *const VK_API_LANG;
		[Field ("VK_API_LANG", "__Internal")]
		NSString Lang { get; }

		// extern NSString *const VK_API_ACCESS_TOKEN;
		[Field ("VK_API_ACCESS_TOKEN", "__Internal")]
		NSString AccessToken { get; }

		// extern NSString *const VK_API_SIG;
		[Field ("VK_API_SIG", "__Internal")]
		NSString Sig { get; }

		// Get users

		// extern NSString *const VK_API_NAME_CASE;
		[Field ("VK_API_NAME_CASE", "__Internal")]
		NSString NameCase { get; }

		// extern NSString *const VK_API_ORDER;
		[Field ("VK_API_ORDER", "__Internal")]
		NSString Order { get; }

		// Get subscriptions

		// extern NSString *const VK_API_EXTENDED;
		[Field ("VK_API_EXTENDED", "__Internal")]
		NSString Extended { get; }

		// Search

		// extern NSString *const VK_API_Q;
		[Field ("VK_API_Q", "__Internal")]
		NSString Q { get; }

		// extern NSString *const VK_API_CITY;
		[Field ("VK_API_CITY", "__Internal")]
		NSString City { get; }

		// extern NSString *const VK_API_COUNTRY;
		[Field ("VK_API_COUNTRY", "__Internal")]
		NSString Country { get; }

		// extern NSString *const VK_API_HOMETOWN;
		[Field ("VK_API_HOMETOWN", "__Internal")]
		NSString Hometown { get; }

		// extern NSString *const VK_API_UNIVERSITY_COUNTRY;
		[Field ("VK_API_UNIVERSITY_COUNTRY", "__Internal")]
		NSString UniversityCountry { get; }

		// extern NSString *const VK_API_UNIVERSITY;
		[Field ("VK_API_UNIVERSITY", "__Internal")]
		NSString University { get; }

		// extern NSString *const VK_API_UNIVERSITY_YEAR;
		[Field ("VK_API_UNIVERSITY_YEAR", "__Internal")]
		NSString UniversityYear { get; }

		// extern NSString *const VK_API_SEX;
		[Field ("VK_API_SEX", "__Internal")]
		NSString Sex { get; }

		// extern NSString *const VK_API_STATUS;
		[Field ("VK_API_STATUS", "__Internal")]
		NSString Status { get; }

		// extern NSString *const VK_API_AGE_FROM;
		[Field ("VK_API_AGE_FROM", "__Internal")]
		NSString AgeFrom { get; }

		// extern NSString *const VK_API_AGE_TO;
		[Field ("VK_API_AGE_TO", "__Internal")]
		NSString AgeTo { get; }

		// extern NSString *const VK_API_BIRTH_DAY;
		[Field ("VK_API_BIRTH_DAY", "__Internal")]
		NSString BirthDay { get; }

		// extern NSString *const VK_API_BIRTH_MONTH;
		[Field ("VK_API_BIRTH_MONTH", "__Internal")]
		NSString BirthMonth { get; }

		// extern NSString *const VK_API_BIRTH_YEAR;
		[Field ("VK_API_BIRTH_YEAR", "__Internal")]
		NSString BirthYear { get; }

		// extern NSString *const VK_API_ONLINE;
		[Field ("VK_API_ONLINE", "__Internal")]
		NSString Online { get; }

		// extern NSString *const VK_API_HAS_PHOTO;
		[Field ("VK_API_HAS_PHOTO", "__Internal")]
		NSString HasPhoto { get; }

		// extern NSString *const VK_API_SCHOOL_COUNTRY;
		[Field ("VK_API_SCHOOL_COUNTRY", "__Internal")]
		NSString SchoolCountry { get; }

		// extern NSString *const VK_API_SCHOOL_CITY;
		[Field ("VK_API_SCHOOL_CITY", "__Internal")]
		NSString SchoolCity { get; }

		// extern NSString *const VK_API_SCHOOL;
		[Field ("VK_API_SCHOOL", "__Internal")]
		NSString School { get; }

		// extern NSString *const VK_API_SCHOOL_YEAR;
		[Field ("VK_API_SCHOOL_YEAR", "__Internal")]
		NSString SchoolYear { get; }

		// extern NSString *const VK_API_RELIGION;
		[Field ("VK_API_RELIGION", "__Internal")]
		NSString Religion { get; }

		// extern NSString *const VK_API_INTERESTS;
		[Field ("VK_API_INTERESTS", "__Internal")]
		NSString Interests { get; }

		// extern NSString *const VK_API_COMPANY;
		[Field ("VK_API_COMPANY", "__Internal")]
		NSString Company { get; }

		// extern NSString *const VK_API_POSITION;
		[Field ("VK_API_POSITION", "__Internal")]
		NSString Position { get; }

		// extern NSString *const VK_API_GROUP_ID;
		[Field ("VK_API_GROUP_ID", "__Internal")]
		NSString GroupId { get; }

		// extern NSString *const VK_API_GROUP_IDS;
		[Field ("VK_API_GROUP_IDS", "__Internal")]
		NSString GroupIds { get; }

		// extern NSString *const VK_API_FRIENDS_ONLY;
		[Field ("VK_API_FRIENDS_ONLY", "__Internal")]
		NSString FriendsOnly { get; }

		// extern NSString *const VK_API_FROM_GROUP;
		[Field ("VK_API_FROM_GROUP", "__Internal")]
		NSString FromGroup { get; }

		// extern NSString *const VK_API_MESSAGE;
		[Field ("VK_API_MESSAGE", "__Internal")]
		NSString Message { get; }

		// extern NSString *const VK_API_ATTACHMENT;
		[Field ("VK_API_ATTACHMENT", "__Internal")]
		NSString Attachment { get; }

		// extern NSString *const VK_API_ATTACHMENTS;
		[Field ("VK_API_ATTACHMENTS", "__Internal")]
		NSString Attachments { get; }

		// extern NSString *const VK_API_SERVICES;
		[Field ("VK_API_SERVICES", "__Internal")]
		NSString Services { get; }

		// extern NSString *const VK_API_SIGNED;
		[Field ("VK_API_SIGNED", "__Internal")]
		NSString Signed { get; }

		// extern NSString *const VK_API_PUBLISH_DATE;
		[Field ("VK_API_PUBLISH_DATE", "__Internal")]
		NSString PublishDate { get; }

		// extern NSString *const VK_API_LAT;
		[Field ("VK_API_LAT", "__Internal")]
		NSString Lat { get; }

		// extern NSString *const VK_API_LONG;
		[Field ("VK_API_LONG", "__Internal")]
		NSString Long { get; }

		// extern NSString *const VK_API_PLACE_ID;
		[Field ("VK_API_PLACE_ID", "__Internal")]
		NSString PlaceId { get; }

		// extern NSString *const VK_API_POST_ID;
		[Field ("VK_API_POST_ID", "__Internal")]
		NSString PostId { get; }

		// Errors

		// extern NSString *const VK_API_ERROR_CODE;
		[Field ("VK_API_ERROR_CODE", "__Internal")]
		NSString ErrorCode { get; }

		// extern NSString *const VK_API_ERROR_MSG;
		[Field ("VK_API_ERROR_MSG", "__Internal")]
		NSString ErrorMsg { get; }

		// extern NSString *const VK_API_ERROR_TEXT;
		[Field ("VK_API_ERROR_TEXT", "__Internal")]
		NSString ErrorText { get; }

		// extern NSString *const VK_API_REQUEST_PARAMS;
		[Field ("VK_API_REQUEST_PARAMS", "__Internal")]
		NSString RequestParams { get; }

		// Captcha

		// extern NSString *const VK_API_CAPTCHA_IMG;
		[Field ("VK_API_CAPTCHA_IMG", "__Internal")]
		NSString CaptchaImg { get; }

		// extern NSString *const VK_API_CAPTCHA_SID;
		[Field ("VK_API_CAPTCHA_SID", "__Internal")]
		NSString CaptchaSid { get; }

		// extern NSString *const VK_API_CAPTCHA_KEY;
		[Field ("VK_API_CAPTCHA_KEY", "__Internal")]
		NSString CaptchaKey { get; }

		// extern NSString *const VK_API_REDIRECT_URI;
		[Field ("VK_API_REDIRECT_URI", "__Internal")]
		NSString RedirectUri { get; }

		// Documents

		// extern NSString* const VK_API_DOC_ID;
		[Field("VK_API_DOC_ID", "__Internal")]
		NSString DocId { get; }

		// extern NSString* const VK_API_ACCESS_KEY;
		[Field("VK_API_ACCESS_KEY", "__Internal")]
		NSString AccessKey { get; }

		// extern NSString* const VK_API_FILE;
		[Field("VK_API_FILE", "__Internal")]
		NSString File { get; }

		// extern NSString* const VK_API_TITLE;
		[Field("VK_API_TITLE", "__Internal")]
		NSString Title { get; }

		// extern NSString* const VK_API_TAGS;
		[Field("VK_API_TAGS", "__Internal")]
		NSString Tags { get; }

		// Photos

		// extern NSString *const VK_API_PHOTO;
		[Field ("VK_API_PHOTO", "__Internal")]
		NSString Photo { get; }

		// extern NSString *const VK_API_ALBUM_ID;
		[Field ("VK_API_ALBUM_ID", "__Internal")]
		NSString AlbumId { get; }

		// Events

		// extern NSString *const VKCaptchaAnsweredEvent;
		[Field ("VKCaptchaAnsweredEvent", "__Internal")]
		NSString CaptchaAnsweredEvent { get; }
	}
}

namespace VKontakte.Core
{
	// @interface VKError : VKObject
	[BaseType (typeof(VKObject))]
	interface VKError
	{
		// extern NSString *const VKSdkErrorDomain;
		[Static]
		[Field ("VKSdkErrorDomain", "__Internal")]
		NSString VKSdkErrorDomain { get; }

		// extern NSString *const VkErrorDescriptionKey;
		[Static]
		[Field ("VkErrorDescriptionKey", "__Internal")]
		NSString DescriptionKey { get; }


		// @property (nonatomic, strong) NSError * httpError;
		[Export ("httpError", ArgumentSemantic.Strong)]
		NSError HttpError { get; set; }

		// @property (nonatomic, strong) VKError * apiError;
		[Export ("apiError", ArgumentSemantic.Strong)]
		VKError ApiError { get; set; }

		// @property (nonatomic, strong) VKRequest * request;
		[Export ("request", ArgumentSemantic.Strong)]
		VKRequest Request { get; set; }

		// @property (assign, nonatomic) NSInteger errorCode;
		[Export ("errorCode")]
		nint ErrorCode { get; set; }

		// @property (nonatomic, strong) NSString * errorMessage;
		[Export ("errorMessage", ArgumentSemantic.Strong)]
		string ErrorMessage { get; set; }

		// @property (nonatomic, strong) NSString * errorReason;
		[Export ("errorReason", ArgumentSemantic.Strong)]
		string ErrorReason { get; set; }

		// @property (nonatomic, strong) NSString * errorText;
		[Export ("errorText", ArgumentSemantic.Strong)]
		string ErrorText { get; set; }

		// @property (nonatomic, strong) NSDictionary * requestParams;
		[Export ("requestParams", ArgumentSemantic.Strong)]
		NSDictionary RequestParams { get; set; }

		// @property (nonatomic, strong) NSString * captchaSid;
		[Export ("captchaSid", ArgumentSemantic.Strong)]
		string CaptchaSid { get; set; }

		// @property (nonatomic, strong) NSString * captchaImg;
		[Export ("captchaImg", ArgumentSemantic.Strong)]
		string CaptchaImg { get; set; }

		// @property (nonatomic, strong) NSString * redirectUri;
		[Export ("redirectUri", ArgumentSemantic.Strong)]
		string RedirectUri { get; set; }

		// @property (nonatomic, strong) id json;
		[Export ("json", ArgumentSemantic.Strong)]
		NSObject Json { get; set; }

		// +(instancetype)errorWithCode:(NSInteger)errorCode;
		[Static]
		[Export ("errorWithCode:")]
		VKError Create (nint errorCode);

		// +(instancetype)errorWithJson:(id)JSON;
		[Static]
		[Export ("errorWithJson:")]
		VKError Create (NSObject json);

		// +(instancetype)errorWithQuery:(NSDictionary *)queryParams;
		[Static]
		[Export ("errorWithQuery:")]
		VKError Create (NSDictionary queryParams);

		// -(void)answerCaptcha:(NSString *)userEnteredCode;
		[Export ("answerCaptcha:")]
		void AnswerCaptcha (string userEnteredCode);
	}

	// @interface VKError (NSError)
	[Category]
	[BaseType (typeof(NSError))]
	interface NSError_VKError
	{
		// @property (readonly, nonatomic) VKError * vkError;
		[Export ("vkError")]
		VKError GetVKError ();

		// +(NSError *)errorWithVkError:(VKError *)vkError;
		[Static]
		[Internal]
		[Export ("errorWithVkError:")]
		NSError FromVKError (VKError vkError);

		// -(NSError *)copyWithVkError:(VKError *)vkError;
		[Export ("copyWithVkError:")]
		NSError CopyWithVKError (VKError vkError);
	}
}

namespace VKontakte.Utils
{
	// @interface MD5 (NSString)
	[Category]
	[BaseType (typeof(NSString))]
	interface NSString_MD5
	{
		// -(NSString *)MD5;
		[Export ("MD5")]
		string MD5 ();
	}

	// @interface MD5 (NSString)
	[Category]
	[BaseType (typeof(NSData))]
	interface NSData_MD5
	{
		// -(NSString *)MD5;
		[Export ("MD5")]
		string MD5 ();
	}

	// @interface OrderedDictionary : NSMutableDictionary
	[BaseType (typeof(NSMutableDictionary))]
	interface OrderedDictionary
	{
		// -(void)insertObject:(id)anObject forKey:(id)aKey atIndex:(NSUInteger)anIndex;
		[Export ("insertObject:forKey:atIndex:")]
		void InsertObject (NSObject anObject, NSObject aKey, nuint anIndex);

		// -(id)keyAtIndex:(NSUInteger)anIndex;
		[Export ("keyAtIndex:")]
		NSObject KeyAtIndex (nuint anIndex);

		// -(NSEnumerator *)reverseKeyEnumerator;
		[Export ("reverseKeyEnumerator")]
		NSEnumerator GetReverseKeyEnumerator ();
	}
}

namespace VKontakte.API.Models
{
	// TODO
	//	// @interface VKPropertyHelper : VKObject
	//	[BaseType (typeof(VKObject))]
	//	interface VKPropertyHelper
	//	{
	//		// @property (readonly, nonatomic) NSString * propertyName;
	//		[Export ("propertyName")]
	//		string PropertyName { get; }
	//
	//		// @property (readonly, nonatomic) NSString * propertyClassName;
	//		[Export ("propertyClassName")]
	//		string PropertyClassName { get; }
	//
	//		// @property (readonly, nonatomic) Class propertyClass;
	//		[Export ("propertyClass")]
	//		Class PropertyClass { get; }
	//
	//		// @property (readonly, nonatomic) BOOL isPrimitive;
	//		[Export ("isPrimitive")]
	//		bool IsPrimitive { get; }
	//
	//		// @property (readonly, nonatomic) BOOL isModelsArray;
	//		[Export ("isModelsArray")]
	//		bool IsModelsArray { get; }
	//
	//		// @property (readonly, nonatomic) BOOL isModel;
	//		[Export ("isModel")]
	//		bool IsModel { get; }
	//
	//		// -(instancetype)initWith:(objc_property_t)prop;
	//		[Export ("initWith:")]
	//		unsafe IntPtr Constructor (objc_property_t* prop);
	//	}

	// TODO
	//	// @protocol VKApiObject <NSObject>
	//	[Protocol, Model]
	//	[BaseType (typeof(NSObject))]
	//	interface VKApiObject
	//	{
	//		// @required +(instancetype)createWithDictionary:(NSDictionary *)dict;
	//		[Static, Abstract]
	//		[Export ("createWithDictionary:")]
	//		VKApiObject CreateWithDictionary (NSDictionary dict);
	//
	//		// @required +(instancetype)createWithArray:(NSArray *)array;
	//		[Static, Abstract]
	//		[Export ("createWithArray:")]
	//		[Verify (StronglyTypedNSArray)]
	//		VKApiObject CreateWithArray (NSObject[] array);
	//	}

	// @interface VKApiObject : VKObject <VKApiObject>
	[BaseType (typeof(VKObject))]
	interface VKApiObject // TODO : IVKApiObject
	{
		// @property (nonatomic, strong) NSDictionary * fields;
		[Export ("fields", ArgumentSemantic.Strong)]
		NSDictionary Fields { get; set; }

		// -(instancetype)initWithDictionary:(NSDictionary *)dict;
		[Export ("initWithDictionary:")]
		IntPtr Constructor (NSDictionary dict);

		// -(NSDictionary *)serialize;
		[Export ("serialize")]
		NSDictionary Serialize ();
	}

	// @interface VKDocs : VKApiObject
	[BaseType(typeof(VKApiObject))]
	interface VKDocs
	{
		// @property (nonatomic, strong) NSNumber * id;
		[Export("id", ArgumentSemantic.Strong)]
		NSNumber id { get; set; }

		// @property (nonatomic, strong) NSNumber * owner_id;
		[Export("owner_id", ArgumentSemantic.Strong)]
		NSNumber owner_id { get; set; }

		// @property (copy, nonatomic) NSString * title;
		[Export("title")]
		string title { get; set; }

		// @property (nonatomic, strong) NSNumber * size;
		[Export("size", ArgumentSemantic.Strong)]
		NSNumber size { get; set; }

		// @property (copy, nonatomic) NSString * ext;
		[Export("ext")]
		string ext { get; set; }

		// @property (copy, nonatomic) NSString * url;
		[Export("url")]
		string url { get; set; }

		// @property (copy, nonatomic) NSString * photo_100;
		[Export("photo_100")]
		string photo_100 { get; set; }

		// @property (copy, nonatomic) NSString * photo_130;
		[Export("photo_130")]
		string photo_130 { get; set; }

		// @property (nonatomic, strong) NSNumber * date;
		[Export("date", ArgumentSemantic.Strong)]
		NSNumber date { get; set; }
	}

	// @interface VKDocsArray : VKApiObjectArray
	[BaseType(typeof(VKApiObjectArray))]
	interface VKDocsArray
	{
	}

	// @interface VKApiObjectArray : VKApiObject <NSFastEnumeration>
	[BaseType (typeof(VKApiObject))]
	interface VKApiObjectArray // TODO : INSFastEnumeration
	{
		// @property (readonly, nonatomic) NSUInteger count;
		[Export ("count")]
		nuint Count { get; }

		// @property (nonatomic, strong) NSMutableArray * items;
		[Export ("items", ArgumentSemantic.Strong)]
		NSMutableArray Items { get; set; }

		// -(instancetype)initWithDictionary:(NSDictionary *)dict objectClass:(Class)objectClass;
		[Export ("initWithDictionary:objectClass:")]
		IntPtr Constructor (NSDictionary dict, Class objectClass);

		// -(instancetype)initWithArray:(NSArray *)array objectClass:(Class)objectClass;
		[Export ("initWithArray:objectClass:")]
		IntPtr Constructor (VKApiObject[] array, Class objectClass);

		// -(instancetype)initWithArray:(NSArray *)array;
		[Export ("initWithArray:")]
		IntPtr Constructor (VKApiObject[] array);

		// -(id)objectAtIndex:(NSInteger)idx;
		[Export ("objectAtIndex:")]
		VKApiObject ObjectAtIndex (nint idx);

		// -(id)objectAtIndexedSubscript:(NSUInteger)idx __attribute__((availability(ios, introduced=6_0)));
		[Introduced (PlatformName.iOS, 6, 0)]
		[Export ("objectAtIndexedSubscript:")]
		VKApiObject ObjectAtIndexedSubscript (nuint idx);

		// -(NSEnumerator *)objectEnumerator;
		[Export ("objectEnumerator")]
		NSEnumerator GetObjectEnumerator ();

		// -(NSEnumerator *)reverseObjectEnumerator;
		[Export ("reverseObjectEnumerator")]
		NSEnumerator GetReverseObjectEnumerator ();

		// -(void)addObject:(id)object;
		[Export ("addObject:")]
		void AddObject (VKApiObject @object);

		// -(void)removeObject:(id)object;
		[Export ("removeObject:")]
		void RemoveObject (VKApiObject @object);

		// -(void)insertObject:(id)object atIndex:(NSUInteger)index;
		[Export ("insertObject:atIndex:")]
		void InsertObject (VKApiObject @object, nuint index);

		// -(id)firstObject;
		[Export ("firstObject")]
		VKApiObject FirstObject { get; }

		// -(id)lastObject;
		[Export ("lastObject")]
		VKApiObject LastObject { get; }

		// -(void)serializeTo:(NSMutableDictionary *)dict withName:(NSString *)name;
		[Export ("serializeTo:withName:")]
		void SerializeTo (NSMutableDictionary dict, string name);

		// -(Class)objectClass;
		[Export ("objectClass")]
		Class ObjectClass { get; }
	}

	// @interface VKAudio : VKApiObject
	[BaseType (typeof(VKApiObject))]
	interface VKAudio
	{
		// @property (nonatomic, strong) NSNumber * id;
		[Export ("id", ArgumentSemantic.Strong)]
		NSNumber id { get; set; }

		// @property (nonatomic, strong) NSNumber * owner_id;
		[Export ("owner_id", ArgumentSemantic.Strong)]
		NSNumber owner_id { get; set; }

		// @property (nonatomic, strong) NSString * artist;
		[Export ("artist", ArgumentSemantic.Strong)]
		string artist { get; set; }

		// @property (nonatomic, strong) NSString * title;
		[Export ("title", ArgumentSemantic.Strong)]
		string title { get; set; }

		// @property (nonatomic, strong) NSNumber * duration;
		[Export ("duration", ArgumentSemantic.Strong)]
		NSNumber duration { get; set; }

		// @property (nonatomic, strong) NSString * url;
		[Export ("url", ArgumentSemantic.Strong)]
		string url { get; set; }

		// @property (nonatomic, strong) NSNumber * lyrics_id;
		[Export ("lyrics_id", ArgumentSemantic.Strong)]
		NSNumber lyrics_id { get; set; }

		// @property (nonatomic, strong) NSNumber * album_id;
		[Export ("album_id", ArgumentSemantic.Strong)]
		NSNumber album_id { get; set; }

		// @property (nonatomic, strong) NSNumber * genre_id;
		[Export ("genre_id", ArgumentSemantic.Strong)]
		NSNumber genre_id { get; set; }

		// @property (assign, nonatomic) BOOL fromCache;
		[Export ("fromCache")]
		bool FromCache { get; set; }

		// @property (assign, nonatomic) BOOL ignoreCache;
		[Export ("ignoreCache")]
		bool IgnoreCache { get; set; }
	}

	// @interface VKAudios : VKApiObjectArray
	[BaseType (typeof(VKApiObjectArray))]
	interface VKAudios
	{
		// @property (nonatomic, strong) VKUser * user;
		[Export ("user", ArgumentSemantic.Strong)]
		VKUser User { get; set; }
	}

	// @interface VKCounters : VKApiObject
	[BaseType (typeof(VKApiObject))]
	interface VKCounters
	{
		// @property (nonatomic, strong) NSNumber * friends;
		[Export ("friends", ArgumentSemantic.Strong)]
		NSNumber friends { get; set; }

		// @property (nonatomic, strong) NSNumber * messages;
		[Export ("messages", ArgumentSemantic.Strong)]
		NSNumber messages { get; set; }

		// @property (nonatomic, strong) NSNumber * photos;
		[Export ("photos", ArgumentSemantic.Strong)]
		NSNumber photos { get; set; }

		// @property (nonatomic, strong) NSNumber * videos;
		[Export ("videos", ArgumentSemantic.Strong)]
		NSNumber videos { get; set; }

		// @property (nonatomic, strong) NSNumber * notifications;
		[Export ("notifications", ArgumentSemantic.Strong)]
		NSNumber notifications { get; set; }

		// @property (nonatomic, strong) NSNumber * groups;
		[Export ("groups", ArgumentSemantic.Strong)]
		NSNumber groups { get; set; }

		// @property (nonatomic, strong) NSNumber * gifts;
		[Export ("gifts", ArgumentSemantic.Strong)]
		NSNumber gifts { get; set; }

		// @property (nonatomic, strong) NSNumber * events;
		[Export ("events", ArgumentSemantic.Strong)]
		NSNumber events { get; set; }

		// @property (nonatomic, strong) NSNumber * albums;
		[Export ("albums", ArgumentSemantic.Strong)]
		NSNumber albums { get; set; }

		// @property (nonatomic, strong) NSNumber * audios;
		[Export ("audios", ArgumentSemantic.Strong)]
		NSNumber audios { get; set; }

		// @property (nonatomic, strong) NSNumber * online_friends;
		[Export ("online_friends", ArgumentSemantic.Strong)]
		NSNumber online_friends { get; set; }

		// @property (nonatomic, strong) NSNumber * mutual_friends;
		[Export ("mutual_friends", ArgumentSemantic.Strong)]
		NSNumber mutual_friends { get; set; }

		// @property (nonatomic, strong) NSNumber * user_videos;
		[Export ("user_videos", ArgumentSemantic.Strong)]
		NSNumber user_videos { get; set; }

		// @property (nonatomic, strong) NSNumber * followers;
		[Export ("followers", ArgumentSemantic.Strong)]
		NSNumber followers { get; set; }

		// @property (nonatomic, strong) NSNumber * user_photos;
		[Export ("user_photos", ArgumentSemantic.Strong)]
		NSNumber user_photos { get; set; }

		// @property (nonatomic, strong) NSNumber * subscriptions;
		[Export ("subscriptions", ArgumentSemantic.Strong)]
		NSNumber subscriptions { get; set; }

		// @property (nonatomic, strong) NSNumber * documents;
		[Export ("documents", ArgumentSemantic.Strong)]
		NSNumber documents { get; set; }

		// @property (nonatomic, strong) NSNumber * topics;
		[Export ("topics", ArgumentSemantic.Strong)]
		NSNumber topics { get; set; }

		// @property (nonatomic, strong) NSNumber * pages;
		[Export ("pages", ArgumentSemantic.Strong)]
		NSNumber pages { get; set; }
	}

	// @interface VKPhotoSize : VKApiObject
	[BaseType (typeof(VKApiObject))]
	interface VKPhotoSize
	{
		// @property (readwrite, copy, nonatomic) NSString * src;
		[Export ("src")]
		string src { get; set; }

		// @property (readwrite, copy, nonatomic) NSNumber * width;
		[Export ("width", ArgumentSemantic.Copy)]
		NSNumber width { get; set; }

		// @property (readwrite, copy, nonatomic) NSNumber * height;
		[Export ("height", ArgumentSemantic.Copy)]
		NSNumber height { get; set; }

		// @property (readwrite, copy, nonatomic) NSString * type;
		[Export ("type")]
		string type { get; set; }
	}

	// @interface VKPhotoSizes : VKApiObjectArray
	[BaseType (typeof(VKApiObjectArray))]
	interface VKPhotoSizes
	{
		// -(VKPhotoSize *)photoSizeWithType:(NSString *)type;
		[Export ("photoSizeWithType:")]
		VKPhotoSize PhotoSizeWithType (string type);
	}

	// @interface VKPhoto : VKApiObject
	[BaseType (typeof(VKApiObject))]
	interface VKPhoto
	{
		// @property (nonatomic, strong) NSNumber * id;
		[Export ("id", ArgumentSemantic.Strong)]
		NSNumber id { get; set; }

		// @property (nonatomic, strong) NSNumber * album_id;
		[Export ("album_id", ArgumentSemantic.Strong)]
		NSNumber album_id { get; set; }

		// @property (nonatomic, strong) NSNumber * owner_id;
		[Export ("owner_id", ArgumentSemantic.Strong)]
		NSNumber owner_id { get; set; }

		// @property (nonatomic, strong) NSString * photo_75;
		[Export ("photo_75", ArgumentSemantic.Strong)]
		string photo_75 { get; set; }

		// @property (nonatomic, strong) NSString * photo_130;
		[Export ("photo_130", ArgumentSemantic.Strong)]
		string photo_130 { get; set; }

		// @property (nonatomic, strong) NSString * photo_604;
		[Export ("photo_604", ArgumentSemantic.Strong)]
		string photo_604 { get; set; }

		// @property (nonatomic, strong) NSString * photo_807;
		[Export ("photo_807", ArgumentSemantic.Strong)]
		string photo_807 { get; set; }

		// @property (nonatomic, strong) NSString * photo_1280;
		[Export ("photo_1280", ArgumentSemantic.Strong)]
		string photo_1280 { get; set; }

		// @property (nonatomic, strong) NSString * photo_2560;
		[Export ("photo_2560", ArgumentSemantic.Strong)]
		string photo_2560 { get; set; }

		// @property (nonatomic, strong) NSNumber * width;
		[Export ("width", ArgumentSemantic.Strong)]
		NSNumber Width { get; set; }

		// @property (nonatomic, strong) NSNumber * height;
		[Export ("height", ArgumentSemantic.Strong)]
		NSNumber height { get; set; }

		// @property (nonatomic, strong) NSString * text;
		[Export ("text", ArgumentSemantic.Strong)]
		string text { get; set; }

		// @property (nonatomic, strong) NSNumber * date;
		[Export ("date", ArgumentSemantic.Strong)]
		NSNumber date { get; set; }

		// @property (nonatomic, strong) VKPhotoSizes * sizes;
		[Export ("sizes", ArgumentSemantic.Strong)]
		VKPhotoSizes sizes { get; set; }

		// @property (readonly, nonatomic) NSString * attachmentString;
		[Export ("attachmentString")]
		string attachmentString { get; }
	}

	// @interface VKPhotoArray : VKApiObjectArray
	[BaseType (typeof(VKApiObjectArray))]
	interface VKPhotoArray
	{
	}

	// @interface VKSchool : VKApiObject
	[BaseType (typeof(VKApiObject))]
	interface VKSchool
	{
		// @property (nonatomic, strong) NSNumber * id;
		[Export ("id", ArgumentSemantic.Strong)]
		NSNumber id { get; set; }

		// @property (nonatomic, strong) NSNumber * country;
		[Export ("country", ArgumentSemantic.Strong)]
		NSNumber country { get; set; }

		// @property (nonatomic, strong) NSNumber * city;
		[Export ("city", ArgumentSemantic.Strong)]
		NSNumber city { get; set; }

		// @property (nonatomic, strong) NSString * name;
		[Export ("name", ArgumentSemantic.Strong)]
		string name { get; set; }

		// @property (nonatomic, strong) NSNumber * year_from;
		[Export ("year_from", ArgumentSemantic.Strong)]
		NSNumber year_from { get; set; }

		// @property (nonatomic, strong) NSNumber * year_to;
		[Export ("year_to", ArgumentSemantic.Strong)]
		NSNumber year_to { get; set; }

		// @property (nonatomic, strong) NSNumber * year_graduated;
		[Export ("year_graduated", ArgumentSemantic.Strong)]
		NSNumber year_graduated { get; set; }

		// @property (nonatomic, strong) NSString * Mclass;
		[Export ("Mclass", ArgumentSemantic.Strong)]
		string @class { get; set; }

		// @property (nonatomic, strong) NSString * speciality;
		[Export ("speciality", ArgumentSemantic.Strong)]
		string speciality { get; set; }

		// @property (nonatomic, strong) NSNumber * type;
		[Export ("type", ArgumentSemantic.Strong)]
		NSNumber type { get; set; }

		// @property (nonatomic, strong) NSString * type_str;
		[Export ("type_str", ArgumentSemantic.Strong)]
		string type_str { get; set; }
	}

	// @interface VKSchools : VKApiObjectArray
	[BaseType (typeof(VKApiObjectArray))]
	interface VKSchools
	{
	}

	// @interface VKUniversity : VKApiObject
	[BaseType (typeof(VKApiObject))]
	interface VKUniversity
	{
		// @property (nonatomic, strong) NSNumber * id;
		[Export ("id", ArgumentSemantic.Strong)]
		NSNumber id { get; set; }

		// @property (nonatomic, strong) NSNumber * country;
		[Export ("country", ArgumentSemantic.Strong)]
		NSNumber country { get; set; }

		// @property (nonatomic, strong) NSNumber * city;
		[Export ("city", ArgumentSemantic.Strong)]
		NSNumber city { get; set; }

		// @property (nonatomic, strong) NSString * name;
		[Export ("name", ArgumentSemantic.Strong)]
		string name { get; set; }

		// @property (nonatomic, strong) NSNumber * faculty;
		[Export ("faculty", ArgumentSemantic.Strong)]
		NSNumber faculty { get; set; }

		// @property (nonatomic, strong) NSString * faculty_name;
		[Export ("faculty_name", ArgumentSemantic.Strong)]
		string faculty_name { get; set; }

		// @property (nonatomic, strong) NSNumber * chair;
		[Export ("chair", ArgumentSemantic.Strong)]
		NSNumber chair { get; set; }

		// @property (nonatomic, strong) NSString * chair_name;
		[Export ("chair_name", ArgumentSemantic.Strong)]
		string chair_name { get; set; }

		// @property (nonatomic, strong) NSNumber * graduation;
		[Export ("graduation", ArgumentSemantic.Strong)]
		NSNumber graduation { get; set; }

		// @property (nonatomic, strong) NSString * education_form;
		[Export ("education_form", ArgumentSemantic.Strong)]
		string education_form { get; set; }

		// @property (nonatomic, strong) NSString * education_status;
		[Export ("education_status", ArgumentSemantic.Strong)]
		string education_status { get; set; }
	}

	// @interface VKUniversities : VKApiObjectArray
	[BaseType (typeof(VKApiObjectArray))]
	interface VKUniversities
	{
	}

	// @interface VKRelative : VKApiObject
	[BaseType (typeof(VKApiObject))]
	interface VKRelative
	{
		// @property (nonatomic, strong) NSNumber * id;
		[Export ("id", ArgumentSemantic.Strong)]
		NSNumber id { get; set; }

		// @property (nonatomic, strong) NSString * type;
		[Export ("type", ArgumentSemantic.Strong)]
		string type { get; set; }
	}

	// @interface VKRelativities : VKApiObjectArray
	[BaseType (typeof(VKApiObjectArray))]
	interface VKRelativities
	{
	}

	// @interface VKGeoObject : VKApiObject
	[BaseType (typeof(VKApiObject))]
	interface VKGeoObject
	{
		// @property (nonatomic, strong) NSNumber * id;
		[Export ("id", ArgumentSemantic.Strong)]
		NSNumber id { get; set; }

		// @property (nonatomic, strong) NSString * title;
		[Export ("title", ArgumentSemantic.Strong)]
		string title { get; set; }
	}

	// @interface VKCity : VKGeoObject
	[BaseType (typeof(VKGeoObject))]
	interface VKCity
	{
	}

	// @interface VKCountry : VKGeoObject
	[BaseType (typeof(VKGeoObject))]
	interface VKCountry
	{
	}

	// TODO: This seems to cause linker errors
	//// @interface VKPersonal : VKObject
	//[BaseType (typeof(VKObject))]
	//interface VKPersonal
	//{
	//	// @property (nonatomic, strong) NSArray * langs;
	//	[Export ("langs", ArgumentSemantic.Strong)]
	//	string[] langs { get; set; }

	//	// @property (nonatomic, strong) NSNumber * political;
	//	[Export ("political", ArgumentSemantic.Strong)]
	//	NSNumber political { get; set; }

	//	// @property (nonatomic, strong) NSString * religion;
	//	[Export ("religion", ArgumentSemantic.Strong)]
	//	string religion { get; set; }

	//	// @property (nonatomic, strong) NSNumber * life_main;
	//	[Export ("life_main", ArgumentSemantic.Strong)]
	//	NSNumber life_main { get; set; }

	//	// @property (nonatomic, strong) NSNumber * people_main;
	//	[Export ("people_main", ArgumentSemantic.Strong)]
	//	NSNumber people_main { get; set; }

	//	// @property (nonatomic, strong) NSString * inspired_by;
	//	[Export ("inspired_by", ArgumentSemantic.Strong)]
	//	string inspired_by { get; set; }

	//	// @property (nonatomic, strong) NSNumber * smoking;
	//	[Export ("smoking", ArgumentSemantic.Strong)]
	//	NSNumber smoking { get; set; }

	//	// @property (nonatomic, strong) NSNumber * alcohol;
	//	[Export ("alcohol", ArgumentSemantic.Strong)]
	//	NSNumber alcohol { get; set; }
	//}

	// @interface VKBanInfo : VKApiObject
	[BaseType (typeof(VKApiObject))]
	interface VKBanInfo
	{
		// @property (nonatomic, strong) NSNumber * admin_id;
		[Export ("admin_id", ArgumentSemantic.Strong)]
		NSNumber admin_id { get; set; }

		// @property (nonatomic, strong) NSNumber * date;
		[Export ("date", ArgumentSemantic.Strong)]
		NSNumber date { get; set; }

		// @property (nonatomic, strong) NSNumber * reason;
		[Export ("reason", ArgumentSemantic.Strong)]
		NSNumber reason { get; set; }

		// @property (nonatomic, strong) NSString * comment;
		[Export ("comment", ArgumentSemantic.Strong)]
		string comment { get; set; }

		// @property (nonatomic, strong) NSNumber * end_date;
		[Export ("end_date", ArgumentSemantic.Strong)]
		NSNumber end_date { get; set; }
	}

	// @interface VKLastSeen : VKApiObject
	[BaseType (typeof(VKApiObject))]
	interface VKLastSeen
	{
		// @property (nonatomic, strong) NSNumber * time;
		[Export ("time", ArgumentSemantic.Strong)]
		NSNumber time { get; set; }

		// @property (nonatomic, strong) NSNumber * platform;
		[Export ("platform", ArgumentSemantic.Strong)]
		NSNumber platform { get; set; }
	}

	// @interface VKExports : VKApiObject
	[BaseType (typeof(VKApiObject))]
	interface VKExports
	{
		// @property (nonatomic, strong) NSNumber * twitter;
		[Export ("twitter", ArgumentSemantic.Strong)]
		NSNumber twitter { get; set; }

		// @property (nonatomic, strong) NSNumber * facebook;
		[Export ("facebook", ArgumentSemantic.Strong)]
		NSNumber facebook { get; set; }

		// @property (nonatomic, strong) NSNumber * livejournal;
		[Export ("livejournal", ArgumentSemantic.Strong)]
		NSNumber livejournal { get; set; }

		// @property (nonatomic, strong) NSNumber * instagram;
		[Export ("instagram", ArgumentSemantic.Strong)]
		NSNumber instagram { get; set; }
	}

	// @interface VKUser : VKApiObject
	[BaseType (typeof(VKApiObject))]
	interface VKUser
	{
		// @property (nonatomic, strong) NSNumber * id;
		[Export ("id", ArgumentSemantic.Strong)]
		NSNumber id { get; set; }

		// @property (nonatomic, strong) NSString * first_name;
		[Export ("first_name", ArgumentSemantic.Strong)]
		string first_name { get; set; }

		// @property (nonatomic, strong) NSString * last_name;
		[Export ("last_name", ArgumentSemantic.Strong)]
		string last_name { get; set; }

		// @property (nonatomic, strong) NSString * first_name_acc;
		[Export ("first_name_acc", ArgumentSemantic.Strong)]
		string first_name_acc { get; set; }

		// @property (nonatomic, strong) NSString * last_name_acc;
		[Export ("last_name_acc", ArgumentSemantic.Strong)]
		string last_name_acc { get; set; }

		// @property (nonatomic, strong) NSString * first_name_gen;
		[Export ("first_name_gen", ArgumentSemantic.Strong)]
		string first_name_gen { get; set; }

		// @property (nonatomic, strong) NSString * last_name_gen;
		[Export ("last_name_gen", ArgumentSemantic.Strong)]
		string last_name_gen { get; set; }

		// @property (nonatomic, strong) NSString * first_name_dat;
		[Export ("first_name_dat", ArgumentSemantic.Strong)]
		string first_name_dat { get; set; }

		// @property (nonatomic, strong) NSString * last_name_dat;
		[Export ("last_name_dat", ArgumentSemantic.Strong)]
		string last_name_dat { get; set; }

		// @property (nonatomic, strong) NSString * first_name_ins;
		[Export ("first_name_ins", ArgumentSemantic.Strong)]
		string first_name_ins { get; set; }

		// @property (nonatomic, strong) NSString * last_name_ins;
		[Export ("last_name_ins", ArgumentSemantic.Strong)]
		string last_name_ins { get; set; }

		// TODO: this seems to cause linker errors
		//// @property (nonatomic, strong) VKPersonal * personal;
		//[Export ("personal", ArgumentSemantic.Strong)]
		//VKPersonal personal { get; set; }

		// @property (nonatomic, strong) NSNumber * sex;
		[Export ("sex", ArgumentSemantic.Strong)]
		NSNumber sex { get; set; }

		// @property (nonatomic, strong) NSNumber * invited_by;
		[Export ("invited_by", ArgumentSemantic.Strong)]
		NSNumber invited_by { get; set; }

		// @property (nonatomic, strong) NSNumber * online;
		[Export ("online", ArgumentSemantic.Strong)]
		NSNumber online { get; set; }

		// @property (nonatomic, strong) NSString * bdate;
		[Export ("bdate", ArgumentSemantic.Strong)]
		string bdate { get; set; }

		// @property (nonatomic, strong) VKCity * city;
		[Export ("city", ArgumentSemantic.Strong)]
		VKCity city { get; set; }

		// @property (nonatomic, strong) VKCountry * country;
		[Export ("country", ArgumentSemantic.Strong)]
		VKCountry country { get; set; }

		// @property (nonatomic, strong) NSMutableArray * lists;
		[Export ("lists", ArgumentSemantic.Strong)]
		NSMutableArray lists { get; set; }

		// @property (nonatomic, strong) NSString * screen_name;
		[Export ("screen_name", ArgumentSemantic.Strong)]
		string screen_name { get; set; }

		// @property (nonatomic, strong) NSNumber * has_mobile;
		[Export ("has_mobile", ArgumentSemantic.Strong)]
		NSNumber has_mobile { get; set; }

		// @property (nonatomic, strong) NSNumber * rate;
		[Export ("rate", ArgumentSemantic.Strong)]
		NSNumber rate { get; set; }

		// @property (nonatomic, strong) NSString * mobile_phone;
		[Export ("mobile_phone", ArgumentSemantic.Strong)]
		string mobile_phone { get; set; }

		// @property (nonatomic, strong) NSString * home_phone;
		[Export ("home_phone", ArgumentSemantic.Strong)]
		string home_phone { get; set; }

		// @property (assign, nonatomic) BOOL can_post;
		[Export ("can_post")]
		bool can_post { get; set; }

		// @property (assign, nonatomic) BOOL can_see_all_posts;
		[Export ("can_see_all_posts")]
		bool can_see_all_posts { get; set; }

		// @property (nonatomic, strong) NSString * status;
		[Export ("status", ArgumentSemantic.Strong)]
		string status { get; set; }

		// @property (nonatomic, strong) VKAudio * status_audio;
		[Export ("status_audio", ArgumentSemantic.Strong)]
		VKAudio status_audio { get; set; }

		// @property (assign, nonatomic) _Bool status_loaded;
		[Export ("status_loaded")]
		bool status_loaded { get; set; }

		// @property (nonatomic, strong) VKLastSeen * last_seen;
		[Export ("last_seen", ArgumentSemantic.Strong)]
		VKLastSeen last_seen { get; set; }

		// @property (nonatomic, strong) NSNumber * relation;
		[Export ("relation", ArgumentSemantic.Strong)]
		NSNumber relation { get; set; }

		// @property (nonatomic, strong) VKUser * relation_partner;
		[Export ("relation_partner", ArgumentSemantic.Strong)]
		VKUser relation_partner { get; set; }

		// @property (nonatomic, strong) VKCounters * counters;
		[Export ("counters", ArgumentSemantic.Strong)]
		VKCounters counters { get; set; }

		// @property (nonatomic, strong) NSString * nickname;
		[Export ("nickname", ArgumentSemantic.Strong)]
		string nickname { get; set; }

		// @property (nonatomic, strong) VKExports * exports;
		[Export ("exports", ArgumentSemantic.Strong)]
		VKExports exports { get; set; }

		// @property (nonatomic, strong) NSNumber * wall_comments;
		[Export ("wall_comments", ArgumentSemantic.Strong)]
		NSNumber wall_comments { get; set; }

		// @property (assign, nonatomic) BOOL can_write_private_message;
		[Export ("can_write_private_message")]
		bool can_write_private_message { get; set; }

		// @property (nonatomic, strong) NSString * phone;
		[Export ("phone", ArgumentSemantic.Strong)]
		string phone { get; set; }

		// @property (nonatomic, strong) NSNumber * online_mobile;
		[Export ("online_mobile", ArgumentSemantic.Strong)]
		NSNumber online_mobile { get; set; }

		// @property (nonatomic, strong) NSNumber * faculty;
		[Export ("faculty", ArgumentSemantic.Strong)]
		NSNumber faculty { get; set; }

		// @property (nonatomic, strong) NSNumber * university;
		[Export ("university", ArgumentSemantic.Strong)]
		NSNumber university { get; set; }

		// @property (nonatomic, strong) VKUniversities * universities;
		[Export ("universities", ArgumentSemantic.Strong)]
		VKUniversities universities { get; set; }

		// @property (nonatomic, strong) VKSchools * schools;
		[Export ("schools", ArgumentSemantic.Strong)]
		VKSchools schools { get; set; }

		// @property (nonatomic, strong) NSNumber * graduation;
		[Export ("graduation", ArgumentSemantic.Strong)]
		NSNumber graduation { get; set; }

		// @property (nonatomic, strong) NSNumber * friendState;
		[Export ("friendState", ArgumentSemantic.Strong)]
		NSNumber friend_state { get; set; }

		// @property (nonatomic, strong) NSString * faculty_name;
		[Export ("faculty_name", ArgumentSemantic.Strong)]
		string faculty_name { get; set; }

		// @property (nonatomic, strong) NSString * university_name;
		[Export ("university_name", ArgumentSemantic.Strong)]
		string university_name { get; set; }

		// @property (nonatomic, strong) NSString * books;
		[Export ("books", ArgumentSemantic.Strong)]
		string books { get; set; }

		// @property (nonatomic, strong) NSString * games;
		[Export ("games", ArgumentSemantic.Strong)]
		string games { get; set; }

		// @property (nonatomic, strong) NSString * interests;
		[Export ("interests", ArgumentSemantic.Strong)]
		string interests { get; set; }

		// @property (nonatomic, strong) NSString * movies;
		[Export ("movies", ArgumentSemantic.Strong)]
		string movies { get; set; }

		// @property (nonatomic, strong) NSString * tv;
		[Export ("tv", ArgumentSemantic.Strong)]
		string tv { get; set; }

		// @property (nonatomic, strong) NSString * about;
		[Export ("about", ArgumentSemantic.Strong)]
		string about { get; set; }

		// @property (nonatomic, strong) NSString * music;
		[Export ("music", ArgumentSemantic.Strong)]
		string music { get; set; }

		// @property (nonatomic, strong) NSString * quoutes;
		[Export ("quoutes", ArgumentSemantic.Strong)]
		string quoutes { get; set; }

		// @property (nonatomic, strong) NSString * activities;
		[Export ("activities", ArgumentSemantic.Strong)]
		string activities { get; set; }

		// @property (nonatomic, strong) NSString * photo_max;
		[Export ("photo_max", ArgumentSemantic.Strong)]
		string photo_max { get; set; }

		// @property (nonatomic, strong) NSString * photo_50;
		[Export ("photo_50", ArgumentSemantic.Strong)]
		string photo_50 { get; set; }

		// @property (nonatomic, strong) NSString * photo_100;
		[Export ("photo_100", ArgumentSemantic.Strong)]
		string photo_100 { get; set; }

		// @property (nonatomic, strong) NSString * photo_200;
		[Export ("photo_200", ArgumentSemantic.Strong)]
		string photo_200 { get; set; }

		// @property (nonatomic, strong) NSString * photo_200_orig;
		[Export ("photo_200_orig", ArgumentSemantic.Strong)]
		string photo_200_orig { get; set; }

		// @property (nonatomic, strong) NSString * photo_400_orig;
		[Export ("photo_400_orig", ArgumentSemantic.Strong)]
		string photo_400_orig { get; set; }

		// @property (nonatomic, strong) NSString * photo_max_orig;
		[Export ("photo_max_orig", ArgumentSemantic.Strong)]
		string photo_max_orig { get; set; }

		// @property (nonatomic, strong) VKPhotoArray * photos;
		[Export ("photos", ArgumentSemantic.Strong)]
		VKPhotoArray photos { get; set; }

		// @property (nonatomic, strong) NSNumber * photos_count;
		[Export ("photos_count", ArgumentSemantic.Strong)]
		NSNumber photos_count { get; set; }

		// @property (nonatomic, strong) VKRelativities * relatives;
		[Export ("relatives", ArgumentSemantic.Strong)]
		VKRelativities relatives { get; set; }

		// @property (assign, nonatomic) NSTimeInterval bdateIntervalSort;
		[Export ("bdateIntervalSort")]
		double bdateIntervalSort { get; set; }

		// @property (nonatomic, strong) NSNumber * verified;
		[Export ("verified", ArgumentSemantic.Strong)]
		NSNumber verified { get; set; }

		// @property (nonatomic, strong) NSString * deactivated;
		[Export ("deactivated", ArgumentSemantic.Strong)]
		string deactivated { get; set; }

		// @property (nonatomic, strong) NSString * site;
		[Export ("site", ArgumentSemantic.Strong)]
		string site { get; set; }

		// @property (nonatomic, strong) NSString * home_town;
		[Export ("home_town", ArgumentSemantic.Strong)]
		string home_town { get; set; }

		// @property (nonatomic, strong) NSNumber * blacklisted;
		[Export ("blacklisted", ArgumentSemantic.Strong)]
		NSNumber blacklisted { get; set; }

		// @property (nonatomic, strong) NSNumber * blacklisted_by_me;
		[Export ("blacklisted_by_me", ArgumentSemantic.Strong)]
		NSNumber blacklisted_by_me { get; set; }

		// @property (nonatomic, strong) NSString * twitter;
		[Export ("twitter", ArgumentSemantic.Strong)]
		string twitter { get; set; }

		// @property (nonatomic, strong) NSString * skype;
		[Export ("skype", ArgumentSemantic.Strong)]
		string skype { get; set; }

		// @property (nonatomic, strong) NSString * facebook;
		[Export ("facebook", ArgumentSemantic.Strong)]
		string facebook { get; set; }

		// @property (nonatomic, strong) NSString * livejournal;
		[Export ("livejournal", ArgumentSemantic.Strong)]
		string livejournal { get; set; }

		// @property (nonatomic, strong) NSString * wall_default;
		[Export ("wall_default", ArgumentSemantic.Strong)]
		string wall_default { get; set; }

		// @property (nonatomic, strong) NSString * contact;
		[Export ("contact", ArgumentSemantic.Strong)]
		string contact { get; set; }

		// @property (nonatomic, strong) NSNumber * request_sent;
		[Export ("request_sent", ArgumentSemantic.Strong)]
		NSNumber request_sent { get; set; }

		// @property (nonatomic, strong) NSNumber * common_count;
		[Export ("common_count", ArgumentSemantic.Strong)]
		NSNumber common_count { get; set; }

		// @property (nonatomic, strong) VKBanInfo * ban_info;
		[Export ("ban_info", ArgumentSemantic.Strong)]
		VKBanInfo ban_info { get; set; }

		// @property (nonatomic, strong) NSString * name;
		[Export ("name", ArgumentSemantic.Strong)]
		string name { get; set; }

		// @property (nonatomic, strong) NSString * name_gen;
		[Export ("name_gen", ArgumentSemantic.Strong)]
		string name_gen { get; set; }

		// @property (nonatomic, strong) NSNumber * followers_count;
		[Export ("followers_count", ArgumentSemantic.Strong)]
		NSNumber followers_count { get; set; }
	}

	// @interface VKUsersArray : VKApiObjectArray
	[BaseType (typeof(VKApiObjectArray))]
	interface VKUsersArray
	{
	}
}

namespace VKontakte
{
	// @interface VKAccessToken : VKObject <NSCoding>
	[BaseType (typeof(VKObject))]
	interface VKAccessToken : INSCoding
	{
		// @property (readonly, copy, nonatomic) NSString * accessToken;
		[Export ("accessToken")]
		string AccessToken { get; }

		// @property (readonly, copy, nonatomic) NSString * userId;
		[Export ("userId")]
		string UserId { get; }

		// @property (readonly, copy, nonatomic) NSString * secret;
		[Export ("secret")]
		string Secret { get; }

		// @property (readonly, copy, nonatomic) NSArray * permissions;
		[Export ("permissions", ArgumentSemantic.Copy)]
		string[] Permissions { get; }

		// @property (readonly, copy, nonatomic) NSString * email;
		[Export ("email")]
		string Email { get; }

		// @property (readonly, assign, nonatomic) NSInteger expiresIn;
		[Export ("expiresIn")]
		nint ExpiresIn { get; }

		// @property (readonly, assign, nonatomic) BOOL httpsRequired;
		[Export ("httpsRequired")]
		bool HttpsRequired { get; }

		// @property (readonly, assign, nonatomic) NSTimeInterval created;
		[Export ("created")]
		double Created { get; }

		// @property (readonly, nonatomic, strong) VKUser * localUser;
		[Export ("localUser", ArgumentSemantic.Strong)]
		VKUser LocalUser { get; }

		// +(instancetype)tokenFromUrlString:(NSString *)urlString;
		[Static]
		[Export ("tokenFromUrlString:")]
		VKAccessToken TokenFromUrlString (string urlString);

		// +(instancetype)tokenWithToken:(NSString *)accessToken secret:(NSString *)secret userId:(NSString *)userId;
		[Static]
		[Export ("tokenWithToken:secret:userId:")]
		VKAccessToken TokenFromToken (string accessToken, string secret, string userId);

		// +(instancetype)savedToken:(NSString *)defaultsKey;
		[Static]
		[Export ("savedToken:")]
		VKAccessToken TokenFromDefaults (string defaultsKey);

		// -(void)saveTokenToDefaults:(NSString *)defaultsKey;
		[Export ("saveTokenToDefaults:")]
		void SaveTokenToDefaults (string defaultsKey);

		// -(BOOL)isExpired;
		[Export ("isExpired")]
		bool IsExpired { get; }

		// +(void)delete:(NSString *)service;
		[Static]
		[Export ("delete:")]
		void Delete (string service);
	}

	// @interface VKAccessTokenMutable : VKAccessToken
	[BaseType (typeof(VKAccessToken))]
	interface VKAccessTokenMutable
	{
		// @property (readwrite, copy, nonatomic) NSString * accessToken;
		[Export ("accessToken")]
		string AccessToken { get; set; }

		// @property (readwrite, copy, nonatomic) NSString * userId;
		[Export ("userId")]
		string UserId { get; set; }

		// @property (readwrite, copy, nonatomic) NSString * secret;
		[Export ("secret")]
		string Secret { get; set; }

		// @property (readwrite, copy, nonatomic) NSArray * permissions;
		[Export ("permissions", ArgumentSemantic.Copy)]
		string[] Permissions { get; set; }

		// @property (assign, readwrite, nonatomic) BOOL httpsRequired;
		[Export ("httpsRequired")]
		bool HttpsRequired { get; set; }

		// @property (assign, readwrite, nonatomic) NSInteger expiresIn;
		[Export ("expiresIn")]
		nint ExpiresIn { get; set; }

		// @property (readwrite, nonatomic, strong) VKUser * localUser;
		[Export ("localUser", ArgumentSemantic.Strong)]
		VKUser LocalUser { get; set; }
	}
}

namespace VKontakte.Views
{
	// @interface VKActivity : UIActivity
	[BaseType (typeof(UIActivity))]
	interface VKActivity
	{
		// extern NSString *const VKActivityTypePost;
		[Static]
		[Field ("VKActivityTypePost", "__Internal")]
		NSString ActivityTypePost { get; }


		// +(BOOL)vkShareExtensionEnabled;
		[Static]
		[Export ("vkShareExtensionEnabled")]
		bool ShareExtensionEnabled { get; }
	}
}

namespace VKontakte.Core
{
	// @interface VKResponse : VKObject
	[BaseType (typeof(VKObject))]
	interface VKResponse
	{
		// @property (nonatomic, weak) VKRequest * _Nullable request;
		[NullAllowed, Export ("request", ArgumentSemantic.Weak)]
		VKRequest Request { get; set; }

		// @property (nonatomic, strong) id json;
		[Export ("json", ArgumentSemantic.Strong)]
		NSObject Json { get; set; }

		// @property (nonatomic, strong) id parsedModel;
		[Export ("parsedModel", ArgumentSemantic.Strong)]
		NSObject ParsedModel { get; set; }

		// @property (copy, nonatomic) NSString * responseString;
		[Export ("responseString")]
		string ResponseString { get; set; }
	}

	// @interface VKRequestTiming : VKObject
	[BaseType (typeof(VKObject))]
	interface VKRequestTiming
	{
		// @property (nonatomic, strong) NSDate * startTime;
		[Export ("startTime", ArgumentSemantic.Strong)]
		NSDate StartTime { get; set; }

		// @property (nonatomic, strong) NSDate * finishTime;
		[Export ("finishTime", ArgumentSemantic.Strong)]
		NSDate FinishTime { get; set; }

		// @property (assign, nonatomic) NSTimeInterval loadTime;
		[Export ("loadTime")]
		double LoadTime { get; set; }

		// @property (assign, nonatomic) NSTimeInterval parseTime;
		[Export ("parseTime")]
		double ParseTime { get; set; }

		// @property (readonly, nonatomic) NSTimeInterval totalTime;
		[Export ("totalTime")]
		double TotalTime { get; }
	}

	// @interface VKRequest : VKObject
	[BaseType (typeof(VKObject))]
	interface VKRequest
	{
		// @property (copy, nonatomic) void (^progressBlock)(VKProgressType, long long, long long);
		[Export ("progressBlock", ArgumentSemantic.Copy)]
		Action<VKProgressType, long, long> ProgressBlock { get; set; }

		// @property (copy, nonatomic) void (^completeBlock)(VKResponse *);
		[Export ("completeBlock", ArgumentSemantic.Copy)]
		Action<VKResponse> CompleteBlock { get; set; }

		// @property (copy, nonatomic) void (^errorBlock)(NSError *);
		[Export ("errorBlock", ArgumentSemantic.Copy)]
		Action<NSError> ErrorBlock { get; set; }

		// @property (assign, nonatomic) int attempts;
		[Export ("attempts")]
		int Attempts { get; set; }

		// @property (assign, nonatomic) BOOL secure;
		[Export ("secure")]
		bool Secure { get; set; }

		// @property (assign, nonatomic) BOOL useSystemLanguage;
		[Export ("useSystemLanguage")]
		bool UseSystemLanguage { get; set; }

		// @property (assign, nonatomic) BOOL parseModel;
		[Export ("parseModel")]
		bool ParseModel { get; set; }

		// @property (assign, nonatomic) BOOL debugTiming;
		[Export ("debugTiming")]
		bool DebugTiming { get; set; }

		// @property (assign, nonatomic) NSInteger requestTimeout;
		[Export ("requestTimeout")]
		nint RequestTimeout { get; set; }

		// @property (assign, nonatomic) dispatch_queue_t responseQueue;
		[Export ("responseQueue", ArgumentSemantic.Assign)]
		DispatchQueue ResponseQueue { get; set; }

		// @property (assign, nonatomic) BOOL waitUntilDone;
		[Export ("waitUntilDone")]
		bool WaitUntilDone { get; set; }

		// @property (readonly, nonatomic) NSString * methodName;
		[Export ("methodName")]
		string MethodName { get; }

		// @property (readonly, nonatomic) NSString * httpMethod;
		[Export ("httpMethod")]
		string HttpMethod { get; }

		// @property (readonly, nonatomic) NSDictionary * methodParameters;
		[Export ("methodParameters")]
		NSDictionary MethodParameters { get; }

		// @property (readonly, nonatomic) NSOperation * executionOperation;
		[Export ("executionOperation")]
		NSOperation ExecutionOperation { get; }

		// @property (readonly, nonatomic) VKRequestTiming * requestTiming;
		[Export ("requestTiming")]
		VKRequestTiming RequestTiming { get; }

		// @property (readonly, nonatomic) BOOL isExecuting;
		[Export ("isExecuting")]
		bool IsExecuting { get; }

		// @property (copy, nonatomic) NSArray * preventThisErrorsHandling;
		[Export ("preventThisErrorsHandling", ArgumentSemantic.Copy)]
		NSNumber[] PreventThisErrorsHandling { get; set; }

		//// +(instancetype)requestWithMethod:(NSString *)method andParameters:(NSDictionary *)parameters andHttpMethod:(NSString *)httpMethod __attribute__((deprecated("")));
		//[Obsolete]
		//[Static]
		//[Export ("requestWithMethod:andParameters:andHttpMethod:")]
		//VKRequest Create (string method, [NullAllowed] NSDictionary parameters, string httpMethod);

		//// +(instancetype)requestWithMethod:(NSString *)method andParameters:(NSDictionary *)parameters __attribute__((deprecated("")));
		//[Obsolete]
		//[Static]
		//[Export ("requestWithMethod:andParameters:")]
		//VKRequest Create (string method, [NullAllowed] NSDictionary parameters);

		//// +(instancetype)requestWithMethod:(NSString *)method andParameters:(NSDictionary *)parameters modelClass:(Class)modelClass __attribute__((deprecated("")));
		//[Obsolete]
		//[Static]
		//[Export ("requestWithMethod:andParameters:modelClass:")]
		//VKRequest Create (string method, [NullAllowed] NSDictionary parameters, Class modelClass);

		//// +(instancetype)requestWithMethod:(NSString *)method andParameters:(NSDictionary *)parameters andHttpMethod:(NSString *)httpMethod classOfModel:(Class)modelClass __attribute__((deprecated("")));
		//[Obsolete]
		//[Static]
		//[Export ("requestWithMethod:andParameters:andHttpMethod:classOfModel:")]
		//VKRequest Create (string method, [NullAllowed] NSDictionary parameters, string httpMethod, Class modelClass);

		// +(instancetype)requestWithMethod:(NSString *)method parameters:(NSDictionary *)parameters;
		[Static]
		[Export ("requestWithMethod:parameters:")]
		VKRequest Create (string method, [NullAllowed] NSDictionary parameters);

		// +(instancetype)requestWithMethod:(NSString *)method parameters:(NSDictionary *)parameters modelClass:(Class)modelClass;
		[Static]
		[Export ("requestWithMethod:parameters:modelClass:")]
		VKRequest Create (string method, [NullAllowed] NSDictionary parameters, Class modelClass);

		// +(instancetype)photoRequestWithPostUrl:(NSString *)url withPhotos:(NSArray *)photoObjects;
		[Static]
		[Export ("photoRequestWithPostUrl:withPhotos:")]
		VKRequest CreatePhotoRequest (string url, VKUploadImage[] photoObjects);

		// -(NSURLRequest *)getPreparedRequest;
		[Export ("getPreparedRequest")]
		NSUrlRequest PreparedRequest { get; }

		// -(void)executeWithResultBlock:(void (^)(VKResponse *))completeBlock errorBlock:(void (^)(NSError *))errorBlock;
		[Export ("executeWithResultBlock:errorBlock:")]
		void Execute (Action<VKResponse> completeBlock, Action<NSError> errorBlock);

		// -(void)executeAfter:(VKRequest *)request withResultBlock:(void (^)(VKResponse *))completeBlock errorBlock:(void (^)(NSError *))errorBlock;
		[Export ("executeAfter:withResultBlock:errorBlock:")]
		void ExecuteAfter (VKRequest request, Action<VKResponse> completeBlock, Action<NSError> errorBlock);

		// -(void)start;
		[Export ("start")]
		void Start ();

		// -(NSOperation *)createExecutionOperation;
		[Export ("createExecutionOperation")]
		NSOperation CreateExecutionOperation ();

		// -(void)repeat;
		[Export ("repeat")]
		void Repeat ();

		// -(void)cancel;
		[Export ("cancel")]
		void Cancel ();

		// -(void)addExtraParameters:(NSDictionary *)extraParameters;
		[Export ("addExtraParameters:")]
		void AddExtraParameters (NSDictionary extraParameters);

		// -(void)setPreferredLang:(NSString *)lang;
		[Export ("setPreferredLang:")]
		void SetPreferredLang (string lang);
	}
}

namespace VKontakte.API.Methods
{
	// @interface VKApiBase : VKObject
	[BaseType (typeof(VKObject))]
	interface VKApiBase
	{
		// -(NSString *)getMethodGroup;
		[Export ("getMethodGroup")]
		string MethodGroup { get; }

		// -(VKRequest *)prepareRequestWithMethodName:(NSString *)methodName andParameters:(NSDictionary *)methodParameters;
		[Export ("prepareRequestWithMethodName:parameters:")]
		VKRequest PrepareRequest (string methodName, NSDictionary methodParameters);

		// -(VKRequest *)prepareRequestWithMethodName:(NSString *)methodName andParameters:(NSDictionary *)methodParameters andClassOfModel:(Class)modelClass;
		[Export ("prepareRequestWithMethodName:parameters:modelClass:")]
		VKRequest PrepareRequest (string methodName, NSDictionary methodParameters, Class modelClass);
	}

	// @interface VKApiUsers : VKApiBase
	[BaseType (typeof(VKApiBase))]
	interface VKApiUsers
	{
		// -(VKRequest *)get;
		[Export ("get")]
		VKRequest Get ();

		// -(VKRequest *)get:(NSDictionary *)params;
		[Export ("get:")]
		VKRequest Get (NSDictionary @params);

		// -(VKRequest *)search:(NSDictionary *)params;
		[Export ("search:")]
		VKRequest Search (NSDictionary @params);

		// -(VKRequest *)isAppUser;
		[Export ("isAppUser")]
		VKRequest IsAppUser ();

		// -(VKRequest *)isAppUser:(NSInteger)userID;
		[Export ("isAppUser:")]
		VKRequest IsAppUser (nint userId);

		// -(VKRequest *)getSubscriptions;
		[Export ("getSubscriptions")]
		VKRequest GetSubscriptions ();

		// -(VKRequest *)getSubscriptions:(NSDictionary *)params;
		[Export ("getSubscriptions:")]
		VKRequest GetSubscriptions (NSDictionary @params);

		// -(VKRequest *)getFollowers;
		[Export ("getFollowers")]
		VKRequest GetFollowers ();

		// -(VKRequest *)getFollowers:(NSDictionary *)params;
		[Export ("getFollowers:")]
		VKRequest GetFollowers (NSDictionary @params);
	}

	// @interface VKApiFriends : VKApiBase
	[BaseType (typeof(VKApiBase))]
	interface VKApiFriends
	{
		// -(VKRequest *)get;
		[Export ("get")]
		VKRequest Get ();

		// -(VKRequest *)get:(NSDictionary *)params;
		[Export ("get:")]
		VKRequest Get (NSDictionary @params);
	}

	// @interface VKApiPhotos : VKApiBase
	[BaseType (typeof(VKApiBase))]
	interface VKApiPhotos
	{
		// -(VKRequest *)getUploadServer:(NSInteger)albumId;
		[Export ("getUploadServer:")]
		VKRequest GetUploadServer (nint albumId);

		// -(VKRequest *)getUploadServer:(NSInteger)albumId andGroupId:(NSInteger)groupId;
		[Export ("getUploadServer:andGroupId:")]
		VKRequest GetUploadServer (nint albumId, nint groupId);

		// -(VKRequest *)getWallUploadServer;
		[Export ("getWallUploadServer")]
		VKRequest GetWallUploadServer ();

		// -(VKRequest *)getWallUploadServer:(NSInteger)groupId;
		[Export ("getWallUploadServer:")]
		VKRequest GetWallUploadServer (nint groupId);

		// -(VKRequest *)save:(NSDictionary *)params;
		[Export ("save:")]
		VKRequest Save (NSDictionary @params);

		// -(VKRequest *)saveWallPhoto:(NSDictionary *)params;
		[Export ("saveWallPhoto:")]
		VKRequest SaveWallPhoto (NSDictionary @params);
	}

	// @interface VKApiWall : VKApiBase
	[BaseType (typeof(VKApiBase))]
	interface VKApiWall
	{
		// -(VKRequest *)post:(NSDictionary *)params;
		[Export ("post:")]
		VKRequest Post (NSDictionary @params);
	}

	// @interface VKApiCaptcha : VKApiBase
	[BaseType (typeof(VKApiBase))]
	interface VKApiCaptcha
	{
		// -(VKRequest *)force;
		[Export ("force")]
		VKRequest Force ();
	}

	// @interface VKApiGroups : VKApiBase
	[BaseType (typeof(VKApiBase))]
	interface VKApiGroups
	{
		// -(VKRequest *)getById:(NSDictionary *)params;
		[Export ("getById:")]
		VKRequest GetById (NSDictionary @params);
	}

	// @interface VKApiDocs : VKApiBase
	[BaseType(typeof(VKApiBase))]
	interface VKApiDocs
	{
		// -(VKRequest *)get;
		[Export("get")]
		VKRequest Get();

		// -(VKRequest *)get:(NSInteger)count;
		[Export("get:")]
		VKRequest Get(nint count);

		// -(VKRequest *)get:(NSInteger)count andOffset:(NSInteger)offset;
		[Export("get:andOffset:")]
		VKRequest Get(nint count, nint offset);

		// -(VKRequest *)get:(NSInteger)count andOffset:(NSInteger)offset andOwnerID:(NSInteger)ownerID;
		[Export("get:andOffset:andOwnerID:")]
		VKRequest Get(nint count, nint offset, nint ownerID);

		// -(VKRequest *)getByID:(NSArray *)IDs;
		[Export("getByID:")]
		VKRequest GetByID(NSNumber[] IDs);

		// -(VKRequest *)getUploadServer;
		[Export("getUploadServer")]
		VKRequest UploadServer();

		// -(VKRequest *)getUploadServer:(NSInteger)group_id;
		[Export("getUploadServer:")]
		VKRequest GetUploadServer(nint group_id);

		// -(VKRequest *)getWallUploadServer;
		[Export("getWallUploadServer")]
		VKRequest WallUploadServer();

		// -(VKRequest *)getWallUploadServer:(NSInteger)group_id;
		[Export("getWallUploadServer:")]
		VKRequest GetWallUploadServer(nint group_id);

		// -(VKRequest *)save:(NSString *)file;
		[Export("save:")]
		VKRequest Save(string file);

		// -(VKRequest *)save:(NSString *)file andTitle:(NSString *)title;
		[Export("save:andTitle:")]
		VKRequest Save(string file, string title);

		// -(VKRequest *)save:(NSString *)file andTitle:(NSString *)title andTags:(NSString *)tags;
		[Export("save:andTitle:andTags:")]
		VKRequest Save(string file, string title, string tags);

		// -(VKRequest *)delete:(NSInteger)ownerID andDocID:(NSInteger)docID;
		[Export("delete:andDocID:")]
		VKRequest Delete(nint ownerID, nint docID);

		// -(VKRequest *)add:(NSInteger)ownerID andDocID:(NSInteger)docID;
		[Export("add:andDocID:")]
		VKRequest Add(nint ownerID, nint docID);

		// -(VKRequest *)add:(NSInteger)ownerID andDocID:(NSInteger)docID andAccessKey:(NSString *)accessKey;
		[Export("add:andDocID:andAccessKey:")]
		VKRequest Add(nint ownerID, nint docID, string accessKey);
	}

}

namespace VKontakte.Image
{
	// @interface VKImageParameters : VKObject
	[BaseType (typeof(VKObject))]
	interface VKImageParameters
	{
		// @property (assign, nonatomic) VKImageType imageType;
		[Export ("imageType", ArgumentSemantic.Assign)]
		VKImageType ImageType { get; set; }

		// @property (assign, nonatomic) CGFloat jpegQuality;
		[Export ("jpegQuality")]
		nfloat JpegQuality { get; set; }

		// +(instancetype)pngImage;
		[Static]
		[Export ("pngImage")]
		VKImageParameters PngImage ();

		// +(instancetype)jpegImageWithQuality:(float)quality;
		[Static]
		[Export ("jpegImageWithQuality:")]
		VKImageParameters JpegImage (float quality);

		// -(NSString *)fileExtension;
		[Export ("fileExtension")]
		string FileExtension { get; }

		// -(NSString *)mimeType;
		[Export ("mimeType")]
		string MimeType { get; }
	}
}

namespace VKontakte.API.Models
{
	// @interface VKLikes : VKApiObject
	[BaseType (typeof(VKApiObject))]
	interface VKLikes
	{
		// @property (nonatomic, strong) NSNumber * count;
		[Export ("count", ArgumentSemantic.Strong)]
		NSNumber count { get; set; }

		// @property (nonatomic, strong) NSNumber * user_likes;
		[Export ("user_likes", ArgumentSemantic.Strong)]
		NSNumber user_likes { get; set; }

		// @property (nonatomic, strong) NSNumber * can_like;
		[Export ("can_like", ArgumentSemantic.Strong)]
		NSNumber can_like { get; set; }

		// @property (nonatomic, strong) NSNumber * can_publish;
		[Export ("can_publish", ArgumentSemantic.Strong)]
		NSNumber can_publish { get; set; }
	}

	// @interface VKGeoPlace : VKApiObject
	[BaseType (typeof(VKApiObject))]
	interface VKGeoPlace
	{
		// @property (nonatomic, strong) NSNumber * id;
		[Export ("id", ArgumentSemantic.Strong)]
		NSNumber id { get; set; }

		// @property (nonatomic, strong) NSString * title;
		[Export ("title", ArgumentSemantic.Strong)]
		string title { get; set; }

		// @property (nonatomic, strong) NSNumber * latitude;
		[Export ("latitude", ArgumentSemantic.Strong)]
		NSNumber latitude { get; set; }

		// @property (nonatomic, strong) NSNumber * longitude;
		[Export ("longitude", ArgumentSemantic.Strong)]
		NSNumber longitude { get; set; }

		// @property (nonatomic, strong) NSNumber * created;
		[Export ("created", ArgumentSemantic.Strong)]
		NSNumber created { get; set; }

		// @property (nonatomic, strong) NSString * icon;
		[Export ("icon", ArgumentSemantic.Strong)]
		string icon { get; set; }

		// @property (nonatomic, strong) NSNumber * group_id;
		[Export ("group_id", ArgumentSemantic.Strong)]
		NSNumber group_id { get; set; }

		// @property (nonatomic, strong) NSNumber * group_photo;
		[Export ("group_photo", ArgumentSemantic.Strong)]
		NSNumber group_photo { get; set; }

		// @property (nonatomic, strong) NSNumber * checkins;
		[Export ("checkins", ArgumentSemantic.Strong)]
		NSNumber checkins { get; set; }

		// @property (nonatomic, strong) NSNumber * updated;
		[Export ("updated", ArgumentSemantic.Strong)]
		NSNumber updated { get; set; }

		// @property (nonatomic, strong) NSNumber * type;
		[Export ("type", ArgumentSemantic.Strong)]
		NSNumber type { get; set; }

		// @property (nonatomic, strong) NSNumber * country;
		[Export ("country", ArgumentSemantic.Strong)]
		NSNumber country { get; set; }

		// @property (nonatomic, strong) NSString * city;
		[Export ("city", ArgumentSemantic.Strong)]
		string city { get; set; }

		// @property (nonatomic, strong) NSString * address;
		[Export ("address", ArgumentSemantic.Strong)]
		string address { get; set; }

		// @property (nonatomic, strong) NSNumber * showmap;
		[Export ("showmap", ArgumentSemantic.Strong)]
		NSNumber showmap { get; set; }
	}

	// @interface VKGroupContact : VKApiObject
	[BaseType (typeof(VKApiObject))]
	interface VKGroupContact
	{
		// @property (nonatomic, strong) NSNumber * user_id;
		[Export ("user_id", ArgumentSemantic.Strong)]
		NSNumber user_id { get; set; }

		// @property (nonatomic, strong) NSString * desc;
		[Export ("desc", ArgumentSemantic.Strong)]
		string desc { get; set; }

		// @property (nonatomic, strong) NSString * email;
		[Export ("email", ArgumentSemantic.Strong)]
		string email { get; set; }
	}

	// @interface VKGroupContacts : VKApiObjectArray
	[BaseType (typeof(VKApiObjectArray))]
	interface VKGroupContacts
	{
	}

	// @interface VKGroupLink : VKApiObject
	[BaseType (typeof(VKApiObject))]
	interface VKGroupLink
	{
		// @property (nonatomic, strong) NSString * url;
		[Export ("url", ArgumentSemantic.Strong)]
		string url { get; set; }

		// @property (nonatomic, strong) NSString * name;
		[Export ("name", ArgumentSemantic.Strong)]
		string name { get; set; }

		// @property (nonatomic, strong) NSString * desc;
		[Export ("desc", ArgumentSemantic.Strong)]
		string desc { get; set; }

		// @property (nonatomic, strong) NSString * photo_50;
		[Export ("photo_50", ArgumentSemantic.Strong)]
		string photo_50 { get; set; }

		// @property (nonatomic, strong) NSString * photo_100;
		[Export ("photo_100", ArgumentSemantic.Strong)]
		string photo_100 { get; set; }
	}

	// @interface VKGroupLinks : VKApiObjectArray
	[BaseType (typeof(VKApiObjectArray))]
	interface VKGroupLinks
	{
	}

	// @interface VKGroup : VKApiObject
	[BaseType (typeof(VKApiObject))]
	interface VKGroup
	{
		// @property (nonatomic, strong) NSNumber * id;
		[Export ("id", ArgumentSemantic.Strong)]
		NSNumber id { get; set; }

		// @property (nonatomic, strong) NSString * name;
		[Export ("name", ArgumentSemantic.Strong)]
		string name { get; set; }

		// @property (nonatomic, strong) NSString * screen_name;
		[Export ("screen_name", ArgumentSemantic.Strong)]
		string screen_name { get; set; }

		// @property (nonatomic, strong) NSNumber * is_closed;
		[Export ("is_closed", ArgumentSemantic.Strong)]
		NSNumber is_closed { get; set; }

		// @property (nonatomic, strong) NSString * type;
		[Export ("type", ArgumentSemantic.Strong)]
		string type { get; set; }

		// @property (nonatomic, strong) NSNumber * is_admin;
		[Export ("is_admin", ArgumentSemantic.Strong)]
		NSNumber is_admin { get; set; }

		// @property (nonatomic, strong) NSNumber * admin_level;
		[Export ("admin_level", ArgumentSemantic.Strong)]
		NSNumber admin_level { get; set; }

		// @property (nonatomic, strong) NSNumber * is_member;
		[Export ("is_member", ArgumentSemantic.Strong)]
		NSNumber is_member { get; set; }

		// @property (nonatomic, strong) VKCity * city;
		[Export ("city", ArgumentSemantic.Strong)]
		VKCity City { get; set; }

		// @property (nonatomic, strong) VKCountry * country;
		[Export ("country", ArgumentSemantic.Strong)]
		VKCountry country { get; set; }

		// @property (nonatomic, strong) VKGeoPlace * place;
		[Export ("place", ArgumentSemantic.Strong)]
		VKGeoPlace place { get; set; }

		// @property (nonatomic, strong) NSString * description;
		[Export ("description", ArgumentSemantic.Strong)]
		string description { get; set; }

		// @property (nonatomic, strong) NSString * wiki_page;
		[Export ("wiki_page", ArgumentSemantic.Strong)]
		string wiki_page { get; set; }

		// @property (nonatomic, strong) NSNumber * members_count;
		[Export ("members_count", ArgumentSemantic.Strong)]
		NSNumber members_count { get; set; }

		// @property (nonatomic, strong) VKCounters * counters;
		[Export ("counters", ArgumentSemantic.Strong)]
		VKCounters counters { get; set; }

		// @property (nonatomic, strong) NSNumber * start_date;
		[Export ("start_date", ArgumentSemantic.Strong)]
		NSNumber start_date { get; set; }

		// @property (nonatomic, strong) NSNumber * end_date;
		[Export ("end_date", ArgumentSemantic.Strong)]
		NSNumber end_date { get; set; }

		// @property (nonatomic, strong) NSNumber * finish_date;
		[Export ("finish_date", ArgumentSemantic.Strong)]
		NSNumber finish_date { get; set; }

		// @property (nonatomic, strong) NSNumber * can_post;
		[Export ("can_post", ArgumentSemantic.Strong)]
		NSNumber can_post { get; set; }

		// @property (nonatomic, strong) NSNumber * can_see_all_posts;
		[Export ("can_see_all_posts", ArgumentSemantic.Strong)]
		NSNumber can_see_all_posts { get; set; }

		// @property (nonatomic, strong) NSNumber * can_create_topic;
		[Export ("can_create_topic", ArgumentSemantic.Strong)]
		NSNumber can_create_topic { get; set; }

		// @property (nonatomic, strong) NSNumber * can_upload_doc;
		[Export ("can_upload_doc", ArgumentSemantic.Strong)]
		NSNumber can_upload_doc { get; set; }

		// @property (nonatomic, strong) NSString * activity;
		[Export ("activity", ArgumentSemantic.Strong)]
		string activity { get; set; }

		// @property (nonatomic, strong) NSString * status;
		[Export ("status", ArgumentSemantic.Strong)]
		string status { get; set; }

		// @property (nonatomic, strong) VKAudio * status_audio;
		[Export ("status_audio", ArgumentSemantic.Strong)]
		VKAudio status_audio { get; set; }

		// @property (nonatomic, strong) VKGroupContacts * contacts;
		[Export ("contacts", ArgumentSemantic.Strong)]
		VKGroupContacts contacts { get; set; }

		// @property (nonatomic, strong) VKGroupLinks * links;
		[Export ("links", ArgumentSemantic.Strong)]
		VKGroupLinks links { get; set; }

		// @property (nonatomic, strong) NSNumber * fixed_post;
		[Export ("fixed_post", ArgumentSemantic.Strong)]
		NSNumber fixed_post { get; set; }

		// @property (nonatomic, strong) NSNumber * verified;
		[Export ("verified", ArgumentSemantic.Strong)]
		NSNumber verified { get; set; }

		// @property (nonatomic, strong) NSString * site;
		[Export ("site", ArgumentSemantic.Strong)]
		string site { get; set; }

		// @property (nonatomic, strong) NSString * photo_50;
		[Export ("photo_50", ArgumentSemantic.Strong)]
		string photo_50 { get; set; }

		// @property (nonatomic, strong) NSString * photo_100;
		[Export ("photo_100", ArgumentSemantic.Strong)]
		string photo_100 { get; set; }

		// @property (nonatomic, strong) NSString * photo_200;
		[Export ("photo_200", ArgumentSemantic.Strong)]
		string photo_200 { get; set; }

		// @property (nonatomic, strong) NSString * photo_max_orig;
		[Export ("photo_max_orig", ArgumentSemantic.Strong)]
		string photo_max_orig { get; set; }

		// @property (nonatomic, strong) NSNumber * is_request;
		[Export ("is_request", ArgumentSemantic.Strong)]
		NSNumber is_request { get; set; }

		// @property (nonatomic, strong) NSNumber * is_invite;
		[Export ("is_invite", ArgumentSemantic.Strong)]
		NSNumber is_invite { get; set; }

		// @property (nonatomic, strong) VKPhotoArray * photos;
		[Export ("photos", ArgumentSemantic.Strong)]
		VKPhotoArray photos { get; set; }

		// @property (nonatomic, strong) NSNumber * photos_count;
		[Export ("photos_count", ArgumentSemantic.Strong)]
		NSNumber photos_count { get; set; }

		// @property (nonatomic, strong) NSNumber * invited_by;
		[Export ("invited_by", ArgumentSemantic.Strong)]
		NSNumber invited_by { get; set; }

		// @property (assign, nonatomic) NSInteger invite_state;
		[Export ("invite_state")]
		nint invite_state { get; set; }

		// @property (nonatomic, strong) NSString * deactivated;
		[Export ("deactivated", ArgumentSemantic.Strong)]
		string deactivated { get; set; }

		// @property (nonatomic, strong) NSNumber * blacklisted;
		[Export ("blacklisted", ArgumentSemantic.Strong)]
		NSNumber blacklisted { get; set; }
	}

	// @interface VKGroups : VKApiObjectArray
	[BaseType (typeof(VKApiObjectArray))]
	interface VKGroups
	{
	}
}

namespace VKontakte.API
{
	// @interface VKApi : NSObject
	[BaseType (typeof(NSObject))]
	interface VKApi
	{
		// +(VKApiUsers *)users;
		[Static]
		[Export ("users")]
		VKApiUsers Users { get; }

		// +(VKApiWall *)wall;
		[Static]
		[Export ("wall")]
		VKApiWall Wall { get; }

		// +(VKApiPhotos *)photos;
		[Static]
		[Export ("photos")]
		VKApiPhotos Photos { get; }

		// +(VKApiFriends *)friends;
		[Static]
		[Export ("friends")]
		VKApiFriends Friends { get; }

		// +(VKApiGroups *)groups;
		[Static]
		[Export ("groups")]
		VKApiGroups Groups { get; }

//		// +(VKRequest *)requestWithMethod:(NSString *)method andParameters:(NSDictionary *)parameters andHttpMethod:(NSString *)httpMethod __attribute__((deprecated("")));
//		[Static]
//		[Export ("requestWithMethod:andParameters:andHttpMethod:")]
//		VKRequest Request (string method, NSDictionary parameters, string httpMethod);

		// +(VKRequest *)requestWithMethod:(NSString *)method andParameters:(NSDictionary *)parameters;
		[Static]
		[Export ("requestWithMethod:andParameters:")]
		VKRequest Request (string method, NSDictionary parameters);

		// +(VKRequest *)uploadWallPhotoRequest:(UIImage *)image parameters:(VKImageParameters *)parameters userId:(NSInteger)userId groupId:(NSInteger)groupId;
		[Static]
		[Export ("uploadWallPhotoRequest:parameters:userId:groupId:")]
		VKRequest UploadWallPhotoRequest (UIImage image, VKImageParameters parameters, nint userId, nint groupId);

		// +(VKRequest *)uploadAlbumPhotoRequest:(UIImage *)image parameters:(VKImageParameters *)parameters albumId:(NSInteger)albumId groupId:(NSInteger)groupId;
		[Static]
		[Export ("uploadAlbumPhotoRequest:parameters:albumId:groupId:")]
		VKRequest UploadAlbumPhotoRequest (UIImage image, VKImageParameters parameters, nint albumId, nint groupId);

		// +(VKRequest *)uploadMessagePhotoRequest:(UIImage *)image parameters:(VKImageParameters *)parameters;
		[Static]
		[Export ("uploadMessagePhotoRequest:parameters:")]
		VKRequest UploadMessagePhotoRequest (UIImage image, VKImageParameters parameters);
	}
}

namespace VKontakte
{
	// @interface VKAuthorizationResult : VKObject
	[BaseType (typeof(VKObject))]
	interface VKAuthorizationResult
	{
		// @property (readonly, nonatomic, strong) VKAccessToken * token;
		[Export ("token", ArgumentSemantic.Strong)]
		VKAccessToken Token { get; }

		// @property (readonly, nonatomic, strong) VKUser * user;
		[Export ("user", ArgumentSemantic.Strong)]
		VKUser User { get; }

		// @property (readonly, nonatomic, strong) NSError * error;
		[Export ("error", ArgumentSemantic.Strong)]
		NSError Error { get; }

		// @property (readonly, assign, nonatomic) VKAuthorizationState state;
		[Export ("state", ArgumentSemantic.Assign)]
		VKAuthorizationState State { get; }
	}

	// @interface VKMutableAuthorizationResult : VKAuthorizationResult
	[BaseType (typeof(VKAuthorizationResult))]
	interface VKMutableAuthorizationResult
	{
		// @property (readwrite, nonatomic, strong) VKAccessToken * token;
		[Export ("token", ArgumentSemantic.Strong)]
		VKAccessToken Token { get; set; }

		// @property (readwrite, nonatomic, strong) VKUser * user;
		[Export ("user", ArgumentSemantic.Strong)]
		VKUser User { get; set; }

		// @property (readwrite, nonatomic, strong) NSError * error;
		[Export ("error", ArgumentSemantic.Strong)]
		NSError Error { get; set; }

		// @property (assign, readwrite, nonatomic) VKAuthorizationState state;
		[Export ("state", ArgumentSemantic.Assign)]
		VKAuthorizationState State { get; set; }
	}
}

namespace VKontakte
{
	[Static]
	partial interface VKPermissions
	{
		// extern NSString *const VK_PER_NOTIFY;
		[Field ("VK_PER_NOTIFY", "__Internal")]
		NSString Notify { get; }

		// extern NSString *const VK_PER_FRIENDS;
		[Field ("VK_PER_FRIENDS", "__Internal")]
		NSString Friends { get; }

		// extern NSString *const VK_PER_PHOTOS;
		[Field ("VK_PER_PHOTOS", "__Internal")]
		NSString Photos { get; }

		// extern NSString *const VK_PER_AUDIO;
		[Field ("VK_PER_AUDIO", "__Internal")]
		NSString Audio { get; }

		// extern NSString *const VK_PER_VIDEO;
		[Field ("VK_PER_VIDEO", "__Internal")]
		NSString Video { get; }

		// extern NSString *const VK_PER_DOCS;
		[Field ("VK_PER_DOCS", "__Internal")]
		NSString Docs { get; }

		// extern NSString *const VK_PER_NOTES;
		[Field ("VK_PER_NOTES", "__Internal")]
		NSString Notes { get; }

		// extern NSString *const VK_PER_PAGES;
		[Field ("VK_PER_PAGES", "__Internal")]
		NSString Pages { get; }

		// extern NSString *const VK_PER_STATUS;
		[Field ("VK_PER_STATUS", "__Internal")]
		NSString Status { get; }

		// extern NSString *const VK_PER_WALL;
		[Field ("VK_PER_WALL", "__Internal")]
		NSString Wall { get; }

		// extern NSString *const VK_PER_GROUPS;
		[Field ("VK_PER_GROUPS", "__Internal")]
		NSString Groups { get; }

		// extern NSString *const VK_PER_MESSAGES;
		[Field ("VK_PER_MESSAGES", "__Internal")]
		NSString Messages { get; }

		// extern NSString *const VK_PER_NOTIFICATIONS;
		[Field ("VK_PER_NOTIFICATIONS", "__Internal")]
		NSString Notifications { get; }

		// extern NSString *const VK_PER_STATS;
		[Field ("VK_PER_STATS", "__Internal")]
		NSString Stats { get; }

		// extern NSString *const VK_PER_ADS;
		[Field ("VK_PER_ADS", "__Internal")]
		NSString Ads { get; }

		// extern NSString *const VK_PER_OFFLINE;
		[Field ("VK_PER_OFFLINE", "__Internal")]
		NSString Offline { get; }

		// extern NSString *const VK_PER_NOHTTPS;
		[Field ("VK_PER_NOHTTPS", "__Internal")]
		NSString NoHttps { get; }

		// extern NSString *const VK_PER_EMAIL;
		[Field ("VK_PER_EMAIL", "__Internal")]
		NSString Email { get; }

		// extern NSString *const VK_PER_EMAIL;
		[Field ("VK_PER_MARKET", "__Internal")]
		NSString Market { get; }
	}
}

namespace VKontakte.Utils
{
	// @interface VKUtil : NSObject
	[BaseType (typeof(NSObject))]
	interface VKUtil
	{
		// +(NSDictionary *)explodeQueryString:(NSString *)queryString;
		[Static]
		[Export ("explodeQueryString:")]
		NSDictionary ExplodeQueryString (string queryString);

		// +(NSString *)generateGUID;
		[Static]
		[Export ("generateGUID")]
		string GenerateGUID ();

		// +(NSNumber *)parseNumberString:(id)number;
		[Static]
		[Export ("parseNumberString:")]
		NSNumber ParseNumberString (NSObject number);

		// +(UIColor *)colorWithRGB:(NSInteger)rgb;
		[Static]
		[Export ("colorWithRGB:")]
		UIColor ColorWithRGB (nint rgb);

		// +(NSString *)queryStringFromParams:(NSDictionary *)params;
		[Static]
		[Export ("queryStringFromParams:")]
		string QueryStringFromParams (NSDictionary @params);
	}

	// @interface RoundedImage (UIImage)
	[Category]
	[BaseType (typeof(UIImage))]
	interface UIImage_RoundedImage
	{
		// -(UIImage *)vks_roundCornersImage:(CGFloat)cornerRadius resultSize:(CGSize)imageSize;
		[Export ("vks_roundCornersImage:resultSize:")]
		UIImage CreateRoundCornersImage (nfloat cornerRadius, CGSize imageSize);
	}
}

namespace VKontakte.Views
{
	// @interface VKCaptchaViewController : UIViewController
	[BaseType (typeof(UIViewController))]
	interface VKCaptchaViewController
	{
		// +(instancetype)captchaControllerWithError:(VKError *)error;
		[Static]
		[Export ("captchaControllerWithError:")]
		VKCaptchaViewController Create (VKError error);

		// -(void)presentIn:(UIViewController *)viewController;
		[Export ("presentIn:")]
		void PresentIn (UIViewController viewController);
	}
}

namespace VKontakte
{
	// @interface VKBatchRequest : VKObject
	[BaseType (typeof(VKObject))]
	interface VKBatchRequest
	{
		// @property (copy, nonatomic) void (^completeBlock)(NSArray *);
		[Export ("completeBlock", ArgumentSemantic.Copy)]
		Action<VKResponse[]> CompleteBlock { get; set; }

		// @property (copy, nonatomic) void (^errorBlock)(NSError *);
		[Export ("errorBlock", ArgumentSemantic.Copy)]
		Action<NSError> ErrorBlock { get; set; }

		// -(instancetype)initWithRequests:(VKRequest *)firstRequest, ... __attribute__((sentinel(0, 1)));
		[Internal]
		[Export ("initWithRequests:", IsVariadic = true)]
		IntPtr Constructor (VKRequest firstRequest, IntPtr varArgs);

		// -(instancetype)initWithRequestsArray:(NSArray *)requests;
		[Export ("initWithRequestsArray:")]
		IntPtr Constructor (VKRequest[] requests);

		// -(void)executeWithResultBlock:(void (^)(NSArray *))completeBlock errorBlock:(void (^)(NSError *))errorBlock;
		[Export ("executeWithResultBlock:errorBlock:")]
		void Execute (Action<VKResponse[]> completeBlock, Action<NSError> errorBlock);

		// -(void)cancel;
		[Export ("cancel")]
		void Cancel ();
	}
}

namespace VKontakte.Image
{
	// @interface VKUploadImage : VKObject
	[BaseType (typeof(VKObject))]
	interface VKUploadImage
	{
		// @property (nonatomic, strong) NSData * imageData;
		[Export ("imageData", ArgumentSemantic.Strong)]
		NSData ImageData { get; set; }

		// @property (nonatomic, strong) UIImage * sourceImage;
		[Export ("sourceImage", ArgumentSemantic.Strong)]
		UIImage SourceImage { get; set; }

		// @property (nonatomic, strong) VKImageParameters * parameters;
		[Export ("parameters", ArgumentSemantic.Strong)]
		VKImageParameters Parameters { get; set; }

		// +(instancetype)uploadImageWithData:(NSData *)data andParams:(VKImageParameters *)params;
		[Static]
		[Export ("uploadImageWithData:andParams:")]
		VKUploadImage Create (NSData data, VKImageParameters @params);

		// +(instancetype)uploadImageWithImage:(UIImage *)image andParams:(VKImageParameters *)params;
		[Static]
		[Export ("uploadImageWithImage:andParams:")]
		VKUploadImage Create (UIImage image, VKImageParameters @params);
	}
}

namespace VKontakte.Views
{
	// @interface VKShareLink : VKObject
	[BaseType (typeof(VKObject))]
	interface VKShareLink
	{
		// @property (copy, nonatomic) NSString * title;
		[Export ("title")]
		string Title { get; set; }

		// @property (copy, nonatomic) NSURL * link;
		[Export ("link", ArgumentSemantic.Copy)]
		NSUrl Link { get; set; }

		// -(instancetype)initWithTitle:(NSString *)title link:(NSURL *)link;
		[Export ("initWithTitle:link:")]
		IntPtr Constructor (string title, NSUrl link);
	}

	// @interface VKShareDialogController : UIViewController
	[BaseType (typeof(UIViewController))]
	interface VKShareDialogController
	{
		// @property (nonatomic, strong) NSArray * uploadImages;
		[Export ("uploadImages", ArgumentSemantic.Strong)]
		VKUploadImage[] UploadImages { get; set; }

		// @property (nonatomic, strong) NSArray * vkImages;
		[Export ("vkImages", ArgumentSemantic.Strong)]
		string[] Images { get; set; }

		// @property (nonatomic, strong) VKShareLink * shareLink;
		[Export ("shareLink", ArgumentSemantic.Strong)]
		VKShareLink ShareLink { get; set; }

		// @property (copy, nonatomic) NSString * text;
		[Export ("text")]
		string Text { get; set; }

		// @property (nonatomic, strong) NSArray * requestedScope;
		[Export ("requestedScope", ArgumentSemantic.Strong)]
		string[] RequestedScope { get; set; }

		// @property (copy, nonatomic) void (^completionHandler)(VKShareDialogController *, VKShareDialogControllerResult);
		[Export ("completionHandler", ArgumentSemantic.Copy)]
		Action<VKShareDialogController, VKShareDialogControllerResult> CompletionHandler { get; set; }

		// @property (assign, nonatomic) BOOL dismissAutomatically;
		[Export ("dismissAutomatically")]
		bool DismissAutomatically { get; set; }

		// @property (readonly, copy, nonatomic) NSString * postId;
		[Export ("postId")]
		string PostId { get; }
	}
}

namespace VKontakte
{
	interface IVKSdkDelegate
	{
	}

	// @protocol VKSdkDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface VKSdkDelegate
	{
		// @required -(void)vkSdkAccessAuthorizationFinishedWithResult:(VKAuthorizationResult *)result;
		[Abstract]
		[Export ("vkSdkAccessAuthorizationFinishedWithResult:")]
		void AccessAuthorizationFinished (VKAuthorizationResult result);

		// @required -(void)vkSdkUserAuthorizationFailed;
		[Abstract]
		[Export ("vkSdkUserAuthorizationFailed")]
		void UserAuthorizationFailed ();

		// @optional -(void)vkSdkAuthorizationStateUpdatedWithResult:(VKAuthorizationResult*)result;
		[Export ("vkSdkAuthorizationStateUpdatedWithResult:")]
		void AuthorizationStateUpdated (VKAuthorizationResult result);

		// @optional -(void)vkSdkAccessTokenUpdated:(VKAccessToken *)newToken oldToken:(VKAccessToken *)oldToken;
		[Export ("vkSdkAccessTokenUpdated:oldToken:")]
		void AccessTokenUpdated (VKAccessToken newToken, VKAccessToken oldToken);

		// @optional -(void)vkSdkTokenHasExpired:(VKAccessToken *)expiredToken;
		[Export ("vkSdkTokenHasExpired:")]
		void TokenHasExpired (VKAccessToken expiredToken);
	}

	interface IVKSdkUIDelegate
	{
	}

	// @protocol VKSdkUIDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface VKSdkUIDelegate
	{
		// @required -(void)vkSdkShouldPresentViewController:(UIViewController *)controller;
		[Abstract]
		[Export ("vkSdkShouldPresentViewController:")]
		void ShouldPresentViewController (UIViewController controller);

		// @required -(void)vkSdkNeedCaptchaEnter:(VKError *)captchaError;
		[Abstract]
		[Export ("vkSdkNeedCaptchaEnter:")]
		void NeedCaptchaEnter (VKError captchaError);

		// @optional -(void)vkSdkWillDismissViewController:(UIViewController *)controller;
		[Export ("vkSdkWillDismissViewController:")]
		void WillDismissViewController (UIViewController controller);

		// @optional -(void)vkSdkDidDismissViewController:(UIViewController *)controller;
		[Export ("vkSdkDidDismissViewController:")]
		void DidDismissViewController (UIViewController controller);
	}

	// @interface VKSdk : NSObject
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface VKSdk
	{
		// @property (readwrite, nonatomic, weak) id<VKSdkUIDelegate> _Nullable uiDelegate;
		[NullAllowed, Export ("uiDelegate", ArgumentSemantic.Weak)]
		IVKSdkUIDelegate UiDelegate { get; set; }

		// @property (readonly, copy, nonatomic) NSString * currentAppId;
		[Export ("currentAppId")]
		string CurrentAppId { get; }

		// @property (readonly, copy, nonatomic) NSString * apiVersion;
		[Export ("apiVersion")]
		string ApiVersion { get; }

		// +(instancetype)instance;
		[Static]
		[Export ("instance")]
		VKSdk Instance { get; }

		// +(BOOL)initialized;
		[Static]
		[Export("initialized")]
		bool Initialized { get; }

		// +(instancetype)initializeWithAppId:(NSString *)appId;
		[Static]
		[Export ("initializeWithAppId:")]
		VKSdk Initialize (string appId);

		// +(instancetype)initializeWithAppId:(NSString *)appId apiVersion:(NSString *)version;
		[Static]
		[Export ("initializeWithAppId:apiVersion:")]
		VKSdk Initialize (string appId, string version);

		// -(void)registerDelegate:(id<VKSdkDelegate>)delegate;
		[Export ("registerDelegate:")]
		void RegisterDelegate (IVKSdkDelegate @delegate);

		// -(void)unregisterDelegate:(id<VKSdkDelegate>)delegate;
		[Export ("unregisterDelegate:")]
		void UnregisterDelegate (IVKSdkDelegate @delegate);

		// +(void)authorize:(NSArray *)permissions;
		[Static]
		[Export ("authorize:")]
		void Authorize (string[] permissions);

		// +(void)authorize:(NSArray *)permissions withOptions:(VKAuthorizationOptions)options;
		[Static]
		[Export ("authorize:withOptions:")]
		void Authorize (string[] permissions, VKAuthorizationOptions options);

		// +(VKAccessToken *)accessToken;
		[Static]
		[Export ("accessToken")]
		VKAccessToken AccessToken { get; }

		// +(BOOL)processOpenURL:(NSURL *)passedUrl fromApplication:(NSString *)sourceApplication;
		[Static]
		[Export ("processOpenURL:fromApplication:")]
		bool ProcessOpenUrl (NSUrl passedUrl, string sourceApplication);

		// +(BOOL)isLoggedIn;
		[Static]
		[Export ("isLoggedIn")]
		bool IsLoggedIn { get; }

		// +(void)wakeUpSession:(NSArray *)permissions completeBlock:(void (^)(VKAuthorizationState, NSError *))wakeUpBlock;
		[Static]
		[Export ("wakeUpSession:completeBlock:")]
		void WakeUpSession (string[] permissions, Action<VKAuthorizationState, NSError> wakeUpBlock);

		// +(void)forceLogout;
		[Static]
		[Export ("forceLogout")]
		void ForceLogout ();

		// +(BOOL)vkAppMayExists;
		[Static]
		[Export ("vkAppMayExists")]
		bool VkAppMayExists { get; }

		// -(BOOL)hasPermissions:(NSArray *)permissions;
		[Export ("hasPermissions:")]
		bool HasPermissions (string[] permissions);

		// +(void)setSchedulerEnabled:(BOOL)enabled;
		[Static]
		[Export ("setSchedulerEnabled:")]
		void SetSchedulerEnabled (bool enabled);
	}

	// @interface HttpsRequired (VKAccessToken)
	[Category]
	[BaseType (typeof(VKAccessToken))]
	interface VKAccessToken_HttpsRequired
	{
		// -(void)setAccessTokenRequiredHTTPS;
		[Export ("setAccessTokenRequiredHTTPS")]
		void SetAccessTokenRequiredHTTPS ();

		// -(void)notifyTokenExpired;
		[Export ("notifyTokenExpired")]
		void NotifyTokenExpired ();
	}

	// @interface CaptchaRequest (VKError)
	[Category]
	[BaseType (typeof(VKError))]
	interface VKError_CaptchaRequest
	{
		// -(void)notifyCaptchaRequired;
		[Export ("notifyCaptchaRequired")]
		void NotifyCaptchaRequired ();

		// -(void)notiftAuthorizationFailed;
		[Export ("notiftAuthorizationFailed")]
		void NotiftAuthorizationFailed ();
	}

	// @interface VKController (UIViewController)
	[Category]
	[BaseType (typeof(UIViewController))]
	interface UIViewController_VKController
	{
		// -(void)vks_presentViewControllerThroughDelegate;
		[Export ("vks_presentViewControllerThroughDelegate")]
		void PresentViewControllerThroughDelegate ();

		// -(void)vks_viewControllerWillDismiss;
		[Export ("vks_viewControllerWillDismiss")]
		void ViewControllerWillDismiss ();

		// -(void)vks_viewControllerDidDismiss;
		[Export ("vks_viewControllerDidDismiss")]
		void ViewControllerDidDismiss ();
	}
}

namespace VKontakte.Views
{
	// @interface VKAuthorizationContext : VKObject
	[BaseType (typeof(VKObject))]
	interface VKAuthorizationContext
	{
		// @property (readonly, nonatomic, strong) NSString * clientId;
		[Export("clientId", ArgumentSemantic.Strong)]
		string ClientId { get; }

		// @property (readonly, nonatomic, strong) NSString * displayType;
		[Export("displayType", ArgumentSemantic.Strong)]
		string DisplayType { get; }

		// @property (readonly, nonatomic, strong) NSArray<NSString *> * scope;
		[Export("scope", ArgumentSemantic.Strong)]
		string[] Scope { get; }

		// @property (readonly, nonatomic) BOOL revoke;
		[Export("revoke")]
		bool Revoke { get; }

		// +(instancetype)contextWithAuthType:(VKAuthorizationType)authType clientId:(NSString *)clientId displayType:(NSString *)displayType scope:(NSArray<NSString *> *)scope revoke:(BOOL)revoke;
		[Static]
		[Export("contextWithAuthType:clientId:displayType:scope:revoke:")]
		VKAuthorizationContext Create (VKAuthorizationType authType, string clientId, string displayType, string[] scope, bool revoke);
	}

	// @interface VKAuthorizeController : UIViewController <UIWebViewDelegate>
	[BaseType (typeof(UIViewController))]
	interface VKAuthorizeController : IUIWebViewDelegate
	{
		// +(void)presentForAuthorizeWithAppId:(NSString *)appId andPermissions:(NSArray *)permissions revokeAccess:(BOOL)revoke displayType:(VKDisplayType)displayType;
		[Static]
		[Export ("presentForAuthorizeWithAppId:andPermissions:revokeAccess:displayType:")]
		void PresentForAuthorize (string appId, string[] permissions, bool revoke, string displayType);

		// +(void)presentForValidation:(VKError *)validationError;
		[Static]
		[Export ("presentForValidation:")]
		void PresentForValidation (VKError validationError);

		// +(NSURL *)buildAuthorizationURLWithContext:(VKAuthorizationContext *)ctx;
		[Static]
		[Export("buildAuthorizationURLWithContext:")]
		NSUrl BuildAuthorizationUrl (VKAuthorizationContext ctx);
	}
}

namespace VKontakte
{
	// @interface VKBundle : VKObject
	[BaseType (typeof(VKObject))]
	interface VKBundle
	{
		// +(NSBundle *)vkLibraryResourcesBundle;
		[Static]
		[Export ("vkLibraryResourcesBundle")]
		NSBundle LibraryResourcesBundle { get; }

		// +(UIImage *)vkLibraryImageNamed:(NSString *)name;
		[Static]
		[Export ("vkLibraryImageNamed:")]
		UIImage LibraryImageNamed (string name);

		// +(NSString *)localizedString:(NSString *)string;
		[Static]
		[Export ("localizedString:")]
		string LocalizedString (string @string);
	}
}

namespace VKontakte.Views
{
	// @interface VKCaptchaView : UIView
	[BaseType (typeof(UIView))]
	interface VKCaptchaView
	{
		// extern CGFloat kCaptchaImageWidth;
		[Static]
		[Field ("kCaptchaImageWidth", "__Internal")]
		nfloat CaptchaImageWidth { get; }

		// extern CGFloat kCaptchaImageHeight;
		[Static]
		[Field ("kCaptchaImageHeight", "__Internal")]
		nfloat CaptchaImageHeight { get; }

		// extern CGFloat kCaptchaViewHeight;
		[Static]
		[Field ("kCaptchaViewHeight", "__Internal")]
		nfloat CaptchaViewHeight { get; }


		// -(id)initWithFrame:(CGRect)frame andError:(VKError *)captchaError;
		[Export ("initWithFrame:andError:")]
		IntPtr Constructor (CGRect frame, VKError captchaError);
	}
}

namespace VKontakte.Core
{
	// @interface VKHTTPClient : VKObject <NSCoding>
	[BaseType (typeof(VKObject))]
	interface VKHTTPClient : INSCoding
	{
		// +(instancetype)getClient;
		[Static]
		[Export ("getClient")]
		VKHTTPClient Instance { get; }

		// @property (readonly, nonatomic, strong) NSOperationQueue * operationQueue;
		[Export ("operationQueue", ArgumentSemantic.Strong)]
		NSOperationQueue OperationQueue { get; }

		// -(NSString *)defaultValueForHeader:(NSString *)header;
		[Export ("defaultValueForHeader:")]
		string DefaultValueForHeader (string header);

		// -(void)setDefaultHeader:(NSString *)header value:(NSString *)value;
		[Export ("setDefaultHeader:value:")]
		void SetDefaultHeader (string header, string value);

		// -(NSMutableURLRequest *)requestWithMethod:(NSString *)method path:(NSString *)path parameters:(NSDictionary *)parameters secure:(BOOL)secure;
		[Export ("requestWithMethod:path:parameters:secure:")]
		NSMutableUrlRequest Request (string method, string path, NSDictionary parameters, bool secure);

		// -(NSMutableURLRequest *)multipartFormRequestWithMethod:(NSString *)method path:(NSString *)path images:(NSArray *)images;
		[Export ("multipartFormRequestWithMethod:path:images:")]
		NSMutableUrlRequest MultipartFormRequest (string method, string path, VKUploadImage[] images);

		// -(void)enqueueOperation:(NSOperation *)operation;
		[Export ("enqueueOperation:")]
		void EnqueueOperation (NSOperation operation);

		// -(void)enqueueBatchOfHTTPRequestOperations:(NSArray *)operations progressBlock:(void (^)(NSUInteger, NSUInteger))progressBlock completionBlock:(void (^)(NSArray *))completionBlock;
		[Export ("enqueueBatchOfHTTPRequestOperations:progressBlock:completionBlock:")]
		void EnqueueOperations (NSOperation[] operations, Action<nuint, nuint> progressBlock, Action<NSOperation[]> completionBlock);
	}

	// @interface VKOperation : NSOperation
	[BaseType (typeof(NSOperation))]
	interface VKOperation
	{
		// @property (assign, readwrite, nonatomic) VKOperationState state;
		[Export ("state", ArgumentSemantic.Assign)]
		VKOperationState State { get; set; }

		// @property (readwrite, nonatomic, strong) NSRecursiveLock * lock;
		[Export ("lock", ArgumentSemantic.Strong)]
		NSRecursiveLock Lock { get; set; }

		// @property (assign, nonatomic) dispatch_queue_t responseQueue;
		[Export ("responseQueue", ArgumentSemantic.Assign)]
		DispatchQueue ResponseQueue { get; set; }
	}

	// @interface VKHTTPOperation : VKOperation <NSURLConnectionDataDelegate, NSURLConnectionDelegate, NSCoding, NSCopying>
	[BaseType (typeof(VKOperation))]
	interface VKHTTPOperation : INSUrlConnectionDataDelegate, INSUrlConnectionDelegate, INSCoding, INSCopying
	{
		// extern NSString *const VKNetworkingOperationDidStart;
		[Static]
		[Field ("VKNetworkingOperationDidStart", "__Internal")]
		NSString NetworkingOperationDidStart { get; }


		// @property (nonatomic, strong) VKRequest * loadingRequest;
		[Export ("loadingRequest", ArgumentSemantic.Strong)]
		VKRequest LoadingRequest { get; set; }

		// +(instancetype)operationWithRequest:(VKRequest *)request;
		[Static]
		[Export ("operationWithRequest:")]
		VKHTTPOperation Create (VKRequest request);

		// @property (nonatomic, strong) NSSet * runLoopModes;
		[Export ("runLoopModes", ArgumentSemantic.Strong)]
		NSSet RunLoopModes { get; set; }

		// @property (readonly, nonatomic, weak) VKRequest * _Nullable vkRequest;
		[NullAllowed, Export ("vkRequest", ArgumentSemantic.Weak)]
		VKRequest VKRequest { get; }

		// @property (readonly, nonatomic, strong) NSURLRequest * request;
		[Export ("request", ArgumentSemantic.Strong)]
		NSUrlRequest Request { get; }

		// @property (readonly, nonatomic, strong) NSError * error;
		[Export ("error", ArgumentSemantic.Strong)]
		NSError Error { get; }

		// @property (readonly, nonatomic, strong) NSData * responseData;
		[Export ("responseData", ArgumentSemantic.Strong)]
		NSData ResponseData { get; }

		// @property (readonly, copy, nonatomic) NSString * responseString;
		[Export ("responseString")]
		string ResponseString { get; }

		// @property (readonly, copy, nonatomic) id responseJson;
		[Export ("responseJson", ArgumentSemantic.Copy)]
		NSObject ResponseJson { get; }

		// @property (readonly, nonatomic, strong) NSHTTPURLResponse * response;
		[Export ("response", ArgumentSemantic.Strong)]
		NSHttpUrlResponse Response { get; }

		// @property (assign, nonatomic) dispatch_queue_t successCallbackQueue;
		[Export ("successCallbackQueue", ArgumentSemantic.Assign)]
		DispatchQueue SuccessCallbackQueue { get; set; }

		// @property (assign, nonatomic) dispatch_queue_t failureCallbackQueue;
		[Export ("failureCallbackQueue", ArgumentSemantic.Assign)]
		DispatchQueue FailureCallbackQueue { get; set; }

		// -(instancetype)initWithURLRequest:(NSURLRequest *)urlRequest;
		[Export ("initWithURLRequest:")]
		IntPtr Constructor (NSUrlRequest urlRequest);

		// -(void)pause;
		[Export ("pause")]
		void Pause ();

		// -(BOOL)isPaused;
		[Export ("isPaused")]
		bool IsPaused { get; }

		// -(void)resume;
		[Export ("resume")]
		void Resume ();

		// -(void)setShouldExecuteAsBackgroundTaskWithExpirationHandler:(void (^)(void))handler;
		[Export ("setShouldExecuteAsBackgroundTaskWithExpirationHandler:")]
		void SetShouldExecuteAsBackgroundTask (Action expirationHandler);

		// -(void)setUploadProgressBlock:(void (^)(NSUInteger, long long, long long))block;
		[Export ("setUploadProgressBlock:")]
		void SetUploadProgressBlock (Action<nuint, long, long> block);

		// -(void)setDownloadProgressBlock:(void (^)(NSUInteger, long long, long long))block;
		[Export ("setDownloadProgressBlock:")]
		void SetDownloadProgressBlock (Action<nuint, long, long> block);

		// -(void)setCompletionBlockWithSuccess:(void (^)(VKHTTPOperation *, id))success failure:(void (^)(VKHTTPOperation *, NSError *))failure;
		[Export ("setCompletionBlockWithSuccess:failure:")]
		void SetCompletionBlock (Action<VKHTTPOperation, NSObject> success, Action<VKHTTPOperation, NSError> failure);
	}

	// @interface VKJSONOperation : VKHTTPOperation
	[BaseType (typeof(VKHTTPOperation))]
	interface VKJSONOperation
	{
	}

	// @interface VKRequestsScheduler : VKObject
	[BaseType (typeof(VKObject))]
	interface VKRequestsScheduler
	{
		// +(instancetype)instance;
		[Static]
		[Export ("instance")]
		VKRequestsScheduler Instance { get; }

		// -(void)setEnabled:(BOOL)enabled;
		[Export ("setEnabled:")]
		void SetEnabled (bool enabled);

		// -(void)scheduleRequest:(VKRequest *)req;
		[Export ("scheduleRequest:")]
		void ScheduleRequest (VKRequest req);

		// -(NSTimeInterval)currentAvailableInterval;
		[Export ("currentAvailableInterval")]
		double CurrentAvailableInterval { get; }
	}
}

namespace VKontakte.Views
{
	// @interface VKSharedTransitioningObject : NSObject <UIViewControllerTransitioningDelegate>
	[BaseType (typeof(NSObject))]
	interface VKSharedTransitioningObject : IUIViewControllerTransitioningDelegate
	{
	}
}

namespace VKontakte.API.Upload
{
	// @interface VKUploadPhotoBase : VKRequest
	[BaseType (typeof(VKRequest))]
	interface VKUploadPhotoBase
	{
		// @property (assign, nonatomic) NSInteger albumId;
		[Export ("albumId")]
		nint AlbumId { get; set; }

		// @property (assign, nonatomic) NSInteger groupId;
		[Export ("groupId")]
		nint GroupId { get; set; }

		// @property (assign, nonatomic) NSInteger userId;
		[Export ("userId")]
		nint UserId { get; set; }

		// @property (nonatomic, strong) VKImageParameters * imageParameters;
		[Export ("imageParameters", ArgumentSemantic.Strong)]
		VKImageParameters ImageParameters { get; set; }

		// @property (nonatomic, strong) UIImage * image;
		[Export ("image", ArgumentSemantic.Strong)]
		UIImage Image { get; set; }

		// -(instancetype)initWithImage:(UIImage *)image parameters:(VKImageParameters *)parameters;
		[Export ("initWithImage:parameters:")]
		IntPtr Constructor (UIImage image, VKImageParameters parameters);
	}

	// @interface VKUploadImageOperation : VKOperation
	[BaseType (typeof(VKOperation))]
	interface VKUploadImageOperation
	{
		// +(instancetype)operationWithUploadRequest:(VKUploadPhotoBase *)request;
		[Static]
		[Export ("operationWithUploadRequest:")]
		VKUploadImageOperation Create (VKUploadPhotoBase request);
	}

	// @interface VKUploadMessagesPhotoRequest : VKUploadPhotoBase
	[BaseType (typeof(VKUploadPhotoBase))]
	interface VKUploadMessagesPhotoRequest
	{
	}

	// @interface VKUploadPhotoRequest : VKUploadPhotoBase
	[BaseType (typeof(VKUploadPhotoBase))]
	interface VKUploadPhotoRequest
	{
		// -(instancetype)initWithImage:(UIImage *)image parameters:(VKImageParameters *)parameters albumId:(NSInteger)albumId groupId:(NSInteger)groupId;
		[Export ("initWithImage:parameters:albumId:groupId:")]
		IntPtr Constructor (UIImage image, VKImageParameters parameters, nint albumId, nint groupId);
	}

	// @interface VKUploadWallPhotoRequest : VKUploadPhotoBase
	[BaseType (typeof(VKUploadPhotoBase))]
	interface VKUploadWallPhotoRequest
	{
		// -(instancetype)initWithImage:(UIImage *)image parameters:(VKImageParameters *)parameters userId:(NSInteger)userId groupId:(NSInteger)groupId;
		[Export ("initWithImage:parameters:userId:groupId:")]
		IntPtr Constructor (UIImage image, VKImageParameters parameters, nint userId, nint groupId);
	}
}
