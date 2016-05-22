using System.Threading.Tasks;

namespace BuzzCat.Core.Lib.Base
{
    public interface IBuzzCatServer
    {
        StompMessage Connect(StompMessage message);
        Task Subscribe(StompMessage message);
        Task Unsubscribe(StompMessage message);
        Task Send(StompMessage message);
    }
}
