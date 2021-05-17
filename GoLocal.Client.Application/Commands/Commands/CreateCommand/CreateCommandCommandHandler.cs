using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Domain.Entities.Identity;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Accessor.Accessors;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Client.Application.Commands.Commands.CreateCommand
{
    public class CreateCommandCommandHandler : AbstractRequestHandler<CreateCommandCommand, string>
    {
        private readonly Context _context;
        private readonly IUserAccessor<User> _user;
        
        public CreateCommandCommandHandler(Context context, IUserAccessor<User> user)
        {
            _context = context;
            _user = user;
        }

        public override async Task<Result<string>> Handle(CreateCommandCommand request, CancellationToken cancellationToken)
        {
            if (!await _context.Services.AnyAsync(m => m.Id == request.ServiceId, cancellationToken))
                return NotFound<Service>(request.ServiceId);
            
            Package package = await _context.Packages
                .SingleOrDefaultAsync(m => m.Id == request.PackageId && m.ItemId == request.ServiceId, cancellationToken);

            if (package == null)
                return NotFound<Package>(request.PackageId);

            User user = await _user.GetUserAsync();

            Command command = new Command(user, package, request.Specifications);

            await _context.Commands.AddAsync(command, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok(command.Id);
        }
    }
}