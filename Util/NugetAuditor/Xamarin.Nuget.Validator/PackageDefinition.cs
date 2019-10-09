using System;
using System.Xml;

namespace Xamarin.Nuget.Validator
{
    public class PackageDefinition
    {
        public string Id
        {
            get;
            set;
        }

        public string ProjectUrl
        {
            get;
            set;
        }

        public string LicenseUrl
        {
            get;
            set;
        }

        public string Authors
        {
            get;
            set;
        }

        public string Owners
        {
            get;
            set;
        }

        public string Copyright
        {
            get;
            set;
        }

        public bool RequireLicenseAcceptance
        {
            get;
            set;
        }

        public PackageDefinition()
        {

        }

        public static PackageDefinition FromXml(XmlDocument xml)
        {
            var newPackage = new PackageDefinition();

            var packagesRoot = xml.ChildNodes[1];

            var metaDataRoot = packagesRoot.ChildNodes[0];

            foreach (XmlNode aNode in metaDataRoot.ChildNodes)
            {
                switch (aNode.Name.ToLower())
                {
                    case "id":
                        {

                            newPackage.Id = aNode.InnerText?.ToLower() ?? String.Empty;
                        }
                        break;
                    case "authors":
                        {
                            newPackage.Authors = aNode.InnerText?.ToLower() ?? String.Empty;
                        }
                        break;
                    case "owners":
                        {
                            newPackage.Owners = aNode.InnerText?.ToLower() ?? String.Empty;
                        }
                        break;
                    case "copyright":
                        {
                            newPackage.Copyright = aNode.InnerText?.ToLower() ?? String.Empty;
                        }
                        break;
                    case "projecturl":
                        {
                            newPackage.ProjectUrl = aNode.InnerText?.ToLower() ?? String.Empty;
                        }
                        break;
                    case "licenseurl":
                        {
                            newPackage.LicenseUrl = aNode.InnerText?.ToLower() ?? String.Empty;
                        }
                        break;
                    case "requirelicenseacceptance":
                        {
                            var text = aNode.InnerText?.ToLower() ?? "false";

                            newPackage.RequireLicenseAcceptance = text.Equals("true", StringComparison.OrdinalIgnoreCase);

                        }
                        break;
                }
            }

            //check for empty values 
            newPackage.Id = newPackage.Id?.ToString() ?? String.Empty;
            newPackage.Authors = newPackage.Authors?.ToString() ?? String.Empty;
            newPackage.Owners = newPackage.Owners?.ToString() ?? String.Empty;
            newPackage.Copyright = newPackage.Copyright?.ToString() ?? String.Empty;
            newPackage.ProjectUrl = newPackage.ProjectUrl?.ToString() ?? String.Empty;
            newPackage.LicenseUrl = newPackage.LicenseUrl?.ToString() ?? String.Empty;


            return newPackage;
        }
    }
}
