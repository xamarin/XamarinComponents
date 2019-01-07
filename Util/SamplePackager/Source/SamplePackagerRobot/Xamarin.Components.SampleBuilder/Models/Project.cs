using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Xamarin.Components.SampleBuilder.Enums;

namespace Xamarin.Components.SampleBuilder.Models
{
    public class Project
    {
        private string _path;

        public string Name { get; set; }

        public ProjectType Type { get; set; }

        public List<ProjectReference> ProjectReferences { get; set; }

        public string PackageId { get; set; }

        public string PackageVersion { get; set; }

        public Project(string path)
        {
            _path = path;

            ProjectReferences = new List<ProjectReference>();

            Name = Path.GetFileNameWithoutExtension(_path);
        }

        internal void AddPackageReferenceSdk(string packageId, string packageVersion)
        {
            var xml = new XmlDocument();
            xml.Load(_path);

            var projectNode = FindProjectNode(xml);

            var packageParent = FindParentNode(projectNode, "PackageReference");

            if (packageParent == null)
            {
                packageParent = xml.CreateElement("ItemGroup", xml.DocumentElement.NamespaceURI);

                projectNode.InsertAfter(packageParent, projectNode.LastChild);
            }

            var newPackage = xml.CreateElement("PackageReference", xml.DocumentElement.NamespaceURI);
            var include = xml.CreateAttribute("Include");
            include.Value = packageId;
            newPackage.Attributes.Append(include);

            var version = xml.CreateAttribute("Version");
            version.Value = packageVersion;
            newPackage.Attributes.Append(version);

            packageParent.AppendChild(newPackage);

            xml.Save(_path);
        }

        internal void AddPackageReferenceClassic(string packageId, string packageVersion)
        {
            var xml = new XmlDocument();
            xml.Load(_path);

            var projectNode = FindProjectNode(xml);

            var packageParent = FindParentNode(projectNode, "PackageReference");

            if (packageParent == null)
            {
                packageParent = xml.CreateElement("ItemGroup", xml.DocumentElement.NamespaceURI);

                projectNode.InsertAfter(packageParent, projectNode.LastChild);
            }

            var newPackage = xml.CreateElement("PackageReference", xml.DocumentElement.NamespaceURI);
            var include = xml.CreateAttribute("Include");
            include.Value = packageId;
            newPackage.Attributes.Append(include);

            var verPackage = xml.CreateElement("Version", xml.DocumentElement.NamespaceURI);
            verPackage.InnerText = packageVersion;

            newPackage.AppendChild(verPackage);

            packageParent.AppendChild(newPackage);

            xml.Save(_path);
        }

        internal void Build()
        {
            var xml = new XmlDocument();
            xml.Load(_path);

            XmlNode projectNode = FindProjectNode(xml);
            
            var toolsVersion = projectNode.Attributes["ToolsVersion"];
            var sdkVersion = projectNode.Attributes["Sdk"];

            if (toolsVersion != null && sdkVersion == null)
                Type = ProjectType.Classic;
            else if (toolsVersion == null & sdkVersion != null)
                Type = ProjectType.SDK;

            switch (Type)
            {
                case ProjectType.SDK:
                    {
                        FindProjectsSdk(projectNode);


                        FindSdkNugetDetails(xml);
                    }
                    break;
                case ProjectType.Classic:
                    {
                        FindProjectsClassic(projectNode);
                    }
                    break;
                default:
                    {
                        throw new Exception("Unable to determine the type of csproj");
                    }
            }
        }

        internal void RemoveReferenceClassic(string projectId)
        {
            var xml = new XmlDocument();
            xml.Load(_path);

            XmlNode projectNode = FindProjectNode(xml);

            XmlNode lstProjectNode = null;

            var references = new List<XmlNode>();

            foreach (XmlNode aNode in projectNode.ChildNodes)
            {
                lstProjectNode = FindParentNode(aNode, "ProjectReference");

                if (lstProjectNode != null)
                {
                    foreach (XmlNode aRef in lstProjectNode.ChildNodes)
                    {
                        references.Add(aRef);
                    }
                    break;
                }
                    
            }

            foreach (var xmlNode in references)
            {
                var xnprojectId = xmlNode.ChildNodes[0].InnerText;

                if (xnprojectId.Equals(projectId, StringComparison.OrdinalIgnoreCase))
                {
                    lstProjectNode.RemoveChild(xmlNode);
                }
            }

            if (lstProjectNode.ChildNodes.Count == 0)
                projectNode.RemoveChild(lstProjectNode);

            xml.Save(_path);
        }

        internal void RemoveReferenceSDK(string projectName)
        {
            var xml = new XmlDocument();
            xml.Load(_path);

            XmlNode projectNode = FindProjectNode(xml);

            XmlNode lstProjectNode = null;

            var references = new List<XmlNode>();

            foreach (XmlNode aNode in projectNode.ChildNodes)
            {
                lstProjectNode = FindParentNode(aNode, "ProjectReference");

                if (lstProjectNode != null)
                {
                    foreach (XmlNode aRef in lstProjectNode.ChildNodes)
                    {
                        references.Add(aRef);
                    }
                    break;
                }

            }

            foreach (XmlNode aProject in references)
            {
                var link = aProject.Attributes["Include"];

                if (link != null)
                {
                    var csProj = projectName.ToLower() + ".csproj";

                    if (link.InnerText.ToLower().Contains(csProj))
                    {
                        lstProjectNode.RemoveChild(aProject);
                    }

                }
            }

            if (lstProjectNode.ChildNodes.Count == 0)
                projectNode.RemoveChild(lstProjectNode);

            xml.Save(_path);
        }

        private XmlNode FindProjectNode(XmlDocument xml)
        {
            XmlNode projectNode = null;
            var loop = 0;

            while (projectNode == null && xml.ChildNodes.Count > loop)
            {
                var node = xml.ChildNodes[loop];

                if (node.Name.Equals("Project", StringComparison.OrdinalIgnoreCase))
                {
                    projectNode = node;

                    break;

                }
                else if (node.InnerXml.ToLower().Contains("project"))
                {
                    projectNode = xml.ChildNodes[loop];

                }

                loop++;
            }

            return projectNode;
        }
        private void FindSdkNugetDetails(XmlDocument node)
        {
            XmlNode propsProjectNode = null;

            foreach (XmlNode aNode in node.ChildNodes)
            {
                propsProjectNode = FindParentNode(aNode, "GeneratePackageOnBuild");

                if (propsProjectNode != null)
                    break;
            }

            if (propsProjectNode != null)
            {
                var packageName = string.Empty;
                var versionNo = string.Empty;

                var pkgId = FindChildNode(propsProjectNode, "PackageId");

                if (pkgId == null)
                {
                    pkgId = FindChildNode(propsProjectNode, "AssemblyName");

                    if (pkgId == null)
                    {
                        packageName = Name;
                    }
                    else
                    {
                        packageName = pkgId.InnerText;
                    }
                }
                else
                {
                    packageName = pkgId.InnerText;
                }

                var versonNo = string.Empty;

                //
                var verNode = FindChildNode(propsProjectNode, "Version");

                if (verNode == null)
                {
                    versionNo = "1.0.0";
                }
                else
                {
                    versionNo = verNode.InnerText;
                }

                PackageId = packageName;
                PackageVersion = versionNo;
            }
        }

        private XmlNode FindChildNode(XmlNode node, string nodeName)
        {
            foreach (XmlNode aChild in node)
            {
                if (aChild.Name.Equals(nodeName, StringComparison.OrdinalIgnoreCase))
                    return aChild;
            }

            return null;
        }

        private void FindProjectsSdk(XmlNode node)
        {
            XmlNode lstProjectNode = null;

            foreach (XmlNode aNode in node.ChildNodes)
            {
                lstProjectNode = FindParentNode(aNode, "ProjectReference");

                if (lstProjectNode != null)
                    break;
            }

            if (lstProjectNode != null)
            {
                foreach (XmlNode aProject in lstProjectNode.ChildNodes)
                {
                    var link = aProject.Attributes["Include"];

                    if (link != null)
                    {
                        var projLink = link.InnerText;

                        var basePath = System.IO.Path.GetDirectoryName(_path);

                        var projPath = Path.Combine(basePath, projLink);

                        var fileInfo = new FileInfo(projPath);

                        var path = fileInfo.FullName;

                        var name = Path.GetFileNameWithoutExtension(path);

                        ProjectReferences.Add(new ProjectReference()
                        {
                            Type = ProjectType.SDK,
                            FullPath = path,
                            Name = name,
                        });
                    }
                }
            }

        }

        private void FindProjectsClassic(XmlNode node)
        {
            XmlNode lstProjectNode = null;

            foreach (XmlNode aNode in node.ChildNodes)
            {
                lstProjectNode = FindParentNode(aNode, "ProjectReference");

                if (lstProjectNode != null)
                    break;
            }

            if (lstProjectNode != null)
            {

                foreach (XmlNode aProject in lstProjectNode.ChildNodes)
                {
                    var projectId = aProject.ChildNodes[0].InnerText;
                    var projectName = aProject.ChildNodes[1].InnerText;

                    ProjectReferences.Add(new ProjectReference()
                    {
                        Type = ProjectType.Classic,
                        Id = projectId,
                        Name = projectName,
                    });
                }
            }
        }

        private XmlNode FindParentNode(XmlNode node, string nodeName)
        {
            if (ContainsNode(node, nodeName))
                return node;

            XmlNode exNode = null;

            foreach (XmlNode aNode in node.ChildNodes)
            {
                exNode = FindParentNode(aNode, nodeName);

                if (exNode != null)
                    break;
            }

            return exNode;
        }

        private bool ContainsNode(XmlNode node, string nodeName)
        {
            if (!node.HasChildNodes)
                return false;

            foreach (XmlNode aNode in node.ChildNodes)
            {
                if (aNode.Name.Equals(nodeName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}
