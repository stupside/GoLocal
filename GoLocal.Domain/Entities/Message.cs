using System;
using GoLocal.Domain.Entities.Identity;

namespace GoLocal.Domain.Entities
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
        public virtual Command Command { get; }
    }
}