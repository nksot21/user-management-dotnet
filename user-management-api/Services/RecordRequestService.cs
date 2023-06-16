using user_management_api.Models;

namespace user_management_api.Services
{
    public class RecordRequestService : IRecordRequestService
    {
        private readonly IMongoService mongoService;
        public RecordRequestService(IMongoService mongoService) { 
            this.mongoService = mongoService;  
        }

        async public Task<bool> SaveRequest(RequestHistory requestHistory)
        {
            try
            {
                await mongoService.MongoContext.RequestHistoryModel.InsertOneAsync(requestHistory);
                return true;
            }catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}