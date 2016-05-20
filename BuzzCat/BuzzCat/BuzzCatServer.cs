namespace BuzzCat
{
    using Core.Lib.Base;
    using Core.Lib.Connection;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using System;

    [HubName("BuzzHub")]
    public class BuzzCatServer : Hub<IBuzzCatClient>, IBuzzCatServer
    {
        private static ConnectionMapping<string> connections = new ConnectionMapping<string>();

        public void Connect(StompMessage message)
        {
            throw new NotImplementedException();
        }

        public void Disconnect(StompMessage message)
        {
            throw new NotImplementedException();
        }

        public void Send(StompMessage message)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(StompMessage message)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(StompMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
