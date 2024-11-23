using Boilerplate.Core.Entities.Auth.Roles;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Infrastructure.Helpers
{
    public partial class EntityRelation
    {
        public void UserRoleRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasIndex(a => new { a.UserId, a.RoleId })
                .IsUnique(true);
            });
        }
    }
}
