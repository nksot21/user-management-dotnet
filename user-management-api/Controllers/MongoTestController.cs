using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using user_management_api.Models;
using user_management_api.Services;

namespace user_management_api.Controllers
{

    [ApiController]
    [Route("/test")]
    public class MongoTestController : ControllerBase
    {
        public readonly IMongoService mongoService;

        public MongoTestController(IMongoService mongoService)
        {
            this.mongoService = mongoService;
        }

        [HttpPost]
        async public Task<IActionResult> Create([FromBody] RequestHistory requestHistory)
        {
            mongoService.MongoContext.RequestHistoryModel.InsertOne(requestHistory);
            return Ok("Create successfully");
        }
    }  
}
