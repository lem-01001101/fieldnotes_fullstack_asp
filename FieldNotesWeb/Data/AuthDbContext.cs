using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FieldNotesWeb.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // seed roles (user, admin, superadmin)
            var adminRoleId = "a0c5cedd-50f1-4ece-a225-95f6feeda985";
            var superAdminRoleId = "491b6f02-d5b8-4443-bf97-7c1be2c11f42";
            var userRoleId = "20588c7b-9053-42bf-b1cf-175bf102fc78";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // seed superadminuser
            var superAdminId = "c760d683-bda3-49ab-97c3-5c72bc620b19";

            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@fieldnotes.com",
                Email = "superadmin@fieldnotes.com",
                NormalizedEmail = "superadmin@fieldnotes.com".ToUpper(),
                NormalizedUserName = "superadmin@fieldnotes.com".ToUpper(),
                Id = superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "Superadmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // add all roles to superadmin user
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
