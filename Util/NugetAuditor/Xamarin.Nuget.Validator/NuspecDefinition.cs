using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Xamarin.Nuget.Validator
{
    [XmlRoot(ElementName = "dependency", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
    public class Dependency
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
    }

    [XmlRoot(ElementName = "group", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
    public class Group
    {
        [XmlElement(ElementName = "dependency", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
        public List<Dependency> Dependency { get; set; }
        [XmlAttribute(AttributeName = "targetFramework")]
        public string TargetFramework { get; set; }
    }

    [XmlRoot(ElementName = "dependencies", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
    public class Dependencies
    {
        [XmlElement(ElementName = "group", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
        public Group Group { get; set; }
    }

    [XmlRoot(ElementName = "metadata", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
    public class Metadata
    {
        [XmlElement(ElementName = "id", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
        public string Id { get; set; }
        [XmlElement(ElementName = "version", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
        public string Version { get; set; }
        [XmlElement(ElementName = "title", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
        public string Title { get; set; }
        [XmlElement(ElementName = "authors", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
        public string Authors { get; set; }
        [XmlElement(ElementName = "owners", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
        public string Owners { get; set; }
        [XmlElement(ElementName = "requireLicenseAcceptance", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
        public bool RequireLicenseAcceptance { get; set; }
        [XmlElement(ElementName = "licenseUrl", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
        public string LicenseUrl { get; set; }
        [XmlElement(ElementName = "projectUrl", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
        public string ProjectUrl { get; set; }
        [XmlElement(ElementName = "iconUrl", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
        public string IconUrl { get; set; }
        [XmlElement(ElementName = "description", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
        public string Description { get; set; }
        [XmlElement(ElementName = "summary", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
        public string Summary { get; set; }
        [XmlElement(ElementName = "copyright", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
        public string Copyright { get; set; }
        [XmlElement(ElementName = "dependencies", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
        public Dependencies Dependencies { get; set; }
    }

    [XmlRoot(ElementName = "package", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
    public class Package
    {
        [XmlElement(ElementName = "metadata", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd")]
        public Metadata Metadata { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }
}
