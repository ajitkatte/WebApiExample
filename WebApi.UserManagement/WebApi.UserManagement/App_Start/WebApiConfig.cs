using System;
using System.Configuration;
using System.Web.Http;
using Microsoft.Practices.Unity;
using WebApi.UserManagement.Logger;
using WebApi.UserManagement.Repository;
using WebApi.UserManagement.Unity;
using WebApi.UserManagement.Validation;

namespace WebApi.UserManagement
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            var repoType = ConfigurationManager.AppSettings["UserRepositoryType"];
            var loggerType = ConfigurationManager.AppSettings["LoggerType"];
            container.RegisterType(typeof(IUserRepository), Type.GetType(repoType), new HierarchicalLifetimeManager());
            container.RegisterType(typeof(ILogger), Type.GetType(loggerType), new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Filters.Add(new ValidationFilter());

            
        }
    }
}
