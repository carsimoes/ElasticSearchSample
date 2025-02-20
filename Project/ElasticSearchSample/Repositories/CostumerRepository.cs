using Elastic.Clients.Elasticsearch;
using ElasticSearchSample.Models;

namespace ElasticSearchSample.Repositories
{
    public class CostumerRepository(ElasticsearchClient elasticClient) : BaseElasticSearchRepository<Costumer>(elasticClient), ICostumerRepository
    {
        protected override string IndexName => "costumer";
    }
}
