using ElasticSearchSample.Models;

namespace ElasticSearchSample.Repositories
{
    public interface IBaseElasticSearchRepository<T> where T : BaseDocument
    {
        Task<T> GetAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> SearchInAllFields(string term);
        Task<bool> CreateIndexAsync(T entity);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(string id, T entity);
        Task DeleteAsync(string id);
    }
}
