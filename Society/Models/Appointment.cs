using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Society.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Date { get; set; }
        public int SpecialistId { get; set; }
        public int DoctorId { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string Message { get; set; }
        public int IsApproved { get; set; }
    }
}