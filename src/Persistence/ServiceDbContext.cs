
namespace Linn.Authorisation.Persistence
{
    using Linn.Authorisation.Domain;
    using Linn.Common.Configuration;

    using Microsoft.EntityFrameworkCore;

    public class ServiceDbContext : DbContext
    {
        public DbSet<Role> Role { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var host = ConfigurationManager.Configuration["DATABASE_HOST"];
            var databaseName = ConfigurationManager.Configuration["DATABASE_NAME"];
            var userId = ConfigurationManager.Configuration["DATABASE_USER_ID"];
            var password = ConfigurationManager.Configuration["DATABASE_PASSWORD"];

            optionsBuilder.UseNpgsql($"User ID={userId};Password={password};Host={host};Database={databaseName};Port=5432;Pooling=true;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.BuildRole(builder);

            base.OnModelCreating(builder);
        }

        private void BuildRole(ModelBuilder builder)
        {
            builder.Entity<Role>().HasKey(role => role.Id);
            builder.Entity<Role>().Property(role => role.Name);
            builder.Entity<Role>().HasMany(role => role.Permissions);
            builder.Entity<Role>().HasMany(role => role.Members);
        }
    }
}
