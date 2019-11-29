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

            var apiV92 = Path.Combine(currentLocation, "ApiDefinitions92.cs");
            var apiV92CS = Path.Combine(currentLocation, "ApiDefinitions92Gen.cs");
            var apiV92CSClean = Path.Combine(currentLocation, "ApiDefinitions92GenClean.cs");
            var apiv92XmlFile = Path.Combine(currentLocation, "Apiv92.xml");

            var apiV92Altered = Path.Combine(currentLocation, "ApiDefinitionnsv92Alternative.cs");

            var api92 = await Load92(apiV92);

            await CodeGenerator.GenerateAsync(api92, apiV92CS);
            var fixedTransform = api92.Transform(apiMetaDataFile);

            if (fixedTransform != null)  //there is a fixed version of the transform
                fixedTransform.WriteToFile(Path.Combine(apiDiffOutput, "Metadata_fixed.xml"));

           // api92.RemovePrefix("MDC");

            await CodeGenerator.GenerateAsync(api92, apiV92CSClean);

            //api92.WriteToFile(apiv92XmlFile);

            ////load the altered v92 file
            var altered = await Transformer.ExtractDefinitionAsync(apiV92Altered);

            //////now compare
            var orgStack = api92.BuildTreePath();
            var newStack = altered.BuildTreePath();

            ChangeManager.Compare(orgStack, newStack, apiDiffOutput);

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
        private static async Task<ApiDefinition> Load92(string fileName)
        {
            var result = await Transformer.ExtractDefinitionAsync(fileName);

            return result;
        }
    }
}
