using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearchSample.Models;
using System.Text.Json;

namespace ElasticSearchSample.Repositories
{
    public abstract class BaseElasticSearchRepository<T>(ElasticsearchClient elasticClient) :
        IBaseElasticSearchRepository<T> where T : BaseDocument
    {
        private readonly ElasticsearchClient _elasticClient = elasticClient;

        protected abstract string IndexName { get; }

        public async Task<T> AddAsync(T entity)
        {
            var response = await _elasticClient.IndexAsync(entity, descriptor => descriptor.Index(IndexName));

            if (!response.IsValidResponse)
                throw new Exception($"{response.ElasticsearchServerError?.Error} - {response.DebugInformation}");

            entity.Id = response.Id;

            return entity;
        }

        public async Task DeleteAsync(string id)
        {
            var response = await _elasticClient.DeleteAsync(id, d => d.Index(IndexName));

            if (!response.IsValidResponse)
                throw new Exception($"{response.ElasticsearchServerError?.Error} - {response.DebugInformation}");
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var request = new SearchRequest<T>
            {
                Query = new MatchAllQuery()
            };

            var response = await _elasticClient.SearchAsync<T>(request);

            if (!response.IsValidResponse)
            {
                throw new Exception($"Search failed: {response.DebugInformation}");
            }
            return GetResponseWithInternalIds(response);
        }        

        public async Task<IEnumerable<T>> SearchInAllFields(string term)
        {
            var fields = typeof(T).GetProperties().Select(p => p.Name.ToLower()).ToArray();

            var multiMatchQuery = new MultiMatchQuery
            {
                Query = term,
                Fields = fields,
                Type = TextQueryType.Phrase,
                Lenient = true
            };

            var response = await _elasticClient.SearchAsync<T>(s => s
                .Query(q => q.MultiMatch(m => m
                    .Query(term)
                    .Fields(fields)
                    .Type(TextQueryType.Phrase)
                    .Lenient(true)
                ))
            );

            if (!response.IsValidResponse)
            {
                throw new Exception($"Search failed: {response.DebugInformation}");
            }

            return GetResponseWithInternalIds(response);

        }

        public async Task<T> GetAsync(string id)
        {
            var response = await _elasticClient.GetAsync<T>(id, g => g.Index(IndexName));

            if (!response.IsValidResponse || !response.Found)
            {
                throw new KeyNotFoundException($"Document with id '{id}' not found.");
            }

            if (response.Source is null)
            {
                throw new InvalidOperationException("Document was found, but source is null.");
            }

            var document = response.Source;

            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty is not null && idProperty.CanWrite)
            {
                idProperty.SetValue(document, id);
            }

            return document;
        }

        public async Task<bool> CreateIndexAsync(T entity)
        {
            var indexExistsResponse = await _elasticClient.Indices.ExistsAsync(IndexName);

            if (!indexExistsResponse.Exists)
            {
                dynamic serializedEntity = JsonSerializer.Serialize(entity);
                await _elasticClient.IndexAsync(serializedEntity, IndexName);
            }

            return true;
        }

        public async Task<T> UpdateAsync(string id, T entity)
        {
            var response = await _elasticClient.UpdateAsync<T, T>(id, u => u
                .Index(IndexName)
                .Doc(entity)
            );

            if (!response.IsValidResponse)
            {
                throw new Exception($"Update failed: {response.DebugInformation}");
            }

            entity.Id = response.Id;

            return entity;
        }

        private static Query BuildMultiMatchQuery<T>(string queryValue) where T : class
        {
            var fields = typeof(T).GetProperties().Select(p => p.Name.ToLower()).ToArray();

            return new MultiMatchQuery
            {
                Query = queryValue,
                Fields = fields,
                Type = TextQueryType.Phrase,
                Lenient = true
            };
        }

        private static IEnumerable<T> GetResponseWithInternalIds(SearchResponse<T> response)
        {
            return response.Hits
                     .Where(hit => hit?.Source is not null)
                     .Select(hit =>
                     {
                         var document = hit.Source!;

                         var idProperty = typeof(T).GetProperty("Id");
                         if (idProperty is not null && idProperty.CanWrite)
                         {
                             idProperty.SetValue(document, hit.Id);
                         }

                         return document;
                     });
        }
    }
}
