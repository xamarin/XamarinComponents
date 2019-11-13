using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.iOS.Binding.Transformer;

namespace Transformeriser3000
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var currentLocation = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            var apiFile = Path.Combine(currentLocation, "ApiDefinitions.cs");
            var apiXmlFile = Path.Combine(currentLocation, "Api.xml");
            var apiFileFixed = Path.Combine(currentLocation, "ApiDefinitionsFixed.cs");
            var apiPathTree  = Path.Combine(currentLocation, "ApiTree.txt"); 

            if (File.Exists(apiFile))
            {
                //build the api defintion
                var apiDefinition = await Transformer.ExtractDefinitionAsync(apiFile);

                //write it to file
                apiDefinition.WriteToFile(apiXmlFile);

                //reload
                var api = Transformer.Load(apiXmlFile);
                api.UpdateHierachy();

                var stack = api.GetFlatPathList();

                var paths = stack.Keys;

                File.WriteAllLines(apiPathTree, paths);

                //generate and save the code file
                await CodeGenerator.GenerateAsync(api, apiFileFixed);
            }

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
