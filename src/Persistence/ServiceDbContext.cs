namespace Linn.Authorisation.Persistence
{
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;

    using Linn.Common.Configuration;

    using Microsoft.EntityFrameworkCore;

    public class ServiceDbContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Privilege> Privileges { get; set; }

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
            this.BuildGroups(builder);
            this.BuildMembers(builder);
            this.BuildPermissions(builder);
            this.BuildPrivileges(builder);

            base.OnModelCreating(builder);
        }

        private void BuildGroups(ModelBuilder builder)
        {
            builder.Entity<Group>().HasKey(a => a.Id);
            builder.Entity<Group>().Property(a => a.Active);
            builder.Entity<Group>().Property(a => a.Name);
            builder.Entity<Group>().HasMany(a => a.Members);
        }

        private void BuildMembers(ModelBuilder builder)
        {
            builder.Entity<Member>().HasKey(a => a.Id);
            builder.Entity<Member>().Property(a => a.AddedByUri);
            builder.Entity<Member>().Property(a => a.DateAdded);
            builder.Entity<Member>().HasKey(a => a.Id);

            builder.Entity<IndividualMember>().HasBaseType<Member>();
            builder.Entity<GroupMember>().HasBaseType<Member>();

            builder.Entity<GroupMember>().HasOne<Group>(a => a.Group);

            builder.Entity<IndividualMember>().Property(a => a.MemberUri);
        }

        private void BuildPermissions(ModelBuilder builder)
        {
            builder.Entity<Permission>().HasKey(a => a.Id);
            builder.Entity<Permission>().HasOne<Privilege>(a => a.Privilege);
            builder.Entity<Permission>().Property(a => a.DateGranted);
            builder.Entity<Permission>().Property(a => a.GrantedByUri);

            builder.Entity<IndividualPermission>().HasBaseType<Permission>();
            builder.Entity<GroupPermission>().HasBaseType<Permission>();

            builder.Entity<IndividualPermission>().Property(a => a.GranteeUri);
            builder.Entity<GroupPermission>().HasOne(a => a.GranteeGroup);

        }

        private void BuildPrivileges(ModelBuilder builder)
        {
            builder.Entity<Privilege>().HasKey(a => a.Id);
            builder.Entity<Privilege>().Property(a => a.Active);
            builder.Entity<Privilege>().Property(a => a.Name);
        }
    }
}
