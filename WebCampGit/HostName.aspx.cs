using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace demomvp
{
    public partial class HostName : System.Web.UI.Page
    {
        private const string DefaultHostNameSandboxProperty = "SANDBOX_FUNCTION_RESOURCE_ID";

        [DllImport("picohelper.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public extern static bool GetSandboxProperty(string propertyId,
           byte[] valueBuffer,
           int valueBufferLength,
           uint flags,
           ref int copiedBytes);

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = GetDefaultHostName();
        }

        private string GetDefaultHostName()
        {
            if (IsSandBoxAvailable())
            {
                int copiedBytes = 0;
                byte[] valueBuffer = new byte[4096];
                if (GetSandboxProperty(DefaultHostNameSandboxProperty, valueBuffer, valueBuffer.Length, 0, ref copiedBytes))
                {
                    string defaultHostName = Encoding.Unicode.GetString(valueBuffer, 0, copiedBytes);
                    if (!string.IsNullOrWhiteSpace(defaultHostName))
                    {
                        return defaultHostName;
                    }
                }
            }

            return string.Empty;
        }

        private static bool IsSandBoxAvailable()
        {
            return File.Exists(Environment.ExpandEnvironmentVariables(@"%windir%\system32\picohelper.dll"));
        }
    }
}