using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BlindCat.Models
{
    public interface IStompMessageBase
    {
        string Command { get; set; }
        Dictionary<string, string> Headers { get; set; }
        string Type { get; set; }
    }

    public interface IStompMessage : IStompMessageBase
    {
        JObject Body { get; set; }
    }

    public interface IStompMessage<T> : IStompMessageBase
    {
        T Body { get; set; }
    }

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
