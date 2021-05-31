using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Client.Application.Queries.Invoices.GetInvoices.Models;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Client.Application.Queries.Invoices.GetInvoices
{
    public class GetInvoicesQuery : AbstractPagedRequest<Invoice, InvoiceDto>
    {
        protected override void ConfigurePaging(PageRequestConfiguration<Invoice> paging)
        {
            paging.MapFor("creation", m => m.Creation);
            paging.MapFor("status", m => m.Status);
        }
    }
}