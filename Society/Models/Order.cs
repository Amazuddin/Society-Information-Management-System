using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Society.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public int ProductAmount { get; set; }
        public string Price { get; set; }
        public string Date { get; set; }
        public string TotalPrice { get; set; }
        public int CustomerId { get; set; }
    }
}