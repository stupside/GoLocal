using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Filtering;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Client.Application.Queries.Commands.GetCommands.Models;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Client.Application.Queries.Commands.GetCommands
{
    public class GetCommandsQueryHandler : AbstractPagedRequestHandler<GetCommandsQuery, CommandDto>
    {
        private readonly Context _context;
        private readonly IUserAccessor<User> _user;

        public GetCommandsQueryHandler(Context context, IUserAccessor<User> user)
        {
            _context = context;
            _user = user;
        }

        public override async Task<Result<Page<CommandDto>>> Handle(GetCommandsQuery request, CancellationToken cancellationToken)
        {
            User user = await _user.GetUserAsync();
            
            int count = await _context.Commands.CountAsync(m => m.UserId == user.Id, cancellationToken);
            
            List<CommandDto> commands = await _context.Commands
                .Include(m => m.Shop)
                .Include(m => m.CommandProposals)
                .Include(m => m.Invoice)
                .Include(m => m.Package).ThenInclude(m => m.Item)
                .Where(m => m.UserId == user.Id)
                .ApplyLimit(request)
                .Select(m => new CommandDto
                {
                    Id = m.Id,
                    Status = m.Status,
                    Price = m.CommandProposals.FirstOrDefault(r => r.Approved).Price,
                    Specification = m.CommandProposals.FirstOrDefault(r => r.Approved).Specification,
                    Item = new ItemDto
                    {
                        Id = m.Package.Item.Id,
                        Name = m.Package.Item.Name,
                        Package = new PackageDto
                        {
                            Id = m.Package.Id,
                            Name = m.Package.Name,
                        },
                    },
                    Invoice = m.Invoice == null ? null : new InvoiceDto
                    {
                        Id = m.Invoice.Id,
                        Creation = m.Invoice.Creation
                    },
                    Shop = new ShopDto
                    {
                        Id = m.Shop.Id,
                        Name = m.Shop.Name,
                        Visibility = m.Shop.Visibility
                    }
                }).AsNoTracking().ToListAsync(cancellationToken);
            
            return Ok(commands, count);      
        }
    }
}