namespace BuzzCatBlind.Lib
{
    using System;
    using Base;
    using Message;
    using Microsoft.AspNet.SignalR;
    using Newtonsoft.Json;
    using Microsoft.AspNet.SignalR.Hubs;

    [HubName("TestCat")]
    public class BuzzCatServer : Hub<IStompClient>, IStompHub
    {
        public void Connect(IStompMessage message)
        {
            if (message.Command == "ECHO")
            {
                var body = message.Body["Message"].ToString();
                Clients.Caller.Message(new EchoMessage("ECHO", "Howdy!"));
            }
            else
            {
                Clients.Caller.Message(new EchoMessage("ECHO", "Um... Who da fuck are you?"));
            }
        }

        public void Disconnect(IStompMessage message)
        {
            throw new NotImplementedException();
        }

        public void Send(IStompMessage message)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(IStompMessage message)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(IStompMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
