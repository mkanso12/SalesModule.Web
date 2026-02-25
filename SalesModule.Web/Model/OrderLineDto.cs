using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesModule.Web.Model
{
    [Serializable]
    public class OrderLineDto
    {
        public Guid LineId { get; set; } = Guid.NewGuid(); // Unique per line
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => Qty * UnitPrice;
    }
}