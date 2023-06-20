using Microsoft.EntityFrameworkCore;
using user_management_api.Data;
using user_management_api.Entities;
using user_management_api.Models;
using user_management_api.Services;

namespace user_management_api.Repositories
{
    public class UserRepository : IUserRepository
    {
        //private IDatabaseService databaseService { get; }
        private readonly ApiDbContext _context;
        public UserRepository(ApiDbContext context) {
            _context = context;
        }
        async public Task Add(IndividualUser individualUser)
        {
            await _context.IndividualUsersModel.AddAsync(individualUser);
            await _context.SaveChangesAsync();
           
        }

        async public Task<List<IndividualUser>> GetAll() {
            return await _context.IndividualUsersModel.ToListAsync();
        }

        async public Task<IndividualUser> GetById(int id)
        {
            return await _context.IndividualUsersModel.FirstOrDefaultAsync(x => (x.Id == id && x.DeletedAt == null));
        }

        async public Task Update(IndividualUser individualUser)
        {
            _context.Update(individualUser);
            await _context.SaveChangesAsync();
        }

        async public Task Delete(IndividualUser individualUser)
        {
            _context.IndividualUsersModel.Remove(individualUser);
            await _context.SaveChangesAsync();
        }

        async public Task<IndividualUser> FindByUsername(string username)
        {
            return await _context.IndividualUsersModel.FirstOrDefaultAsync(x => (x.Username == username && x.DeletedAt == null));
        }
    }
}
