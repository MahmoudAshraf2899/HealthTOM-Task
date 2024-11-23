using Boilerplate.Core.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Infrastructure.Helpers
{
    public partial class EntityRelation
    {
        public void UserRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(a => new { a.UserName, a.Email })
                .IsUnique(true);
            });
        }
    }
}
