using Microsoft.EntityFrameworkCore;
using Tsak.WebshopProducts2021.DataAccess.Entities;
using Tsak.WebshopProducts2021.Security.Model;

namespace Tsak.WebshopProducts2021.Security
{
        public class AuthDbContext: DbContext
        {
            public AuthDbContext(DbContextOptions<AuthDbContext> options): base(options) {}

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<UserPermission>()
                    .HasKey(u => new {u.PermissionId, u.UserId});
            }

            public DbSet<Permission> Permissions { get; set; }
            public DbSet<LoginUser> LoginUsers { get; set; }
            public DbSet<UserPermission> UserPermissions { get; set; }
        }
}