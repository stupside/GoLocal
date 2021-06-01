using System;
using GoLocal.Core.Domain.Enums;

namespace GoLocal.Core.Artisan.Application.Queries.Commands.GetCommands.Models
{
    public class CommandDto
    {
        public string Id { get; init; }
        
        public DateTime Creation { get; init; }
        public CommandStatus Status { get; init; }
        public float Price { get; init; }
        public string Specification { get; init; }
        
        public ItemDto Item { get; init; }
        public UserDto User { get; init; }
        
    }
}