using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Back_EndApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new TokenValidationHandler());
            // Configuration et services API Web

            // Itinéraires de l'API Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //forcer la sérialisation en json
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            //suppression de formater en XML
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
