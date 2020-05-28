using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace demomvp
{
    public partial class FusionLog : System.Web.UI.Page
    {
        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public static extern int RegOpenKeyEx(
           UIntPtr hKey,
           string subKey,
           int ulOptions,
           int samDesired,
           out UIntPtr hkResult);

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern uint RegQueryValueEx(
            UIntPtr hKey,
            string lpValueName,
            int lpReserved,
            ref RegistryValueKind lpType,
            IntPtr lpData,
            ref int lpcbData);

        public static UIntPtr HKEY_LOCAL_MACHINE = new UIntPtr(0x80000002u);
        static readonly int KEY_QUERY_VALUE = 0x0001;

        protected void Page_Load(object sender, EventArgs e)
        {
            var output = new StringBuilder();
            output.AppendLine($"Page Load started");
            int size = 1024;
            StringBuilder keyBuffer = new StringBuilder();
            RegistryValueKind type = RegistryValueKind.Unknown;
            UIntPtr hKey = UIntPtr.Zero;
            IntPtr pResult = IntPtr.Zero;

            output.AppendLine($"Calling RegOpenKeyEx for Fusion");

            var dwRet = RegOpenKeyEx(HKEY_LOCAL_MACHINE, "Software\\Microsoft\\Fusion", 0, KEY_QUERY_VALUE, out hKey);

            output.AppendLine($"RegOpenKeyEx returned {dwRet}");

            if (dwRet == 0)
            {
                output.AppendLine($"going to call RegQueryValueEx");
                // Get the size of buffer we will need
                uint retVal = RegQueryValueEx(hKey, "EnableLog", 0, ref type, IntPtr.Zero, ref size);

                output.AppendLine($"RegQueryValueEx retVal is {retVal} and size is {size}");

                if (size == 0)
                {
                    return;
                }

                pResult = Marshal.AllocHGlobal(size);

                output.AppendLine($"Calling RegQueryValueEx SECOND time");

                retVal = RegQueryValueEx(hKey, "EnableLog", 0, ref type, pResult, ref size);

                output.AppendLine($"RegQueryValueEx retval is {retVal} and size is {size}");

                if (retVal != 0)
                {
                    output.AppendLine($"Error querying value '{Marshal.GetLastWin32Error()} with return value = { retVal}");
                }
                else
                {
                    switch (type)
                    {
                        case RegistryValueKind.String:
                            output.AppendLine("Value = " + Marshal.PtrToStringAnsi(pResult));
                            break;
                        case RegistryValueKind.DWord:
                            output.AppendLine("Value = " + Marshal.ReadInt32(pResult).ToString());
                            break;
                        case RegistryValueKind.QWord:
                            output.AppendLine("Value = " +  Marshal.ReadInt64(pResult).ToString());
                            break;
                    }
                }

            }

            lblMessage.Text = output.ToString().Replace(Environment.NewLine, "<br/>"); ;
        }
    }
}