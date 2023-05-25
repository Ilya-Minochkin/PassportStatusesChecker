using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class PgDbContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ReadinessResponse> ReadinessResponses { get; set; }
        public DbSet<PublicStatus> PublicStatuses { get; set; }
        public DbSet<InternalStatus> InternalStatuses { get; set; }

        public PgDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
        }
    }

}