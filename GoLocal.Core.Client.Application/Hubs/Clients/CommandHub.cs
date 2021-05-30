using System;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Core.Client.Application.Hubs.Clients.ClientCommands.SendMessage;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Persistence.EntityFramework;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace GoLocal.Core.Client.Application.Hubs.Clients
{
    [Authorize]
    public class CommandHub : Hub<ICommandClient>
    {
        private readonly IUserAccessor<User> _accessor;
        private readonly Context _context;
        private readonly IMediator _mediator;

        public CommandHub(IMediator mediator, Context context, IUserAccessor<User> accessor)
        {
            _mediator = mediator;
            _context = context;
            _accessor = accessor;
        }

        [HubMethodName("send_message")]
        public async Task SendMessage(SendMessageCommand command)
            => await _mediator.Publish(command);
    }
}