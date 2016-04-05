using Autofac;
using System.Linq;

namespace MundoCompilado.MinhaMeta.Domain
{
    internal class Config : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            var types = ThisAssembly.GetTypes().ToArray();
            builder.RegisterTypes(types).AsImplementedInterfaces().AsSelf();
            builder.RegisterGeneric(typeof(Domain<>)).As(typeof(IDomain<>)).AsSelf();
        }
    }
}
