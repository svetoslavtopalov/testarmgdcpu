using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace demomvp
{
    public partial class StackOverflowManagedException : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var ex = new Example();
            ex.Execute();
        }
    }

    public class Example
    {
        private const int MAX_RECURSIVE_CALLS = Int32.MaxValue;
        static int ctr = 0;


        public void Execute()
        {
            ctr++;

            if (ctr <= MAX_RECURSIVE_CALLS)
                Execute();

            ctr--;
        }
    }
}