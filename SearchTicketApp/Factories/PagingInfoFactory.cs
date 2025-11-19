using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SearchTicketApp.Models.Result;
using SearchTicketApp.Shared;

namespace SearchTicketApp.Factories
{
    public static class PagingInfoFactory
    {
        public static async Task<PagingInfo<T>> CreateFromQueryable<T>(IQueryable<T> queryable, int page, int pageSize)
        {
            var count = await queryable.CountAsync();

            var pagedItems = await queryable.
                Skip((page - 1) * pageSize).
                Take(pageSize).
                ToListAsync();


            return PagingInfo<T>.Create(pagedItems, count, page, pageSize);
        }
    }
}
