namespace BuzzCat
{
    using App.Constants;
    using Core.Lib.Base;
    using Core.Lib.Connection;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize(RequireOutgoing = false)]
    [HubName("BuzzHub")]
    public class BuzzCatServer : Hub<IBuzzCatClient>, IBuzzCatServer
    {
        private static ConnectionCache<string> connections = new ConnectionCache<string>();

        public Task Connect(StompMessage message)
        {
            throw new NotImplementedException();
        }

        public Task Disconnect(StompMessage message)
        {
            throw new NotImplementedException();
        }

        public Task Send(StompMessage message)
        {
            throw new NotImplementedException();
        }

        public Task Subscribe(StompMessage message)
        {
            throw new NotImplementedException();
        }

        public Task Unsubscribe(StompMessage message)
        {
            throw new NotImplementedException();
        }

        #region overrides
        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;
            connections.Add(name, Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            try
            {
                string name = Context.User.Identity.Name;
                connections.Remove(name, Context.ConnectionId);
                return base.OnDisconnected(stopCalled);
            }
            catch (Exception ex)
            {
                Clients.Caller.Error(new StompMessage()
                {
                    Body = new JObject(ex),
                    Command = CommandNames.ERROR,
                    Type = "Exception"
                });
                return base.OnDisconnected(stopCalled);
            }
        }

        public override Task OnReconnected()
        {
            string name = Context.User.Identity.Name;
            if (!connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                connections.Add(name, Context.ConnectionId);
            }
            return base.OnReconnected();
        }
        #endregion
    }
}
