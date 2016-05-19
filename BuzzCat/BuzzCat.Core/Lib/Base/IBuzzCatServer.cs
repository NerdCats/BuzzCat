namespace BuzzCat.Core.Lib.Base
{
    public interface IBuzzCatServer
    {
        void Connect(StompMessage message);
        void Disconnect(StompMessage message);
        void Subscribe(StompMessage message);
        void Unsubscribe(StompMessage message);
        void Send(StompMessage message);
    }
}
