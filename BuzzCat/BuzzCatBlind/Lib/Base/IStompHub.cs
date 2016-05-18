namespace BuzzCatBlind.Lib.Base
{
    using Message;

    public interface IStompHub
    {
        IStompMessage Connect(IStompMessage message);
        IStompMessage Disconnect(IStompMessage message);
        IStompMessage Subscribe(IStompMessage message);
        IStompMessage Unsubscribe(IStompMessage message);
        IStompMessage Send(IStompMessage message);
    }
}
