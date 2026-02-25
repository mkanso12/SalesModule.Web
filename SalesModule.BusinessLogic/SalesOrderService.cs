using System;
using System.Collections.Generic;
using System.Linq;
using SalesModule.DataAccess;
using SalesModule.DataAccess.DTOs;

namespace SalesModule.BusinessLogic
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly ISalesOrderDataAccess _salesOrderDataAccess;
        private readonly IItemDataAccess _itemDataAccess;

        public SalesOrderService(ISalesOrderDataAccess salesOrderDataAccess, IItemDataAccess itemDataAccess)
        {
            _salesOrderDataAccess = salesOrderDataAccess;
            _itemDataAccess = itemDataAccess;
        }

        public List<SalesOrder> GetAllOrders()
        {
            return _salesOrderDataAccess.GetAll();
        }

        public SalesOrder GetOrder(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Order ID must be a positive integer.", nameof(id));
            return _salesOrderDataAccess.GetById(id);
        }

        public void CreateOrder(SalesOrder order, List<SalesOrderLine> lines)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            if (order.CustomerId <= 0) throw new ArgumentException("Customer is required.");
            if (lines == null || lines.Count == 0) throw new ArgumentException("Order must have at least one line.");

            foreach (var line in lines)
            {
                if (line.ItemId <= 0) throw new ArgumentException("Invalid item selected.");
                if (line.Qty <= 0) throw new ArgumentException("Quantity must be greater than zero.");
                if (line.UnitPrice < 0) throw new ArgumentException("Unit price cannot be negative.");

                var item = _itemDataAccess.GetById(line.ItemId);
                if (item == null)
                    throw new ArgumentException($"Item with ID {line.ItemId} does not exist.");
            }

            order.Date = DateTime.Now;
            order.Status = "Open";

            order.SalesOrderLines.Clear();
            foreach (var line in lines)
            {
                order.SalesOrderLines.Add(line);
            }

            _salesOrderDataAccess.Insert(order);
        }

        public void UpdateOrder(SalesOrder order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            if (order.Id <= 0)
                throw new ArgumentException("Invalid order ID.");
            _salesOrderDataAccess.Update(order);
        }

        public void DeleteOrder(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Order ID must be a positive integer.", nameof(id));
            _salesOrderDataAccess.Delete(id);
        }
        public List<SalesOrder> GetOrdersAvailableForInvoice()
        {
            return _salesOrderDataAccess.GetOrdersNotInvoiced();
        }
        public List<AvailableOrderDto> GetOrdersAvailableForInvoiceDisplay()
        {
            return _salesOrderDataAccess.GetOrdersAvailableForInvoiceDisplay();
        }
    }
}