using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Transports;
using Newtonsoft.Json;
using System;
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
            Task<object> result = buzzCatProxy.Invoke<object>("Connect", new Object());
            result.Wait();
            Console.WriteLine(JsonConvert.SerializeObject(result.Result)); 

            Console.Read();
        }
    }
}
