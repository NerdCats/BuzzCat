using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BuzzCatBlind.Lib.Message
{
    public class EchoMessage : IStompMessage
    {
        public string Command { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string Type { get; set; }
        public JObject Body { get; set; }

        public EchoMessage(string command)
        {
            this.Command = command;
        }

        public EchoMessage(string command, string body) : this(command)
        {
            this.Body = (JObject)body;
            this.Type = this.Body.GetType().ToString();
        }

    }
}
