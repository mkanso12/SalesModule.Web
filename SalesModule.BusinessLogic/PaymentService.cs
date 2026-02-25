using SalesModule.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SalesModule.BusinessLogic
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentDataAccess _paymentDataAccess;
        private readonly IInvoiceDataAccess _invoiceDataAccess;
        private readonly IGLTransactionDataAccess _glDataAccess;

        public PaymentService(IPaymentDataAccess paymentDataAccess, IInvoiceDataAccess invoiceDataAccess, IGLTransactionDataAccess glDataAccess)
        {
            _paymentDataAccess = paymentDataAccess;
            _invoiceDataAccess = invoiceDataAccess;
            _glDataAccess = glDataAccess;
        }
        public Payment GetPayment(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Payment ID must be a positive integer.", nameof(id));
            return _paymentDataAccess.GetPayment(id);
        }
        public List<Payment> GetAllPayments()
        {
            return _paymentDataAccess.GetAll();
        }

        public void CreatePayment(int customerId, int invoiceId, decimal amount)
        {
            if (customerId <= 0) throw new ArgumentException("Customer is required.");
            if (invoiceId <= 0) throw new ArgumentException("Invoice is required.");
            if (amount <= 0) throw new ArgumentException("Amount must be positive.");

            var invoice = _invoiceDataAccess.GetInvoice(invoiceId);
            if (invoice == null) throw new ArgumentException("Invoice not found.");
            if (invoice.Status == "Paid") throw new ArgumentException("Invoice already paid.");

            using (var scope = new TransactionScope())
            {
                var payment = new Payment
                {
                    CustomerId = customerId,
                    Date = DateTime.Now,
                    Amount = amount,
                    InvoiceId = invoiceId,
                    Status = "Posted"
                };

                _paymentDataAccess.Insert(payment);

                // Update invoice status
                _invoiceDataAccess.UpdateStatus(invoiceId, "Paid");

                // Create GL transaction
                CreateGLForPayment(payment);

                scope.Complete();
            }
        }

        private void CreateGLForPayment(Payment payment)
        {
            const string cashAccount = "1000 Cash";
            const string receivableAccount = "1100 AR";

            var transaction = new GLTransaction
            {
                Date = payment.Date
            };

            var lines = new List<GLTransactionLine>
             {
            new GLTransactionLine
            {
            Account = cashAccount,
            Debit = payment.Amount,
            Credit = 0
            },
            new GLTransactionLine
            {
            Account = receivableAccount,
            Debit = 0,
            Credit = payment.Amount
             }
              };
            decimal totalDebit = lines.Sum(l => l.Debit);
            decimal totalCredit = lines.Sum(l => l.Credit);
            if (totalDebit != totalCredit)
                throw new InvalidOperationException("GL Transaction is not balanced.");

            _glDataAccess.Insert(transaction, lines);
        }
    }
}