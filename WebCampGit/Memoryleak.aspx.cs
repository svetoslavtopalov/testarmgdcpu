using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace demomvp
{
    public partial class Memoryleak : System.Web.UI.Page
    {
        const int BYTES_LEAK = 1024 * 1024 * 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Application["StaticDictionary"] == null)
            {
                Application["StaticDictionary"] = new ConcurrentDictionary<string, List<byte[]>>();
            }

            var dict = Application["StaticDictionary"] as ConcurrentDictionary<string, List<byte[]>>;

            var byteArray = new byte[BYTES_LEAK];
            
            if (dict.ContainsKey("StaticEntry"))
            {
                dict["StaticEntry"].Add(byteArray);
            }
            else
            {
                dict.TryAdd("StaticEntry", new List<byte[]> { byteArray });
            }

            lblMessage.Text = $"Memory {BYTES_LEAK / 1024} Kbytes leaked at " + DateTime.Now.ToString();
        }
    }
}