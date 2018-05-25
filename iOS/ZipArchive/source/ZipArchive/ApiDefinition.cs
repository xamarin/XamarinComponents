using System;
using Foundation;
using ObjCRuntime;

namespace MiniZip.ZipArchive
{
	interface IZipArchiveDelegate
	{
	}

	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "ZipArchiveDelegate")]
	interface ZipArchiveDelegate
	{
		// @optional - (void)ErrorMessage:(NSString *)msg;
		[Export ("ErrorMessage:")]
		[EventName ("OnError")]
		void ErrorMessage (string msg);

		// @optional - (BOOL)OverWriteOperation:(NSString *)file;
		[Export ("OverWriteOperation:")]
		[DelegateName ("ZipArchiveCondition")]
		[DefaultValue ("false")]
		bool OverWriteOperation (string file);
	}

	[BaseType (typeof(NSObject), Delegates = new [] { "Delegate" }, Events = new [] { typeof(ZipArchiveDelegate) }, Name = "ZipArchive")]
	interface ZipArchive
	{
		// @required - (id)initWithFileManager:(NSFileManager *)fileManager;
		[Export ("initWithFileManager:")]
		IntPtr Constructor (NSFileManager fileManager);

		// @property (retain, nonatomic) id<ZipArchiveDelegate> delegate;
		[Export ("delegate", ArgumentSemantic.Retain)]
		[NullAllowed]
		IZipArchiveDelegate Delegate { get; set; }

		// @property (readonly, nonatomic) unsigned long numFiles;
		[Export ("numFiles")]
		nuint NumFiles { get; }

		// @property (copy, nonatomic) ZipArchiveProgressUpdateBlock progressBlock;
		[Export ("progressBlock", ArgumentSemantic.Copy)]
		Action<int, int, nuint> ProgressBlock { get; set; }

		// @property (nonatomic, assign) ZipArchiveCompression compression;
		[Export ("compression", ArgumentSemantic.Assign)]
		Compression Compression { get; }

		// @property (assign, nonatomic) NSStringEncoding stringEncoding;
		[Export ("stringEncoding", ArgumentSemantic.UnsafeUnretained)]
		nuint StringEncoding { get; set; }

		// @property (readonly, nonatomic) NSArray * unzippedFiles;
		[Export ("unzippedFiles")]
		NSObject [] UnzippedFiles { get; }

		// @required - (BOOL)CreateZipFile2:(NSString *)zipFile;
		[Export ("CreateZipFile2:")]
		bool CreateZipFile (string zipFile);

		// @required - (BOOL)CreateZipFile2:(NSString*)zipFile append:(BOOL)isAppend;
		[Export ("CreateZipFile2:append:")]
		bool CreateZipFile (string zipFile, bool isAppend);

		// @required - (BOOL)CreateZipFile2:(NSString *)zipFile Password:(NSString *)password;
		[Export ("CreateZipFile2:Password:")]
		bool CreateZipFile (string zipFile, string password);
		
		// @required - (BOOL)CreateZipFile2:(NSString*)zipFile Password:(NSString*)password append:(BOOL)isAppend;
		[Export ("CreateZipFile2:Password:append:")]
		bool CreateZipFile (string zipFile, string password, bool isAppend);

		// @required - (BOOL)addFileToZip:(NSString *)file newname:(NSString *)newname;
		[Export ("addFileToZip:newname:")]
		bool AddFile (string file, string newname);
		
		// @required - (BOOL)addDataToZip:(NSData*)data fileAttributes:(NSDictionary *)attr newname:(NSString*)newname;
		[Export ("addDataToZip:fileAttributes:newname:")]
		bool AddFile (NSData data, NSDictionary attr, string newname);

		// @required - (BOOL)CloseZipFile2;
		[Export ("CloseZipFile2")]
		bool CloseZipFile ();

		// @required - (BOOL)UnzipOpenFile:(NSString *)zipFile;
		[Export ("UnzipOpenFile:")]
		bool UnzipOpenFile (string zipFile);

		// @required - (BOOL)UnzipOpenFile:(NSString *)zipFile Password:(NSString *)password;
		[Export ("UnzipOpenFile:Password:")]
		bool UnzipOpenFile (string zipFile, string password);

		// @required - (BOOL)UnzipFileTo:(NSString *)path overWrite:(BOOL)overwrite;
		[Export ("UnzipFileTo:overWrite:")]
		bool UnzipFileTo (string path, bool overwrite);

		// @required - (BOOL)UnzipCloseFile;
		[Export ("UnzipCloseFile")]
		bool UnzipCloseFile ();

		// @required - (NSDictionary *)UnzipFileToMemory;
		[Export ("UnzipFileToMemory")]
		NSDictionary UnzipFileToMemory ();

		// @required - (NSArray *)getZipFileContents;
		[Export ("getZipFileContents")]
		NSObject [] GetZipFileContents ();
	}
}
