namespace BuzzCat.Core.Lib.Base
{
    using System.Threading.Tasks;

    public interface IBuzzCatClient
    {
        Task Message(StompMessage message);
        Task Reciept(StompMessage message);
        Task Error(StompMessage message);
        Task Connected(StompMessage message);
    }
}
