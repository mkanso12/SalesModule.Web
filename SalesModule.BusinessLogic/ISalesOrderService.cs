using System.Collections.Generic;
using SalesModule.DataAccess;
using SalesModule.DataAccess.DTOs;

namespace SalesModule.BusinessLogic
{
    public interface ISalesOrderService
    {
        List<SalesOrder> GetAllOrders();
        SalesOrder GetOrder(int id);
        void CreateOrder(SalesOrder order, List<SalesOrderLine> lines);
        void UpdateOrder(SalesOrder order);
        void DeleteOrder(int id);
        List<SalesOrder> GetOrdersAvailableForInvoice();
        List<AvailableOrderDto> GetOrdersAvailableForInvoiceDisplay();
    }
}