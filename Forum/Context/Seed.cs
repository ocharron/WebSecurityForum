using Forum.Entities;
using Forum.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Forum.Context
{
    public static class Seed
    {
        private static readonly PasswordHasher<User> _passwordHasher = new();

        public static void RunSeed(this ModelBuilder builder)
        {
            IdentityRole<Guid> adminRole = AddRole(builder, Constantes.ADMIN_ROLE);
            IdentityRole<Guid> userRole = AddRole(builder, Constantes.USER_ROLE);

            User adminUser = AddUser(builder, "Admin", "Qwerty123!");
            User normalUser = AddUser(builder, "User", "Qwerty123!");

            AddUserToRole(builder, adminUser, adminRole);
            AddUserToRole(builder, normalUser, userRole);
        }

        private static void AddUserToRole(ModelBuilder builder, User newUser, IdentityRole<Guid> role)
        {
            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                UserId = newUser.Id,
                RoleId = role.Id,
            });
        }

        private static IdentityRole<Guid> AddRole(ModelBuilder builder, string name)
        {
            IdentityRole<Guid> newRole = new()
            {
                Id = Guid.NewGuid(),
                Name = name,
                NormalizedName = name.ToUpper()
            };
            builder.Entity<IdentityRole<Guid>>().HasData(newRole);

            return newRole;
        }

        private static User AddUser(ModelBuilder builder, string userName, string password)
        {
            User newUser = new(userName)
            {
                Id = Guid.NewGuid(),
                NormalizedUserName = userName.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, password + Constantes.GLOBAL_PEPPER);
            builder.Entity<User>().HasData(newUser);

            return newUser;
        }
    }
}
