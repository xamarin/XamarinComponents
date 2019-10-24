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

            if (File.Exists(apiFile))
            {
                await Transformer.ExtractDefinitionAsync(apiFile, apiXmlFile);

                var api = Transformer.Load(apiXmlFile);

                CodeGenerator.GenerateAsync(api, apiFileFixed);
            }

            Console.WriteLine("Hello World!");
        }
    }
}
