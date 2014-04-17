using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Lisa.Zuma.BlueJay.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Filters.Add(new AuthorizeAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "NoteApi",
                routeTemplate: "api/dossier/{dossierId}/notes/{id}",
                defaults: new { controller = "note", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "NoteMediaApi",
                routeTemplate: "api/dossier/{dossierId}/notes/{noteId}/media/{id}",
                defaults: new { controller = "media", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DossierDetailApi",
                routeTemplate: "api/dossier/{dossierId}/detail/{id}",
                defaults: new { controller = "dossierDetail", id = RouteParameter.Optional }
            );

            RegisterSerialization(config);
        }

        public static void RegisterSerialization(HttpConfiguration config)
        {
            var jsonFormatter = config.Formatters.JsonFormatter;
            
            // Get serializer and perform configuration
            var serializerSettings = jsonFormatter.SerializerSettings;
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            serializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

            // Remove XML formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
