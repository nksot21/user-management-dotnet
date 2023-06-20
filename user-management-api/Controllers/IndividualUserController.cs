using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using user_management_api.Entities;
using user_management_api.Helpers;
using user_management_api.Models;
using user_management_api.Services;

namespace user_management_api.Controllers
{


    [ApiController]
    [ResponseCache(Duration = 60)]
    [Route("/api/v2/user")]
    public class IndividualUserController : Controller
    {
        private readonly IUserServices UserService;

        private readonly IConfiguration Configuration;
        private readonly ICacheService CacheService;

        public IndividualUserController(IUserServices userService, IConfiguration configuration, ICacheService cacheService)
        {
            UserService = userService;
            Configuration = configuration;
            CacheService = cacheService;
        }


        [HttpGet]
        [Authorize]
        async public Task<IActionResult> GetAllUsers()
        {
            List<IndividualUser> lst = await UserService.GetAllUser();
            List<UserResponseModel> userResponses = lst.ConvertAll(usr => new UserResponseModel(usr.Fullname, usr.Username, usr.Email, usr.CreatedAt, usr.UpdatedAt));
            return Ok(new Response { Status = 200, Message = "Get All User Successfully", Data = userResponses });
        }


        [HttpGet("{id}")]
        [Authorize]
        async public Task<IActionResult> GetUserById(int id)
        {

            var usr = await UserService.GetUserById(id);
            if (usr == null)
                throw new Exception("User not found");
            UserResponseModel userResponse = new UserResponseModel(usr.Fullname, usr.Username, usr.Email, usr.CreatedAt, usr.UpdatedAt);

            return Ok(new Response { Status = 200, Message = "Get User By Id Successfully", Data = userResponse });
        }

        [HttpPost]
        async public Task<IActionResult> Register([FromBody] CreateUserModel user)
        {
            // Check username
            IndividualUser individualUser = await UserService.FindByUsername(user.Username);
            if (individualUser != null)
            {
                throw new Exception("User already existed");
            }

            // hash password
            var hashedPassword = PasswordHelper.HashPassword(user.Password);
            if (hashedPassword.hashedPassword == null)
            {
                throw new Exception("Hash-Password error");
            }

            // Convert fr request to user model
            var newUser = new IndividualUser(user.Fullname, user.Username, user.Email, hashedPassword.hashedPassword, hashedPassword.salt);

            // Save
            bool result = await UserService.CreateUser(newUser);
            if (!result)
            {
                throw new Exception("Create failed");
            }
            return Ok(new Response { Status = 200, Message = "Create User Successfully", Data = { } });

        }

        [HttpGet("profile")]
        [Authorize]
        async public Task<IActionResult> GetProfile()
        {
            // Get Token 
            var token = await HttpContext.GetTokenAsync("access_token");
            if (token == null) {
                throw new Exception("Get Bearer Token failed");
            }
               
            // Get Payload / Check cache token
            var username = JWTHelper.GetPayload(token);
            if(username == null)
            {
                throw new Exception("Invalid token");
            }
            var result = await CacheService.Get(username);
            if (result == null)
            {
                throw new Exception("Expired Tokens");
            }

            // Get User
            IndividualUser usr = await UserService.FindByUsername(username);
            UserResponseModel response = new UserResponseModel(usr.Fullname, usr.Username, usr.Email, usr.CreatedAt, usr.UpdatedAt);
            return Ok(new Response { Status = 200, Message = "Get Profile Successfully", Data = response });
        }

        [HttpPut("{id}")]
        [Authorize]
        async public Task<IActionResult> Update([FromBody] UpdateUserModel userReq, int id)
        {
            var individualUser = await UserService.GetUserById(id);
            if (individualUser == null)
            {
                throw new Exception("User not found");
            }
            bool result = await UserService.Update(userReq, individualUser);
            if (!result)
                throw new Exception("Update failed");
            return Ok(new Response { Status = 200, Message = "Update User Successfully", Data = { } });
        }

        [HttpDelete("{id}")]
        [Authorize]
        async public Task<IActionResult> Delete(int id)
        {
            var individualUser = await UserService.GetUserById(id);
            if (individualUser == null)
            {
                throw new Exception("User not found");
            }
            bool result = await UserService.DeleteUser(individualUser);
            if (!result)
            {
                throw new Exception("Delete failed");
            }
            return Ok(new Response { Status = 200, Message = "Delete User Successfully", Data = { } });
        }

        [HttpPut("delete/{id}")]
        [Authorize]
        async public Task<IActionResult> UpdateDelete(int id)
        {
            var individualUser = await UserService.GetUserById(id);
            if (individualUser == null)
            {
                throw new Exception("User not found");
            }
            bool result = await UserService.UpdateDeletedAt(individualUser);
            if (!result)
            {
                throw new Exception("Delete failed");
            }
            return Ok(new Response { Status = 200, Message = "Delete User Successfully", Data = { } });
        }

        async private Task<(IndividualUser user, string jwtToken)> handleLogin(LoginModel loginData)
        {
            // Validate Email
            IndividualUser user = await UserService.FindByUsername(loginData.Username);
            if (user == null)
            {
                throw new Exception("User Not Found");
            }

            var result = PasswordHelper.VerifyPassword(loginData.Password, user.Password, user.Salt);
            if (result == false)
            {
                throw new Exception("Incorrect password");
            }

            // Generate Access Token
            var jwtToken = JWTHelper.GenerateJWT(user.Username, Configuration["JWT:Issuer"], Configuration["JWT:Audience"], Configuration["JWT:Key"]);
            return (user,  jwtToken);
        }

        [HttpPost("login/")]
        async public Task<IActionResult> Login([FromBody] LoginModel loginData)
        {
            // Check/handle Login Data
            var data = await handleLogin(loginData);

            // Save Access Token to Redis
            var cacheResult = await CacheService.Set(data.user.Username, data.jwtToken, DateTime.Now.AddMinutes(200));
            if (cacheResult == false)
            {
                throw new Exception("Cache access token failed");
            }

            // Response
            var response = new LoginResponseModel()
            {
                Username = data.user.Username,
                AccessToken = data.jwtToken,
            };
            return Ok(new Response() { Status = 200, Message = "Login Successfully", Data = response });
        }

        [HttpGet("logout")]
        [Authorize]
        async public Task<IActionResult> Logout()
        {
            // Get Token 
            var token = await HttpContext.GetTokenAsync("access_token");
            if (token == null)
            {
                throw new Exception("Get Bearer Token failed");
            }

            // Get Payload
            var username = JWTHelper.GetPayload(token);
            var getResult = await CacheService.Get(username);
            if(getResult == null) {
                throw new Exception("Invalid token");
            }
            var deleteResult = await CacheService.Delete(username);
            if(deleteResult == false)
            {
                throw new Exception("Delete failed");
            }
            return Ok(new Response(200, "Logout successfully", null));
        }

        public class TokenReceiveModel
        {
            public string Key { get; set; }
        }

        [HttpPost("cache")]
        async public Task<IActionResult> GetCacheData([FromBody] TokenReceiveModel token)
        {
            var result = await CacheService.Get(token.Key);
            if(result == null)
                throw new Exception("Key not found");
            return Ok(result);
        }

        [HttpPost("delete-cache")]
        async public Task<IActionResult> DeleteCacheData([FromBody] TokenReceiveModel token)
        {
            var result = await CacheService.Delete(token.Key);
            if (result == false)
                throw new Exception("Delete failed");
            return Ok(result);
        }

    }
}
