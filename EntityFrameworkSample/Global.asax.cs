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
            var config = new HttpConfiguration();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<SampleDatabase>().As<ISampleDatabase>().InstancePerRequest();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
