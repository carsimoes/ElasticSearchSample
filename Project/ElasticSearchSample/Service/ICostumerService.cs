using ElasticSearchSample.Models;

namespace ElasticcSearchSample.Service
{
    public interface ICostumerService
    {
        public Task<Costumer> GetAsync(string id);
        public Task<IEnumerable<Costumer>> GetAllAsync();
        public Task<IEnumerable<Costumer>> SearchInAllFields(string term);
        public Task<bool> CreateIndexAsync(Costumer entity);
        public Task<Costumer> AddAsync(Costumer entity);
        public Task<Costumer> UpdateAsync(string id, Costumer entity);
        public Task DeleteAsync(string id);
    }
}