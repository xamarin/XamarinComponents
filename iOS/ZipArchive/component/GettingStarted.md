MiniZip.ZipArchive is a Xamarin.iOS binding based on the open source
Objective-C ZipArchive library.  It adds the ability to zip and unzip
files to your app.

For quick and simple usage, use the EasyZip and EasyUnZip methods.

## Examples

### Stepwise zip creation

```
var zip = new ZipArchive ();
zip.CreateZipFile ("myfiles.zip", "passw0rd");
zip.AddFolder ("my_directory", "prefix");
zip.CloseZipFile ();
```

### Unzip a file:

```
var zip = new ZipArchive ();
zip.UnzipOpenFile ("myfiles.zip", "passw0rd");
zip.UnzipFileTo ("my_directory1", true);

zip.OnError += (sender, args) => {
	Console.WriteLine ("Error while unzipping: {0}", args);
};

zip.UnzipCloseFile ();
```
