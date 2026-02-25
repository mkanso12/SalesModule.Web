using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesModule.DataAccess
{
    public class InvoiceDataAccess : IInvoiceDataAccess
    {
        private readonly string _connectionString;

        public InvoiceDataAccess(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void PostInvoice(int invoiceId)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                db.sp_PostInvoice(invoiceId);
            }
        }

        public Invoice GetInvoice(int invoiceId)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Invoice>(i => i.InvoiceLines);
                loadOptions.LoadWith<InvoiceLine>(il => il.Item);
                db.LoadOptions = loadOptions;

                return db.Invoices.FirstOrDefault(i => i.Id == invoiceId);
            }
        }
        public List<Invoice> GetAll()
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Invoice>(i => i.Customer);
                loadOptions.LoadWith<Invoice>(i => i.InvoiceLines);
                loadOptions.LoadWith<InvoiceLine>(il => il.Item);
                db.LoadOptions = loadOptions;
                return db.Invoices.ToList();
            }
        }

        public void Insert(Invoice invoice, List<InvoiceLine> lines)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                // Attach lines to invoice
                foreach (var line in lines)
                {
                    invoice.InvoiceLines.Add(line);
                }
                db.Invoices.InsertOnSubmit(invoice);
                db.SubmitChanges();
            }
        }
        public List<Invoice> GetOpenInvoicesByCustomer(int customerId)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Invoice>(i => i.Customer);
                db.LoadOptions = loadOptions;
                return db.Invoices
                         .Where(i => i.CustomerId == customerId && (i.Status == "Open" || i.Status == "PartiallyPaid"))
                         .ToList();
            }
        }

        public void UpdateStatus(int invoiceId, string status, decimal? amountPaid = null)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                var invoice = db.Invoices.FirstOrDefault(i => i.Id == invoiceId);
                if (invoice != null)
                {
                    invoice.Status = status;
                    db.SubmitChanges();
                }
            }
        }
    }
}