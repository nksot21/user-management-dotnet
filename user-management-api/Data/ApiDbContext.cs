using Microsoft.EntityFrameworkCore;
using user_management_api.Models;

namespace user_management_api.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<IndividualUser> individualUsersModel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=UserManagementDB");
        }
    }
}
