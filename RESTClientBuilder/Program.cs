using System;
using System.Configuration;

namespace RESTClientBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Rest Client Builder.");

            var configurationFilepath = ConfigurationManager.AppSettings["RestTrafficGenerateFile"];

            new BuilderManager(configurationFilepath).Execute();

            Console.WriteLine("Process Complete. Press Any Key To Exit.");
            Console.ReadKey();
        }
    }
}

