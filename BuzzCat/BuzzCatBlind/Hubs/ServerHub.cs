namespace BuzzCat.Hubs
{
    using Lib.Base;
    using Utilities;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using System;
    using System.Threading.Tasks;

    [HubName("BuzzCat")]
    public class ServerHub : Hub<IClient>, IServerHub
    {
        private static ConnectionMapping<string> connections =
            new ConnectionMapping<string>();

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
            foreach (var conn in connections.GetConnections(user))
            {
                Clients.Client(conn).show(name, message);
            }
            Clients.Caller.show(name, message);
        }

        // Hide your identity
        public void TalkToAs(string name, string user, string message)
        {
            foreach (var conn in connections.GetConnections(name))
            {
                Clients.Client(conn).show(name, message);
            }
            var caller = Context.QueryString["Name"];
            string talkAs = name + " as " + caller;
            Clients.Caller.show(talkAs, message);
        }

        //public IDictionary<string, HashSet<string>> ServerStatus()
        //{
        //    return connections;
        //}

        public override Task OnConnected()
        {
            var name = Context.QueryString["Name"];
            var connectionId = Context.ConnectionId;

            if (!connections.Add(name, connectionId))
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

            if (!connections.Remove(name, connectionId))
            {
                Console.WriteLine("Error removing user: {0}, {1}",
                    name, connectionId);
            }
            else
            {
                Console.WriteLine("Removed user: {0}, {1}",
                    name, connectionId);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}
