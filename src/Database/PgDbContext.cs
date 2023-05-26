using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database
{
    public class PgDbContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ReadinessResponse> ReadinessResponses { get; set; }
        public DbSet<PublicStatus> PublicStatuses { get; set; }
        public DbSet<InternalStatus> InternalStatuses { get; set; }
        public DbSet<Application> Applications { get; set; }

        public PgDbContext(DbContextOptions<PgDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public PgDbContext() 
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PublicStatus>()
                .HasOne(ps => ps.Response)
                .WithOne(r => r.PublicStatus)
                .HasForeignKey<ReadinessResponse>(r => r.PublicStatusId);

            modelBuilder.Entity<InternalStatus>()
                .HasOne(s => s.Response)
                .WithOne(r => r.InternalStatus)
                .HasForeignKey<ReadinessResponse>(r => r.InternalStatusId);

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
        //}
    }

    public class PgDbContextFactory : IDesignTimeDbContextFactory<PgDbContext>
    {
        public PgDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PgDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=passport_statuses_checker;Username=postgres;Password=postgres");
            
            return new PgDbContext(optionsBuilder.Options);
        }
    }

}