using Eticaret.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret2.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public  virtual List<OrderLine> OrderLines { get; set; }
        public int OrderId { get; set; }
    }
    public class OrderLine
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int Quantity { get; set; }
        public int ProducytId { get; set; }
        public virtual Product Product { get; set; }
    }
}