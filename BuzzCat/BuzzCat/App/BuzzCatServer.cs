namespace BuzzCat
{
    using App.Constants;
    using Core.Lib.Base;
    using Core.Lib.Connection;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using Newtonsoft.Json.Linq;
    using NLog;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    //[Authorize(RequireOutgoing = false)]
    [HubName("BuzzHub")]
    public class BuzzCatServer : Hub<IBuzzCatClient>, IBuzzCatServer
    {
        private static ConnectionCache<string> connections = new ConnectionCache<string>();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public StompMessage Connect(StompMessage message)
        {
            // The reason we are sending back just a connected frame is because of:
            // 1. Signalr has its own conenct and disconnection protocol that handles authorization anyway
            return new StompMessage()
            {
                Command = CommandNames.CONNECTED
            };
        }

        public Task Send(StompMessage message)
        {
            throw new NotImplementedException();
        }

        public Task Subscribe(StompMessage message)
        {
            if (message.Command.ToLower() != "subscribe")
            {
                logger.Warn("Wrong command type {0} in message", message.Command);
                this.Clients.Caller.Error(new StompMessage()
                {
                    Command = CommandNames.ERROR,
                    Body = JObject.FromObject(new NotSupportedException("Command type not supported for this method"))
                });
            }

            //TODO: Need to subscribe to a feed or something here 
            return this.Clients.Caller.Reciept(
                new StompMessage()
                {
                    Command = CommandNames.RECIEPT,
                    Body = JObject.FromObject(string.Format("temp-id = {0}", 12345))
                });
        }

        public Task Unsubscribe(StompMessage message)
        {
            throw new NotImplementedException();
        }

        #region overrides
        public override Task OnConnected()
        {
            try
            {
                string name = Context.User.Identity.Name;
                connections.Add(name, Context.ConnectionId);
            }
            catch (Exception ex)
            {
                Clients.Caller.Error(new StompMessage()
                {
                    Body = JObject.FromObject(ex),
                    Command = CommandNames.ERROR,
                    Type = "Exception"
                });
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            try
            {
                string name = Context.User.Identity.Name;
                connections.Remove(name, Context.ConnectionId);
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "Exception on disconnection");
            }
            return base.OnDisconnected(stopCalled);
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
