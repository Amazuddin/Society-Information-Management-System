using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Society.Models
{
    public class Orderview
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public int ProductAmount { get; set; }
        public string Price { get; set; }
        public string Date { get; set; }
        public string TotalPrice { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
    }
}