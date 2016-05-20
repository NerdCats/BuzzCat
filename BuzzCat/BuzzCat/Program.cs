namespace BuzzCat
{
    using App.Settings;
    using Its.Configuration;
    using Microsoft.Owin.Hosting;
    using NLog;
    using System;

    public class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            logger.Info("*************************************");
            logger.Info("Starting BuzzCat Broadcast Server");
            logger.Info("*************************************");

            logger.Debug("Fetching AppSettings");
            AppSettings settings = Settings.Get<AppSettings>();

            StartOptions options = new StartOptions();

            if (settings.Urls != null)
            {
                logger.Debug("AppSettings urls are not empty, adding urls to the startup sequence");

                foreach (var url in settings.Urls)
                {
                    options.Urls.Add(url);
                }
            }
            else
            {
                logger.Debug("AppSettings urls empty, starting with localhost:1337 ");
                options.Urls.Add("http://localhost:1377");
            }

            using (WebApp.Start<Startup>(options))
            {
                foreach (var url in options.Urls)
                {
                    logger.Info("Server running at: {0}", url);
                }
                Console.ReadLine();
            }
        }
    }
}
