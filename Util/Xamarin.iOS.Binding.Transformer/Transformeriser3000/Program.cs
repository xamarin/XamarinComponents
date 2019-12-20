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

            var apiDiffOutput = Path.Combine(currentLocation, "diffs");

            var apiV92 = Path.Combine(currentLocation, "ApiDefinitions92.cs");
            var apiV92Xam = Path.Combine(currentLocation, "ApiDefinition92Xam.cs");
            var apiV92CSClean = Path.Combine(currentLocation, "ApiDefinitions92GenClean.cs");
            var apiMetaDataFile = Path.Combine(apiDiffOutput, "Metadata.xml");

            var api92 = await LoadDefinition(apiV92);
            var api92Xam = await LoadDefinition(apiV92Xam);

            //flatten the deprecated categoris
            api92.FlattenCategories("_Deprecated", "_ToBeDeprecated");

            //var thing = api92.Namespaces[0].Types.FirstOrDefault(x => x.Name.Equals("MDCTonalPalette", StringComparison.OrdinalIgnoreCase));

            var meta = api92.Compare(api92Xam);


            meta.WriteToFile(Path.Combine(currentLocation, "metadata.xml"));

            //var fixedTransform = api92.Transform(apiMetaDataFile);

            // await CodeGenerator.GenerateAsync(api92, apiV92CSClean);




            //api92.RemovePrefix("MDC");


            //if (!Directory.Exists(apiDiffOutput))
            //    Directory.CreateDirectory(apiDiffOutput);

            ////loading file
            //Console.WriteLine("Loading file...");
            //await CodeGenerator.GenerateAsync(api92, apiV92CS);

            //Console.WriteLine("Transforming file...");
            //var fixedTransform = api92.Transform(apiMetaDataFile);

            //if (fixedTransform != null)  //there is a fixed version of the transform
            //    fixedTransform.WriteToFile(Path.Combine(apiDiffOutput, "Metadata_fixed.xml"));

            //// 
            //Console.WriteLine($"Generating...{apiV92CSClean}");
            //await CodeGenerator.GenerateAsync(api92, apiV92CSClean);

            ////api92.WriteToFile(apiv92XmlFile);

            //////load the altered v92 file
            //Console.WriteLine($"Loading ...{apiV92Altered}");
            //var altered = await Transformer.ExtractDefinitionAsync(apiV92Altered);

            ////////now compare
            //Console.WriteLine($"Build comparison trees...");
            //var orgStack = api92.BuildTreePath();
            //var newStack = altered.BuildTreePath();

            //ChangeManager.Compare(orgStack, newStack, apiDiffOutput);

            //generate the current file
            //var apiDefinition = await Transformer.ExtractDefinitionAsync(apiFile);

            //now compare
            //var orgStack = apiDefinition.BuildTreePath();
            //var newStack = api92.BuildTreePath();

            //ChangeManager.Compare(orgStack, newStack, apiDiffOutput);

            ////Load the original
            //var apiDefinitionTest = await Transformer.ExtractDefinitionAsync(apiFileOrig);
            //apiDefinitionTest.Transform(apiMetaDataFile);

            //await CodeGenerator.GenerateAsync(apiDefinitionTest, apiFileNew);


            //if (File.Exists(apiFile))
            //{
            //    //build the api defintion
            //    var apiDefinition = await Transformer.ExtractDefinitionAsync(apiFile);

            //    //////////write it to file
            //    apiDefinition.WriteToFile(apiXmlFile);

            //    //////reload
            //    ////var api = Transformer.Load(apiXmlFile);
            //    ////api.UpdateHierachy();
            //    ///
            //    //var testdels = apiDefinitionTest.Namespaces[0].Delegates;
            //    //var tdels = apiDefinition.Namespaces[0].Delegates;

            //    var stack = apiDefinition.BuildTreePath();
            //    //var newStack = apiDefinitionTest.BuildTreePath();

            //    //Console.WriteLine("");
            //    //////////generate and save the code file
            //    await CodeGenerator.GenerateAsync(apiDefinition, apiFileFixed);

            //    ////////now load the original file
            //    var apiDefinitionOrig = await Transformer.ExtractDefinitionAsync(apiFileOrig);
            //    var orgStack = apiDefinitionOrig.BuildTreePath();

            //    if (!Directory.Exists(apiDiffOutput))
            //         Directory.CreateDirectory(apiDiffOutput);

            //    ChangeManager.Compare(orgStack, stack, apiDiffOutput);

            //}

            Console.WriteLine("Finished!");
            Console.ReadLine();
        }

        /// <summary>
        /// Load the version 92 of the file
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        private static async Task<ApiDefinition> LoadDefinition(string fileName)
        {
            var result = await Transformer.ExtractDefinitionAsync(fileName);

            return result;
        }
    }
}
