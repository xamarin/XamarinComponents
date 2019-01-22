using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Components.SampleBuilder.Models
{
    internal class ProjectSpec
    {
        public string Name { get; set; }

        public string SourceProjectPath { get; set; }

        public string TempProjectPath { get; set; }

        public string OriginalSolutionPath { get; set; }

        public string TempSolutionPath { get; set; }
    }
}
