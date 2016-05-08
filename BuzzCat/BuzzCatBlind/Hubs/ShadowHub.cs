using BuzzCatBlind.Utilities;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace BuzzCatBlind.Hubs
{
    [HubName("ShadowHub")]
    public class ShadowHub : Hub<IShadowClient>, IShadowHub
    {
        public void SendLocation(object asset)
        {
            Clients.Others.getLocation(asset);
        }
    }
}
