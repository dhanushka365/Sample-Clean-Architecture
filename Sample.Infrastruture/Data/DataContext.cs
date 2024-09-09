using Microsoft.EntityFrameworkCore;
using Sample.Domain;

namespace Sample.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring
        (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Sample");
        }
        public DbSet<User> Users { get; set; }
    }
}
