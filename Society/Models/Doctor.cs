using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Society.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Degree { get; set; }
        public string Fees { get; set; }
        public int SpecialistId { get; set; }
    }
}