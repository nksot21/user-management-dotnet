using Microsoft.EntityFrameworkCore;
using user_management_api.Data;

namespace user_management_api.Services
{
    public interface IDatabaseService
    {
        ApiDbContext Context { get; }
    }

}
