using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using StackExchange.Redis;

namespace user_management_api.Data
{
    public class RedisContext : IRedisContext
    {


        private readonly Lazy<ConnectionMultiplexer> lazyConnection;
        public RedisContext(IConfiguration configuration)
        {
            lazyConnection = new Lazy<ConnectionMultiplexer>(() => {
                return ConnectionMultiplexer.Connect(configuration["RedisDBSettings:ConnectionStrings"]);
            });
        }

        public  ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
