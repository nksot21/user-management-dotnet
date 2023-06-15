using Microsoft.EntityFrameworkCore;
using user_management_api.Data;

namespace user_management_api.Services
{
    public class DatabaseService : IDatabaseService
    {
        public ApiDbContext Context { get; }
        public DatabaseService() {
            Context = new ApiDbContext();
        }
    }
}
