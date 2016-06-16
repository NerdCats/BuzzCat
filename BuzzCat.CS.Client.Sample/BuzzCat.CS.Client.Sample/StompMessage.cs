namespace BuzzCat.CS.Client.Sample
{
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;

    public class StompMessage
    {
        public string Command { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string Type { get; set; }
        public JObject Body { get; set; }
    }
}
