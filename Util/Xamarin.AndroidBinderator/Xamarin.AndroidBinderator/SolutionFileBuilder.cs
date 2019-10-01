using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace AndroidBinderator
{
    public static class SolutionFileBuilder
    {
        public static string Build (BindingConfig config, Dictionary<string, BindingProjectModel> projects)
        {
			var csprojNamespaces = new XmlNamespaceManager(new NameTable());
			csprojNamespaces.AddNamespace("ns", "http://schemas.microsoft.com/developer/msbuild/2003");

			var slnFileInfo = new FileInfo(config.SolutionFile);

			// Collect all the projects to be added to the .sln
			var allProjects = new List<(string guid, string name, string key)>();

			// First go through additional projects specified in the config
			foreach (var ap in config.AdditionalProjects)
			{
				var prjPath = Path.Combine(config.BasePath, ap);
				var prjPathFileInfo = new FileInfo(prjPath);

				if (!File.Exists(prjPath))
					throw new FileNotFoundException(prjPath);

				var prjName = Path.GetFileNameWithoutExtension(prjPathFileInfo.Name);

				// Try and find an existing project guid in the .csproj
				// which might not exist if it's an SDK style project
				var xml = XDocument.Load(prjPath);
				var xelem = xml.XPathSelectElement("/ns:Project/ns:PropertyGroup/ns:ProjectGuid", csprojNamespaces);

				var prjGuid = xelem?.Value ?? Guid.NewGuid().ToString("B");
				var prjKey = GetRelativePath(slnFileInfo.FullName, prjPathFileInfo.FullName).Replace("/", "\\");

				allProjects.Add((prjGuid, prjName, prjKey));
			}

			// Add all of the generated projects
			foreach (var p in projects)
			{
				var groupId = p.Value.MavenGroupId;
				var prjName = groupId + "." + p.Value.Name;
				var prjKey = GetRelativePath(slnFileInfo.FullName, p.Key).Replace("/", "\\");
				var prjGuid = "{" + p.Value.Id + "}";

				allProjects.Add((prjGuid, prjName, prjKey));
			}

			var s = new StringBuilder();

            s.AppendLine();
            s.AppendLine("Microsoft Visual Studio Solution File, Format Version 12.00");
            s.AppendLine("# Visual Studio 2012");

            //Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "@(project.Name)", "@(project.Name)\@(project.Name).csproj", "@(project.Id)"
            foreach (var project in allProjects)
            {
                s.AppendLine("Project(\"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}\") = \"" + project.name + "\", \"" + project.key + "\", \"" + project.guid + "\"");
                s.AppendLine("EndProject");
            }
            
            s.AppendLine("Global");

            s.AppendLine("\tGlobalSection(SolutionConfigurationPlatforms) = preSolution");
            s.AppendLine("\t\tDebug|Any CPU = Debug|Any CPU");
            s.AppendLine("\t\tRelease|Any CPU = Release|Any CPU");
            s.AppendLine("\tEndGlobalSection");

            s.AppendLine("\tGlobalSection(ProjectConfigurationPlatforms) = postSolution");
            foreach (var project in allProjects) {
                s.AppendLine("\t\t" + project.guid + ".Debug|Any CPU.ActiveCfg = Debug|Any CPU");
                s.AppendLine("\t\t" + project.guid + ".Debug|Any CPU.Build.0 = Debug|Any CPU");
                s.AppendLine("\t\t" + project.guid + ".Release|Any CPU.ActiveCfg = Release|Any CPU");
                s.AppendLine("\t\t" + project.guid + ".Release|Any CPU.Build.0 = Release|Any CPU");
            }
            s.AppendLine("\tEndGlobalSection");

            s.AppendLine("EndGlobal");

            return s.ToString();
        }

		static string GetRelativePath(string basePath, string filePath)
		{
			var path1 = new Uri(basePath);
			var path2 = new Uri(filePath);
			Uri diff = path1.MakeRelativeUri(path2);
			return diff.OriginalString;
		}
	}
}
