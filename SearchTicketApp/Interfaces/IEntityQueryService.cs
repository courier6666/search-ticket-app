using SearchTicketApp.Data.Models.Abstract;
using SearchTicketApp.Shared;

namespace SearchTicketApp.Interfaces
{
    public interface IEntityQueryService<TEntityQuery>
        where TEntityQuery : Entity
    {
        Task<TEntityQuery?> GetByIdAsync(int id);

        Task<ICollection<TEntityQuery>> GetAllAsync();

        Task<PagingInfo<TEntityQuery>> GetAllPagedAsync(int page, int pageSize);
    }
}
