using Boilerplate.Core.Entities.Auth;
using Boilerplate.Core.Entities.Auth.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Infrastructure.Configuration
{
    public class MicrosoftIdentityConfiguration
    {
        public  MicrosoftIdentityConfiguration(ModelBuilder builder)
        {
            builder.Entity<User>()
                .ToTable("Users", "security");
            builder.Entity<IdentityUserRole<string>>()
                .ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>()
                .ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>()
                .ToTable("UserLogins", "security");
            builder.Entity<IdentityUserToken<string>>()
                .ToTable("UserTokens", "security");
            builder.Entity<IdentityRole>()
                .ToTable("Roles", "security");
            builder.Entity<IdentityRoleClaim<string>>()
                .ToTable("RoleClaims", "security");
        }
    }
}