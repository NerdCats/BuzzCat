namespace BuzzCat.Lib.Base
{
    internal interface IShadowHub
    {
        void SendLocation(object asset);
        void SendLocationTo(string to, object asset);
        void SendLocationToMultiple(string[] to, object asset);
    }
}
