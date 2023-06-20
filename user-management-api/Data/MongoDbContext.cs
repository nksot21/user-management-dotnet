using MongoDB.Driver;
using user_management_api.Models;

namespace user_management_api.Data
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase database;
        public MongoDbContext(IConfiguration configuration) { 

            var client = new MongoClient(configuration["UserManagementDatabase:ConnectionString"]);
            database = client.GetDatabase(configuration["UserManagementDatabase:DatabaseName"]);
        }

        public IMongoCollection<RequestHistory> RequestHistoryModel => database.GetCollection<RequestHistory>("requestHistory");
    }
}
