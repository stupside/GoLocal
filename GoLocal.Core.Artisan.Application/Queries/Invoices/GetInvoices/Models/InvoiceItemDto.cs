using System;

namespace GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoices.Models
{
    public class InvoiceItemDto
    {
        public string Id { get; init; }
        
        public int Quantity { get; init; }
        public float Price { get; init; }
        
        public string Description { get; init; }
        
        public DateTime Creation { get; init; }
    }
}