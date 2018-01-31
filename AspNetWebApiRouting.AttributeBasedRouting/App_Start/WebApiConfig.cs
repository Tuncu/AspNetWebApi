using AspNetWebApiRouting.AttributeBasedRouting.Constraints;
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
            var constraintResolver = new System.Web.Http.Routing.DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("lastletter", typeof(LastLetter));


            // Web API routes
            config.MapHttpAttributeRoutes(constraintResolver);//Eski web api yapılarda bu tanım gelmeyebilir web api kullanmak için web api kullanmak lazım.//Custom constraint var ise eklenmeldir.

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
