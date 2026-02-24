using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesModule.DataAccess
{
    public interface IPaymentDataAccess
    {
        Payment GetPayment(int id);
    }
}
