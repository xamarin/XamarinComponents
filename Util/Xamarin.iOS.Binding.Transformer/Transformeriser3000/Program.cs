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
            var apiFileOrig = Path.Combine(currentLocation, "ApiDefinitionsOrig.cs");
            var apiXmlFile = Path.Combine(currentLocation, "Api.xml");
            var apiFileFixed = Path.Combine(currentLocation, "ApiDefinitionsFixed.cs");
            var apiPathTree  = Path.Combine(currentLocation, "ApiTree.txt");

            var apiDiffOutput = Path.Combine(currentLocation, "diffs");

            if (File.Exists(apiFile))
            {
                //build the api defintion
                var apiDefinition = await Transformer.ExtractDefinitionAsync(apiFile);

                ////write it to file
                apiDefinition.WriteToFile(apiXmlFile);

                ////reload
                //var api = Transformer.Load(apiXmlFile);
                //api.UpdateHierachy();


                apiDefinition.UpdateHierachy();
                var stack = apiDefinition.BuildTreePath();

                ////generate and save the code file
                //await CodeGenerator.GenerateAsync(api, apiFileFixed);

                //now load the original file
                var apiDefinitionOrig = await Transformer.ExtractDefinitionAsync(apiFileOrig);
                apiDefinitionOrig.UpdateHierachy();

                var orgStack = apiDefinitionOrig.BuildTreePath();

                if (!Directory.Exists(apiDiffOutput))
                    Directory.CreateDirectory(apiDiffOutput);

                ChangeManager.Compare(orgStack, stack, apiDiffOutput);

            }

            Console.WriteLine("Finished!");
            Console.ReadLine();
        }
    }
}
