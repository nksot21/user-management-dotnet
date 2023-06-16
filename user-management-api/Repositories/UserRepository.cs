using Microsoft.EntityFrameworkCore;
using user_management_api.Entities;
using user_management_api.Models;
using user_management_api.Services;

namespace user_management_api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IDatabaseService databaseService { get; }
        public UserRepository(IDatabaseService databaseService) {
            this.databaseService = databaseService;
        }
        async public Task Add(IndividualUser individualUser)
        {
            await databaseService.Context.AddAsync(individualUser);
            await databaseService.Context.SaveChangesAsync();
        }

        async public Task<List<IndividualUser>> GetAll() {
            return await databaseService.Context.IndividualUsersModel.ToListAsync();
        }

        async public Task<IndividualUser> GetById(int id)
        {
            return await databaseService.Context.IndividualUsersModel.FirstOrDefaultAsync(x => (x.Id == id && x.DeletedAt == null));
        }

        async public Task Update(IndividualUser individualUser)
        {
            databaseService.Context.Update(individualUser);
            await databaseService.Context.SaveChangesAsync();
        }

        async public Task Delete(IndividualUser individualUser)
        {
            databaseService.Context.IndividualUsersModel.Remove(individualUser);
            await databaseService.Context.SaveChangesAsync();
        }
    }
}
