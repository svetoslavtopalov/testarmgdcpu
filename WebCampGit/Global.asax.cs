using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.SessionState;

namespace demomvp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            bool hangOnStartup = false;
            if (WebConfigurationManager.AppSettings["HANG_ON_STARTUP"] != null && bool.TryParse(WebConfigurationManager.AppSettings["HANG_ON_STARTUP"], out hangOnStartup))
            {
                if (hangOnStartup)
                {
                    Thread.Sleep((int)TimeSpan.FromMinutes(14).TotalMilliseconds);
                }
            }

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}