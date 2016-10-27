using System;

using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;

namespace Dropbox.CoreApi.iOS
{
	// @interface DBQuota : NSObject <NSCoding>
	[BaseType(typeof(NSObject))]
	interface DBQuota : INSCoding
	{
		// -(id)initWithDictionary:(NSDictionary *)dict;
		[Export("initWithDictionary:")]
		IntPtr Constructor(NSDictionary dict);

		// @property (readonly, nonatomic) long long normalConsumedBytes;
		[Export("normalConsumedBytes")]
		long NormalConsumedBytes { get; }

		// @property (readonly, nonatomic) long long sharedConsumedBytes;
		[Export("sharedConsumedBytes")]
		long SharedConsumedBytes { get; }

		// @property (readonly, nonatomic) long long totalConsumedBytes;
		[Export("totalConsumedBytes")]
		long TotalConsumedBytes { get; }

		// @property (readonly, nonatomic) long long totalBytes;
		[Export("totalBytes")]
		long TotalBytes { get; }
	}

	// @interface DBAccountInfo : NSObject <NSCoding>
	[BaseType(typeof(NSObject))]
	interface DBAccountInfo : INSCoding
	{
		// -(id)initWithDictionary:(NSDictionary *)dict;
		[Export("initWithDictionary:")]
		IntPtr Constructor(NSDictionary dict);

		// @property (readonly, nonatomic) NSString * email;
		[Export("email")]
		string Email { get; }

		// @property (readonly, nonatomic) NSString * country;
		[Export("country")]
		string Country { get; }

		// @property (readonly, nonatomic) NSString * displayName;
		[Export("displayName")]
		string DisplayName { get; }

		// @property (readonly, nonatomic) DBQuota * quota;
		[Export("quota")]
		DBQuota Quota { get; }

		// @property (readonly, nonatomic) NSString * userId;
		[Export("userId")]
		string UserId { get; }

		// @property (readonly, nonatomic) NSString * referralLink;
		[Export("referralLink")]
		string ReferralLink { get; }
	}

	// @interface DBAccountInfo : NSObject <NSCoding>
	[DisableDefaultCtor]
	[BaseType (typeof(NSObject), Name = "DBAccountInfo")]
	interface AccountInfo : INSCoding
	{
		// -(id)initWithDictionary:(NSDictionary *)dict;
		[Export ("initWithDictionary:")]
		IntPtr Constructor (NSDictionary dict);

		// @property (readonly, nonatomic) NSString * email;
		[Export ("email")]
		string Email { get; }

		// @property (readonly, nonatomic) NSString * country;
		[Export ("country")]
		string Country { get; }

		// @property (readonly, nonatomic) NSString * displayName;
		[Export ("displayName")]
		string DisplayName { get; }

		// @property (readonly, nonatomic) DBQuota * quota;
		[Export ("quota")]
		Quota Quota { get; }

		// @property (readonly, nonatomic) NSString * userId;
		[Export ("userId")]
		string UserId { get; }

		// @property (readonly, nonatomic) NSString * referralLink;
		[Export ("referralLink")]
		string ReferralLink { get; }
	}

	// @interface DBDeltaEntry : NSObject <NSCoding>
	[DisableDefaultCtor]
	[BaseType (typeof(NSObject), Name = "DBDeltaEntry")]
	interface DeltaEntry : INSCoding
	{
		// -(id)initWithArray:(NSArray *)array;
		[Export ("initWithArray:")]
		IntPtr Constructor (NSObject[] array);

		// @property (readonly, nonatomic) NSString * lowercasePath;
		[Export ("lowercasePath")]
		string LowercasePath { get; }

		// @property (readonly, nonatomic) DBMetadata * metadata;
		[Export ("metadata")]
		Metadata Metadata { get; }
	}

	// @interface DBJSON : DBJsonBase <DBJsonParser, DBJsonWriter>
	[BaseType (typeof(JsonBase), Name = "DBJSON")]
	interface Json : JsonWriterDelegate
	{

		[Export ("objectWithString:")]
		NSObject ObjectWithString (string repr);

		// -(id)fragmentWithString:(NSString *)jsonrep error:(NSError **)error;
		[Export ("fragmentWithString:error:")]
		NSObject FragmentFromString (string jsonrep, out NSError error);

		// -(id)objectWithString:(NSString *)jsonrep error:(NSError **)error;
		[Export ("objectWithString:error:")]
		NSObject ObjectFromString (string jsonrep, out NSError error);

		// -(id)objectWithString:(id)value allowScalar:(BOOL)x error:(NSError **)error;
		[Export ("objectWithString:allowScalar:error:")]
		NSObject ObjectFromString (NSObject value, bool escalar, out NSError error);

		// -(NSString *)stringWithObject:(id)value error:(NSError **)error;
		[Export ("stringWithObject:error:")]
		string StringFromObject (NSObject value, out NSError error);

		// -(NSString *)stringWithFragment:(id)value error:(NSError **)error;
		[Export ("stringWithFragment:error:")]
		string StringFromFragment (NSObject value, out NSError error);

		// -(NSString *)stringWithObject:(id)value allowScalar:(BOOL)x error:(NSError **)error;
		[Export ("stringWithObject:allowScalar:error:")]
		string StringFromObject (NSObject value, bool escalar, out NSError error);
	}

	// @interface DBJsonBase : NSObject
	[BaseType (typeof(NSObject), Name = "DBJsonBase")]
	interface JsonBase
	{
		
		// @property NSUInteger maxDepth;
		[Export ("maxDepth", ArgumentSemantic.Assign)]
		nuint MaxDepth { get; set; }

		// @property (readonly, copy) NSArray * errorTrace;
		[Export ("errorTrace", ArgumentSemantic.Copy)]
		NSError[] ErrorTrace { get; }

		// -(void)addErrorWithCode:(NSUInteger)code description:(NSString *)str;
		[Export ("addErrorWithCode:description:")]
		void AddErrorWithCode (nuint code, string str);

		// -(void)clearErrorTrace;
		[Export ("clearErrorTrace")]
		void ClearErrorTrace ();
	}

	// @interface DBJsonParser : DBJsonBase <DBJsonParser>
	[BaseType (typeof(JsonBase), Name = "DBJsonParser")]
	interface JsonParser
	{
		[Export ("objectWithString:")]
		NSObject ObjectWithString (string repr);
	}

	interface IJsonWriterDelegate
	{

	}

	// @protocol DBJsonWriter
	[Protocol]
	[BaseType (typeof(NSObject), Name = "DBJsonWriter")]
	interface JsonWriterDelegate
	{
		// @required @property BOOL humanReadable;
		[Export ("humanReadable")]
		bool GetHumanReadable ();

		[Export ("setHumanReadable:")]
		void SetHumanReadable (bool humanReadable);

		// @required @property BOOL sortKeys;
		[Export ("sortKeys")]
		bool GetSortKeys ();

		// @required @property BOOL sortKeys;
		[Export ("setSortKeys:")]
		bool SetSortKeys (bool sortKeys);

		// @required -(NSString *)stringWithObject:(id)value;
		[Abstract]
		[Export ("stringWithObject:")]
		string StringWithObject (NSObject value);
	}

	// @interface DBMetadata : NSObject <NSCoding>
	[DisableDefaultCtor]
	[BaseType (typeof(NSObject), Name = "DBMetadata")]
	interface Metadata : INSCoding
	{
		// -(id)initWithDictionary:(NSDictionary *)dict;
		[Export ("initWithDictionary:")]
		IntPtr Constructor (NSDictionary dict);

		// @property (readonly, nonatomic) BOOL thumbnailExists;
		[Export ("thumbnailExists")]
		bool ThumbnailExists { get; }

		// @property (readonly, nonatomic) long long totalBytes;
		[Export ("totalBytes")]
		long TotalBytes { get; }

		// @property (readonly, nonatomic) NSDate * lastModifiedDate;
		[Export ("lastModifiedDate")]
		NSDate LastModifiedDate { get; }

		// @property (readonly, nonatomic) NSDate * clientMtime;
		[Export ("clientMtime")]
		NSDate ClientMtime { get; }

		// @property (readonly, nonatomic) NSString * path;
		[Export ("path")]
		string Path { get; }

		// @property (readonly, nonatomic) BOOL isDirectory;
		[Export ("isDirectory")]
		bool IsDirectory { get; }

		// @property (readonly, nonatomic) NSArray * contents;
		[Export ("contents")]
		Metadata[] Contents { get; }

		// @property (readonly, nonatomic) NSString * hash;
		[Export ("hash")]
		string Hash { get; }

		// @property (readonly, nonatomic) NSString * humanReadableSize;
		[Export ("humanReadableSize")]
		string HumanReadableSize { get; }

		// @property (readonly, nonatomic) NSString * root;
		[Export ("root")]
		string Root { get; }

		// @property (readonly, nonatomic) NSString * icon;
		[Export ("icon")]
		string Icon { get; }

		// @property (readonly, nonatomic) long long revision;
		[Export ("rev")]
		string Revision { get; }

		// @property (readonly, nonatomic) BOOL isDeleted;
		[Export ("isDeleted")]
		bool IsDeleted { get; }

		// @property (readonly, nonatomic) NSString * filename;
		[Export ("filename")]
		string Filename { get; }
	}

	// @interface DBQuota : NSObject <NSCoding>
	[DisableDefaultCtor]
	[BaseType (typeof(NSObject), Name = "DBQuota")]
	interface Quota : INSCoding
	{
		// -(id)initWithDictionary:(NSDictionary *)dict;
		[Export ("initWithDictionary:")]
		IntPtr Constructor (NSDictionary dict);

		// @property (readonly, nonatomic) long long normalConsumedBytes;
		[Export ("normalConsumedBytes")]
		long NormalConsumedBytes { get; }

		// @property (readonly, nonatomic) long long sharedConsumedBytes;
		[Export ("sharedConsumedBytes")]
		long SharedConsumedBytes { get; }

		// @property (readonly, nonatomic) long long totalConsumedBytes;
		[Export ("totalConsumedBytes")]
		long TotalConsumedBytes { get; }

		// @property (readonly, nonatomic) long long totalBytes;
		[Export ("totalBytes")]
		long TotalBytes { get; }
	}

	// @interface DBRequest : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof(NSObject), Name = "DBRequest")]
	interface Request
	{
		// +(void)setNetworkRequestDelegate:(id<DBNetworkRequestDelegate>)delegate;
		[Static]
		[Export ("setNetworkRequestDelegate:")]
		void SetNetworkRequestDelegate (INetworkRequestDelegate aDelegate);

		// -(id)initWithURLRequest:(NSURLRequest *)request andInformTarget:(id)target selector:(SEL)selector;
		[Export ("initWithURLRequest:andInformTarget:selector:")]
		IntPtr Constructor (NSUrlRequest request, NSObject target, Selector selector);

		// -(void)cancel;
		[Export ("cancel")]
		void Cancel ();

		// -(id)parseResponseAsType:(Class)cls;
		[Export ("parseResponseAsType:")]
		NSObject ParseResponseAsType (Class cls);

		// @property (assign, nonatomic) SEL failureSelector;
		[Export ("failureSelector", ArgumentSemantic.Assign)]
		Selector FailureSelector { get; set; }

		// @property (assign, nonatomic) SEL downloadProgressSelector;
		[Export ("downloadProgressSelector", ArgumentSemantic.Assign)]
		Selector DownloadProgressSelector { get; set; }

		// @property (assign, nonatomic) SEL uploadProgressSelector;
		[Export ("uploadProgressSelector", ArgumentSemantic.Assign)]
		Selector UploadProgressSelector { get; set; }

		// @property (retain, nonatomic) NSString * resultFilename;
		[Export ("resultFilename", ArgumentSemantic.Retain)]
		string ResultFilename { get; set; }

		// @property (retain, nonatomic) NSDictionary * userInfo;
		[Export ("userInfo", ArgumentSemantic.Retain)]
		NSDictionary UserInfo { get; set; }

		// @property (retain, nonatomic) NSString * sourcePath;
		[Export ("sourcePath", ArgumentSemantic.Retain)]
		string SourcePath { get; set; }

		// @property (readonly, nonatomic) NSURLRequest * request;
		[Export ("request")]
		NSUrlRequest UrlRequest { get; }

		// @property (readonly, nonatomic) NSHTTPURLResponse * response;
		[Export ("response")]
		NSHttpUrlResponse Response { get; }

		// @property (readonly, nonatomic) NSDictionary * xDropboxMetadataJSON;
		[Export ("xDropboxMetadataJSON")]
		NSDictionary XDropboxMetadataJson { get; }

		// @property (readonly, nonatomic) NSInteger statusCode;
		[Export ("statusCode")]
		nint StatusCode { get; }

		// @property (readonly, nonatomic) CGFloat downloadProgress;
		[Export ("downloadProgress")]
		nfloat DownloadProgress { get; }

		// @property (readonly, nonatomic) CGFloat uploadProgress;
		[Export ("uploadProgress")]
		nfloat UploadProgress { get; }

		// @property (readonly, nonatomic) NSData * resultData;
		[Export ("resultData")]
		NSData ResultData { get; }

		// @property (readonly, nonatomic) NSString * resultString;
		[Export ("resultString")]
		string ResultString { get; }

		// @property (readonly, nonatomic) NSObject * resultJSON;
		[Export ("resultJSON")]
		NSObject ResultJson { get; }

		// @property (readonly, nonatomic) NSError * error;
		[Export ("error")]
		NSError Error { get; }
	}

	interface INetworkRequestDelegate
	{

	}

	// @protocol DBNetworkRequestDelegate
	[Model]
	[Protocol]
	[BaseType (typeof(NSObject), Name = "DBNetworkRequestDelegate")]
	interface NetworkRequestDelegate
	{
		// @required -(void)networkRequestStarted;
		[Abstract]
		[Export ("networkRequestStarted")]
		void NetworkRequestStarted ();

		// @required -(void)networkRequestStopped;
		[Abstract]
		[Export ("networkRequestStopped")]
		void NetworkRequestStopped ();
	}

	// @interface DBRestClient : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof(NSObject),
		Name = "DBRestClient",
		Delegates = new [] { "Delegate" },
		Events = new [] { typeof(RestClientDelegate) })]
	interface RestClient
	{
		// -(id)initWithSession:(DBSession *)session;
		[Export ("initWithSession:")]
		IntPtr Constructor (Session session);

		// -(id)initWithSession:(DBSession *)session userId:(NSString *)userId;
		[Export ("initWithSession:userId:")]
		IntPtr Constructor (Session session, string userId);

		// -(void)cancelAllRequests;
		[Export ("cancelAllRequests")]
		void CancelAllRequests ();

		// -(void)loadMetadata:(NSString *)path withHash:(NSString *)hash;
		[Export ("loadMetadata:withHash:")]
		void LoadMetadataWithHash (string path, string hash);

		// -(void)loadMetadata:(NSString *)path;
		[Export ("loadMetadata:")]
		void LoadMetadata (string path);

		// -(void)loadMetadata:(NSString *)path atRev:(NSString *)rev;
		[Export ("loadMetadata:atRev:")]
		void LoadMetadataWithRevision (string path, string rev);

		// -(void)loadDelta:(NSString *)cursor;
		[Export ("loadDelta:")]
		void LoadDelta (string cursor);

		// -(void)loadFile:(NSString *)path intoPath:(NSString *)destinationPath;
		[Export ("loadFile:intoPath:")]
		void LoadFile (string path, string destinationPath);

		// -(void)loadFile:(NSString *)path atRev:(NSString *)rev intoPath:(NSString *)destPath;
		[Export ("loadFile:atRev:intoPath:")]
		void LoadFile (string path, string rev, string destPath);

		// -(void)cancelFileLoad:(NSString *)path;
		[Export ("cancelFileLoad:")]
		void CancelFileLoad (string path);

		// -(void)loadThumbnail:(NSString *)path ofSize:(NSString *)size intoPath:(NSString *)destinationPath;
		[Export ("loadThumbnail:ofSize:intoPath:")]
		void LoadThumbnail (string path, string size, string destinationPath);

		// -(void)cancelThumbnailLoad:(NSString *)path size:(NSString *)size;
		[Export ("cancelThumbnailLoad:size:")]
		void CancelThumbnailLoad (string path, string size);

		// -(void)uploadFile:(NSString *)filename toPath:(NSString *)path withParentRev:(NSString *)parentRev fromPath:(NSString *)sourcePath;
		[Export ("uploadFile:toPath:withParentRev:fromPath:")]
		void UploadFile (string filename, string path, [NullAllowed] string parentRev, string sourcePath);

		// -(void)cancelFileUpload:(NSString *)path;
		[Export ("cancelFileUpload:")]
		void CancelFileUpload (string path);

		// -(void)uploadFileChunk:(NSString *)uploadId offset:(unsigned long long)offset fromPath:(NSString *)localPath;
		[Export ("uploadFileChunk:offset:fromPath:")]
		void UploadFileChunk (string uploadId, ulong offset, string localPath);

		// -(void)uploadFile:(NSString *)filename toPath:(NSString *)parentFolder withParentRev:(NSString *)parentRev fromUploadId:(NSString *)uploadId;
		[Export ("uploadFile:toPath:withParentRev:fromUploadId:")]
		void UploadFileFromUploadId (string filename, string parentFolder, string parentRev, [NullAllowed] string uploadId);

		// -(void)loadRevisionsForFile:(NSString *)path;
		[Export ("loadRevisionsForFile:")]
		void LoadRevisionsForFile (string path);

		// -(void)loadRevisionsForFile:(NSString *)path limit:(NSInteger)limit;
		[Export ("loadRevisionsForFile:limit:")]
		void LoadRevisionsForFile (string path, nint limit);

		// -(void)restoreFile:(NSString *)path toRev:(NSString *)rev;
		[Export ("restoreFile:toRev:")]
		void RestoreFile (string path, string rev);

		// -(void)createFolder:(NSString *)path;
		[Export ("createFolder:")]
		void CreateFolder (string path);

		// -(void)deletePath:(NSString *)path;
		[Export ("deletePath:")]
		void DeletePath (string path);

		// -(void)copyFrom:(NSString *)fromPath toPath:(NSString *)toPath;
		[Export ("copyFrom:toPath:")]
		void Copy (string fromPath, string toPath);

		// -(void)createCopyRef:(NSString *)path;
		[Export ("createCopyRef:")]
		void CreateCopyRef (string path);

		// -(void)copyFromRef:(NSString *)copyRef toPath:(NSString *)toPath;
		[Export ("copyFromRef:toPath:")]
		void CopyFromRef (string copyRef, string toPath);

		// -(void)moveFrom:(NSString *)fromPath toPath:(NSString *)toPath;
		[Export ("moveFrom:toPath:")]
		void MoveFrom (string fromPath, string toPath);

		// -(void)loadAccountInfo;
		[Export ("loadAccountInfo")]
		void LoadAccountInfo ();

		// -(void)searchPath:(NSString *)path forKeyword:(NSString *)keyword;
		[Export ("searchPath:forKeyword:")]
		void SearchPath (string path, string keyword);

		// -(void)loadSharableLinkForFile:(NSString *)path;
		[Export ("loadSharableLinkForFile:")]
		void LoadSharableLinkForFile (string path);

		// -(void)loadSharableLinkForFile:(NSString *)path shortUrl:(BOOL)createShortUrl;
		[Export ("loadSharableLinkForFile:shortUrl:")]
		void LoadSharableLinkForFile (string path, bool createShortUrl);

		// -(void)loadStreamableURLForFile:(NSString *)path;
		[Export ("loadStreamableURLForFile:")]
		void LoadStreamableURLForFile (string path);

		// -(NSUInteger)requestCount;
		[Export ("requestCount")]
		nuint RequestCount { get; }

		[NullAllowed]
		[Export ("delegate", ArgumentSemantic.Assign)]
		IRestClientDelegate Delegate { get; set; }
	}

	interface IRestClientDelegate
	{

	}

	// @protocol DBRestClientDelegate <NSObject>
	[Model]
	[Protocol]
	[BaseType (typeof(NSObject), Name = "DBRestClientDelegate")]
	interface RestClientDelegate
	{
		[EventArgs ("RestClientMetadataLoaded")]
		[EventName ("MetadataLoaded")]
		[Export ("restClient:loadedMetadata:")]
		void LoadedMetadata (RestClient client, Metadata metadata);

		// @optional -(void)restClient:(DBRestClient *)client metadataUnchangedAtPath:(NSString *)path;
		[EventArgs ("RestClientMetadataUnchanged")]
		[Export ("restClient:metadataUnchangedAtPath:")]
		void MetadataUnchanged (RestClient client, string path);

		// @optional -(void)restClient:(DBRestClient *)client loadMetadataFailedWithError:(NSError *)error;
		[EventArgs ("RestClientError")]
		[Export ("restClient:loadMetadataFailedWithError:")]
		void LoadMetadataFailed (RestClient client, NSError error);

		// @optional -(void)restClient:(DBRestClient *)client loadedDeltaEntries:(NSArray *)entries reset:(BOOL)shouldReset cursor:(NSString *)cursor hasMore:(BOOL)hasMore;
		[EventArgs ("RestClientDeltaEntriesLoaded")]
		[EventName ("DeltaEntriesLoaded")]
		[Export ("restClient:loadedDeltaEntries:reset:cursor:hasMore:")]
		void LoadedDeltaEntries (RestClient client, DeltaEntry[] entries, bool shouldReset, string cursor, bool hasMore);

		// @optional -(void)restClient:(DBRestClient *)client loadDeltaFailedWithError:(NSError *)error;
		[EventArgs ("RestClientError")]
		[Export ("restClient:loadDeltaFailedWithError:")]
		void LoadDeltaFailed (RestClient client, NSError error);

		// @optional -(void)restClient:(DBRestClient *)client loadedAccountInfo:(DBAccountInfo *)info;
		[EventArgs ("RestClientAccountInfoLoaded")]
		[EventName ("AccountInfoLoaded")]
		[Export ("restClient:loadedAccountInfo:")]
		void LoadedAccountInfo (RestClient client, AccountInfo info);

		// @optional -(void)restClient:(DBRestClient *)client loadAccountInfoFailedWithError:(NSError *)error;
		[EventArgs ("RestClientError")]
		[Export ("restClient:loadAccountInfoFailedWithError:")]
		void LoadAccountInfoFailed (RestClient client, NSError error);

		// @optional -(void)restClient:(DBRestClient *)client loadedFile:(NSString *)destPath;
		[EventArgs ("RestClientFileLoaded")]
		[EventName ("FileLoaded")]
		[Export ("restClient:loadedFile:")]
		void LoadedFile (RestClient client, string destPath);

		// @optional -(void)restClient:(DBRestClient *)client loadedFile:(NSString *)destPath contentType:(NSString *)contentType metadata:(DBMetadata *)metadata;
		[EventArgs ("RestClientFileWithMetadataLoaded")]
		[EventName ("FileWithMetadataLoaded")]
		[Export ("restClient:loadedFile:contentType:metadata:")]
		void LoadedFileWithMetada (RestClient client, string destPath, string contentType, Metadata metadata);

		// @optional -(void)restClient:(DBRestClient *)client loadProgress:(CGFloat)progress forFile:(NSString *)destPath;
		[EventArgs ("RestClientLoadProgress")]
		[Export ("restClient:loadProgress:forFile:")]
		void LoadProgress (RestClient client, nfloat progress, string destPath);

		// @optional -(void)restClient:(DBRestClient *)client loadFileFailedWithError:(NSError *)error;
		[EventArgs ("RestClientError")]
		[Export ("restClient:loadFileFailedWithError:")]
		void LoadFileFailed (RestClient client, NSError error);

		// @optional -(void)restClient:(DBRestClient *)client loadedThumbnail:(NSString *)destPath metadata:(DBMetadata *)metadata;
		[EventArgs ("RestClientThumbnailLoaded")]
		[EventName ("ThumbnailLoaded")]
		[Export ("restClient:loadedThumbnail:metadata:")]
		void LoadedThumbnail (RestClient client, string destPath, Metadata metadata);

		// @optional -(void)restClient:(DBRestClient *)client loadThumbnailFailedWithError:(NSError *)error;
		[EventArgs ("RestClientError")]
		[Export ("restClient:loadThumbnailFailedWithError:")]
		void LoadThumbnailFailed (RestClient client, NSError error);

		// @optional -(void)restClient:(DBRestClient *)client uploadedFile:(NSString *)destPath from:(NSString *)srcPath metadata:(DBMetadata *)metadata;
		[EventArgs ("RestClientFileUploaded")]
		[EventName ("FileUploaded")]
		[Export ("restClient:uploadedFile:from:metadata:")]
		void UploadedFile (RestClient client, string destPath, string srcPath, Metadata metadata);

		// @optional -(void)restClient:(DBRestClient *)client uploadProgress:(CGFloat)progress forFile:(NSString *)destPath from:(NSString *)srcPath;
		[EventArgs ("RestClientUploadProgress")]
		[Export ("restClient:uploadProgress:forFile:from:")]
		void UploadProgress (RestClient client, nfloat progress, string destPath, string srcPath);

		// @optional -(void)restClient:(DBRestClient *)client uploadFileFailedWithError:(NSError *)error;
		[EventArgs ("RestClientError")]
		[Export ("restClient:uploadFileFailedWithError:")]
		void UploadFileFailed (RestClient client, NSError error);

		// @optional -(void)restClient:(DBRestClient *)client uploadedFileChunk:(NSString *)uploadId newOffset:(unsigned long long)offset fromFile:(NSString *)localPath expires:(NSDate *)expiresDate;
		[EventArgs ("RestClientFileChunkUploaded")]
		[EventName ("FileChunkUploaded")]
		[Export ("restClient:uploadedFileChunk:newOffset:fromFile:expires:")]
		void UploadedFileChunk (RestClient client, string uploadId, ulong offset, string localPath, NSDate expiresDate);

		// @optional -(void)restClient:(DBRestClient *)client uploadFileChunkFailedWithError:(NSError *)error;
		[EventArgs ("RestClientError")]
		[Export ("restClient:uploadFileChunkFailedWithError:")]
		void UploadFileChunkFailed (RestClient client, NSError error);

		// @optional -(void)restClient:(DBRestClient *)client uploadFileChunkProgress:(CGFloat)progress forFile:(NSString *)uploadId offset:(unsigned long long)offset fromPath:(NSString *)localPath;
		[EventArgs ("RestClientUploadFileChunkProgress")]
		[Export ("restClient:uploadFileChunkProgress:forFile:offset:fromPath:")]
		void UploadFileChunkProgress (RestClient client, nfloat progress, string uploadId, ulong offset, string localPath);

		// @optional -(void)restClient:(DBRestClient *)client uploadedFile:(NSString *)destPath fromUploadId:(NSString *)uploadId metadata:(DBMetadata *)metadata;
		[EventArgs ("RestClientFileFromUploadIdUploaded")]
		[EventName ("FileFromUploadIdUploaded")]
		[Export ("restClient:uploadedFile:fromUploadId:metadata:")]
		void UploadedFileFromUploadId (RestClient client, string destPath, string uploadId, Metadata metadata);

		// @optional -(void)restClient:(DBRestClient *)client uploadFromUploadIdFailedWithError:(NSError *)error;
		[EventArgs ("RestClientError")]
		[Export ("restClient:uploadFromUploadIdFailedWithError:")]
		void UploadFromUploadIdFailed (RestClient client, NSError error);

		// @optional -(void)restClient:(DBRestClient *)client loadedRevisions:(NSArray *)revisions forFile:(NSString *)path;
		[EventArgs ("RestClientRevisionsLoaded")]
		[EventName ("RevisionsLoaded")]
		[Export ("restClient:loadedRevisions:forFile:")]
		void LoadedRevisions (RestClient client, Metadata[] revisions, string path);

		// @optional -(void)restClient:(DBRestClient *)client loadRevisionsFailedWithError:(NSError *)error;
		[EventArgs ("RestClientError")]
		[Export ("restClient:loadRevisionsFailedWithError:")]
		void LoadRevisionsFailed (RestClient client, NSError error);

		// @optional -(void)restClient:(DBRestClient *)client restoredFile:(DBMetadata *)fileMetadata;
		[EventArgs ("RestClientFileRestored")]
		[EventName ("FileRestored")]
		[Export ("restClient:restoredFile:")]
		void RestoredFile (RestClient client, Metadata fileMetadata);

		// @optional -(void)restClient:(DBRestClient *)client restoreFileFailedWithError:(NSError *)error;
		[EventArgs ("RestClientError")]
		[Export ("restClient:restoreFileFailedWithError:")]
		void RestoreFileFailed (RestClient client, NSError error);

		// @optional -(void)restClient:(DBRestClient *)client createdFolder:(DBMetadata *)folder;
		[EventArgs ("RestClientFolderCreated")]
		[EventName ("FolderCreated")]
		[Export ("restClient:createdFolder:")]
		void CreatedFolder (RestClient client, Metadata folder);

		// @optional -(void)restClient:(DBRestClient *)client createFolderFailedWithError:(NSError *)error;
		[EventArgs ("RestClientError")]
		[Export ("restClient:createFolderFailedWithError:")]
		void CreateFolderFailed (RestClient client, NSError error);

		// @optional -(void)restClient:(DBRestClient *)client deletedPath:(NSString *)path;
		[EventArgs ("RestClientPathDeleted")]
		[EventName ("PathDeleted")]
		[Export ("restClient:deletedPath:")]
		void DeletedPath (RestClient client, string path);

		// @optional -(void)restClient:(DBRestClient *)client deletePathFailedWithError:(NSError *)error;
		[EventArgs ("RestClientError")]
		[Export ("restClient:deletePathFailedWithError:")]
		void DeletePathFailed (RestClient client, NSError error);

		// @optional -(void)restClient:(DBRestClient *)client copiedPath:(NSString *)fromPath to:(DBMetadata *)to;
		[EventArgs ("RestClientPathCopied")]
		[EventName ("PathCopied")]
		[Export ("restClient:copiedPath:to:")]
		void CopiedPath (RestClient client, string fromPath, Metadata to);

		// @optional -(void)restClient:(DBRestClient *)client copyPathFailedWithError:(NSError *)error;
		[EventArgs ("RestClientError")]
		[Export ("restClient:copyPathFailedWithError:")]
		void CopyPathFailed (RestClient client, NSError error);

		// @optional -(void)restClient:(DBRestClient *)client createdCopyRef:(NSString *)copyRef forPath:(NSString *)path;
		[EventArgs ("RestClientCopyRefCreated")]
		[EventName ("CopyRefCreated")]
		[Export ("restClient:createdCopyRef:forPath:")]
		void CreatedCopyRef (RestClient client, string copyRef, string path);

		// @optional -(void)restClient:(DBRestClient *)client createCopyRefFailedWithError:(NSError *)error;
		[EventArgs ("RestClientError")]
		[Export ("restClient:createCopyRefFailedWithError:")]
		void CreateCopyRefFailed (RestClient client, NSError error);

		// @optional -(void)restClient:(DBRestClient *)client copiedRef:(NSString *)copyRef to:(DBMetadata *)to;
		[EventArgs ("RestClientRefCopied")]
		[EventName ("RefCopied")]
		[Export ("restClient:copiedRef:to:")]
		void CopiedRef (RestClient client, string copyRef, Metadata to);

		// @optional -(void)restClient:(DBRestClient *)client copyFromRefFailedWithError:(NSError *)error;
		[EventArgs ("RestClientError")]
		[Export ("restClient:copyFromRefFailedWithError:")]
		void CopyFromRefFailed (RestClient client, NSError error);

		// @optional -(void)restClient:(DBRestClient *)client movedPath:(NSString *)from_path to:(DBMetadata *)result;
		[EventArgs ("RestClientPathMoved")]
		[EventName ("PathMoved")]
		[Export ("restClient:movedPath:to:")]
		void MovedPath (RestClient client, string from_path, Metadata result);

		// @optional -(void)restClient:(DBRestClient *)client movePathFailedWithError:(NSError *)error;
		[EventArgs ("RestClientError")]
		[Export ("restClient:movePathFailedWithError:")]
		void MovePathFailedWithError (RestClient client, NSError error);

		// @optional -(void)restClient:(DBRestClient *)restClient loadedSearchResults:(NSArray *)results forPath:(NSString *)path keyword:(NSString *)keyword;
		[EventArgs ("RestClientSearchResultsLoaded")]
		[EventName ("SearchResultsLoaded")]
		[Export ("restClient:loadedSearchResults:forPath:keyword:")]
		void LoadedSearchResults (RestClient restClient, Metadata[] results, string path, string keyword);

		// @optional -(void)restClient:(DBRestClient *)restClient searchFailedWithError:(NSError *)error;
		[EventArgs ("RestClientError")]
		[Export ("restClient:searchFailedWithError:")]
		void SearchFailed (RestClient restClient, NSError error);

		// @optional -(void)restClient:(DBRestClient *)restClient loadedSharableLink:(NSString *)link forFile:(NSString *)path;
		[EventArgs ("RestClientSharableLinkLoaded")]
		[EventName ("SharableLinkLoaded")]
		[Export ("restClient:loadedSharableLink:forFile:")]
		void LoadedSharableLink (RestClient restClient, string link, string path);

		// @optional -(void)restClient:(DBRestClient *)restClient loadSharableLinkFailedWithError:(NSError *)error;
		[EventArgs ("RestClientError")]
		[Export ("restClient:loadSharableLinkFailedWithError:")]
		void LoadSharableLinkFailed (RestClient restClient, NSError error);

		// @optional -(void)restClient:(DBRestClient *)restClient loadedStreamableURL:(NSURL *)url forFile:(NSString *)path;
		[EventArgs ("RestClientStreamableUrlLoaded")]
		[EventName ("StreamableUrlLoaded")]
		[Export ("restClient:loadedStreamableURL:forFile:")]
		void LoadedStreamableUrl (RestClient restClient, NSUrl url, string path);

		// @optional -(void)restClient:(DBRestClient *)restClient loadStreamableURLFailedWithError:(NSError *)error;
		[EventArgs ("RestClientError")]
		[Export ("restClient:loadStreamableURLFailedWithError:")]
		void LoadStreamableURLFailed (RestClient restClient, NSError error);
	}

	// @interface DBSession : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof(NSObject), 
		Name = "DBSession",
		Delegates = new [] { "Delegate" },
		Events = new [] { typeof(SessionDelegate) })]
	interface Session
	{
		// extern NSString * kDBSDKVersion;
		[Field ("kDBSDKVersion", "__Internal")]
		NSString SDKVersion { get; }

		// extern NSString * kDBDropboxAPIHost;
		[Field ("kDBDropboxAPIHost", "__Internal")]
		NSString DropboxAPIHost { get; }

		// extern NSString * kDBDropboxAPIContentHost;
		[Field ("kDBDropboxAPIContentHost", "__Internal")]
		NSString DropboxAPIContentHost { get; }

		// extern NSString * kDBDropboxWebHost;
		[Field ("kDBDropboxWebHost", "__Internal")]
		NSString DropboxWebHost { get; }

		// extern NSString * kDBDropboxAPIVersion;
		[Field ("kDBDropboxAPIVersion", "__Internal")]
		NSString DropboxAPIVersion { get; }

		// extern NSString * kDBRootDropbox;
		[Field ("kDBRootDropbox", "__Internal")]
		NSString RootDropbox { get; }

		// extern NSString * kDBRootAppFolder;
		[Field ("kDBRootAppFolder", "__Internal")]
		NSString RootAppFolder { get; }

		// extern NSString * kDBProtocolHTTPS;
		[Field ("kDBProtocolHTTPS", "__Internal")]
		NSString ProtocolHttps { get; }

		// +(DBSession *)sharedSession;
		// +(void)setSharedSession:(DBSession *)session;
		[Static]
		[Export ("sharedSession")]
		Session SharedSession { get; set; }

		// -(id)initWithAppKey:(NSString *)key appSecret:(NSString *)secret root:(NSString *)root;
		[Export ("initWithAppKey:appSecret:root:")]
		IntPtr Constructor (string key, string secret, string root);

		// -(BOOL)isLinked;
		[Export ("isLinked")]
		bool IsLinked { get; }

		// -(void)unlinkAll;
		[Export ("unlinkAll")]
		void UnlinkAll ();

		// -(void)unlinkUserId:(NSString *)userId;
		[Export ("unlinkUserId:")]
		void Unlink (string userId);

		// -(MPOAuthCredentialConcreteStore *)credentialStoreForUserId:(NSString *)userId;
		[Export ("credentialStoreForUserId:")]
		MPOAuth.CredentialConcreteStore CredentialStore (string userId);

		// -(void)updateAccessToken:(NSString *)token accessTokenSecret:(NSString *)secret forUserId:(NSString *)userId;
		[Export ("updateAccessToken:accessTokenSecret:forUserId:")]
		void UpdateAccessToken (string token, string secret, string userId);

		// @property (readonly, nonatomic) NSString * root;
		[Export ("root")]
		string Root { get; }

		// @property (readonly, nonatomic) NSArray * userIds;
		[Export ("userIds")]
		string[] UserIds { get; }

		[NullAllowed]
		[Export ("delegate", ArgumentSemantic.Assign)]
		ISessionDelegate Delegate { get; set; }

		// Category DBSession (iOS)
		// +(NSDictionary *)parseURLParams:(NSString *)query;
		[Static]
		[Export ("parseURLParams:")]
		NSDictionary ParseUrlParams (string query);
	}

	interface ISessionDelegate
	{

	}

	// @protocol DBSessionDelegate
	[Model]
	[Protocol]
	[BaseType (typeof(NSObject), Name = "DBSessionDelegate")]
	interface SessionDelegate
	{
		// @required -(void)sessionDidReceiveAuthorizationFailure:(DBSession *)session userId:(NSString *)userId;
		[Abstract]
		[EventArgs ("SessionAuthorizationFailureReceived")]
		[EventName ("AuthorizationFailureReceived")]
		[Export ("sessionDidReceiveAuthorizationFailure:userId:")]
		void DidReceiveAuthorizationFailure (Session session, string userId);
	}

	[Category]
	[BaseType (typeof(Session))]
	interface Session_iOS
	{
		// -(NSString *)appScheme;
		[Export ("appScheme")]
		string GetAppScheme ();

		// -(void)linkFromController:(UIViewController *)rootController;
		[Export ("linkFromController:")]
		void LinkFromController (UIViewController rootController);

		// -(void)linkUserId:(NSString *)userId fromController:(UIViewController *)rootController;
		[Export ("linkUserId:fromController:")]
		void LinkUserId (string userId, UIViewController rootController);

		// -(BOOL)handleOpenURL:(NSURL *)url;
		[Export ("handleOpenURL:")]
		bool HandleOpenUrl (NSUrl url);
	}
}

namespace Dropbox.CoreApi.iOS.MPOAuth
{
	interface IOAuthInternalClient
	{
		
	}

	// @protocol MPOAuthAPIInternalClient
	[Protocol]
	[BaseType (typeof(NSObject), Name = "MPOAuthAPIInternalClient")]
	interface OAuthInternalClient
	{
	}

	// @interface MPOAuthAPI : NSObject <MPOAuthAPIInternalClient>
	[DisableDefaultCtor]
	[BaseType (typeof(NSObject), Name = "MPOAuthAPI")]
	interface OAuth : OAuthInternalClient
	{
		// extern NSString *const MPOAuthNotificationAccessTokenReceived;
		[Field ("MPOAuthNotificationAccessTokenReceived", "__Internal")]
		NSString AuthNotificationAccessTokenReceived { get; }

		// extern NSString *const MPOAuthNotificationAccessTokenRejected;
		[Field ("MPOAuthNotificationAccessTokenRejected", "__Internal")]
		NSString AuthNotificationAccessTokenRejected { get; }

		// extern NSString *const MPOAuthNotificationAccessTokenRefreshed;
		[Field ("MPOAuthNotificationAccessTokenRefreshed", "__Internal")]
		NSString AuthNotificationAccessTokenRefreshed { get; }

		// extern NSString *const MPOAuthNotificationOAuthCredentialsReady;
		[Field ("MPOAuthNotificationOAuthCredentialsReady", "__Internal")]
		NSString AuthNotificationOAuthCredentialsReady { get; }

		// extern NSString *const MPOAuthNotificationErrorHasOccurred;
		[Field ("MPOAuthNotificationErrorHasOccurred", "__Internal")]
		NSString AuthNotificationErrorHasOccurred { get; }

		// extern NSString *const MPOAuthCredentialRequestTokenKey;
		[Field ("MPOAuthCredentialRequestTokenKey", "__Internal")]
		NSString AuthCredentialRequestTokenKey { get; }

		// extern NSString *const MPOAuthCredentialRequestTokenSecretKey;
		[Field ("MPOAuthCredentialRequestTokenSecretKey", "__Internal")]
		NSString AuthCredentialRequestTokenSecretKey { get; }

		// extern NSString *const MPOAuthCredentialAccessTokenKey;
		[Field ("MPOAuthCredentialAccessTokenKey", "__Internal")]
		NSString AuthCredentialAccessTokenKey { get; }

		// extern NSString *const MPOAuthCredentialAccessTokenSecretKey;
		[Field ("MPOAuthCredentialAccessTokenSecretKey", "__Internal")]
		NSString AuthCredentialAccessTokenSecretKey { get; }

		// extern NSString *const MPOAuthCredentialSessionHandleKey;
		[Field ("MPOAuthCredentialSessionHandleKey", "__Internal")]
		NSString AuthCredentialSessionHandleKey { get; }

		// extern NSString *const MPOAuthTokenRefreshDateDefaultsKey;
		[Field ("MPOAuthTokenRefreshDateDefaultsKey", "__Internal")]
		NSString AuthTokenRefreshDateDefaultsKey { get; }

		// @property (readonly, retain, nonatomic) id<MPOAuthCredentialStore,MPOAuthParameterFactory> credentials;
		[Export ("credentials", ArgumentSemantic.Retain)]
		NSObject Credentials { get; }

		// @property (readonly, retain, nonatomic) NSURL * baseURL;
		[Export ("baseURL", ArgumentSemantic.Retain)]
		NSUrl BaseUrl { get; }

		// @property (readonly, retain, nonatomic) NSURL * authenticationURL;
		[Export ("authenticationURL", ArgumentSemantic.Retain)]
		NSUrl AuthenticationUrl { get; }

		// @property (readwrite, retain, nonatomic) MPOAuthAuthenticationMethod * authenticationMethod;
		[Export ("authenticationMethod", ArgumentSemantic.Retain)]
		AuthenticationMethod AuthenticationMethod { get; set; }

		// @property (assign, readwrite, nonatomic) MPOAuthSignatureScheme signatureScheme;
		[Export ("signatureScheme", ArgumentSemantic.Assign)]
		SignatureScheme SignatureScheme { get; set; }

		// @property (readonly, assign, nonatomic) MPOAuthAuthenticationState authenticationState;
		[Export ("authenticationState", ArgumentSemantic.Assign)]
		AuthenticationState AuthenticationState { get; }

		// -(id)initWithCredentials:(NSDictionary *)inCredentials andBaseURL:(NSURL *)inURL;
		[Export ("initWithCredentials:andBaseURL:")]
		IntPtr Constructor (NSDictionary credentials, NSUrl baseUrl);

		// -(id)initWithCredentials:(NSDictionary *)inCredentials authenticationURL:(NSURL *)inAuthURL andBaseURL:(NSURL *)inBaseURL;
		[Export ("initWithCredentials:authenticationURL:andBaseURL:")]
		IntPtr Constructor (NSDictionary credentials, NSUrl authUrl, NSUrl baseUrl);

		// -(id)initWithCredentials:(NSDictionary *)inCredentials authenticationURL:(NSURL *)inAuthURL andBaseURL:(NSURL *)inBaseURL autoStart:(BOOL)aFlag;
		[Export ("initWithCredentials:authenticationURL:andBaseURL:autoStart:")]
		IntPtr Constructor (NSDictionary credentials, NSUrl AuthUrl, NSUrl baseUrl, bool aFlag);

		// -(void)authenticate;
		[Export ("authenticate")]
		void Authenticate ();

		// -(BOOL)isAuthenticated;
		[Export ("isAuthenticated")]
		bool IsAuthenticated { get; }

		// -(void)performMethod:(NSString *)inMethod withTarget:(id)inTarget andAction:(SEL)inAction;
		[Export ("performMethod:withTarget:andAction:")]
		void PerformMethod (string method, NSObject target, Selector action);

		// -(void)performMethod:(NSString *)inMethod atURL:(NSURL *)inURL withParameters:(NSArray *)inParameters withTarget:(id)inTarget andAction:(SEL)inAction;
		[Export ("performMethod:atURL:withParameters:withTarget:andAction:")]
		void PerformMethod (string method, NSUrl url, NSObject[] parameters, NSObject target, Selector action);

		// -(void)performPOSTMethod:(NSString *)inMethod atURL:(NSURL *)inURL withParameters:(NSArray *)inParameters withTarget:(id)inTarget andAction:(SEL)inAction;
		[Export ("performPOSTMethod:atURL:withParameters:withTarget:andAction:")]
		void PerformPostMethod (string method, NSUrl url, NSObject[] parameters, NSObject target, Selector action);

		// -(void)performURLRequest:(NSURLRequest *)inRequest withTarget:(id)inTarget andAction:(SEL)inAction;
		[Export ("performURLRequest:withTarget:andAction:")]
		void PerformUrlRequest (NSUrlRequest request, NSObject target, Selector action);

		// -(NSData *)dataForMethod:(NSString *)inMethod;
		[Export ("dataForMethod:")]
		NSData DataForMethod (string method);

		// -(NSData *)dataForMethod:(NSString *)inMethod withParameters:(NSArray *)inParameters;
		[Export ("dataForMethod:withParameters:")]
		NSData DataForMethod (string method, NSObject[] parameters);

		// -(NSData *)dataForURL:(NSURL *)inURL andMethod:(NSString *)inMethod withParameters:(NSArray *)inParameters;
		[Export ("dataForURL:andMethod:withParameters:")]
		NSData DataForURL (NSUrl url, string method, NSObject[] parameters);

		// -(id)credentialNamed:(NSString *)inCredentialName;
		[Export ("credentialNamed:")]
		NSObject CredentialNamed (string credentialName);

		// -(void)setCredential:(id)inCredential withName:(NSString *)inName;
		[Export ("setCredential:withName:")]
		void SetCredential (NSObject credential, string name);

		// -(void)removeCredentialNamed:(NSString *)inName;
		[Export ("removeCredentialNamed:")]
		void RemoveCredentialNamed (string name);

		// -(void)discardCredentials;
		[Export ("discardCredentials")]
		void DiscardCredentials ();
	}

	// @interface MPOAuthAPIRequestLoader : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof(NSObject), Name = "MPOAuthAPIRequestLoader")]
	interface ApiRequestLoader
	{
		// @property (readwrite, retain, nonatomic) id<MPOAuthCredentialStore,MPOAuthParameterFactory> credentials;
		[Export ("credentials", ArgumentSemantic.Retain)]
		NSObject Credentials { get; set; }

		// @property (readwrite, retain, nonatomic) MPOAuthURLRequest * oauthRequest;
		[Export ("oauthRequest", ArgumentSemantic.Retain)]
		UrlRequest OauthRequest { get; set; }

		// @property (readwrite, retain, nonatomic) MPOAuthURLResponse * oauthResponse;
		[Export ("oauthResponse", ArgumentSemantic.Retain)]
		UrlResponse OauthResponse { get; set; }

		// @property (readonly, retain, nonatomic) NSData * data;
		[Export ("data", ArgumentSemantic.Retain)]
		NSData Data { get; }

		// @property (readonly, retain, nonatomic) NSString * responseString;
		[Export ("responseString", ArgumentSemantic.Retain)]
		string ResponseString { get; }

		// @property (assign, readwrite, nonatomic) id target;
		[Export ("target", ArgumentSemantic.Assign)]
		NSObject Target { get; set; }

		// @property (assign, readwrite, nonatomic) SEL action;
		[Export ("action", ArgumentSemantic.Assign)]
		Selector Action { get; set; }

		// -(id)initWithURL:(NSURL *)inURL;
		[Export ("initWithURL:")]
		IntPtr Constructor (NSUrl url);

		// -(id)initWithRequest:(MPOAuthURLRequest *)inRequest;
		[Export ("initWithRequest:")]
		IntPtr Constructor (UrlRequest request);

		// -(void)loadSynchronously:(BOOL)inSynchronous;
		[Export ("loadSynchronously:")]
		void LoadSynchronously (bool synchronous);
	}

	// @interface MPOAuthAuthenticationMethod : NSObject
	[BaseType (typeof(NSObject), Name = "MPOAuthAuthenticationMethod")]
	interface AuthenticationMethod
	{
		// extern NSString *const MPOAuthAccessTokenURLKey;
		[Field ("MPOAuthAccessTokenURLKey", "__Internal")]
		NSString AccessTokenUrlKey { get; }

		// @property (assign, readwrite, nonatomic) MPOAuthAPI * oauthAPI;
		[Export ("oauthAPI", ArgumentSemantic.Assign)]
		OAuth OAuth { get; set; }

		// @property (readwrite, retain, nonatomic) NSURL * oauthGetAccessTokenURL;
		[Export ("oauthGetAccessTokenURL", ArgumentSemantic.Retain)]
		NSUrl OauthGetAccessTokenUrl { get; set; }

		// -(id)initWithAPI:(MPOAuthAPI *)inAPI forURL:(NSURL *)inURL;
		[Export ("initWithAPI:forURL:")]
		IntPtr Constructor (OAuth oAuth, NSUrl url);

		// -(id)initWithAPI:(MPOAuthAPI *)inAPI forURL:(NSURL *)inURL withConfiguration:(NSDictionary *)inConfig;
		[Export ("initWithAPI:forURL:withConfiguration:")]
		IntPtr Constructor (OAuth oAuth, NSUrl url, NSDictionary config);

		// -(void)authenticate;
		[Export ("authenticate")]
		void Authenticate ();

		// -(void)setTokenRefreshInterval:(NSTimeInterval)inTimeInterval;
		[Export ("setTokenRefreshInterval:")]
		void SetTokenRefreshInterval (double timeInterval);

		// -(void)refreshAccessToken;
		[Export ("refreshAccessToken")]
		void RefreshAccessToken ();
	}

	// @interface MPOAuthAuthenticationMethodOAuth : MPOAuthAuthenticationMethod <MPOAuthAPIInternalClient>
	[BaseType (typeof(AuthenticationMethod), Name = "MPOAuthAuthenticationMethodOAuth")]
	interface AuthenticationMethodOAuth : OAuthInternalClient
	{
		// extern NSString *const MPOAuthNotificationRequestTokenReceived;
		[Field ("MPOAuthNotificationRequestTokenReceived", "__Internal")]
		NSString AuthNotificationRequestTokenReceived { get; }

		// extern NSString *const MPOAuthNotificationRequestTokenRejected;
		[Field ("MPOAuthNotificationRequestTokenRejected", "__Internal")]
		NSString AuthNotificationRequestTokenRejected { get; }

		// @property (assign, readwrite, nonatomic) id<MPOAuthAuthenticationMethodOAuthDelegate> delegate;
		[NullAllowed]
		[Export ("delegate", ArgumentSemantic.Assign)]
		IAuthenticationMethodOAuthDelegate Delegate { get; set; }

		// @property (readwrite, retain, nonatomic) NSURL * oauthRequestTokenURL;
		[Export ("oauthRequestTokenURL", ArgumentSemantic.Retain)]
		NSUrl RequestTokenUrl { get; set; }

		// @property (readwrite, retain, nonatomic) NSURL * oauthAuthorizeTokenURL;
		[Export ("oauthAuthorizeTokenURL", ArgumentSemantic.Retain)]
		NSUrl AuthorizeTokenUrl { get; set; }

		// -(void)authenticate;
		[New]
		[Export ("authenticate")]
		void Authenticate ();
	}

	interface IAuthenticationMethodOAuthDelegate
	{

	}

	// @protocol MPOAuthAuthenticationMethodOAuthDelegate <NSObject>
	[Model]
	[Protocol]
	[BaseType (typeof(NSObject), Name = "MPOAuthAuthenticationMethodOAuthDelegate")]
	interface AuthenticationMethodOAuthDelegate
	{
		// @required -(NSURL *)callbackURLForCompletedUserAuthorization;
		[Abstract]
		[Export ("callbackURLForCompletedUserAuthorization")]
		NSUrl CallbackUrlForCompletedUserAuthorization ();

		// @required -(BOOL)automaticallyRequestAuthenticationFromURL:(NSURL *)inAuthURL withCallbackURL:(NSURL *)inCallbackURL;
		[Abstract]
		[Export ("automaticallyRequestAuthenticationFromURL:withCallbackURL:")]
		bool AutomaticallyRequestAuthenticationFromUrl (NSUrl authUrl, NSUrl callbackUrl);

		// @optional -(NSString *)oauthVerifierForCompletedUserAuthorization;
		[Export ("oauthVerifierForCompletedUserAuthorization")]
		string OauthVerifierForCompletedUserAuthorization ();

		// @optional -(void)authenticationDidFailWithError:(NSError *)error;
		[Export ("authenticationDidFailWithError:")]
		void AuthenticationDidFailWithError (NSError error);
	}

	// @interface MPOAuthConnection : NSURLConnection
	[DisableDefaultCtor]
	[BaseType (typeof(NSUrlConnection), Name = "MPOAuthConnection")]
	interface Connection
	{
		// @property (readonly, nonatomic) id<MPOAuthCredentialStore,MPOAuthParameterFactory> credentials;
		[Export ("credentials")]
		NSObject Credentials { get; }

		// +(MPOAuthConnection *)connectionWithRequest:(MPOAuthURLRequest *)inRequest delegate:(id)inDelegate credentials:(NSObject<MPOAuthCredentialStore,MPOAuthParameterFactory> *)inCredentials;
		[Static]
		[Export ("connectionWithRequest:delegate:credentials:")]
		Connection FromRequest (UrlRequest request, NSObject aDelegate, NSObject credentials);

		// +(NSData *)sendSynchronousRequest:(MPOAuthURLRequest *)inRequest usingCredentials:(NSObject<MPOAuthCredentialStore,MPOAuthParameterFactory> *)inCredentials returningResponse:(MPOAuthURLResponse **)outResponse error:(NSError **)inError;
		[Static]
		[Export ("sendSynchronousRequest:usingCredentials:returningResponse:error:")]
		NSData SendSynchronousRequest (UrlRequest request, NSObject credentials, out UrlResponse outResponse, out NSError error);

		// -(id)initWithRequest:(MPOAuthURLRequest *)inRequest delegate:(id)inDelegate credentials:(NSObject<MPOAuthCredentialStore,MPOAuthParameterFactory> *)inCredentials;
		[Export ("initWithRequest:delegate:credentials:")]
		IntPtr Constructor (UrlRequest request, NSObject aDelegate, NSObject credentials);
	}

	// @interface MPOAuthCredentialConcreteStore : NSObject <MPOAuthCredentialStore, MPOAuthParameterFactory>
	[DisableDefaultCtor]
	[BaseType (typeof(NSObject), Name = "MPOAuthCredentialConcreteStore")]
	interface CredentialConcreteStore : CredentialStoreDelegate, ParameterFactoryDelegate
	{
		// extern NSString * kMPOAuthCredentialConsumerKey;
		[Field ("kMPOAuthCredentialConsumerKey", "__Internal")]
		NSString ConsumerKey { get; }

		// extern NSString * kMPOAuthCredentialConsumerSecret;
		[Field ("kMPOAuthCredentialConsumerSecret", "__Internal")]
		NSString ConsumerSecretKey { get; }

		// extern NSString * kMPOAuthCredentialUsername;
		[Field ("kMPOAuthCredentialUsername", "__Internal")]
		NSString UsernameKey { get; }

		// extern NSString * kMPOAuthCredentialPassword;
		[Field ("kMPOAuthCredentialPassword", "__Internal")]
		NSString PasswordKey { get; }

		// extern NSString * kMPOAuthCredentialRequestToken;
		[Field ("kMPOAuthCredentialRequestToken", "__Internal")]
		NSString RequestTokenKey { get; }

		// extern NSString * kMPOAuthCredentialRequestTokenSecret;
		[Field ("kMPOAuthCredentialRequestTokenSecret", "__Internal")]
		NSString RequestTokenSecretKey { get; }

		// extern NSString * kMPOAuthCredentialAccessToken;
		[Field ("kMPOAuthCredentialAccessToken", "__Internal")]
		NSString AccessTokenKey { get; }

		// extern NSString * kMPOAuthCredentialAccessTokenSecret;
		[Field ("kMPOAuthCredentialAccessTokenSecret", "__Internal")]
		NSString AccessTokenSecretKey { get; }

		// extern NSString * kMPOAuthCredentialSessionHandle;
		[Field ("kMPOAuthCredentialSessionHandle", "__Internal")]
		NSString SessionHandleKey { get; }

		// Symbol not found in binaries
		// extern NSString * kMPOAuthCredentialRealm;
		//		[Field ("kMPOAuthCredentialRealm", "__Internal")]
		//		NSString CredentialRealmKey { get; }

		// extern NSString * kMPOAuthSignatureMethod;
		[Field ("kMPOAuthSignatureMethod", "__Internal")]
		NSString SignatureMethodKey { get; }

		// @property (readonly, retain, nonatomic) NSURL * baseURL;
		[Export ("baseURL", ArgumentSemantic.Retain)]
		NSUrl BaseUrl { get; }

		// @property (readonly, retain, nonatomic) NSURL * authenticationURL;
		[Export ("authenticationURL", ArgumentSemantic.Retain)]
		NSUrl AuthenticationUrl { get; }

		// @property (readonly, nonatomic) NSString * tokenSecret;
		[Export ("tokenSecret")]
		string TokenSecret { get; }

		// @property (readwrite, retain, nonatomic) NSString * requestToken;
		[Export ("setRequestToken:")]
		void SetRequestToken (string requestToken);

		// @property (readwrite, retain, nonatomic) NSString * requestTokenSecret;
		[Export ("setRequestTokenSecret:", ArgumentSemantic.Retain)]
		string SetRequestTokenSecret (string requestTokenSecret);

		// @property (readwrite, retain, nonatomic) NSString * accessToken;
		[Export ("setAccessToken:")]
		void SetAccessToken (string accessToken);

		// @property (readwrite, retain, nonatomic) NSString * accessTokenSecret;
		[Export ("setAccessTokenSecret:")]
		void SetAccessTokenSecret (string accessTokenSecret);

		// @property (readwrite, retain, nonatomic) NSString * sessionHandle;
		[Export ("sessionHandle", ArgumentSemantic.Retain)]
		string SessionHandle { get; set; }

		// -(id)initWithCredentials:(NSDictionary *)inCredential;
		[Export ("initWithCredentials:")]
		IntPtr Constructor (NSDictionary credential);

		// -(id)initWithCredentials:(NSDictionary *)inCredentials forBaseURL:(NSURL *)inBaseURL;
		[Export ("initWithCredentials:forBaseURL:")]
		IntPtr Constructor (NSDictionary credentials, NSUrl baseUrl);

		// -(id)initWithCredentials:(NSDictionary *)inCredentials forBaseURL:(NSURL *)inBaseURL withAuthenticationURL:(NSURL *)inAuthenticationURL;
		[Export ("initWithCredentials:forBaseURL:withAuthenticationURL:")]
		IntPtr Constructor (NSDictionary credentials, NSUrl baseUrl, NSUrl authenticationUrl);

		// -(void)setCredential:(id)inCredential withName:(NSString *)inName;
		[Export ("setCredential:withName:")]
		void AddCredential (NSObject credential, string name);

		// -(void)removeCredentialNamed:(NSString *)inName;
		[Export ("removeCredentialNamed:")]
		void RemoveCredential (string name);
	}

	// @interface MPOAuthCredentialConcreteStore (KeychainAdditions)
	[Category]
	[BaseType (typeof(CredentialConcreteStore))]
	interface CredentialConcreteStore_KeychainAdditions
	{
		// - (void)addToKeychainUsingName:(NSString *)inName andValue:(NSString *)inValue;
		[Export ("addToKeychainUsingName:andValue:")]
		void AddToKeychain (string name, string value);

		// - (NSString *)findValueFromKeychainUsingName:(NSString *)inName;
		[Export ("findValueFromKeychainUsingName:")]
		string FindValueFromKeychain (string name);

		// - (void)removeValueFromKeychainUsingName:(NSString *)inName;
		[Export ("removeValueFromKeychainUsingName:")]
		void RemoveValueFromKeychain (string name);
	}

	interface ICredentialStoreDelegate
	{

	}

	// @protocol MPOAuthCredentialStore <NSObject>
	[Protocol]
	[BaseType (typeof(NSObject), Name = "MPOAuthCredentialStore")]
	interface CredentialStoreDelegate
	{
		// @required @property (readonly, nonatomic) NSString * consumerKey;
		[Export ("consumerKey")]
		string GetConsumerKey ();

		// @required @property (readonly, nonatomic) NSString * consumerSecret;
		[Export ("consumerSecret")]
		string GetConsumerSecret ();

		// @required @property (readonly, nonatomic) NSString * username;
		[Export ("username")]
		string GetUsername ();

		// @required @property (readonly, nonatomic) NSString * password;
		[Export ("password")]
		string GetPassword ();

		// @required @property (readonly, retain, nonatomic) NSString * requestToken;
		[Export ("requestToken")]
		string GetRequestToken ();

		// @required @property (readonly, retain, nonatomic) NSString * requestTokenSecret;
		[Export ("requestTokenSecret")]
		string GetRequestTokenSecret ();

		// @required @property (readonly, retain, nonatomic) NSString * accessToken;
		[Export ("accessToken")]
		string GetAccessToken ();

		// @required @property (readonly, retain, nonatomic) NSString * accessTokenSecret;
		[Export ("accessTokenSecret")]
		string GetAccessTokenSecret ();

		// @required -(NSString *)credentialNamed:(NSString *)inCredentialName;
		[Export ("credentialNamed:")]
		string CredentialNamed (string credentialName);

		// @required -(void)discardOAuthCredentials;
		[Export ("discardOAuthCredentials")]
		void DiscardOAuthCredentials ();
	}

	interface IParameterFactoryDelegate
	{

	}

	// @protocol MPOAuthParameterFactory <NSObject>
	[Protocol]
	[BaseType (typeof(NSObject), Name = "MPOAuthParameterFactory")]
	interface ParameterFactoryDelegate
	{
		// @required @property (readwrite, retain, nonatomic) NSString * signatureMethod;
		[Export ("signatureMethod")]
		string GetSignatureMethod ();

		[Export ("setSignatureMethod:")]
		void SetSignatureMethod (string signatureMethod);

		// @required @property (readonly, nonatomic) NSString * signingKey;
		[Export ("signingKey")]
		string GetSigningKey ();

		// @required @property (readonly, nonatomic) NSString * timestamp;
		[Export ("timestamp")]
		string GetTimestamp ();

		// @required -(NSArray *)oauthParameters;
		[Export ("oauthParameters")]
		NSObject[] GetOauthParameters ();

		// @required -(MPURLRequestParameter *)oauthConsumerKeyParameter;
		[Export ("oauthConsumerKeyParameter")]
		UrlRequestParameter GetOauthConsumerKeyParameter ();

		// @required -(MPURLRequestParameter *)oauthTokenParameter;
		[Export ("oauthTokenParameter")]
		UrlRequestParameter GetOauthTokenParameter ();

		// @required -(MPURLRequestParameter *)oauthSignatureMethodParameter;
		[Export ("oauthSignatureMethodParameter")]
		UrlRequestParameter GetOauthSignatureMethodParameter ();

		// @required -(MPURLRequestParameter *)oauthTimestampParameter;
		[Export ("oauthTimestampParameter")]
		UrlRequestParameter GetOauthTimestampParameter ();

		// @required -(MPURLRequestParameter *)oauthNonceParameter;
		[Export ("oauthNonceParameter")]
		UrlRequestParameter GetOauthNonceParameter ();

		// @required -(MPURLRequestParameter *)oauthVersionParameter;
		[Export ("oauthVersionParameter")]
		UrlRequestParameter GetOauthVersionParameter ();
	}

	// @interface MPOAuthSignatureParameter : MPURLRequestParameter
	[DisableDefaultCtor]
	[BaseType (typeof(UrlRequestParameter), Name = "MPOAuthSignatureParameter")]
	interface SignatureParameter
	{
		// +(NSString *)signatureBaseStringUsingParameterString:(NSString *)inParameterString forRequest:(MPOAuthURLRequest *)inRequest;
		[Static]
		[Export ("signatureBaseStringUsingParameterString:forRequest:")]
		string SignatureBaseString (string parameterString, UrlRequest request);

		// +(NSString *)HMAC_SHA1SignatureForText:(NSString *)inText usingSecret:(NSString *)inSecret;
		[Static]
		[Export ("HMAC_SHA1SignatureForText:usingSecret:")]
		string HMACSha1Signature (string text, string secret);

		// -(id)initWithText:(NSString *)inText andSecret:(NSString *)inSecret forRequest:(MPOAuthURLRequest *)inRequest usingMethod:(NSString *)inMethod;
		[Export ("initWithText:andSecret:forRequest:usingMethod:")]
		IntPtr Constructor (string text, string secret, UrlRequest request, string method);
	}

	// @interface MPOAuthURLRequest : NSObject
	[DisableDefaultCtor]
	[BaseType (typeof(NSObject), Name = "MPOAuthURLRequest")]
	interface UrlRequest
	{
		// @property (readwrite, retain, nonatomic) NSURL * url;
		[Export ("url", ArgumentSemantic.Retain)]
		NSUrl Url { get; set; }

		// @property (readwrite, retain, nonatomic) NSString * HTTPMethod;
		[Export ("HTTPMethod", ArgumentSemantic.Retain)]
		string HttpMethod { get; set; }

		// @property (readonly, retain, nonatomic) NSURLRequest * urlRequest;
		[Export ("urlRequest", ArgumentSemantic.Retain)]
		NSUrlRequest Request { get; }

		// @property (readwrite, retain, nonatomic) NSMutableArray * parameters;
		[Export ("parameters", ArgumentSemantic.Retain)]
		NSMutableArray Parameters { get; set; }

		// -(id)initWithURL:(NSURL *)inURL andParameters:(NSArray *)parameters;
		[Export ("initWithURL:andParameters:")]
		IntPtr Constructor (NSUrl url, NSObject[] parameters);

		// -(id)initWithURLRequest:(NSURLRequest *)inRequest;
		[Export ("initWithURLRequest:")]
		IntPtr Constructor (NSUrlRequest request);

		// -(void)addParameters:(NSArray *)inParameters;
		[Export ("addParameters:")]
		void AddParameters (NSObject[] parameters);

		// -(NSMutableURLRequest *)urlRequestSignedWithSecret:(NSString *)inSecret usingMethod:(NSString *)inScheme;
		[Export ("urlRequestSignedWithSecret:usingMethod:")]
		NSMutableUrlRequest UrlRequestSignedWithSecret (string secret, string scheme);
	}

	// @interface MPOAuthURLResponse : NSObject
	[BaseType (typeof(NSObject), Name = "MPOAuthURLResponse")]
	interface UrlResponse
	{
		// @property (readonly, retain, nonatomic) NSURLResponse * urlResponse;
		[Export ("urlResponse", ArgumentSemantic.Retain)]
		NSUrlResponse Response { get; }

		// @property (readonly, retain, nonatomic) NSDictionary * oauthParameters;
		[Export ("oauthParameters", ArgumentSemantic.Retain)]
		NSDictionary OauthParameters { get; }
	}

	// @interface MPURLRequestParameter : NSObject
	[BaseType (typeof(NSObject), Name = "MPURLRequestParameter")]
	interface UrlRequestParameter
	{
		// @property (readwrite, copy, nonatomic) NSString * name;
		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; set; }

		// @property (readwrite, copy, nonatomic) NSString * value;
		[Export ("value", ArgumentSemantic.Copy)]
		string Value { get; set; }

		// +(NSArray *)parametersFromString:(NSString *)inString;
		[Static]
		[Export ("parametersFromString:")]
		UrlRequestParameter[] Parameters (string aString);

		// +(NSArray *)parametersFromDictionary:(NSDictionary *)inDictionary;
		[Static]
		[Export ("parametersFromDictionary:")]
		UrlRequestParameter[] Parameters (NSDictionary dictionary);

		// +(NSDictionary *)parameterDictionaryFromString:(NSString *)inString;
		[Static]
		[Export ("parameterDictionaryFromString:")]
		NSDictionary ParameterDictionary (string aString);

		// +(NSString *)parameterStringForParameters:(NSArray *)inParameters;
		[Static]
		[Export ("parameterStringForParameters:")]
		string ParameterString (UrlRequestParameter[] parameters);

		// +(NSString *)parameterStringForDictionary:(NSDictionary *)inParameterDictionary;
		[Static]
		[Export ("parameterStringForDictionary:")]
		string ParameterString (NSDictionary dictionary);

		// -(id)initWithName:(NSString *)inName andValue:(NSString *)inValue;
		[Export ("initWithName:andValue:")]
		IntPtr Constructor (string name, string value);

		// -(NSString *)URLEncodedParameterString;
		[Export ("URLEncodedParameterString")]
		string UrlEncodedParameterString { get; }
	}

	// @interface NSDictionary (Dropbox)
	[Category]
	[BaseType (typeof(NSDictionary))]
	interface NSDictionary_Dropbox
	{
		// - (NSString *)urlRepresentation;
		[Export ("urlRepresentation")]
		string UrlRepresentation ();
	}

	// @interface NSObject (NSObject_DBJSON)
	[Category]
	[BaseType (typeof(NSObject))]
	interface NSObject_DBJson
	{
		// - (NSString *)urlRepresentation;
		[Export ("JSONRepresentation")]
		string JsonRepresentation ();
	}

	[Category]
	[BaseType (typeof(NSString))]
	interface NSString_Extension
	{
		// - (id)JSONValue;
		[Export ("JSONValue")]
		NSObject JsonValue ();

		// - (NSString*)normalizedDropboxPath;
		[Export ("normalizedDropboxPath")]
		string NormalizedDropboxPath ();

		// - (BOOL)isEqualToDropboxPath:(NSString*)otherPath;
		[Export ("isEqualToDropboxPath:")]
		bool IsEqualToDropboxPath (string otherPath);

		// - (BOOL)isIPAddress;
		[Export ("isIPAddress")]
		bool IsIPAddress ();

		// - (NSString *)stringByAddingURIPercentEscapesUsingEncoding:(NSStringEncoding)inEncoding;
		[Export ("stringByAddingURIPercentEscapesUsingEncoding:")]
		string StringByAddingUriPercentEscapes (NSStringEncoding encoding);
	}

	[Category]
	[BaseType (typeof(NSUrl))]
	interface NSUrl_Extension
	{
		// - (NSString *)stringByAddingURIPercentEscapesUsingEncoding:(NSStringEncoding)inEncoding;
		[Export ("stringByAddingURIPercentEscapesUsingEncoding:")]
		string StringByAddingUriPercentEscapes (NSStringEncoding encoding);

		// - (NSURL *)urlByAddingParameters:(NSArray *)inParameters;
		[Export ("urlByAddingParameters:")]
		NSUrl UrlByAddingParameters (NSObject[] parameters);

		// - (NSURL *)urlByAddingParameterDictionary:(NSDictionary *)inParameters;
		[Export ("urlByAddingParameterDictionary:")]
		NSUrl UrlByAddingParameters (NSDictionary dictionary);

		// - (NSURL *)urlByRemovingQuery;
		[Export ("urlByRemovingQuery")]
		NSUrl UrlByRemovingQuery ();

		// - (NSString *)absoluteNormalizedString;
		[Export ("absoluteNormalizedString")]
		string AbsoluteNormalizedString ();

		// - (BOOL)domainMatches:(NSString *)inString;
		[Export ("domainMatches:")]
		bool DomainMatches (string aString);

		// - (NSStringEncoding)encoding;
		[Export ("encoding")]
		NSStringEncoding Encoding ();
	}
}