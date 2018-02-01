using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AspNetWebApi.EfCRUD
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; //Curcilar referance'ı durdurur

            //config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All; //Curcilar referance'ı durdurur fakat json pars yapmak gerekiyor

            /*Model sınıflar üzerinde properteylere [JsonIgnor] kullanılabilir
             * xml için [IgnoreDataMember]
             * */

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
