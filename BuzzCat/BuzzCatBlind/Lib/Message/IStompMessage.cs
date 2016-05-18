namespace BuzzCatBlind.Lib.Message
{
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;

    public interface IStompMessageBase
    {
        string Command { get; set; }
        Dictionary<string, string> Headers { get; set; }
    }

    public interface IStompMessage : IStompMessageBase
    {
        JObject Body { get; set; }
    }

    public interface IStompMessage<T> : IStompMessageBase
    {
        T Body { get; set; }
    }
}
