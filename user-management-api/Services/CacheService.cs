
using MongoDB.Bson.IO;
using StackExchange.Redis;
using user_management_api.Data;

namespace user_management_api.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase database;
        public CacheService(IRedisContext context)
        {
            database = context.Connection.GetDatabase();
        }

         async public Task<string> Get(string key)
        {
           var value = await database.StringGetAsync(key);
            if (!string.IsNullOrEmpty(value))
            {
                return value.ToString();
            }
            return null;
        }
        async public Task<bool> Set(string key, string value, DateTimeOffset timeOffset)
        {
            try
            {
                TimeSpan expiryTime = timeOffset.DateTime.Subtract(DateTime.Now);

                var isSet = await database.StringSetAsync(key, value, expiryTime);
                return isSet;
            }
            catch(Exception ex) {
                return false;
            }
           
        }
        async public Task<bool> Delete(string key)
        {

            var result = await database.KeyDeleteAsync(key);
            return result;
        }

    }
}
