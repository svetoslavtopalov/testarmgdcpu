using System;
using System.Text.RegularExpressions;


namespace demomvp
{
    public partial class HighCPUPage2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dtStart = DateTime.Now;
            Regex regex = new Regex(@"(^(?=^.{1,254}$)[a-z0-9!#$%'*/=?^_`{|}~\-&.]+(?:\.[a-z0-9!#$%'*/=?^_`{|}~\-&.]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9]))+(?:\.[a-z]{2,6})?(\.[a-z]{2,6})$)|(^[a-zA-Z0-9\!\#\$\%\&\'\*\-\/\=\?\^_\`\{\|\}\~]+(\.[a-zA-Z0-9\!\#\$\%\&\'\*\-\/\=\?\^_\`\{\|\}\~]+)*@[a-zA-Z0-9]([a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(\.[a-zA-Z0-9]([a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*\.[a-zA-Z]{2,6}$)|(^(?=^.{1,254}$)(("".+?"")|([0-9a-zA-Z](((\.(?!\.))|([-!#\$%&'\*/=\?\^`\{\}\|~\w]))*[0-9a-zA-Z])*))@(([0-9a-zA-Z][\.])|([0-9a-zA-Z][-A-Z0-9a-z]*[0-9a-zA-Z]\.){1,63})+[a-zA-Z0-9]{2,6}$)|(^[0-9]{1,10}$)");
            Response.Write(regex.IsMatch("someincorrectemailaddress@ymydomain.c"));
            Response.Write($"<br/> Took { DateTime.Now.Subtract(dtStart).TotalSeconds }seconds");

        }
    }
}
