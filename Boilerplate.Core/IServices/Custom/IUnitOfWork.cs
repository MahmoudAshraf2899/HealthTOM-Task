using Boilerplate.Contracts.IServices.Repositories.Auth;
using Boilerplate.Contracts.IServices.Repositories.Auth.Roles;
using Boilerplate.Contracts.IServices.Repositories.Migrations;
using Boilerplate.Core.IServices.Custom.Repositories;
using Boilerplate.Core.IServices.Repositories.Patients;
using Boilerplate.Core.IServices.Repositories.Visits;
using Microsoft.EntityFrameworkCore.Storage;

namespace Boilerplate.Core.IServices.Custom
{
    public interface IUnitOfWork : IDisposable
    {
        public IRoleRepository Roles { get; }
        public IRoleTranslationRepository RoleTranslations { get; }
        public IUserRepository Users { get; }
        public IUserRoleRepository UserRoles { get; }
        public ILicenceRepository Licences { get; }
        public ITimeLogRepository TimeLogs { get; }

        #region Permission
        public IPermissionModuleRepository PermissionModule { get; }
        public IRolePermissionRepository RolePermission { get; }
        #endregion

        #region Log
        public ILogSystemRepository LogSystem { get; }
        public ILogErrorRepository LogError { get; }
        #endregion

        #region Migrations
        public IMigrationRepository Migrations { get; }
        #endregion

        #region Patient
        public IPatientRepository Patient { get; }
        #endregion

        #region Visit
        public IVisitRepository Visit { get; }
        #endregion
        public IDbContextTransaction Transaction();
        public int Complete();
        void ChangeTracker();
    }
}
