using user_management_api.Entities;

namespace user_management_api.Repositories
{
    public interface IUserRepository
    {
        public Task Add(IndividualUser individualUser);
        public Task<List<IndividualUser>> GetAll();

        public Task<IndividualUser> GetById(int id);

        public  Task Update(IndividualUser individualUser);

        public Task Delete(IndividualUser individualUser);

    }
}
