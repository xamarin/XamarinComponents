using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Components.SampleBuilder.Enums;

namespace Xamarin.Components.SampleBuilder.Models
{
    public class ProjectReference
    {
        public ProjectType Type { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string FullPath { get; set; }
    }
}
