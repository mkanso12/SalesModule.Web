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
    }
}