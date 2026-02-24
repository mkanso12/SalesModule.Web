using SalesModule.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesModule.BusinessLogic
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentDataAccess _paymentDataAccess;

        public PaymentService(IPaymentDataAccess paymentDataAccess)
        {
            _paymentDataAccess = paymentDataAccess;
        }

        public Payment GetPayment(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Payment ID must be a positive integer.", nameof(id));
            return _paymentDataAccess.GetPayment(id);
        }
    }
}