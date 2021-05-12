using System;
using GoLocal.Domain.Entities.Identity;
using GoLocal.Domain.Enums;

namespace GoLocal.Domain.Entities
{
    public class Command
    {
        public string Id { get; set; }
        
        public float Price { get; set; }
        public string Specification { get; set; }
        
        public CommandStatus Status { get; set; }

        public DateTime Creation { get; }
        
        public Command()
        {
            Creation = DateTime.UtcNow;
        }

        public Command(User user, Package package, string specification)
            : this()
        {
            UserId = user.Id;
            PackageId = package.Id;
            
            Specification = specification;
            Status = CommandStatus.Pending;
        }

        public string UserId { get; }
        public virtual User User { get; }
        
        public int PackageId { get; }
        public virtual Package Package { get; }
    }
}