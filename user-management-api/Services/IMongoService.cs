using user_management_api.Data;

namespace user_management_api.Services
{
    public interface IMongoService
    {
        MongoDbContext MongoContext { get; }
    }
}
