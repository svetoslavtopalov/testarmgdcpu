using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace demomvp
{
    public partial class MemoryCounters : System.Web.UI.Page
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentProcess();

        [StructLayout(LayoutKind.Sequential, Size = 40)]
        private struct PROCESS_MEMORY_COUNTERS_EX
        {
            public uint cb;             // The size of the structure, in bytes (DWORD).
            public uint PageFaultCount;         // The number of page faults (DWORD).
            public uint PeakWorkingSetSize;     // The peak working set size, in bytes (SIZE_T).
            public uint WorkingSetSize;         // The current working set size, in bytes (SIZE_T).
            public uint QuotaPeakPagedPoolUsage;    // The peak paged pool usage, in bytes (SIZE_T).
            public uint QuotaPagedPoolUsage;    // The current paged pool usage, in bytes (SIZE_T).
            public uint QuotaPeakNonPagedPoolUsage; // The peak nonpaged pool usage, in bytes (SIZE_T).
            public uint QuotaNonPagedPoolUsage;     // The current nonpaged pool usage, in bytes (SIZE_T).
            public uint PagefileUsage;          // The Commit Charge value in bytes for this process (SIZE_T). Commit Charge is the total amount of memory that the memory manager has committed for a running process.
            public uint PeakPagefileUsage;      // The peak value in bytes of the Commit Charge during the lifetime of this process (SIZE_T).
            public uint PrivateUsage;
        }

        [StructLayout(LayoutKind.Sequential, Size = 72)]
        private struct PROCESS_MEMORY_COUNTERS_EX_x64
        {
            public uint cb;
            public uint PageFaultCount;
            public UInt64 PeakWorkingSetSize;
            public UInt64 WorkingSetSize;
            public UInt64 QuotaPeakPagedPoolUsage;
            public UInt64 QuotaPagedPoolUsage;
            public UInt64 QuotaPeakNonPagedPoolUsage;
            public UInt64 QuotaNonPagedPoolUsage;
            public UInt64 PagefileUsage;
            public UInt64 PeakPagefileUsage;
            public UInt64 PrivateUsage;
        }

        [DllImport("Kernel32.dll", EntryPoint = "RtlZeroMemory", SetLastError = false)]
        static extern void ZeroMemory(IntPtr dest, IntPtr size);

        [DllImport("psapi.dll", SetLastError = true)]
        static extern bool GetProcessMemoryInfo(IntPtr hProcess, out PROCESS_MEMORY_COUNTERS_EX counters, uint size);

        [DllImport("psapi.dll", SetLastError = true)]
        static extern bool GetProcessMemoryInfo(IntPtr hProcess, out PROCESS_MEMORY_COUNTERS_EX_x64 counters, uint size);


        protected void Page_Load(object sender, EventArgs e)
        {
            if (IntPtr.Size == 4)
            {
                // 32-bit
                IntPtr currentProcessHandle = GetCurrentProcess();
                PROCESS_MEMORY_COUNTERS_EX memoryCounters;

                memoryCounters.cb = (uint)Marshal.SizeOf(typeof(PROCESS_MEMORY_COUNTERS_EX));
                if (GetProcessMemoryInfo(currentProcessHandle, out memoryCounters, memoryCounters.cb))
                {
                    lblMessage.Text = "memoryCounters.PrivateUsage in MB = " + memoryCounters.PrivateUsage / (1024 * 1024);
                }
                else
                {
                    throw new Exception("GetProcessMemoryInfo returned false. Error Code is " +
          Marshal.GetLastWin32Error());
                }
            }
            else if (IntPtr.Size == 8)
            {
                // 64-bit
                IntPtr currentProcessHandle = GetCurrentProcess();
                PROCESS_MEMORY_COUNTERS_EX_x64 memoryCounters;

                memoryCounters.cb = (uint)Marshal.SizeOf(typeof(PROCESS_MEMORY_COUNTERS_EX_x64));
                if (GetProcessMemoryInfo(currentProcessHandle, out memoryCounters, memoryCounters.cb))
                {
                    lblMessage.Text = "memoryCounters.PrivateUsage in MB = " + memoryCounters.PrivateUsage / (1024 * 1024);
                }
                else
                {
                    throw new Exception("GetProcessMemoryInfo returned false. Error Code is " +
          Marshal.GetLastWin32Error());
                }
            }
            
        }
    }
}