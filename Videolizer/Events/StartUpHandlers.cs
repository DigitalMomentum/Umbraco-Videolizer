using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core;

namespace Videolizer.Events
{
    public class StartUpHandlers : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            RouteTable.Routes.MapRoute(
               name: "VideolizerOAuthProcess",
               url: "umbraco/backoffice/Plugins/Videolizer/Controllers/{controller}/{action}/{id}",
               defaults: new { controller = "OauthYT", action = "Index", id = UrlParameter.Optional }
           );
        }
    }
}