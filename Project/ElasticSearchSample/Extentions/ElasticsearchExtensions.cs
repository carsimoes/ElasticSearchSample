using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace ElasticSearchSample.Extentions
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var uri = configuration["ElasticsearchSettings:uri"];
            var defaultIndex = configuration["ElasticsearchSettings:defaultIndex"];
            var username = configuration["ElasticsearchSettings:username"];
            var password = configuration["ElasticsearchSettings:password"];

            var settings = new ElasticsearchClientSettings(new Uri(uri));

            if (!string.IsNullOrEmpty(defaultIndex))
            {
                settings = settings.DefaultIndex(defaultIndex);
            }

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                settings = settings.Authentication(new BasicAuthentication(username, password));
            }

            var client = new ElasticsearchClient(settings);

            services.AddSingleton(client);
        }
    }
}
