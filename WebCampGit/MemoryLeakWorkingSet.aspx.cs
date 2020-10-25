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
            if (Application["StaticDictionary"] == null)
            {
                Application["StaticDictionary"] = new ConcurrentDictionary<string, List<UserTrackingModel>>();
            }
            var dict = Application["StaticDictionary"] as ConcurrentDictionary<string, List<UserTrackingModel>>;
            var userTrackingModel = new UserTrackingModel(Guid.NewGuid().ToString());

            if (dict.ContainsKey("StaticEntry"))
            {
                dict["StaticEntry"].Add(userTrackingModel);
            }
            else
            {
                dict.TryAdd("StaticEntry", new List<UserTrackingModel> { userTrackingModel });
            }

            foreach(var item in dict)
            {
                var userModels = item.Value;
                foreach(var m in userModels)
                {
                    foreach(var b in m.EncryptedBlob)
                    {
                        var c = b.ToString();
                    }
                }
            }
        }
    }
}