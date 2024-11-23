using Autofac;
using Boilerplate.Shared.Helpers;
using Boilerplate.Shared.Interfaces;
using Module = Autofac.Module;

namespace Boilerplate.Shared
{
    public class SharedModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Culture>().As<ICulture>()
                .InstancePerLifetimeScope();
        }
    }
}
