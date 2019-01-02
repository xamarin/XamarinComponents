using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Xamarin.Components.SampleBuilder.Models
{
    public class SolutionSpec
    {
        public string Path { get; set; }

        private List<SolutionProject> _projects;

        private List<SolutionProject> Projects
        {
            get
            {
                if (_projects == null)
                    _projects = new List<SolutionProject>();

                return _projects;
            }
        }

        internal void Build()
        {

            var lines = File.ReadAllLines(Path);

            var projectLines = lines.Where(x => x.ToLower().Contains("project") 
                                        && !x.ToLower().Contains("end")
                                        && !x.ToLower().Contains("projectconfiguration"));

            foreach (var aLine in projectLines)
            {
                var lne = aLine.Substring(aLine.IndexOf("=")).Replace("=", "").Trim();

                var vals = lne.Split(',');

                var projectName = vals[0].Replace("\"", "").Trim();

                var csprojPath = vals[1].Replace("\"", "").Trim();

                var projectId = vals[2].Replace("\"", "").Trim();

                var basePath = System.IO.Path.GetDirectoryName(Path);

                var absPath = new FileInfo(System.IO.Path.Combine(basePath, csprojPath)).FullName;

                var newSolutionP = new SolutionProject()
                {
                    ProjectName = projectName,
                    RelativePath = csprojPath,
                    ProjectId = projectId,
                    AbsolutePath = absPath,
                };

                newSolutionP.Build();

                Projects.Add(newSolutionP);
            }


        }


        
    }


}
