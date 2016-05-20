using BuzzCat.App.Settings;
using Its.Configuration;
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
            var enableDetailedErrors = false;  
                
            app.Properties["host.AppMode"] = Settings.Get<AppSettings>().Env;
            app.UseCors(CorsOptions.AllowAll);

            if (app.Properties["host.AppMode"].ToString() == "development")
            {
                app.UseErrorPage();
                enableDetailedErrors = true;
            }

            app.MapSignalR("/buzz", new HubConfiguration()
            {
                EnableDetailedErrors = enableDetailedErrors
            });

            GlobalHost.TraceManager.Switch.Level = SourceLevels.Information;
        }
    }
}
