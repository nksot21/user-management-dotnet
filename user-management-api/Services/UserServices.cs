﻿using Microsoft.EntityFrameworkCore;
using user_management_api.Data;
using user_management_api.Entities;
using user_management_api.Helpers;
using user_management_api.Models;

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
        private IDatabaseService databaseService { get; }

        public UserServices(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }
        //public async Task<(bool, string)> CreateUser(IndividualUser user)
             public async Task<bool> CreateUser(IndividualUser user)
        {
            try
            {
                await databaseService.Context.IndividualUsersModel.AddAsync(user);
                await databaseService.Context.SaveChangesAsync();
                //return (true, "Thnhaf công");
                return true;
            }catch (Exception ex)
            {
                return  false;
            }
        }

        public async Task<List<IndividualUser>> GetAllUser()
        {
            List<IndividualUser> allUserLst = await databaseService.Context.IndividualUsersModel.ToListAsync();
            List<IndividualUser> userLst = allUserLst.Where(usr => usr.DeletedAt == null).ToList();
            return userLst;
        }

        public async Task<IndividualUser> GetUserById(int id)
        {
            var usr = await databaseService.Context.IndividualUsersModel.FirstOrDefaultAsync(x => (x.Id == id && x.DeletedAt == null));
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
                databaseService.Context.Update(user);
                await databaseService.Context.SaveChangesAsync();
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
                databaseService.Context.IndividualUsersModel.Remove(urs);
                await databaseService.Context.SaveChangesAsync();
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
                await databaseService.Context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<IndividualUser> FindByUsername(string username)
        {
            IndividualUser individualUser = await databaseService.Context.IndividualUsersModel.FirstOrDefaultAsync(x => (x.Username == username && x.DeletedAt == null));
            return individualUser;
        }

        public bool CheckPassword(IndividualUser user, string password)
        {
            return true;
        }
    }


}