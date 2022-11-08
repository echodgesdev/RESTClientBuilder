using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTClientBuilder.Settings
{
    public class SwaggerSpec
    {
        public string ImportSwaggerURL { get; set; }
        public string ExportNamespace { get; set; }
        public string ExportClassName { get; set; }
    }
}
