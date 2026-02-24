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
    }
}
