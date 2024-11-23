using Autofac;
using Boilerplate.Contracts.Helpers;
using Boilerplate.Contracts.Interfaces.Custom;
using Module = Autofac.Module;

namespace Boilerplate.Contracts
{
    public class ContractsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HolderOfDTO>().As<IHolderOfDTO>()
                .InstancePerLifetimeScope();
        }
    }
}
