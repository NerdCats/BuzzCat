using BuzzCat.App;
using BuzzCat.App.Settings;
using Its.Configuration;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System.Diagnostics;
using System;
using Microsoft.Owin.Security.OAuth;

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

            ConfigureAuth(app);

            GlobalHost.TraceManager.Switch.Level = SourceLevels.Information;
            GlobalHost.HubPipeline.AddModule(new LoggingPipelineModule());
            app.MapSignalR("/buzz", new HubConfiguration()
            {
                EnableDetailedErrors = enableDetailedErrors
            });
        }

        private void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            { 
            } );
        }
    }
}
