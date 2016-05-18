namespace BuzzCat
{
    using Microsoft.AspNet.SignalR;
    using Microsoft.Owin.Cors;
    using Owin;
    using System.Diagnostics;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage();

            // Detailed error messages for development.
            app.Properties["host.AppMode"] = "development";

            app.Map("/signalr", map =>
            {
                var config = new HubConfiguration
                {
                    // EnableDetailedErrors set to true will allow the clients
                    // to get detailed exceptions thrown in Hub methods.
                    // Good for Development, NO NO fro Production.
                    EnableDetailedErrors = true
                };

                map.UseCors(CorsOptions.AllowAll)
                    .RunSignalR(config);
            });

            GlobalHost.TraceManager.Switch.Level =
                SourceLevels.Information;
        }
    }
}
