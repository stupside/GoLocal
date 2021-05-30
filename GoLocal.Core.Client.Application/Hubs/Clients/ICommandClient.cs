using System.Threading.Tasks;

namespace GoLocal.Core.Client.Application.Hubs.Clients
{
    public interface ICommandClient
    {
        Task ReceiveMessage(int sid, string cid, string message);
    }
}