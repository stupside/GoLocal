using System.Collections.Generic;
using System.Threading.Tasks;
using GoLocal.Core.Client.Application.Hubs.Clients.ClientCommands.GetHistory.Models;

namespace GoLocal.Core.Client.Application.Hubs.Clients
{
    public interface ICommandClient
    {
        Task ReceiveHistory(List<MessageDto> messages);
        Task ReceiveMessage(string cid, string message);
    }
}