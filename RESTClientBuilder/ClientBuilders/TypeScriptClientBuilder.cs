using NSwag;
using NSwag.CodeGeneration.TypeScript;
using RESTClientBuilder.Settings;
using System;
using System.Text.RegularExpressions;

namespace ChrisRestClientBuilder.ClientBuilders
{
    public class TypeScriptClientBuilder : IClientBuilder
    {
        public string GenerateClientText(SwaggerSpec spec)
        {
            Console.WriteLine($"Reading Spec for {spec.ImportSwaggerURL}.");

            System.Net.WebClient wclient = new System.Net.WebClient();

            Console.WriteLine($"Connecting to {spec.ImportSwaggerURL}.");

            var document = OpenApiDocument.FromJsonAsync(wclient.DownloadString(spec.ImportSwaggerURL)).Result;

            wclient.Dispose();

            var settings = new TypeScriptClientGeneratorSettings
            {
                UseGetBaseUrlMethod = true,
                GenerateClientInterfaces = true,
                ClassName = "{controller}Client",
                TypeScriptGeneratorSettings =
                {
                    Namespace = spec.ExportNamespace
                }
            };

            Console.WriteLine($"Generating code for {spec.ExportClassName} in namespace {spec.ExportNamespace}.");

            var generator = new TypeScriptClientGenerator(document, settings);
            var code = generator.GenerateFile();

            Console.WriteLine("Done.");

            return code;
        }

        public string PostProcessBaseUrls(string replacement, string input)
        {
            string pattern = @"http:\/\/.*\/";

            return Regex.Replace(input, pattern, replacement);
        }
    }
}