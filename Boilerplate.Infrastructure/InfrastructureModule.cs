using Autofac;
using Boilerplate.Core.IServices.Custom;
using Boilerplate.Infrastructure.Services.Custom;
using Module = Autofac.Module;

namespace Boilerplate.Infrastructure
{
    public class InfrastructureModule : Module
    {
        // IOC Container Methods
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>()
                .InstancePerLifetimeScope();

        }
    }
}
