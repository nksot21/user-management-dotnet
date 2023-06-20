using Microsoft.EntityFrameworkCore;
using user_management_api.Data;
using user_management_api.Entities;
using user_management_api.Helpers;
using user_management_api.Models;
using user_management_api.Repositories;

namespace user_management_api.Services
{
    public interface IUserServices
    {
        public Task<bool>  CreateUser(IndividualUser user); 
        public Task<List<IndividualUser>> GetAllUser();
        public Task<IndividualUser> GetUserById(int id);
        public Task<bool> Update(UpdateUserModel userReq, IndividualUser user);

        public Task<bool> DeleteUser(IndividualUser urs);
        public Task<bool> UpdateDeletedAt(IndividualUser urs);
        public Task<IndividualUser> FindByUsername(string username);

        public bool CheckPassword(IndividualUser user, string password);
    }
    public class UserServices : IUserServices
    {
        //private readonly IUserRepository userRepository;
        private readonly IRepository<IndividualUser> UserRepo;

        public UserServices(IRepository<IndividualUser> userRepo)
        {

            UserRepo = userRepo;
        }

        //public async Task<(bool, string)> CreateUser(IndividualUser user)
             public async Task<bool> CreateUser(IndividualUser user)
        {
            try
            {
                await UserRepo.CreateAsync(user);
                //return (true, "Thnhaf công");
                return true;
            }catch (Exception ex)
            {
                return  false;
            }
        }

        public async Task<List<IndividualUser>> GetAllUser()
        {
            List<IndividualUser> allUserLst = await UserRepo.GetAllAsync();
            List<IndividualUser> userLst = allUserLst.Where(usr => usr.DeletedAt == null).ToList();
            return userLst;
        }

        public async Task<IndividualUser> GetUserById(int id)
        {
            var usr = await UserRepo.GetByIdAsync(id);
            return usr;
        }

        public async Task<bool> Update( UpdateUserModel userReq, IndividualUser user)
        {
            try
            {
                user.Fullname = userReq.Fullname;
                user.Username = userReq.Username;
                user.Email = userReq.Email;
                user.UpdatedAt = DateTime.Now;
                await UserRepo.UpdateAsync(user);
                return true;
            }catch(Exception ex)
            {
                return false;
            }


        }

        public async Task<bool> DeleteUser(IndividualUser urs)
        {
            try
            {
                await UserRepo.DeleteByIdAsync(urs);
                return true;
            }catch(Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> UpdateDeletedAt(IndividualUser urs)
        {
            try
            {
                urs.DeletedAt = DateTime.Now;
                await UserRepo.UpdateAsync(urs);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<IndividualUser> FindByUsername(string username)
        {
            List<IndividualUser> individualUser = UserRepo.GetByQueryAsync(user => user.Username == username && user.DeletedAt == null);
            return individualUser[0];
        }

        public bool CheckPassword(IndividualUser user, string password)
        {
            return true;
        }
    }


}
