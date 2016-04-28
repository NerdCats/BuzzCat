using BuzzCatBlind.Utilities;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuzzCatBlind.Hubs
{
    [HubName("BuzzCat")]
    public class ServerHub : Hub<IClient>, IServerHub
    {
        private static ConcurrentDictionary<string, HashSet<string>> connections =
            new ConcurrentDictionary<string, HashSet<string>>();

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
            HashSet<string> connectionId;
            if (!connections.TryGetValue(user.Trim(), out connectionId))
            {
                Console.WriteLine("User '{0}' not found!", user);
                Clients.Caller.notify("User '" + user + "' not found!");
            }
            else
            {
                foreach (var conn in connectionId)
                {
                    Clients.Client(conn).show(name, message);
                }
                Clients.Caller.show(name, message);
            }
        }

        // Hide your identity
        public void TalkToAs(string name, string user, string message)
        {
            HashSet<string> connectionId;
            if (!connections.TryGetValue(user, out connectionId))
            {
                Console.WriteLine("User '{0}' not found!", user);
                Clients.Caller.notify("User '" + user + "' not found!");
            }
            else
            {
                foreach (var conn in connectionId)
                {
                    Clients.Client(conn).show(name, message);
                }

                var caller = Context.QueryString["Name"];
                string talkAs = name + " as " + caller;
                Clients.Caller.show(talkAs, message);
            }
        }

        public IDictionary<string, HashSet<string>> ServerStatus()
        {
            return connections;
        }

        public override Task OnConnected()
        {
            var name = Context.QueryString["Name"];
            var connectionId = Context.ConnectionId;

            HashSet<string> connSet;
            if (!connections.TryGetValue(name, out connSet))
            {
                connSet = new HashSet<string>();
                lock (connSet)
                {
                    connSet.Add(connectionId);
                }
            }
            else
            {
                lock (connSet)
                {
                    connSet.Add(connectionId);
                }
            }
            if (!connections.TryAdd(name, connSet))
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
            string connectionId = Context.ConnectionId;

            HashSet<string> connSet;
            if (!connections.TryGetValue(name, out connSet))
            {
                Console.WriteLine("Error! user: {0}, not found!", name);
            }
            else
            {
                if (!connSet.Contains(connectionId))
                {
                    Console.WriteLine("Error! Connection: {0} was not alive.");
                }
                else
                {
                    lock (connSet)
                    {
                        connSet.Remove(connectionId);
                    }
                    connSet.Remove(connectionId);

                    if (connSet.Count == 0)
                    {
                        connections.TryRemove(name, out connSet);
                        Console.WriteLine("Removed user: {0}", name);
                    }
                    Console.WriteLine("Removed user: {0}, {1}",
                        name, connectionId);
                }
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}
