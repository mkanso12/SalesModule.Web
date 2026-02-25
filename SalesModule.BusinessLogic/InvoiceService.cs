using SalesModule.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;

namespace SalesModule.BusinessLogic
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceDataAccess _invoiceDataAccess;
        private readonly ISalesOrderDataAccess _salesOrderDataAccess;

        public InvoiceService(IInvoiceDataAccess invoiceDataAccess, ISalesOrderDataAccess salesOrderDataAccess)
        {
            _invoiceDataAccess = invoiceDataAccess;
            _salesOrderDataAccess = salesOrderDataAccess;
        }

        public void PostInvoice(int invoiceId)
        {
            if (invoiceId <= 0)
                throw new ArgumentException("Invoice ID must be a positive integer.", nameof(invoiceId));
            var invoice = _invoiceDataAccess.GetInvoice(invoiceId);
            if (invoice == null)
                throw new InvalidOperationException($"Invoice with ID {invoiceId} not found.");
            if (invoice.Status != "Open")
                throw new InvalidOperationException($"Invoice {invoiceId} is not in Open state (current status: {invoice.Status}).");
            _invoiceDataAccess.PostInvoice(invoiceId);

        }

        public Invoice GetInvoice(int invoiceId)
        {
            return _invoiceDataAccess.GetInvoice(invoiceId);
        }
        public List<Invoice> GetAllInvoices()
        {
            return _invoiceDataAccess.GetAll();
        }
        public Invoice CreateInvoiceFromOrder(int salesOrderId)
        {
            var order = _salesOrderDataAccess.GetById(salesOrderId);
            if (order == null)
                throw new ArgumentException("Sales order not found.");
            if (order.Status != "Open")
                throw new ArgumentException("Order is not open.");

            decimal netTotal = order.SalesOrderLines.Sum(l => l.Qty * l.UnitPrice);
            decimal taxTotal = netTotal * 0.11m;
            decimal grossTotal = netTotal + taxTotal;

            var invoice = new Invoice
            {
                SalesOrderId = order.Id,
                CustomerId = order.CustomerId,
                Date = DateTime.Now,
                NetTotal = netTotal,
                TaxTotal = taxTotal,
                GrossTotal = grossTotal,
                Status = "Open"
            };

            var lines = new List<InvoiceLine>();
            foreach (var orderLine in order.SalesOrderLines)
            {
                var line = new InvoiceLine
                {
                    ItemId = orderLine.ItemId,
                    Qty = orderLine.Qty,
                    UnitPrice = orderLine.UnitPrice,
                    TaxAmount = orderLine.Qty * orderLine.UnitPrice * 0.11m
                };
                lines.Add(line);
            }

            _invoiceDataAccess.Insert(invoice, lines);
            return invoice;
        }
        public List<Invoice> GetOpenInvoicesByCustomer(int customerId)
        {
            if (customerId <= 0)
                throw new ArgumentException("Invalid customer ID.");
            return _invoiceDataAccess.GetOpenInvoicesByCustomer(customerId);
        }
    }
}

