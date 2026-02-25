using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesModule.DataAccess
{
    public interface ICustomerDataAccess
    {
        List<Customer> GetAll();
        Customer GetById(int id);
        void Insert(Customer customer);
        void Update(Customer customer);
        void Delete(int id);
    }
}