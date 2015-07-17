using Autofac;
using Autofac.Integration.WebApi;
using EntityFrameworkSample.DataAccessLayer;
using EntityFrameworkSample.DataAccessLayer.EntityFramework;
using System.Reflection;
using System.Web.Http;

namespace EntityFrameworkSample
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            this.RegisterServices();
        }

        private void RegisterServices()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SampleDatabase>().As<ISampleDatabase>().InstancePerRequest();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}