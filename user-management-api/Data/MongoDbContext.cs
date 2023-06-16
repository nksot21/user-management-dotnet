using MongoDB.Driver;
using user_management_api.Models;

namespace user_management_api.Data
{
    public class MongoDbContext
    {
        private readonly  IMongoDatabase _database;
        public MongoDbContext(string databaseName, string connectionStr) { 

            var client = new MongoClient(connectionStr);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<RequestHistory> RequestHistoryModel => _database.GetCollection<RequestHistory>("requestHistory");
    }
}
