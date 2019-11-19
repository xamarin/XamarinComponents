using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.iOS.Binding.Transformer;
using Xamarin.iOS.Binding.Transformer.Models.Metadata;

namespace Transformeriser3000
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var currentLocation = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            var apiFile = Path.Combine(currentLocation, "ApiDefinitions.cs");
            var apiFileOrig = Path.Combine(currentLocation, "ApiDefinitionsOrig.cs");
            var apiMetaDataFile = Path.Combine(currentLocation, "Metadata.xml");
            var apiXmlFile = Path.Combine(currentLocation, "Api.xml");
            var apiFileFixed = Path.Combine(currentLocation, "ApiDefinitionsFixed.cs");
            var apiPathTree  = Path.Combine(currentLocation, "ApiTree.txt");

            var apiFileNew = Path.Combine(currentLocation, "ApiDefinitionsNew.cs");

            var apiDiffOutput = Path.Combine(currentLocation, "diffs");

            ////Load the original
            var apiDefinitionTest = await Transformer.ExtractDefinitionAsync(apiFileOrig);
            apiDefinitionTest.Transform(apiMetaDataFile);

            await CodeGenerator.GenerateAsync(apiDefinitionTest, apiFileNew);


            if (File.Exists(apiFile))
            {
                //build the api defintion
                var apiDefinition = await Transformer.ExtractDefinitionAsync(apiFile);

                //////////write it to file
                //apiDefinition.WriteToFile(apiXmlFile);

                //////reload
                ////var api = Transformer.Load(apiXmlFile);
                ////api.UpdateHierachy();
                ///
                var testdels = apiDefinitionTest.Namespaces[0].Delegates;
                var tdels = apiDefinition.Namespaces[0].Delegates;
                
                var stack = apiDefinition.BuildTreePath();
                var newStack = apiDefinitionTest.BuildTreePath();

                Console.WriteLine("");
                ////////generate and save the code file
                //await CodeGenerator.GenerateAsync(apiDefinition, apiFileFixed);

                //////now load the original file
                //var apiDefinitionOrig = await Transformer.ExtractDefinitionAsync(apiFileOrig);
                //apiDefinitionOrig.UpdateHierachy();

                //var orgStack = apiDefinitionOrig.BuildTreePath();

                //if (!Directory.Exists(apiDiffOutput))
                //    Directory.CreateDirectory(apiDiffOutput);

                ChangeManager.Compare(stack, newStack, apiDiffOutput);

            }

            Console.WriteLine("Finished!");
            Console.ReadLine();
        }
    }
}
