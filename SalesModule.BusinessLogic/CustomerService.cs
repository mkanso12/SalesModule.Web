using SalesModule.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesModule.BusinessLogic
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerDataAccess _customerDataAccess;

        public CustomerService(ICustomerDataAccess customerDataAccess)
        {
            _customerDataAccess = customerDataAccess;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerDataAccess.GetAll();
        }

        public Customer GetCustomer(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Customer ID must be a positive integer.", nameof(id));
            return _customerDataAccess.GetById(id);
        }

        public void CreateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));
            if (string.IsNullOrWhiteSpace(customer.Name))
                throw new ArgumentException("Customer name is required.");

            _customerDataAccess.Insert(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));
            if (customer.Id <= 0)
                throw new ArgumentException("Invalid customer ID.");
            if (string.IsNullOrWhiteSpace(customer.Name))
                throw new ArgumentException("Customer name is required.");

            _customerDataAccess.Update(customer);
        }

        public void DeleteCustomer(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Customer ID must be a positive integer.", nameof(id));
            _customerDataAccess.Delete(id);
        }
    }
}