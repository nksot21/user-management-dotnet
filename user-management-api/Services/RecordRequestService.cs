using user_management_api.Data;
using user_management_api.Models;

namespace user_management_api.Services
{
    public class RecordRequestService : IRecordRequestService
    {
        private readonly IMongoDbContext mongoService;
        public RecordRequestService(IMongoDbContext mongoService) { 
            this.mongoService = mongoService;  
        }

        async public Task<bool> SaveRequest(RequestHistory requestHistory)
        {
            try
            {
                await mongoService.RequestHistoryModel.InsertOneAsync(requestHistory);
                return true;
            }catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}