using user_management_api.Data;

namespace user_management_api.Services
{
    public class MongoService : IMongoService
    {
        private readonly IConfiguration configuration;
        public MongoDbContext MongoContext { get; set; }

        public MongoService(IConfiguration configuration) {
            var connectionStr = configuration["UserManagementDatabase:ConnectionString"];
            var databaseName = configuration["UserManagementDatabase:DatabaseName"];
            this.MongoContext = new MongoDbContext(databaseName, connectionStr);
        }
    }
}
