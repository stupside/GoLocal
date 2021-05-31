using System;

namespace GoLocal.Core.Client.Application.Hubs.Clients.ClientCommands.GetHistory.Models
{
    public class MessageDto
    {
        public int Id { get; init; }
        public string Body { get; init; }
        public UserDto User { get; init; }
        public DateTime Created { get; init; }
    }
}