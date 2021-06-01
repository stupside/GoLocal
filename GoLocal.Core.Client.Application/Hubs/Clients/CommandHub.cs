using System.Threading.Tasks;
using GoLocal.Core.Client.Application.Hubs.Clients.ClientCommands.GetHistory;
using GoLocal.Core.Client.Application.Hubs.Clients.ClientCommands.SendMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace GoLocal.Core.Client.Application.Hubs.Clients
{
    [Authorize]
    public class CommandHub : Hub<ICommandClient>
    {
        private readonly IMediator _mediator;

        public CommandHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HubMethodName("send_message")]
        public async Task SendMessage(SendMessageCommand command)
            => await _mediator.Publish(command);

        [HubMethodName("history_message")]
        public async Task GetHistory(GetHistoryQuery query)
            => await _mediator.Publish(query);
    }
}