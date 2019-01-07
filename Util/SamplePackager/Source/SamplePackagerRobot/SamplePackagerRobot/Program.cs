using System;
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
                var parent = new DirectoryInfo(cpPath).Parent.Parent.Parent.Parent.Parent.Parent;

                var solutionPath = Path.Combine(parent.FullName, @"TestSolution\SampleApplication\SampleApplication.sln");

                var outPutPath = @"C:\SamplePackagerOutput\BlahComponent";

                SolutionProcessor.Process(solutionPath, outPutPath);

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
