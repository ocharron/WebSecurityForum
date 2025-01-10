using Forum.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Forum.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Commentaire> Commentaires { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<SecurityLog> SecurityLogs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ignore the default ASP.NET Identity tables that will not be used
            modelBuilder.Ignore<IdentityUserLogin<Guid>>();

            // Change default ASP.NET Identity tables names
            modelBuilder.Entity<User>(entity => entity.ToTable(name: "Users"));
            modelBuilder.Entity<IdentityRole<Guid>>(entity => entity.ToTable(name: "Roles"));
            modelBuilder.Entity<IdentityUserRole<Guid>>(entity => entity.ToTable("UsersRoles"));
            modelBuilder.Entity<IdentityUserToken<Guid>>(entity => entity.ToTable("UsersTokens"));
            modelBuilder.Entity<IdentityUserClaim<Guid>>(entity => entity.ToTable("UsersClaims"));
            modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity => entity.ToTable("RolesClaims"));

            modelBuilder.RunSeed();
        }
    }
}
