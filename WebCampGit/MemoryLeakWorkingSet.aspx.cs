using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace demomvp
{
    public partial class MemoryLeakWorkingSet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Application["StaticDictionaryWorkingSet"] == null)
            {
                Application["StaticDictionaryWorkingSet"] = new List<List<byte[]>>();
            }
            var dict = Application["StaticDictionaryWorkingSet"] as List<List<byte[]>>;

            var rnd = new Random();
            var list = new List<byte[]>();
            int i = 0;
            while (i < 16100)
            {
                i++;
                byte[] b = new byte[1024];
                b[rnd.Next(0, b.Length)] = byte.MaxValue;
                list.Add(b);
            }

            dict.Add(list);
        }
    }
}