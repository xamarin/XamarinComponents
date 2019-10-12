using System;
using System.IO;

namespace Xamarin.AndroidBinderator.Tests
{
	public class BaseTest : IDisposable
	{
		public BaseTest()
		{
			RootDirectory = GetTempDirectory();
		}

		public string RootDirectory { get; }

		public void Dispose()
		{
			if (Directory.Exists(RootDirectory))
				Directory.Delete(RootDirectory, true);
		}

		public string CreateTemplate(string filename, string contents)
		{
			var template = Path.Combine(RootDirectory, filename);
			File.WriteAllText(template, contents);
			return template;
		}

		public string CreateTemplate(string contents = null) =>
			CreateTemplate("Template.cshtml", contents ?? @"<Project Sdk=""Microsoft.NET.Sdk""></Project>");

		public static string GetTempDirectory(bool createDirectory = true)
		{
			var newPath = Path.Combine(
				Path.GetTempPath(),
				"Xamarin.AndroidBinderator.Tests",
				Guid.NewGuid().ToString());

			if (createDirectory && !Directory.Exists(newPath))
				Directory.CreateDirectory(newPath);

			return newPath;
		}
	}
}
