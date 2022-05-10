using System.Collections.Generic;
using System.Threading.Tasks;

namespace TabStripDemo.Repositories
{
    public interface IRepository<TEntity, in TPk> where TEntity : class
    {
        Task<List<TEntity>> GetAsync();
        Task<TEntity> GetByEmailAsync(TPk id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, TPk id);
        Task<TEntity> DeleteAsync(TPk id);
    }
}
