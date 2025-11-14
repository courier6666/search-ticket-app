using SearchTicketApp.Data.Models.Abstract;

namespace SearchTicketApp.Interfaces
{
    public interface IEntityCommandService<in TEntityCommand>
        where TEntityCommand : Entity
    {
        Task AddAsync(TEntityCommand entity);

        Task DeleteAsync(TEntityCommand entity);

        Task UpdateAsync(TEntityCommand entity);

        Task DeleteByIdAsync(int id);
    }
}
