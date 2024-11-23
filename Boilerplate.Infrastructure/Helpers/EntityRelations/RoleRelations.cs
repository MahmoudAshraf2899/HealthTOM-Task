using Boilerplate.Core.Entities.Auth.Roles;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Infrastructure.Helpers
{
    public partial class EntityRelation
    {
        public void RoleRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasMany(d => d.RoleTranslations)
                   .WithOne(p => p.Role)
                   .HasForeignKey(d => d.RoleId)
                   .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.RolePermissions)
                  .WithOne(p => p.Role)
                  .HasForeignKey(d => d.RoleId)
                  .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.UserRoles)
                  .WithOne(p => p.Role)
                  .HasForeignKey(d => d.RoleId)
                  .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(a => new { a.Name })
                    .IsUnique(true);
            });

            modelBuilder.Entity<RoleTranslation>(entity =>
            {
                entity.HasIndex(a => new { a.RoleId, a.Locale })
                .IsUnique(true);
            });
        }
    }
}
