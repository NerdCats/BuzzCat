using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Transports;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuzzCat.CS.Client.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var hubConnection = new HubConnection("http://localhost:8177/buzz");
            IHubProxy buzzCatProxy = hubConnection.CreateHubProxy("BuzzHub");
            buzzCatProxy.On("Error", (ex) =>
            {
                Console.WriteLine("Error Occured");
            });

            hubConnection.Start(new WebSocketTransport()).Wait();

            var message = new StompMessage()
            {
                Command = "Connect",
                Headers = new Dictionary<string, string>()
                {
                    { "content-type", "application/json;charset=utf-8" }
                },
                Type = "StompMessage",
                Body = JObject.Parse(
                    @"{
                            asset_id: '123456',
                            subscriber: 'Shaphil'
                      }"
                )
            };
            Task<object> result = buzzCatProxy.Invoke<object>("Connect", message);
            result.Wait();
            Console.WriteLine(JsonConvert.SerializeObject(result.Result));

            Console.Read();
        }
    }
}
