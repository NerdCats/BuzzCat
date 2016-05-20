using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System.Diagnostics;

[assembly: OwinStartup(typeof(BuzzCat.Startup))]
namespace BuzzCat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage();
            app.Properties["host.AppMode"] = "development";

            app.UseCors(CorsOptions.AllowAll);

            app.MapSignalR("/buzz", new HubConfiguration()
            {
                EnableDetailedErrors = true
            });

            GlobalHost.TraceManager.Switch.Level = SourceLevels.Information;
        }
    }
}
