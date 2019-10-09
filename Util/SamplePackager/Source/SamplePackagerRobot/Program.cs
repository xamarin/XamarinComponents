using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xamarin.Components.SampleBuilder;

namespace SamplePackagerRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var cpPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                //find the root path of the SamplePackager
                var parent = new DirectoryInfo(cpPath).Parent.Parent.Parent.Parent.Parent;

                //var solutionPath = Path.Combine(parent.FullName, @"TestSolution\SampleApplication\SampleApplication.sln");
                //var solutionPath = Path.Combine(parent.FullName, @"TestSolution2\Samples\SampleApplication.sln");

                var solutionPath = @"C:\Users\dave\Desktop\ARCore\samples\HelloAR.sln";

                var outPutPath = @"C:\SamplePackagerOutput\ARCoreSamples";

                //var packageVersions = new Dictionary<string, string>()
                //{
                //    {"StandardSample","1.1.0" },
                //    {"AndroidLibary","1.1.0" },
                //};

                var outputfile = SolutionProcessor.Process(solutionPath, outPutPath);

                Console.WriteLine("Complete");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            Console.ReadLine();

        }
    }
}
