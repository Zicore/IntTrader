using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntTrader.Web.Hubs;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(IntTrader.Web.Startup))]
namespace IntTrader.Web
{
    public class Startup
    {
        public Startup()
        {
            //AppDomain.CurrentDomain.Load(typeof(TickerHub).Assembly.FullName);
        }

        public void Configuration(IAppBuilder app)
        {
            IntTrader.Web.WebService.Initialize();

            app.MapSignalR();
            app.UseNancy();
        }
    }
}
