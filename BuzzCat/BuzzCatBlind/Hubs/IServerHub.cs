using System.Collections.Generic;

namespace BuzzCatBlind.Hubs
{
    internal interface IServerHub
    {
        void Talk(string message);
        void TalkAs(string name, string message);
        void TalkTo(string user, string message);
        void TalkToAs(string name, string user, string message);
        IDictionary<string, HashSet<string>> ServerStatus();
    }
}
