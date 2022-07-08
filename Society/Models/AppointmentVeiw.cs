using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Society.Models
{
    public class AppointmentVeiw
    {
        public string DoctorName { get; set; }
        public string Designation { get; set; }
        public string Specialist { get; set; }
        public string Date { get; set; }
        public int Approval { get; set; }
    }
}