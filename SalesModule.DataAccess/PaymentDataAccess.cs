using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesModule.DataAccess
{
    public class PaymentDataAccess : IPaymentDataAccess
    {
        private readonly string _connectionString;

        public PaymentDataAccess(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public Payment GetPayment(int id)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Payment>(p => p.Customer);
                loadOptions.LoadWith<Payment>(p => p.Invoice);
                db.LoadOptions = loadOptions;

                return db.Payments.FirstOrDefault(p => p.Id == id);
            }
        }
        public List<Payment> GetAll()
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Payment>(p => p.Customer);
                loadOptions.LoadWith<Payment>(p => p.Invoice);
                db.LoadOptions = loadOptions;
                return db.Payments.ToList();
            }
        }

        public void Insert(Payment payment)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                db.Payments.InsertOnSubmit(payment);
                db.SubmitChanges();
            }
        }
    }
}