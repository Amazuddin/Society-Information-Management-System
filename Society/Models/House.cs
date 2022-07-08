using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Society.Models
{
    public class House
    {
        public int Id { get; set; }
        public string HouseNo { get; set; }
        public string Image { get; set; }
        public string Information { get; set; }
        public string Price { get; set; }
    }
}