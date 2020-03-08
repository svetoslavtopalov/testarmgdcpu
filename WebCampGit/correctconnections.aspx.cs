using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace demomvp
{
    public partial class correctconnections : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            DateTime now = DateTime.Now;
            WebClient webClient = new WebClient();
            HttpResponse response = base.Response;
            object length = webClient.DownloadString("http://www.bing.com/").Length;
            double totalMilliseconds = DateTime.Now.Subtract(now).TotalMilliseconds;
            response.Write(string.Format("Bing Returned {0} bytes of content in {1} ms", length, totalMilliseconds.ToString()));
            lblMessage.Text = ConnectionUtilities.PrintConnections();

        }
    }
}