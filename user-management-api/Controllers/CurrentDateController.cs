using Microsoft.AspNetCore.Mvc;

namespace user_management_api.Controllers
{
    [ApiController]
    [Route("/api/v1/date")]
    public class CurrentDateController : Controller
    {

        [HttpGet]
        public IActionResult getCurrenyDate()
        {
            try
            {
                var date = DateTime.Now;
                return StatusCode(200, date);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            //return DateTime.Now;
        }
    }
}
