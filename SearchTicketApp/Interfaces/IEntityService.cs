using SearchTicketApp.Data.Models.Abstract;
using SearchTicketApp.Models.Dto.Abstract;
using SearchTicketApp.Shared;
using Entity = SearchTicketApp.Data.Models.Abstract.Entity;

namespace SearchTicketApp.Interfaces
{
    public interface IEntityService<TEntityCommand, TEntityQuery>
        where TEntityCommand : Entity
        where TEntityQuery : Entity
    {
        Task AddAsync(TEntityCommand entity);

        Task DeleteAsync(TEntityCommand entity);

        Task UpdateAsync(TEntityCommand entity);

        Task DeleteByIdAsync(int id);

        Task<TEntityQuery?> GetByIdAsync(int id);

        Task<ICollection<TEntityQuery>> GetAllAsync();

        Task<PagingInfo<TEntityQuery>> GetPagedAsync();
    }
}
