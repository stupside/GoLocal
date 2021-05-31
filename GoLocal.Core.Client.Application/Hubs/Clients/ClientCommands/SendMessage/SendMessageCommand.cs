using MediatR;

namespace GoLocal.Core.Client.Application.Hubs.Clients.ClientCommands.SendMessage
{
    public class SendMessageCommand : INotification
    {
        public string CommandId { get; init; }
        public string Body { get; init; }

        public SendMessageCommand()
        {
        }

        public SendMessageCommand(string commandId, string body)
        {
            CommandId = commandId;
            Body = body;
        }
    }
}