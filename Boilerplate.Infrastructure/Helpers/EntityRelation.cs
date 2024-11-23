using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Infrastructure.Helpers
{
    public partial class EntityRelation
    {
        public EntityRelation(ModelBuilder modelBuilder)
        {
            CreateRelationBetweenEntityAndDefaultValues(modelBuilder);
        }
        private void CreateRelationBetweenEntityAndDefaultValues(ModelBuilder modelBuilder)
        { 

            RoleRelations(modelBuilder);

            UserRelations(modelBuilder);

            UserRoleRelations(modelBuilder);

        }
    }
}
