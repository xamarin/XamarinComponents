using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Components.SampleBuilder.Models
{
    public class SolutionProject
    {
        public string ProjectName { get; set; }

        public string RelativePath { get; set; }

        public string ProjectId { get; set; }

        public string AbsolutePath { get; set; }

        public Project Project { get; set; }

        internal void Build()
        {
            Project = new Project(AbsolutePath);
            Project.Build();



        }

        internal void AddPackageReference(string packageId, string packageVersion)
        {
            throw new NotImplementedException();
        }

        internal void RemoveProjectReference(SolutionProject referencedProject)
        {
            throw new NotImplementedException();
        }
    }
}
