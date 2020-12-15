using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace demomvp
{
    public partial class CrashVars : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs eg)
        {
            var builder = new StringBuilder();
            foreach (DictionaryEntry e in System.Environment.GetEnvironmentVariables())
            {
                if(e.Key.ToString().ToUpper().Contains("CRASH"))
                {
                    builder.AppendLine(e.Key + ":" + e.Value + "<br/>");
                }
            }
            lblMessage.Text = builder.ToString();
        }
    }
}