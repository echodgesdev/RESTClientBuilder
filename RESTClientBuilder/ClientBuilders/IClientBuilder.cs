using RESTClientBuilder.Settings;

namespace ChrisRestClientBuilder.ClientBuilders
{
    public interface IClientBuilder
    {
        string GenerateClientText(SwaggerSpec spec);

        string PostProcessBaseUrls(string replacement, string input);
    }
}