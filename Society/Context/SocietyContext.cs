using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Society.Models;

namespace Society.Context
{
    public class SocietyContext : DbContext
    {
        public DbSet<House> Houses { get; set; }
        public DbSet<BookigHouse> BookigHouses { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<SocietyRegistration> SocietyRegistrations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Specialist> Specialists { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Security> Securities { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}