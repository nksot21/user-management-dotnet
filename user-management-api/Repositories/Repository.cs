using Microsoft.EntityFrameworkCore;
using user_management_api.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;
using user_management_api.Entities;
using MongoDB.Driver.Linq;

namespace user_management_api.Repositories
{
    public class Repository  <T>: IRepository <T> where T : class, IEntity
    {
        private readonly ApiDbContext Context;
        private DbSet<T> Entities;

        public Repository(ApiDbContext apiDbContext ) {
            Context = apiDbContext;
            Entities = Context.Set<T>();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await Entities.FirstOrDefaultAsync(entities => entities.Id == id && entities.DeletedAt == null);
        }
        public List<T> GetByQueryAsync(Func<T, bool> query)
        {

             return Entities.Where(query).ToList();
        }

        public async Task CreateAsync(T entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(entities.GetType().Name);
            }
            await Entities.AddAsync(entities);
            await Context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entities)
        {
            Context.Update(entities);
            await Context.SaveChangesAsync();
        }
        public async Task DeleteByIdAsync(T entities)
        {
            Entities.Remove(entities);
            await Context.SaveChangesAsync();
        }
    }
}
