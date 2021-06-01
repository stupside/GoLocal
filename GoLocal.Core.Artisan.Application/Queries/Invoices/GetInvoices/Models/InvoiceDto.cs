using System;
using System.Collections.Generic;
using System.Linq;
using GoLocal.Core.Domain.Enums;

namespace GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoices.Models
{
    public class InvoiceDto
    {
        public int Id { get; init; }
        public InvoiceStatus Status { get; init; }
        public IEnumerable<InvoiceItemDto> InvoiceItems { get; init; }
        public float TotalPrice => InvoiceItems.Sum(m => m.Price * m.Quantity);
        
        public DateTime Creation { get; init; }
    }
}