using SalesModule.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesModule.BusinessLogic
{
    public interface IInvoiceService
    {
        void PostInvoice(int invoiceId);
        Invoice GetInvoice(int invoiceId);
        List<Invoice> GetAllInvoices();
        Invoice CreateInvoiceFromOrder(int salesOrderId);
        List<Invoice> GetOpenInvoicesByCustomer(int customerId);
        List<Invoice> GetPostedInvoicesByCustomer(int customerId);
    }
}
