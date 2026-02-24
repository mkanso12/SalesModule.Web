using SalesModule.DataAccess;
using System;
using System.Configuration;
using System.Data.Linq;
using System.Linq;

namespace SalesModule.BusinessLogic
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceDataAccess _invoiceDataAccess;

        public InvoiceService(IInvoiceDataAccess invoiceDataAccess)
        {
            _invoiceDataAccess = invoiceDataAccess;
        }

        public void PostInvoice(int invoiceId)
        {
            if (invoiceId <= 0)
                throw new ArgumentException("Invoice ID must be a positive integer.", nameof(invoiceId));
            var invoice = _invoiceDataAccess.GetInvoice(invoiceId);
            if (invoice == null)
                throw new InvalidOperationException($"Invoice with ID {invoiceId} not found.");
            if (invoice.Status != "Open")
                throw new InvalidOperationException($"Invoice {invoiceId} is not in Open state (current status: {invoice.Status}).");
            _invoiceDataAccess.PostInvoice(invoiceId);

        }

        public Invoice GetInvoice(int invoiceId)
        {
            return _invoiceDataAccess.GetInvoice(invoiceId);
        }
    }
}

