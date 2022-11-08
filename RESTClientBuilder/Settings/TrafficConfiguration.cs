using System.Collections.Generic;

namespace RESTClientBuilder.Settings
{
    public class TrafficConfiguration
    {
        public string CSharpOutputPathRootDirectory { get; set; }
        public string TypeScriptOutputPathRootDirectory { get; set; }
        public string BaseUrlDefault { get; set; }
        public List<SwaggerSpec> SwaggerSpecs { get; set; }
    }
}
