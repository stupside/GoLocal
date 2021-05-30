using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Persistence.EntityFramework;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Client.Application.Hubs.Clients.ClientCommands.SendMessage
{
    public class SendMessageCommandHandler : INotificationHandler<SendMessageCommand>
    {
        private readonly Context _context;
        private readonly IUserAccessor<User> _accessor;
        private readonly IHubContext<CommandHub, ICommandClient> _hub;

        public SendMessageCommandHandler(IHubContext<CommandHub, ICommandClient> hub, IUserAccessor<User> accessor, Context context)
        {
            _hub = hub;
            _accessor = accessor;
            _context = context;
        }

        public async Task Handle(SendMessageCommand notification, CancellationToken cancellationToken)
        {
            User user = await _accessor.GetUserAsync();
            
            var command = await _context.Commands
                .Include(m => m.Invoice)
                .Where(m => m.Id == notification.CommandId && m.ShopId == notification.ShopId)
                .Select(m => new { m.Id, m.UserId, m.ShopId, OwnerId = m.Shop.UserId, m.Invoice.Status })
                .SingleOrDefaultAsync(cancellationToken);

            if(command == null || command.Status is InvoiceStatus.Picked)
                return;
            
            _hub.Clients.All?.ReceiveMessage(notification.ShopId, notification.CommandId, notification.Body);
            
            if (command.UserId == user.Id)
            {
                _hub.Clients.User(command.OwnerId)?.ReceiveMessage(notification.ShopId, notification.CommandId, notification.Body);
            }
            else if (command.OwnerId == user.Id)
            {
                _hub.Clients.User(command.UserId)?.ReceiveMessage(notification.ShopId, notification.CommandId, notification.Body);
            }
            else
            {
                return;
            }

            Message message = new Message(command.Id, user.Id, notification.Body);
            
            await _context.Messages.AddAsync(message, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}