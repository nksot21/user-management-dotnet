using MongoDB.Driver;
using user_management_api.Models;

namespace user_management_api.Data
{
    public interface IMongoDbContext
    {
        IMongoCollection<RequestHistory> RequestHistoryModel { get; }
    }
}
