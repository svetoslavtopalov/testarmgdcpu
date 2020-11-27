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

            lblMessage.Text = $"Memory leaked at " + DateTime.Now.ToString();
        }
    }

    public class UserTrackingModel
    {
        const int BYTES_LEAK = 1024 * 1024 * 3;
        public DateTime PageVisited { get; set; }
        public string SessionId { get; set; }
        public byte[] EncryptedBlob { get; set; }

        public UserTrackingModel(string uniqueId)
        {
            SessionId = uniqueId;
            PageVisited = DateTime.Now;
            EncryptedBlob = new byte[BYTES_LEAK];
        }
    }
}