using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Artisan.Application.Commands.Commands.GenerateCommandInvoice
{
    public class GenerateCommandInvoiceCommandHandler : AbstractRequestHandler<GenerateCommandInvoiceCommand, int>
    {
        private readonly Context _context;

        public GenerateCommandInvoiceCommandHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result<int>> Handle(GenerateCommandInvoiceCommand request, CancellationToken cancellationToken)
        {
            Command command = await _context.Commands
                .SingleOrDefaultAsync(m => m.Id == request.CommandId, cancellationToken);

            if (command == null)
                return NotFound<Command>(request.CommandId);

            if (command.InvoiceId != 0)
                return BadRequest("An invoice already exists for this command");

            CommandProposal proposal =
                await _context.CommandProposals
                    .SingleOrDefaultAsync(
                    m => m.Approved && m.CommandId == request.CommandId, cancellationToken);

            if (proposal == null)
                return BadRequest("You can't generate an invoice for this command");

            Invoice invoice = new Invoice(command, proposal);

            await _context.Invoices.AddAsync(invoice, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok(invoice.Id);
        }
    }
}