using Autofac;
using Boilerplate.Application.Services;
using Boilerplate.Application.Services.Agency;
using Boilerplate.Application.Services.Auth;
using Boilerplate.Application.Services.LicenceService;
using Boilerplate.Application.Services.Lookups;
using Boilerplate.Application.Services.Migrations;
using Boilerplate.Application.Services.PasswordGeneration;
using Boilerplate.Application.Services.Patients;
using Boilerplate.Application.Services.ThumbnailService;
using Boilerplate.Application.Services.Visits;
using Boilerplate.Contracts.Interfaces.Services.Patient;
using Boilerplate.Contracts.Interfaces.Services.Visit;
using Boilerplate.Contracts.IServices.Services;
using Boilerplate.Contracts.IServices.Services.Auth;
using Boilerplate.Contracts.IServices.Services.EncryptionAndDecryption;
using Boilerplate.Contracts.IServices.Services.Lookups;
using Boilerplate.Contracts.IServices.Services.Migrations;
using Boilerplate.Contracts.IServices.Services.PasswordGeneration;
using Boilerplate.Contracts.IServices.Services.Permissions;
using Boilerplate.Contracts.IServices.Services.ThumbnailService;
using Boilerplate.Contracts.models.ThumbnailModel;

namespace Boilerplate.Application
{
    public class ApplicationModule : Module
    {
        // IOC Container Method
        protected override void Load(ContainerBuilder builder)
        {

            #region Patient
            builder.RegisterType<PatientService>().As<IPatientService>()
                   .InstancePerLifetimeScope();
            #endregion

            #region Visit
            builder.RegisterType<VisitService>().As<IVisitService>()
                   .InstancePerLifetimeScope();
            #endregion

            #region Auth, Role, User

            builder.RegisterType<AuthService>().As<IAuthService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RoleService>().As<IRoleService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserService>().As<IUserService>()
                .InstancePerLifetimeScope();

            #endregion

            #region Permissions and Licence

            builder.RegisterType<PermissionService>().As<IPermissionService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<PermissionModuleService>().As<IPermissionModuleService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<LicenceService>().As<ILicenceService>()
                .InstancePerLifetimeScope();

            #endregion

            #region PasswordGeneration

            builder.RegisterType<PasswordGenerationService>().As<IPasswordGenerationService>().SingleInstance();

            #endregion

            #region Lookups

            builder.RegisterType<LookupService>().As<ILookupService>()
                .InstancePerLifetimeScope();

            #endregion

            #region Migrations

            builder.RegisterType<MigrationService>().As<IMigrationService>()
                .InstancePerLifetimeScope();

            #endregion

            #region Encryption And Decryption
            builder.RegisterType<EncryptionAndDecryptionService>().As<IEncryptionAndDecryptionService>()
                .InstancePerLifetimeScope();
            #endregion

            #region ThumbnailService

            builder.RegisterType<ImageThumbnailService>().As<IThumbnailService<ImageThumbnailData>>()
                .InstancePerLifetimeScope();
            builder.RegisterType<PDFThumbnailService>().As<IThumbnailService<PdfThumbnailData>>()
                .InstancePerLifetimeScope();

            #endregion
        }
    }
}