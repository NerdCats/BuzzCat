using Microsoft.Owin.Hosting;
using System;

namespace BuzzCatBlind
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:8080";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Server running at: {0}", url);
                Console.ReadLine();
            }
        }
    }
}
