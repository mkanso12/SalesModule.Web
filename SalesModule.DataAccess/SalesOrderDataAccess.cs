using SalesModule.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace SalesModule.DataAccess
{
    public class SalesOrderDataAccess : ISalesOrderDataAccess
    {
        private readonly string _connectionString;

        public SalesOrderDataAccess(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<SalesOrder> GetAll()
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<SalesOrder>(o => o.Customer);
                loadOptions.LoadWith<SalesOrder>(o => o.SalesOrderLines);
                loadOptions.LoadWith<SalesOrderLine>(l => l.Item);
                db.LoadOptions = loadOptions;
                return db.SalesOrders.ToList();
            }
        }

        public SalesOrder GetById(int id)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<SalesOrder>(o => o.Customer);
                loadOptions.LoadWith<SalesOrder>(o => o.SalesOrderLines);
                loadOptions.LoadWith<SalesOrderLine>(l => l.Item);
                db.LoadOptions = loadOptions;
                return db.SalesOrders.FirstOrDefault(o => o.Id == id);
            }
        }

        public void Insert(SalesOrder order)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                db.SalesOrders.InsertOnSubmit(order);
                db.SubmitChanges();
            }
        }

        public void Update(SalesOrder order)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                var existing = db.SalesOrders.FirstOrDefault(o => o.Id == order.Id);
                if (existing != null)
                {
                    existing.CustomerId = order.CustomerId;
                    existing.Date = order.Date;
                    existing.Status = order.Status;
                    db.SubmitChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                var order = db.SalesOrders.FirstOrDefault(o => o.Id == id);
                if (order != null)
                {
                    db.SalesOrderLines.DeleteAllOnSubmit(order.SalesOrderLines);
                    db.SalesOrders.DeleteOnSubmit(order);
                    db.SubmitChanges();
                }
            }
        }
        public List<SalesOrder> GetOrdersNotInvoiced()
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                var invoicedOrderIds = db.Invoices.Select(i => i.SalesOrderId).Distinct();
                return db.SalesOrders
                         .Where(o => o.Status == "Open" && !invoicedOrderIds.Contains(o.Id))
                         .ToList();
            }
        }
        public List<AvailableOrderDto> GetOrdersAvailableForInvoiceDisplay()
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                var invoicedOrderIds = db.Invoices.Select(i => i.SalesOrderId).Distinct();
                var query = from o in db.SalesOrders
                            where o.Status == "Open" && !invoicedOrderIds.Contains(o.Id)
                            join c in db.Customers on o.CustomerId equals c.Id
                            select new AvailableOrderDto
                            {
                                Id = o.Id,
                                DisplayText = $"Order #{o.Id} - {c.Name} ({o.Date:yyyy-MM-dd})"
                            };
                return query.ToList();
            }
        }

    }
}