using System;
using GoLocal.Domain.Entities.Identity;

namespace GoLocal.Domain.Entities
{
    public class CommandProposal
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public string Specification { get; set; }
        public bool Accepted { get; set; }
        public DateTime Created { get; set; }
        
        public CommandProposal()
        {
            Accepted = false;
            Created = DateTime.UtcNow;
        }
        
        public CommandProposal(Command command, User user, float price, string specification)
            : this()
        {
            CommandId = command.Id;
            UserId = user.Id;
            Price = price;
            Specification = specification;
        }

        public string UserId { get; init; }
        public virtual User User { get; }
        
        public string CommandId { get; }
    }
}