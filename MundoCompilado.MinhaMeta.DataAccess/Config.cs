using Autofac;
using MundoCompilado.MinhaMeta.DataAccess.Model;
using System.Linq;

namespace MundoCompilado.MinhaMeta.DataAccess
{
    internal class Config : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<Context>().InstancePerLifetimeScope();
            var types = ThisAssembly.GetTypes().ToArray();
            builder.RegisterTypes(types).AsImplementedInterfaces().AsSelf();
            builder.RegisterGeneric(typeof(Data<>)).As(typeof(IData<>)).AsSelf();
        }
    }
}
