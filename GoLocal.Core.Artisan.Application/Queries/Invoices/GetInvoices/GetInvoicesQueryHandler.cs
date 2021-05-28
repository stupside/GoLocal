using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoices.Models;

namespace GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoices
{
    public class GetInvoicesQueryHandler : AbstractPagedRequestHandler<GetInvoicesQuery, InvoiceDto>
    {
        public override Task<Result<Page<InvoiceDto>>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}