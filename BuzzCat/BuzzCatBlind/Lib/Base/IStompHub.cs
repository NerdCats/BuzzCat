namespace BuzzCatBlind.Lib.Base
{
    using Message;

    public interface IStompHub
    {
        void Connect(IStompMessage message);
        void Disconnect(IStompMessage message);
        void Subscribe(IStompMessage message);
        void Unsubscribe(IStompMessage message);
        void Send(IStompMessage message);
    }
}
