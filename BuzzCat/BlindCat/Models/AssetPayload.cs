using ConsoleClient.Models.GeoJson;

namespace ConsoleClient.Models
{
    public class AssetPayload
    {
        public string AssetId { get; set; }
        public Point Point { get; set; }
        public string Name { get; set; }
    }
}
