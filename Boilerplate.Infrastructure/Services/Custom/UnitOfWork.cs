using Boilerplate.Contracts.Helpers;
using Boilerplate.Contracts.IServices.Repositories.Auth;
using Boilerplate.Contracts.IServices.Repositories.Auth.Roles;  
using Boilerplate.Contracts.IServices.Repositories.Migrations;
using Boilerplate.Core.Entities.Patient;
using Boilerplate.Core.Entities.Visit;
using Boilerplate.Core.IServices.Custom;
using Boilerplate.Core.IServices.Custom.Repositories;
using Boilerplate.Core.IServices.Repositories.Patients;
using Boilerplate.Core.IServices.Repositories.Visits;
using Boilerplate.Infrastructure.DBContexts;
using Boilerplate.Infrastructure.Repositories.Custom.Log_System;
using Boilerplate.Infrastructure.Services.Repositories;
using Boilerplate.Infrastructure.Services.Repositories.Auth;
using Boilerplate.Infrastructure.Services.Repositories.Auth.Roles;
using Boilerplate.Infrastructure.Services.Repositories.Migrations;
using Boilerplate.Infrastructure.Services.Repositories.Patients;
using Boilerplate.Infrastructure.Services.Repositories.Visits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Boilerplate.Infrastructure.Services.Custom
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BoilerplateDBContext _context;

        public IRoleRepository Roles { get; private set; }
        public IRoleTranslationRepository RoleTranslations { get; }
        public IUserRepository Users { get; private set; }
        public IUserRoleRepository UserRoles { get; private set; }
        public ILicenceRepository Licences { get; private set; }
        public ITimeLogRepository TimeLogs { get; private set; }

        #region  Permissions
        public IPermissionModuleRepository PermissionModule { get; private set; }
        public IRolePermissionRepository RolePermission { get; private set; }
        #endregion

        #region Log
        public ILogSystemRepository LogSystem { get; private set; }
        public ILogErrorRepository LogError { get; private set; }
        #endregion

        #region Migrations
        public IMigrationRepository Migrations { get; }
        #endregion

        #region Patient
        public IPatientRepository Patient { get; private set; }

        #endregion

        #region Visit
        public IVisitRepository Visit { get; private set; }
        #endregion

        public UnitOfWork(BoilerplateDBContext context)
        {
            _context = context;
            
            #region Patient
            Patient = new PatientRepository(_context);
            #endregion

            #region Visit
            Visit = new VisitRepository(_context);
            #endregion

            #region Role
            Roles = new RoleRepository(_context);
            RoleTranslations = new RoleTranslationRepository(_context);
            #endregion

            #region User
            Users = new UserRepository(_context);
            UserRoles = new UserRoleRepository(_context);
            #endregion

            Licences = new LicenceRepository(_context);
            TimeLogs = new TimeLogRepository(_context);

            #region Permissions
            PermissionModule = new PermissionModuleRepository(_context);
            RolePermission = new RolePermissionRepository(_context);
            #endregion            

            #region log
            LogError = new LogErrorRepository(_context);
            LogSystem = new LogSystemRepository(_context);
            #endregion

            #region Migrations
            Migrations = new MigrationRepository(_context);
            #endregion

        }
        public IDbContextTransaction Transaction()
        {
            return _context.Database.BeginTransaction();
        }

        public int Complete()
        {
            onBeforeSaving();
            return _context.SaveChanges();
        }
        public void ChangeTracker()
        {
            _context.ChangeTracker.Clear();
        }
        public bool HasProperty(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }
        protected virtual void onBeforeSaving()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    if (HasProperty(entry.Entity, "CreatedAt"))
                    {
                        entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                    }
                    if (HasProperty(entry.Entity, "CreatedBy"))
                    {
                        entry.Property("CreatedBy").CurrentValue = UserDetails.userId;
                    }
                    if (HasProperty(entry.Entity, "UpdatedAt"))
                    {
                        entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                    }
                    if (HasProperty(entry.Entity, "UpdatedBy"))
                    {
                        entry.Property("UpdatedBy").CurrentValue = UserDetails.userId;
                    }
                }
                if (entry.State == EntityState.Modified)
                {
                    if (HasProperty(entry.Entity, "CreatedAt"))
                    {
                        entry.Property("CreatedAt").IsModified = false;
                    }
                    if (HasProperty(entry.Entity, "CreatedBy"))
                    {
                        entry.Property("CreatedBy").IsModified = false;
                    }
                    if (HasProperty(entry.Entity, "UpdatedAt"))
                    {
                        entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                    }
                    if (HasProperty(entry.Entity, "UpdatedBy"))
                    {
                        entry.Property("UpdatedBy").CurrentValue = UserDetails.userId;
                    }
                }
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
