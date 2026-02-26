using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesModule.DataAccess
{
    public interface IInvoiceDataAccess
    {
        void PostInvoice(int invoiceId);
        Invoice GetInvoice(int invoiceId);
        List<Invoice> GetAll();
        void Insert(Invoice invoice, List<InvoiceLine> lines);
        List<Invoice> GetOpenInvoicesByCustomer(int customerId);
        void UpdateStatus(int invoiceId, string status, decimal? amountPaid = null);
        List<Invoice> GetPostedInvoicesByCustomer(int customerId);
    }
}
