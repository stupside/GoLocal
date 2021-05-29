using System;
using GoLocal.Core.Domain.Enums;

namespace GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoice.Models
{
    public class InvoiceItemDto
    {
        public string Id { get; init; }
        
        public int Quantity { get; init; }
        public float Price { get; init; }
        public float Total => Quantity * Price;
        
        public string Description { get; init; }
        public InvoiceStatus Status { get; init; }

        public PackageDto Package { get; init; }
        
        public CommentDto Comment { get; init; }

        public DateTime Creation { get; init; }
    }
}