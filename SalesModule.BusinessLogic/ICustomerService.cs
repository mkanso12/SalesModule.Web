using SalesModule.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesModule.BusinessLogic
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        Customer GetCustomer(int id);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
    }
}