using OrganogramaApp.WebApp.App_Start;
using OrganogramaApp.WebApp.Config;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OrganogramaApp.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            InjecaoDependencia.Injetar();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            /// When using cookie-based session state, ASP.NET does not allocate storage for session data until the Session object is used. 
            /// As a result, a new session ID is generated for each page request until the session object is accessed. 
            /// If your application requires a static session ID for the entire session, 
            /// you can either implement the Session_Start method in the application's Global.asax file and store data in the Session object to fix the session ID, 
            /// or you can use code in another part of your application to explicitly store data in the Session object.
            base.Session["init"] = 0;
        }

        protected void Application_BeginRequest()
        {
            if (!Request.Url.Host.Contains("processoeletronico.es.gov.br"))
            //if (Request.IsLocal)
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }
    }
}
