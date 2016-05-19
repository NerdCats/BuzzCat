namespace BuzzCat
{
    using Core.Lib.Base;
    using Microsoft.AspNet.SignalR;
    using System;

    public class BuzzCatServer : Hub<IBuzzCatClient>, IBuzzCatServer
    {
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
