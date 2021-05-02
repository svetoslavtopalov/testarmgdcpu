using SharedComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace demomvp
{
    public partial class YSOD : System.Web.UI.Page
    {
        // This page works locally but throws an exception when you 
        // browse this in Azure App Service
        protected void Page_Load(object sender, EventArgs e)
        {
            Utilities.GetData();
            throw new ApplicationException("An exception occurred");
            message.Text = "Page Loaded Successfully";
        }
    }
}