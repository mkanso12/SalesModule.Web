using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesModule.API.Models
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int? InvoiceId { get; set; }
        public string InvoiceStatus { get; set; }
        public string Status { get; set; }
    }
}