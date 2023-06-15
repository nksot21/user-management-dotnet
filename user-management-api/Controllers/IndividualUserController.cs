using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using user_management_api.Data;
using user_management_api.Helpers;
using user_management_api.Entities;
using user_management_api.Models;
using user_management_api.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace user_management_api.Controllers
{


    [ApiController]
    [Route("/api/v2/user")]
    public class IndividualUserController : Controller
    {
        private readonly IUserServices userService;
       
        private readonly IConfiguration configuration;
        public IndividualUserController(IUserServices userService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }


        [HttpGet]
        [Authorize]
        async public Task<IActionResult> GetAllUsers()
        {
            //try
            //{
                List<IndividualUser> lst = await userService.GetAllUser();
                List<UserResponseModel> userResponses = lst.ConvertAll(usr => new UserResponseModel(usr.Fullname, usr.Username, usr.Email, usr.CreatedAt, usr.UpdatedAt));
                return Ok(new Response { Status = 200, Message = "Get All User Successfully", Data = userResponses });
            //}
            //catch(Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }


        [HttpGet("{id}")]
        [Authorize]
        async public Task<IActionResult> GetUserById(int id)
        {
            //try
            //{
                var usr = await userService.GetUserById(id);
                if (usr == null)
                    throw new Exception("User not found");
                UserResponseModel userResponse = new UserResponseModel(usr.Fullname, usr.Username, usr.Email, usr.CreatedAt, usr.UpdatedAt);
            
            return Ok(new Response { Status=200, Message="Get User By Id Successfully", Data=userResponse});
            //}catch(Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }

        [HttpPost]
        async public Task<IActionResult> Register([FromBody] CreateUserModel user)
        {
            //try
            //{
            // Check username
            IndividualUser individualUser = await userService.FindByUsername(user.Username);
            if (individualUser != null)
            {
                throw new Exception("User already existed");
            }
            // hash password
            var hashedPassword = PasswordHelper.hashPassword(user.Password);
            if (hashedPassword.hashedPassword == null)
                {
                    throw new Exception("Hash-Password error");
                }
                // Convert fr request to user model
            var newUser = new IndividualUser(user.Fullname, user.Username, user.Email, hashedPassword.hashedPassword, hashedPassword.salt);

                // Save
                bool result = await userService.CreateUser(newUser);
                if (!result)
                {
                    throw new Exception("Create failed");
                }
            return Ok(new Response { Status = 200, Message = "Create User Successfully", Data = { } });
            //}catch(Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}

        }

        [HttpGet("profile")]
        [Authorize]
        async public Task<IActionResult> GetProfile()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            if (token == null)
                throw new Exception("Get Bearer Token failed");
            var username = JWTHelper.GetPayload(token);
            IndividualUser usr = await userService.FindByUsername(username);
            UserResponseModel response = new UserResponseModel(usr.Fullname, usr.Username, usr.Email, usr.CreatedAt, usr.UpdatedAt);
            return Ok(new Response { Status = 200, Message = "Get Profile Successfully", Data = response });
        }

        [HttpPut("{id}")]
        [Authorize]
        async public Task<IActionResult> Update([FromBody] UpdateUserModel userReq, int id)
        {
            //try
            //{
                var individualUser = await userService.GetUserById(id);
                if (individualUser == null)
                {
                    throw new Exception("User not found");
                }
                bool result = await userService.Update(userReq, individualUser);
                if (!result)
                    throw new Exception("Update failed");
            return Ok(new Response { Status = 200, Message = "Update User Successfully", Data = { } });
            //}catch(Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }

        [HttpDelete("{id}")]
        [Authorize]
        async public Task<IActionResult> Delete(int id)
        {
            //try
            //{
                var individualUser = await userService.GetUserById(id);
                if (individualUser == null)
                {
                    throw new Exception("User not found");
                }
                bool result = await userService.DeleteUser(individualUser);
                if (!result)
                {
                    throw new Exception("Delete failed");
                }
                return Ok(new Response { Status = 200, Message = "Delete User Successfully", Data = { } });
            //}catch(Exception ex) {
            //    return BadRequest(ex.Message);
            //}
        }

        [HttpPut("delete/{id}")]
        [Authorize]
        async public Task<IActionResult> UpdateDelete(int id)
        {
            //try
            //{
                var individualUser = await userService.GetUserById(id);
                if (individualUser == null)
                {
                    throw new Exception("User not found");
                }
                bool result = await userService.UpdateDeletedAt(individualUser);
                if (!result)
                {
                    throw new Exception("Delete failed");
                }
            return Ok(new Response { Status = 200, Message = "Delete User Successfully", Data = { } });
            //}catch(Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }

        [HttpPost("login/")]
        async public Task<IActionResult> Login([FromBody] LoginModel loginData)
        {
            // Validate Email
            IndividualUser individualUser = await userService.FindByUsername(loginData.Username);
            if (individualUser == null)
            {
                throw new Exception("User Not Found");
            }
            var result = PasswordHelper.verifyPassword(loginData.Password, individualUser.Password, individualUser.Salt);
            if (result == false)
            {
                throw new Exception("Incorrect password");
            }

            // Generate Access Token
            var jwtToken = JWTHelper.GenerateJWT(individualUser.Username, configuration["JWT:Issuer"], configuration["JWT:Audience"], configuration["JWT:Key"]);
            var response = new LoginResponseModel()
            {
                Username = individualUser.Username,
                AccessToken = jwtToken,
            };
            return Ok(new Response() { Status = 200, Message = "Login Successfully", Data = response });
        }

        //private IActionResult res()
        //{
        //    retu
        //}


        // POST: IndividualUser/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: IndividualUser/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: IndividualUser/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: IndividualUser/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: IndividualUser/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
