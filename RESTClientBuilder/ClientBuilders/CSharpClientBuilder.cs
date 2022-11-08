using NSwag;
using NSwag.CodeGeneration.CSharp;
using RESTClientBuilder.Settings;
using System;
using System.Text.RegularExpressions;

namespace ChrisRestClientBuilder.ClientBuilders
{
    public class CSharpClientBuilder : IClientBuilder
    {
        public string GenerateClientText(SwaggerSpec spec)
        {
            Console.WriteLine($"Reading Spec for {spec.ImportSwaggerURL}.");

            System.Net.WebClient wclient = new System.Net.WebClient();

            Console.WriteLine($"Connecting to {spec.ImportSwaggerURL}.");

            var document = OpenApiDocument.FromJsonAsync(wclient.DownloadString(spec.ImportSwaggerURL)).Result;

            wclient.Dispose();

            var settings = new CSharpClientGeneratorSettings
            {
                UseBaseUrl = true,
                GenerateClientInterfaces = true,
                InjectHttpClient = true,
                GenerateBaseUrlProperty = true,
                ClassName = "{controller}Client",
                CSharpGeneratorSettings =
                {
                    Namespace = spec.ExportNamespace
                }
            };

            Console.WriteLine($"Generating code for {spec.ExportClassName} in namespace {spec.ExportNamespace}.");

            var generator = new CSharpClientGenerator(document, settings);
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