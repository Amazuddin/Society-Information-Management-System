using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Society.Context;
using Society.Models;

namespace Society.Controllers
{
    public class SocietyController : Controller
    {
        //
        // GET: /Society/
        public ActionResult Index()
        {
            ViewBag.Index = "active";
            return View();
        }

//***********************************************  House *********************************************//
        public ActionResult HouseRent()
        {
            ViewBag.HouseRent = "active";
            List<House> houses;
            using (var ctx = new SocietyContext())
            {
                houses = ctx.Houses.ToList();
            }
            return View(houses);

        }

        public ActionResult HouseRentForm()
        {
            ViewBag.HouseRentForm = "active";
            return View();

        }

        [HttpPost]
        public ActionResult HouseRentForm(BookigHouse bookigHouse)
        {
            using (var ctx = new SocietyContext())
            {
                ctx.BookigHouses.Add(bookigHouse);
                ctx.SaveChanges();
            }

            ViewBag.Message = "Your Booking taken Successfully.";

            return View();

        }

//****************************************** School ***********************************************//
        public ActionResult School()
        {
            ViewBag.School = "active";
            return View();

        }

        [HttpPost]
        public ActionResult School(Admission admission)
        {
            using (var ctx = new SocietyContext())
            {
                ctx.Admissions.Add(admission);
                ctx.SaveChanges();
            }

            ViewBag.Message = '1';

            return View();

        }

//******************************************  Super Shop  ***************************************************//
        public ActionResult SuperShop()
        {
            ViewBag.SuperShop = "active";
            return View();
        }

        public ActionResult Cloth()
        {
            return View();
        }

        public ActionResult Food()
        {
            return View();
        }

        public ActionResult Medicine()
        {
            return View();
        }

        public ActionResult Necessery()
        {
            return View();
        }

        public ActionResult OrderForm(string data1, string data2)
        {
            ViewBag.Order = "active";
            ViewBag.Data1 = data1;
            ViewBag.Data2 = data2;
            return View();
        }

        [HttpPost]
        public ActionResult OrderForm(Order orderList)
        {

            string chan = (orderList.ProductAmount*Convert.ToInt32(orderList.Price)).ToString();

            ViewBag.Order = "active";
            orderList.CustomerId = Convert.ToInt32(Session["SocietyId"]);
            using (var ctx = new SocietyContext())
            {
                orderList.TotalPrice = chan + " " + "BDT";
                ctx.Orders.Add(orderList);
                ctx.SaveChanges();
            }
            ViewBag.Yes = '1';
            return View();
        }

        public ActionResult Purchases()
        {
            ViewBag.Purchases = "active";
            int id = Convert.ToInt32(Session["SocietyId"]);
            List<Order> ord = new List<Order>();
            using (var ctx = new SocietyContext())
            {
                var q =
                    ctx.Orders.Where(k => k.CustomerId == id)
                        .Select(c => new {c.ProductCode, c.ProductAmount, c.TotalPrice, c.Date});
                foreach (var d in q)
                {
                    Order ap = new Order();
                    ap.ProductCode = d.ProductCode;
                    ap.Date = d.Date;
                    ap.ProductAmount = d.ProductAmount;
                    ap.TotalPrice = d.TotalPrice;
                    ord.Add(ap);
                }
            }
            return View(ord);
        }

//*****************************************************  Clinic ***********************************************************//
        public ActionResult Clinic()
        {
            ViewBag.Clinic = "active";
            return View();
        }

        public ActionResult DoctorInformation()
        {
            ViewBag.DoctorInformation = "active";
            List<Specialist> categories;


            using (var ctx = new SocietyContext())
            {

                categories = ctx.Specialists.ToList();
            }

            ViewBag.Category = categories;

            return View();
        }

        public JsonResult GetAllDoctorInfoById(int id)
        {

            List<Doctor> doctor = new List<Doctor>();

            using (var ctx = new SocietyContext())
            {
                var a =
                    ctx.Doctors.Where(s => s.SpecialistId == id)
                        .Select(c => new {c.Id, c.Image, c.Name, c.Degree, c.Fees});
                ;
                foreach (var k in a)
                {
                    Doctor doc = new Doctor();
                    doc.Id = k.Id;
                    doc.Name = k.Name;
                    doc.Degree = k.Degree;
                    doc.Image = k.Image;
                    doc.Fees = k.Fees;
                    doctor.Add(doc);
                }
            }
            return Json(doctor);
        }

        public ActionResult Appointment()
        {
            ViewBag.Appointment = "active";
            List<Specialist> categories;


            using (var ctx = new SocietyContext())
            {

                categories = ctx.Specialists.ToList();
            }

            ViewBag.Category = categories;
            return View();
        }

        public JsonResult GetAllDoctorNameById(int id)
        {

            List<Doctor> doctors = new List<Doctor>();

            using (var ctx = new SocietyContext())
            {
                var a = ctx.Doctors.Where(s => s.SpecialistId == id);
                foreach (var k in a)
                {
                    Doctor doc = new Doctor();
                    doc.Id = k.Id;
                    doc.Name = k.Name;
                    doctors.Add(doc);
                }
            }
            return Json(doctors);
        }

        [HttpPost]
        public ActionResult Appointment(Appointment patientAppointment)
        {

            ViewBag.Appointment = "active";
            patientAppointment.PatientId = Convert.ToInt32(Session["SocietyId"]);
            List<Specialist> specialists;
            using (var db = new SocietyContext())
            {
                db.Appointments.Add(patientAppointment);
                db.SaveChanges();
                specialists = db.Specialists.ToList();
            }
            ViewBag.Category = specialists;
            ViewBag.Error = '1';
            return View();
        }

        public ActionResult AppointmentNotification()
        {
            ViewBag.AppointmentNotification = "active";
            int id = (int) Session["SocietyId"];

            List<AppointmentVeiw> appointmentList = new List<AppointmentVeiw>();
            using (var db = new SocietyContext())
            {
                ViewBag.Patient = db.SocietyRegistrations.FirstOrDefault(r => r.Id == id);
                var q = (from a in db.Appointments
                    join p in db.SocietyRegistrations on a.PatientId equals p.Id
                    join d in db.Doctors on a.DoctorId equals d.Id
                    join s in db.Specialists on a.SpecialistId equals s.Id
                    where a.PatientId == id
                    select new
                    {
                        appointmentDate = a.Date,
                        doctorName = d.Name,
                        docdesignation = d.Degree,
                        Special = s.Category,
                        aApproval = a.IsApproved
                    });
                foreach (var d in q)
                {
                    AppointmentVeiw ap = new AppointmentVeiw();
                    ap.Date = d.appointmentDate;
                    ap.DoctorName = d.doctorName;
                    ap.Designation = d.docdesignation;
                    ap.Specialist = d.Special;
                    ap.Approval = d.aApproval;
                    appointmentList.Add(ap);
                }
            }
            return View(appointmentList);
        }

//*************************************************  Security  ******************************************************//
        public ActionResult Security()
        {
            ViewBag.Security = "active";
            List<Security> selist = new List<Security>();
            using (var db = new SocietyContext())
            {
                selist = db.Securities.ToList();
            }
            ViewBag.Security = selist;
            return View();
        }

        //*************************************************Admin********************************************************//
        public ActionResult CustomersOrderlist()
        {
            ViewBag.CustomersOrderlist = "active";
            List<Orderview> orders = new List<Orderview>();
            using (var db = new SocietyContext())
            {
                var q = (from a in db.Orders
                    join p in db.SocietyRegistrations on a.CustomerId equals p.Id
                    where a.CustomerId == p.Id
                    select new
                    {
                        orderDate = a.Date,
                        customerName = p.Name,
                        Productcode = a.ProductCode,
                        totalPrice = a.TotalPrice,
                        productAmount = a.ProductAmount,
                        price = a.Price,
                        Contact = p.Mobile,
                        address = p.Address
                    });
                foreach (var d in q)
                {
                    Orderview ap = new Orderview();
                    ap.CustomerName = d.customerName;
                    ap.Address = d.address;
                    ap.Mobile = d.Contact;
                    ap.ProductAmount = d.productAmount;
                    ap.ProductCode = d.Productcode;
                    ap.Price = d.price;
                    ap.TotalPrice = d.totalPrice;
                    ap.Date = d.orderDate;
                    orders.Add(ap);
                }
            }
            return View(orders);
        }
        public ActionResult DoctorAppointment()
        {
            ViewBag.DoctorAppointment = "active";
            return View();
        }
        
        public JsonResult GetAppointments(string date)
        {
            List<PatientAppoitmenntview> pt = new List<PatientAppoitmenntview>();

            using (var ctx = new SocietyContext())
            {
                var data = (from a in ctx.Appointments
                            join p in ctx.SocietyRegistrations on a.PatientId equals p.Id
                            join d in ctx.Doctors on a.DoctorId equals d.Id
                            join s in ctx.Specialists on d.SpecialistId equals  s.Id
                            where a.Date == date
                            select new
                            {
                                pName = p.Name,
                                dName = d.Name,
                                ddesig = d.Degree,
                                special=s.Category
                            });
                foreach (var dc in data)
                {
                    PatientAppoitmenntview p = new PatientAppoitmenntview();
                    
                    p.PatientName = dc.pName;
                    p.DoctorName = dc.dName;
                    p.Designation = dc.ddesig;
                    p.Specialist = dc.special;
                    pt.Add(p);
                }

            }

            return Json(pt);
        }
       
    }
}
