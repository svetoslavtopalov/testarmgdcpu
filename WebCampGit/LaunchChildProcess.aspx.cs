using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace demomvp
{
    public partial class LaunchChildProcess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Process.Start("tcpping.exe", "127.0.0.1 -n 120");
            lblMessage.Text = $"Launched child process tcpping at {DateTime.UtcNow} UTC";
        }
    }
}