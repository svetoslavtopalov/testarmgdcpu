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
            if (bool.TryParse(WebConfigurationManager.AppSettings["FAIL_ON_STARTUP"], out bool failOnStartup))
            {
                if (failOnStartup 
                    && !HttpContext.Current.Request.Path.ToLower().Contains("fusionlog"))
                {
                    // Default to 500.31
                    int statusCode = 500;
                    int subStatusCode = 31;

                    if (WebConfigurationManager.AppSettings["STARTUP_ERROR_CODE"] != null)
                    {
                        string failureCode = WebConfigurationManager.AppSettings["STARTUP_ERROR_CODE"];
                        var failureCodeArray = failureCode.Split('.');
                        if (failureCodeArray.Length > 1)
                        {
                            if (int.TryParse(failureCodeArray[1], out int substatus))
                            {
                                subStatusCode = substatus;
                            }
                        }
                        if (int.TryParse(failureCodeArray[0], out int status))
                        {
                            statusCode = status;
                        }
                    }

                    HttpContext.Current.Response.StatusCode = statusCode;
                    HttpContext.Current.Response.SubStatusCode = subStatusCode;
                    var httpApplication = sender as HttpApplication;
                    httpApplication.CompleteRequest();
                }
            }
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