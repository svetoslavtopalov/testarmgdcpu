using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace demomvp
{
    public partial class FailExit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["exception"] !=null)
            {
                Environment.FailFast("calling Environment.FailFast", new ApplicationException("Some custom exception"));
            }
            else
            {
                Environment.FailFast("calling Environment.FailFast");
            }
        }
    }
}