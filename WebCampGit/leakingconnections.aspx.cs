using SharedComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace demomvp
{
    public partial class leakingconnections : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            (new WebClient()).OpenRead("http://www.bing.com/");

            lblMessage.Text = ConnectionUtilities.PrintConnections();
        }
    }
}