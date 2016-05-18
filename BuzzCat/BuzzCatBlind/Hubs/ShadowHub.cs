namespace BuzzCat.Hubs
{
    using Lib.Base;
    using Utilities;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using System.Collections.Generic;

    [HubName("ShadowHub")]
    public class ShadowHub : Hub<IShadowClient>, IShadowHub 
    {
        private static ConnectionMapping<string> connections =
            new ConnectionMapping<string>();
        
        public void SendLocation(object asset)
        {
            Clients.Others.getLocation(asset);
        }

        public void SendLocationTo(string to, object asset)
        {
            IList<string> clientsList = new List<string>(connections.GetConnections(to));
            Clients.Clients(clientsList).getLocation(asset);
        }

        public void SendLocationToMultiple(string[] to, object asset)
        {
            IList<string> clientsList;
            foreach (var client in to)
            {
                clientsList = new List<string>(connections.GetConnections(client));
                Clients.Clients(clientsList).getLocation(asset);
            }
        }
    }
}
