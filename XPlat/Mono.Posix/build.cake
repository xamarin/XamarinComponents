#addin nuget:?package=Cake.FileHelpers&version=3.2.1
#addin nuget:?package=Cake.Docker&version=0.11.0
#tool nuget:?package=Microsoft.DotNet.BuildTools.GenAPI&version=1.0.0-beta-00081&prerelease

var TARGET = Argument("t", Argument("target", "ci"));

var DEFAULT_SN_EXE = "sn";
if (IsRunningOnWindows()) {
	var MSSDK = "C:/Program Files (x86)/Microsoft SDKs";
	DEFAULT_SN_EXE = GetFiles($"{MSSDK}/Windows/v10.0A/bin/*/sn.exe").First().FullPath;
}

var SN_EXE = Argument("sn", EnvironmentVariable("SN_EXE") ?? DEFAULT_SN_EXE);

var MONO_VERSION = "5.20.1.34";
var MONO_GIT_REPO = "mono/mono";
var MONOGIT_VERSION = $"mono-{MONO_VERSION}";
var MONOPOSIXHELPER_JENKINSBUILD = "46";

var NUGET_VERSION = "5.20.1";

var DOCKER_PULL_TARGET = "microsoft/dotnet@sha256:83ea086418516a9b6311acf393f1474d241015902b380ac7c3dfe90f32b72dad";
var DOCKER_RUN_IMAGE = "411cc86cfa2e";

var DISTRO_LIST = new [] {
	"centos-6-x86",
	"centos-6-x64",
	"debian-8-arm",
	"debian-8-armel",
	"debian-8-arm64",
};
var WINDOWS_LIST = new [] {
	"win-7-x64",
	"win-7-x86",
};
var MAC_LIST = new [] {
	"osx-10.7-universal",
};

Task("externals")
	.Does(() =>
{
	EnsureDirectoryExists("./externals/");

	// mono source
	if (!FileExists("./externals/mono-source.zip")) {
		DownloadFile($"https://github.com/{MONO_GIT_REPO}/archive/{MONOGIT_VERSION}.zip", "./externals/mono-source.zip");
	}
	if (!DirectoryExists("./externals/mono")) {
		Unzip("./externals/mono-source.zip", "./externals/");
		MoveDirectory($"./externals/mono-{MONOGIT_VERSION}", "./externals/mono");
	}

	// Consts.cs.in > Consts.cs
	var constscs = "./externals/mono/mcs/build/common/Consts.cs";
	if (!FileExists(constscs)) {
		CopyFile("./externals/mono/mcs/build/common/Consts.cs.in", constscs);
		ReplaceTextInFiles(constscs, "@MONO_VERSION@", MONO_VERSION);
		ReplaceTextInFiles(constscs, "@MONO_CORLIB_VERSION@", "0");
	}

	// libMonoPosixHelper.so
	foreach (var distro in DISTRO_LIST) {
		var dest = $"./externals/artifacts/{distro}/libMonoPosixHelper.so";
		if (!FileExists(dest)) {
			var so = $"https://xamjenkinsartifact.blob.core.windows.net/ng-extract-libmonoposixhelper/{MONOPOSIXHELPER_JENKINSBUILD}/results/{distro}/libMonoPosixHelper.so";
			EnsureDirectoryExists($"./externals/artifacts/{distro}/");
			DownloadFile(so, dest);
		}
	}
	// libMonoPosixHelper.dll
	foreach (var distro in WINDOWS_LIST) {
		var dest = $"./externals/artifacts/{distro}/libMonoPosixHelper.dll";
		if (!FileExists(dest)) {
			var so = $"https://xamjenkinsartifact.blob.core.windows.net/ng-extract-libmonoposixhelper/{MONOPOSIXHELPER_JENKINSBUILD}/results/{distro}/libMonoPosixHelper.dll";
			EnsureDirectoryExists($"./externals/artifacts/{distro}/");
			DownloadFile(so, dest);
		}
	}
	// libMonoPosixHelper.dylib
	foreach (var distro in MAC_LIST) {
		var dest = $"./externals/artifacts/{distro}/libMonoPosixHelper.dylib";
		if (!FileExists(dest)) {
			var dylib = $"https://xamjenkinsartifact.blob.core.windows.net/ng-extract-libmonoposixhelper/{MONOPOSIXHELPER_JENKINSBUILD}/results/{distro}/libMonoPosixHelper.dylib";
			EnsureDirectoryExists($"./externals/artifacts/{distro}/");
			DownloadFile(dylib, dest);
		}
	}
	// MonoPosixHelper.dll
	foreach (var distro in WINDOWS_LIST) {
		var dest = $"./externals/artifacts/{distro}/MonoPosixHelper.dll";
		if (!FileExists(dest)) {
			var so = $"https://xamjenkinsartifact.blob.core.windows.net/ng-extract-libmonoposixhelper/{MONOPOSIXHELPER_JENKINSBUILD}/results/{distro}/MonoPosixHelper.dll";
			EnsureDirectoryExists($"./externals/artifacts/{distro}/");
			DownloadFile(so, dest);
		}
	}
});

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	// build for windows
	MSBuild($"./externals/mono/mcs/class/Mono.Posix/Mono.Posix.NETStandard-netstandard_2_0.csproj", c => c
		.SetConfiguration("Release")
		.WithRestore()
		.WithTarget("Build")
		.WithProperty("DebugType", "portable")
		.WithProperty("ForceUseLibC", "false")
		.WithProperty("OutputPath", MakeAbsolute((DirectoryPath)"./working/lib/any/").FullPath));

	// build for unix
	MSBuild($"./externals/mono/mcs/class/Mono.Posix/Mono.Posix.NETStandard-netstandard_2_0.csproj", c => c
		.SetConfiguration("Release")
		.WithRestore()
		.WithTarget("Build")
		.WithProperty("DebugType", "portable")
		.WithProperty("ForceUseLibC", "true")
		.WithProperty("OutputPath", MakeAbsolute((DirectoryPath)"./working/lib/unix/").FullPath));

	// generate the type forwards
	EnsureDirectoryExists("./working/cs/");
	StartProcess(Context.Tools.Resolve("GenAPI.exe"), 
		"-w:TypeForwards ./working/lib/any/Mono.Posix.NETStandard.dll -out ./working/cs/Mono.Posix.generated.cs");

	// build for the type forwards
	MSBuild($"./source/Mono.Posix.TypeForwards.csproj", c => c
		.SetConfiguration("Release")
		.WithRestore()
		.WithTarget("Build")
		.WithProperty("OutputPath", MakeAbsolute((DirectoryPath)"./working/ref/").FullPath));

	// make sure the files are signed correctly
	foreach (var dll in GetFiles("./working/lib/*/*.dll")) {
		StartProcess(SN_EXE, $"-R {dll} ./externals/mono/mcs/class/Open.snk");
	}
});

Task("nuget")
	.IsDependentOn("libs")
	.Does(() =>
{
	NuGetPack("./nuget/Mono.Posix.NETStandard.nuspec", new NuGetPackSettings {
		BasePath = MakeAbsolute((DirectoryPath)"./").FullPath,
		OutputDirectory = "./output/",
		Version = NUGET_VERSION,
	});
});

Task("tests")
	.IsDependentOn("nuget")
	.Does(() =>
{
	// prepare
	EnsureDirectoryExists("./working/");
	CleanDirectories("./working/");
	EnsureDirectoryExists("./working/test/nugets");
	EnsureDirectoryExists("./working/test/tests");

	// copy the files to the working directory
	CopyDirectory("./tests/", "./working/test/");
	CopyDirectory("./externals/mono/mcs/class/Mono.Posix/Test/", "./working/test/tests");
	CopyFiles("./output/*.nupkg", "./working/test/nugets/");

	// run the tests
	DockerBuild(
		new DockerImageBuildSettings { Tag = new [] { "monoposix/tests" } },
		"./working/test/");

	// copy the test results out
	DockerCreate(
		new DockerContainerCreateSettings {
			Name = "dummy",
			Interactive = true,
			Tty = true,
		},
		"monoposix/tests",
		"bash");
	DockerCp("dummy:/working/TestResults.xml", "./output/");
	DockerRm("dummy");
});

Task("clean")
	.Does(() =>
{
	CleanDirectories("./externals/");
	CleanDirectories("./output/");
	CleanDirectories("./working/");
});


Task("ci")
	.IsDependentOn("nuget")
	.IsDependentOn("tests");

RunTarget(TARGET);
