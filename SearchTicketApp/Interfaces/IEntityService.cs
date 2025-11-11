using SearchTicketApp.Data.Models.Abstract;
using SearchTicketApp.Shared;

namespace SearchTicketApp.Interfaces
{
    public interface IEntityService<TEntity>
    {
        Task AddAsync(TEntity entity);

        Task DeleteAsync(Entity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteByIdAsync(int id);

        Task<TEntity> GetByIdAsync(int id);

        Task<ICollection<TEntity>> GetAllAsync();

        Task<PagingInfo<TEntity>> GetPagedAsync();
    }
}
