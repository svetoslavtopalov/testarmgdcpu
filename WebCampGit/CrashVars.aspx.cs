using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace demomvp
{
    public partial class CrashVars : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs eg)
        {
            foreach (DictionaryEntry e in System.Environment.GetEnvironmentVariables())
            {
                if(e.Key.ToString().ToUpper().Contains("CRASH"))
                {
                    Response.Write(e.Key + ":" + e.Value + "<br/>");
                }
                
            }
        }
    }
}