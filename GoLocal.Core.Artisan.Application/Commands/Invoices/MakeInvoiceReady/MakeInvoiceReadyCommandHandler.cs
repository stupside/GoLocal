using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Artisan.Application.Commands.Invoices.MakeInvoiceReady
{
    public class MakeInvoiceReadyCommandHandler : AbstractRequestHandler<MakeInvoiceReadyCommand>
    {
        private readonly Context _context;

        public MakeInvoiceReadyCommandHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result> Handle(MakeInvoiceReadyCommand request, CancellationToken cancellationToken)
        {
            Invoice invoice =
                await _context.Invoices.SingleOrDefaultAsync(m =>
                    m.ShopId == request.ShopId && m.Id == request.InvoiceId, cancellationToken);

            if (invoice == null)
                return NotFound<Invoice>(request.InvoiceId);

            if (invoice.Status is not InvoiceStatus.Pending)
                return BadRequest(
                    $"You can't change the status of this invoice because the status is set to {invoice.Status}");

            invoice.Status = InvoiceStatus.Ready;

            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}