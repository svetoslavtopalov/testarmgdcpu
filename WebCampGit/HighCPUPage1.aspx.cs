using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace demomvp
{
    public partial class HighCPUPage1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int percent = 75;
            if (Request.QueryString["percent"] != null)
            {
                int.TryParse(Request.QueryString["percent"].ToString(), out percent);
            }
            ForceCpuUsage(percent);
        }

        //https://stackoverflow.com/questions/2514544/simulate-steady-cpu-load-and-spikes
        void ForceCpuUsage(int percentage)
        {
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                (new Thread(() =>
                {
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    while (true)
                    {
                        // Make the loop go on for "percentage" milliseconds then sleep the 
                        // remaining percentage milliseconds. So 40% utilization means work 40ms and sleep 60ms
                        if (watch.ElapsedMilliseconds > percentage)
                        {
                            Thread.Sleep(100 - percentage);
                            watch.Reset();
                            watch.Start();
                        }
                    }
                })).Start();
            }

        }
    }
}