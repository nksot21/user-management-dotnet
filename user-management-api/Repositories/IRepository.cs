using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;

namespace user_management_api.Repositories
{
    public interface IRepository <T> where T : class, IEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        List<T> GetByQueryAsync(Func<T, bool> query);

        Task CreateAsync(T entities);
        Task UpdateAsync(T entities);
        Task DeleteByIdAsync(T entities);
    }
}
