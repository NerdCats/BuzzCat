namespace BuzzCat.Core.Lib.Base
{
    public interface IBuzzCatClient
    {
        void Message(StompMessage message);
        void Reciept(StompMessage message);
        void Error(StompMessage message);
        void Connected(StompMessage message);
    }
}
