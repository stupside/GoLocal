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

        public Message(Command command, User user, string message)
            : this()
        {
            CommandId = command.Id;
            UserId = user.Id;
            
            Body = message;
        }

        public string UserId { get; }
        public virtual User User { get; }
        
        public string CommandId { get; }
    }
}