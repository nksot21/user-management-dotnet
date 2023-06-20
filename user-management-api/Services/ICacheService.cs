using user_management_api.Data;

namespace user_management_api.Services
{
    public interface ICacheService
    {
        public Task<string> Get(string key);
        public Task<bool> Set(string key, string value, DateTimeOffset timeOffset);
        public Task<bool> Delete(string key);
    }
}
