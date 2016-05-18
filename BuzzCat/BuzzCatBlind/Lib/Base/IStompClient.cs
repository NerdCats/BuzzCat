namespace BuzzCatBlind.Lib.Base
{
    using Message;

    public interface IStompClient
    {
        void Message(IStompMessage message);
        void Reciept(IStompMessage message);
        void Error(IStompMessage message);
        void Connected(IStompMessage message);
    }
}
