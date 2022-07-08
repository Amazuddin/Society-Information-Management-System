using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Society.Models
{
    public class SocietyRegistration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
    }
}