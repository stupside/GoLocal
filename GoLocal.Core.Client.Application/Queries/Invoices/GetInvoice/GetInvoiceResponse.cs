using System;
using System.Collections.Generic;
using System.Linq;
using GoLocal.Core.Client.Application.Queries.Invoices.GetInvoice.Models;
using GoLocal.Core.Domain.Enums;

namespace GoLocal.Core.Client.Application.Queries.Invoices.GetInvoice
{
    public class GetInvoiceResponse
    {
        public int Id { get; init; }
        public InvoiceStatus Status { get; init; }
        public ShopDto Shop { get; init; }
        public IEnumerable<InvoiceItemDto> InvoiceItems { get; init; }
        public float TotalPrice => InvoiceItems.Sum(m => m.Price * m.Quantity);
        public DateTime Creation { get; init; }
    }
}