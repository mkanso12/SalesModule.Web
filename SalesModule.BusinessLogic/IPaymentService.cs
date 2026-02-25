using SalesModule.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesModule.BusinessLogic
{
    public interface IPaymentService
    {
        Payment GetPayment(int id);
        List<Payment> GetAllPayments();
        void CreatePayment(int customerId, int invoiceId, decimal amount);
    }
}
