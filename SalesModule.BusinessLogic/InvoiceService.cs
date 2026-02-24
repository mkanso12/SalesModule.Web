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
            _invoiceDataAccess.PostInvoice(invoiceId);
        }

        public Invoice GetInvoice(int invoiceId)
        {
            return _invoiceDataAccess.GetInvoice(invoiceId);
        }
    }
}

