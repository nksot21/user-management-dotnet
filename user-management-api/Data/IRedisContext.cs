using StackExchange.Redis;

namespace user_management_api.Data
{
    public interface IRedisContext
    {
        public ConnectionMultiplexer Connection { get; }
    }
}
