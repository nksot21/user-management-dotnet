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
        private readonly IUserRepository userRepository;

        public UserServices(IUserRepository userRepository)
        {

            this.userRepository = userRepository;
        }

        //public async Task<(bool, string)> CreateUser(IndividualUser user)
             public async Task<bool> CreateUser(IndividualUser user)
        {
            try
            {
                await userRepository.Add(user);
                //return (true, "Thnhaf công");
                return true;
            }catch (Exception ex)
            {
                return  false;
            }
        }

        public async Task<List<IndividualUser>> GetAllUser()
        {
            List<IndividualUser> allUserLst = await userRepository.GetAll();
            List<IndividualUser> userLst = allUserLst.Where(usr => usr.DeletedAt == null).ToList();
            return userLst;
        }

        public async Task<IndividualUser> GetUserById(int id)
        {
            var usr = await userRepository.GetById(id);
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
                await userRepository.Update(user);
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
                await userRepository.Delete(urs);
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
                await userRepository.Update(urs);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<IndividualUser> FindByUsername(string username)
        {
            IndividualUser individualUser = await userRepository.FindByUsername(username);
            return individualUser;
        }

        public bool CheckPassword(IndividualUser user, string password)
        {
            return true;
        }
    }


}
