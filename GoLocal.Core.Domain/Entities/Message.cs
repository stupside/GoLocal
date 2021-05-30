using System;
using GoLocal.Core.Domain.Entities.Identity;

namespace GoLocal.Core.Domain.Entities
{
    public class Message
    {
        public int Id { get; set; }
        
        public string Body { get; }
        
        public DateTime Creation { get; }

        public Message()
        {
            Creation = DateTime.UtcNow;
        }

        public Message(string commandId, string userId, string message)
            : this()
        {
            CommandId = commandId;
            UserId = userId;
            
            Body = message;
        }

        public string UserId { get; }
        public virtual User User { get; }
        
        public string CommandId { get; }
    }
}