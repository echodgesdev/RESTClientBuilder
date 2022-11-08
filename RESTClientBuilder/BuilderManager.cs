using ChrisRestClientBuilder.ClientBuilders;
using Newtonsoft.Json;
using RESTClientBuilder.Settings;
using System;
using System.Collections.Generic;
using System.IO;

namespace RESTClientBuilder
{
    public class BuilderManager
    {
        string _trafficConfigurationFilepath;

        public BuilderManager(string trafficConfigurationFilepath)
        {
            _trafficConfigurationFilepath = trafficConfigurationFilepath;
        }

        public void Execute()
        {
            var trafficGenerateConfig = LoadTrafficGenerateConfig();

            if (string.IsNullOrEmpty(trafficGenerateConfig.CSharpOutputPathRootDirectory) == false)
            {
                Export(new CSharpClientBuilder(),
                        trafficGenerateConfig.SwaggerSpecs,
                        trafficGenerateConfig.CSharpOutputPathRootDirectory,
                        trafficGenerateConfig.BaseUrlDefault,
                        ".cs"
                       );
            }

            if (string.IsNullOrEmpty(trafficGenerateConfig.TypeScriptOutputPathRootDirectory) == false)
            {
                Export(new TypeScriptClientBuilder(),
                        trafficGenerateConfig.SwaggerSpecs,
                        trafficGenerateConfig.TypeScriptOutputPathRootDirectory,
                        trafficGenerateConfig.BaseUrlDefault,
                        ".ts"
                      );
            }
        }

        private TrafficConfiguration LoadTrafficGenerateConfig()
        {
            Console.WriteLine("Reading Rest API Specs from configuration file.");

            string text = System.IO.File.ReadAllText(_trafficConfigurationFilepath);

            var configuration = JsonConvert.DeserializeObject<TrafficConfiguration>(text);

            Console.WriteLine("Swagger URL - Namespace - Class Name");

            foreach (var s in configuration.SwaggerSpecs)
            {
                Console.WriteLine($"{s.ImportSwaggerURL} - {s.ExportNamespace} - {s.ExportClassName}");
            }

            Console.WriteLine("Done.");

            return configuration;
        }

        static TrafficConfiguration LoadTrafficGenerateConfig(string filePath)
        {
            Console.WriteLine("Reading Rest API Specs from configuration file.");

            string text = System.IO.File.ReadAllText(filePath);

            var configuration = JsonConvert.DeserializeObject<TrafficConfiguration>(text);

            Console.WriteLine("Swagger URL - Namespace - Class Name");

            foreach (var s in configuration.SwaggerSpecs)
            {
                Console.WriteLine($"{s.ImportSwaggerURL} - {s.ExportNamespace} - {s.ExportClassName}");
            }

            Console.WriteLine("Done.");

            return configuration;
        }

        private void Export(IClientBuilder clientBuilder, List<SwaggerSpec> specs, string outputPathRootDirectory, string baseUrlDefault, string extension)
        {
            ClearDirectory(outputPathRootDirectory);

            foreach (var spec in specs)
            {
                var rawText = clientBuilder.GenerateClientText(spec);
                var postProcessedText = clientBuilder.PostProcessBaseUrls(baseUrlDefault, rawText);
                WriteFile(outputPathRootDirectory, spec, postProcessedText, extension);
            }
        }

        private void ClearDirectory(string rootDirectory)
        {
            Console.WriteLine($"Clearing Directory {rootDirectory}");

            if (Directory.Exists(rootDirectory))
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(rootDirectory);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            Console.WriteLine($"Done.");
        }

        private void WriteFile(string outputPathRootDirectory, SwaggerSpec spec, string fileContents, string extension)
        {
            var subDirectory = spec.ExportNamespace.Replace('.', '\\');

            var dir = $@"{outputPathRootDirectory}\{subDirectory}";

            System.IO.Directory.CreateDirectory(dir);

            var fileOutputName = $@"{dir}\{spec.ExportClassName}{extension}";

            Console.WriteLine($"Writing {fileOutputName}.");

            File.WriteAllText(fileOutputName, fileContents);

            Console.WriteLine($"Done.");
        }

    }
}
