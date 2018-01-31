using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AspNetWebApiRouting.AttributeBasedRouting
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();//Eski web api yapılarda bu tanım gelmeyebilir web api kullanmak için web api kullanmak lazım.

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
