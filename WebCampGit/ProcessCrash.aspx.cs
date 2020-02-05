using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace demomvp
{
    public partial class ProcessCrash : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var ts = new ThreadStart(MyCrashingFunction);
            var t = new Thread(ts);
            t.Start();

            Response.Write("Thread Started");
        }

        void MyCrashingFunction()
        {
            throw new ApplicationException("Catch me if you can!");
        }
    }
}