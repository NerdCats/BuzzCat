namespace BuzzCatBlind.Lib.Base
{
    using Message;

    public interface IStompClient
    {
        IStompMessage Message(IStompMessage message);
        IStompMessage Reciept(IStompMessage message);
        IStompMessage Error(IStompMessage message);
        IStompMessage Connected(IStompMessage message);
    }
}
