using ElasticcSearchSample.Service;
using ElasticSearchSample.Repositories;
using ElasticSearchSample.Service;

namespace ElasticSearchSample.Extentions
{
    public static class ApiConfigurationExtentions
    {
        public static void AddApiConfiguration(this IServiceCollection services)
        {
            
            services.AddTransient<ICostumerRepository, CostumerRepository>();
            services.AddTransient<ICostumerService, CostumerService>();

            services.AddControllers();
        }

    }
}
