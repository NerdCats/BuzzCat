using System.Threading.Tasks;

namespace BuzzCat.Core.Lib.Base
{
    public interface IBuzzCatServer
    {
        Task Connect(StompMessage message);
        Task Disconnect(StompMessage message);
        Task Subscribe(StompMessage message);
        Task Unsubscribe(StompMessage message);
        Task Send(StompMessage message);
    }
}
