================
Overview
================

The ChrisRestClientBuilder is a tool that streamlines building TypeScript and C# clients to any API that uses swagger.
At its core, the program is a wrapper to the NSwag Nuget package.

Contents
--------

RestClientBuilder - The REST client builder.

DummyConsumer - A console app that demonstrates the usage of generated traffic C# classes

DummyAPI - A simple API that uses Swagger


================
Configuration
================

The configuration of which APIs to generate proxies for is managed in a "Traffic Generate" JSON file. 
The path to the file is managed in the App.Config which allows the tool to be used in multiple projects across an organization.

Sample Traffic Generate JSON File
----------------------------------

{
	"baseUrlDefault": "http://updateTheBaseUrlInYourFacade/",
	"cSharpOutputPathRootDirectory": "C:\\Generated\\CSharp",
	"typeScriptOutputPathRootDirectory":"C:\\ChrisRestClientBuilderUnitTests\\Generated\\TS",
	"swaggerSpecs": [
		{
			"importSwaggerURL": "http://localhost/SystemAdminServices/swagger/docs/v2",
			"exportNamespace": "Traffic.RestClients.SystemAdminServices",
			"exportClassName": "SystemAdminSerivcesClients"
		}
	]
}

============
App Settings
============
RestTrafficGenerateFile - Base directory of a "generated traffic" file

============
Usage
============

1. Create a "generated traffic file" that is configured to point to an API that uses swagger (e.g. the ChrisDummyAPI).
	Note: If you don't want clients for a particular language, leave their outputpath root directrory blank.
2. Edit the App Settings to point to a "generated traffic" file
3. Run the RestClientBuilder and allow it to generate C# / TypeScript classes.