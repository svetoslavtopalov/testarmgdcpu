using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace demomvp
{
    class Test
    {
        public Task ShowAsync()
        {
            return Task.Run(() => {
                Task.Delay(2000);
                throw new Exception("My Own Exception");
            });
        }
        public async void Call()
        {
            await ShowAsync();
        }
    }

    public partial class NestedExceptionCrash : System.Web.UI.Page
    {
        private void RaiseEvent()
        {
            RaiseEventVoidAsync();
        }

        private void RaiseEventVoidAsync()
        {
            throw new Exception("Error!");
        }

        protected  void Page_Load(object sender, EventArgs e)
        {
            //var test = new Test();
            //MethodInfo mi = typeof(Test).GetMethod("Call");
            //mi.Invoke(test, null);

            ThreadPool.QueueUserWorkItem(new WaitCallback(CrashMe));

            //RaiseEvent();
            //await Task.Run(async()=> await LoadSomeData());
            //Test t = new Test();
            //try
            //{
            //    t.Call();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }

        void CrashMe(object obj)
        {
            try
            {
                RaiseEventVoidAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("The CrashMe function crashed with an inner exception", ex);
            }
        }

        public async Task LoadSomeData()
        {
            await AnotherAsyncMethod();
        }

        private async Task AnotherAsyncMethod()
        {
            HttpClient client = new HttpClient();
            await client.GetStringAsync("http://somenonexistingdomainblabla.com");
        }
    }
}