using user_management_api.Models;

namespace user_management_api.Services
{
    public interface IRecordRequestService
    {
        public Task<bool> SaveRequest(RequestHistory requestHistory);
    }
}
