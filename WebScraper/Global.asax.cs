using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using WebScraper.Controllers;
using WebScraper.Repository;
using WebScraper.Services;
using WebScraper.Services.Interface;
using WebScraper.Services.Services;

namespace WebScraper
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            RegisterTypes();
        }

        private void RegisterTypes()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SiteRepository>().As<ISiteRepository>();
            builder.RegisterType<GoogleSite>().As<ISEOFactory>();
            builder.RegisterType<SearchHelper>().As<ISearchHelper>();
            builder.RegisterType<ParseHelper>().As<IParseHelper>();
            builder.RegisterType<HomeController>().InstancePerRequest();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
