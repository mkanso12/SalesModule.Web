using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesModule.DataAccess
{
    public class CustomerDataAccess : ICustomerDataAccess
    {
        private readonly string _connectionString;

        public CustomerDataAccess(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<Customer> GetAll()
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                return db.Customers.ToList();
            }
        }

        public Customer GetById(int id)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                return db.Customers.FirstOrDefault(c => c.Id == id);
            }
        }

        public void Insert(Customer customer)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                db.Customers.InsertOnSubmit(customer);
                db.SubmitChanges();
            }
        }

        public void Update(Customer customer)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                var existing = db.Customers.FirstOrDefault(c => c.Id == customer.Id);
                if (existing != null)
                {
                    existing.Name = customer.Name;
                    db.SubmitChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                var customer = db.Customers.FirstOrDefault(c => c.Id == id);
                if (customer != null)
                {
                    db.Customers.DeleteOnSubmit(customer);
                    db.SubmitChanges();
                }
            }
        }
    }
}