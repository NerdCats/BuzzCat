using BuzzCatBlind.Utilities;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BuzzCatBlind.Hubs
{
    [HubName("BuzzCat")]
    public class ServerHub : Hub<IClient>, IServerHub
    {
        private static ConcurrentDictionary<string, string> connections =
            new ConcurrentDictionary<string, string>();

        public void Talk(string message)
        {
            var name = Context.QueryString["Name"];
            Clients.All.show(name, message);
        }

        // Hide your identity
        public void TalkAs(string name, string message)
        {
            Clients.All.show(name, message);
        }

        public void TalkTo(string user, string message)
        {
            var name = Context.QueryString["Name"];
            string connectionId;
            if (!connections.TryGetValue(user.Trim(), out connectionId))
            {
                Console.WriteLine("User '{0}' not found!", user);
                Clients.Caller.notify("User '" + user + "' not found!");
            }
            else
            {
                Clients.Client(connectionId).show(name, message);
                Clients.Caller.show(name, message);
            }
        }

        // Hide your identity
        public void TalkToAs(string name, string user, string message)
        {
            string connectionId;
            if (!connections.TryGetValue(user, out connectionId))
            {
                Console.WriteLine("User '{0}' not found!", user);
                Clients.Caller.notify("User '" + user + "' not found!");
            }
            else
            {
                Clients.Client(connectionId).show(name, message);

                var caller = Context.QueryString["Name"];
                string talkAs = name + " as " + caller;
                Clients.Caller.show(talkAs, message);
            }
        }

        public IDictionary<string, string> ServerStatus()
        {
            return connections;
        }

        public override Task OnConnected()
        {
            var name = Context.QueryString["Name"];
            var connectionId = Context.ConnectionId;
            if (!connections.TryAdd(name, connectionId))
            {
                Console.WriteLine("Could not add user: {0}, {1}",
                    name, connectionId);
            }
            else
            {
                Console.WriteLine("Successfully added user: {0}, {1}",
                    name, connectionId);
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var name = Context.QueryString["Name"];
            string connectionId;
            if (!connections.TryRemove(name, out connectionId))
            {
                Console.WriteLine("Error removing user: {0}", name);
            }
            else
            {
                Console.WriteLine("Successfully removed user: {0}, {1}",
                    name, connectionId);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}
