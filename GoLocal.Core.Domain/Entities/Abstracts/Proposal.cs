using System;
using GoLocal.Core.Domain.Entities.Identity;

namespace GoLocal.Core.Domain.Entities.Abstracts
{
    public abstract class Proposal
    {
        public int Id { get; set; }
        public bool Approved { get; set; }
        public DateTime Created { get; set; }
        
        public Proposal()
        {
            Approved = false;
            Created = DateTime.UtcNow;
        }

        protected Proposal(Command command, User user)
        {
            UserId = user.Id;
            CommandId = command.Id;
        }

        public string UserId { get; init; }
        public virtual User User { get; }
        
        public string CommandId { get; }
        public virtual Command Command { get; }
    }
}