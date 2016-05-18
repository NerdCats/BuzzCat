using BlindCat.Models;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;

namespace BlindCat
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> queryString = new Dictionary<string, string>();
            queryString.Add("Name", "Shaphil");

            string url = "http://localhost:8080";
            var hubConnection = new HubConnection(url, queryString);

            string hubName = "TestCat";
            IHubProxy hubProxy = hubConnection.CreateHubProxy(hubName);

            hubProxy.On<IStompMessage>("message", (data) => {
                Console.WriteLine(data.Body);
            });

            

            //hubProxy.On<AssetPayload>("getLocation", x =>
            //{
            //    Console.WriteLine(x.AssetId);
            //    Console.WriteLine(x.Name);
            //    x.Point.coordinates.ForEach(Console.WriteLine);
            //});

            hubConnection.Start().Wait();

            var input = new EchoMessage("ECHO");

            while (true)
            {
                hubProxy.Invoke("connect", input);
            }
        }
    }
}
