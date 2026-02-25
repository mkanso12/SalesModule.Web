using SalesModule.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesModule.DataAccess
{
    public interface ISalesOrderDataAccess
    {
        List<SalesOrder> GetAll();
        SalesOrder GetById(int id);
        void Insert(SalesOrder order);
        void Update(SalesOrder order);
        void Delete(int id);
        List<SalesOrder> GetOrdersNotInvoiced();
        List<AvailableOrderDto> GetOrdersAvailableForInvoiceDisplay();
    }
}