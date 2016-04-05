using System;
using Microsoft.Owin;
using Owin;
using System.Web.Http.Dependencies;
using Autofac;
using System.Web.Http;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web;
using System.Web.Http.OData.Extensions;
using Microsoft.Owin.Security.OAuth;
using MundoCompilado.MinhaMeta.Api.Providers;

[assembly: OwinStartup(typeof(MundoCompilado.MinhaMeta.Api.App_Start.Startup))]

namespace MundoCompilado.MinhaMeta.Api.App_Start
{
    public class Startup
    {
        public static IDependencyResolver Resolver { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            var config = new HttpConfiguration();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            builder.Register(c => new HttpContextWrapper(HttpContext.Current)).As<HttpContextBase>().InstancePerLifetimeScope();
            builder.RegisterAssemblyModules(AppDomain.CurrentDomain.GetAssemblies());
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            Resolver = config.DependencyResolver;

            config.AddODataQueryFilter();

            ConfigureOAuth(app);
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            GlobalConfiguration.Configuration.EnsureInitialized();
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthorizationServerProvider()
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
