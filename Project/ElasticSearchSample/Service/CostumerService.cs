using ElasticcSearchSample.Service;
using ElasticSearchSample.Models;
using ElasticSearchSample.Repositories;

namespace ElasticSearchSample.Service
{
    public class CostumerService(ICostumerRepository costumerRepository) : ICostumerService
    {
        private readonly ICostumerRepository _costumerRepository = costumerRepository;

        public async Task<Costumer> AddAsync(Costumer entity)
        {
            return await _costumerRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _costumerRepository.DeleteAsync(id);
        }

        public async Task<bool> CreateIndexAsync(Costumer entity)
        {
            return await _costumerRepository.CreateIndexAsync(entity);
        }

        public async Task<IEnumerable<Costumer>> GetAllAsync()
        {
            return await _costumerRepository.GetAllAsync();
        }

        public async Task<Costumer> GetAsync(string id)
        {
            return await _costumerRepository.GetAsync(id);
        }

        public async Task<IEnumerable<Costumer>> SearchInAllFields(string term)
        {
            return await _costumerRepository.SearchInAllFields(term);
        }

        public async Task<Costumer> UpdateAsync(string id, Costumer entity)
        {
            return await _costumerRepository.UpdateAsync(id, entity);
        }
    }
}
