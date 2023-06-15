using Microsoft.AspNetCore.Mvc;
using user_management_api.Models;

namespace user_management_api.Controllers
{
    public static class UserList
    {
        public static List<User> users = new List<User>(){
            new User(){Fullname= "Jennie 1", Username= "Jennie 1" }
        };
    }

    [ApiController]
    [Route("/api/v1/users")]
    public class UserController : Controller
    {


        [HttpGet]
        public IActionResult getAllUsers()
        {
            try
            {
                List<User> userExisted = UserList.users.FindAll(user => user.DeletedAt == null);
                return Ok(userExisted);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult getUserById(int id)
        {
            try
            {
                int index = UserList.users.FindIndex(user => (user.Id == id && user.DeletedAt == null));
                if (index == -1)
                {
                    throw
                        new Exception("User not found");
                }
                return Ok(UserList.users[index]);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult createUsers([FromBody] User user) {
            try
            {
                UserList.users.Add(user);
                return Ok("Create user successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult updateUsers(int id, [FromBody]  User user)
        {
            try
            {
                int index = UserList.users.FindIndex(user => (user.Id == id && user.DeletedAt == null));
                if(index == -1)
                {
                    throw
                        new Exception("User not found");
                }
                user.UpdatedAt = DateTime.Now;
                UserList.users[index] = user;
                return Ok("Update user successfully");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("delete/{id}")]
        public IActionResult softDeleteUsers(int id)
        {
            try
            {
                int index = UserList.users.FindIndex(user => (user.Id == id && user.DeletedAt == null));
                if (index == -1)
                {
                    throw
                        new Exception("User not found");
                }
                UserList.users[index].DeletedAt = DateTime.Now;
                //return Ok(UserList.users[index]);
                return Ok("Remove user successfully");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult deleteUsers(int id)
        {
            try
            {
                int index = UserList.users.FindIndex(user => (user.Id == id && user.DeletedAt == null));
                if (index == -1)
                {
                    throw
                        new Exception("User not found");
                }
                UserList.users.RemoveAt(index);
                return Ok("Remove user successfully");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
