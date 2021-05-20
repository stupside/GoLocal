using System;

namespace GoLocal.Artisan.Application.Queries.Invoices.GetInvoice.Models
{
    public class CommentDto
    {
        public int Id { get; init; }
        
        public int Rate { get; init; }
        public string Body { get; init; }
        
        public DateTime Creation { get; init; }
    }
}