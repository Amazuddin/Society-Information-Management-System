using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Society.Context;
using Society.Models;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace Society.Controllers
{
    public class AuthenticationController : Controller
    {
        //
        // GET: /Authentication/
//************************************************ Society Member***********************************************//
        public ActionResult Registration()
        {
            ViewBag.Registration = "active";
            return View();
        }
        [HttpPost]
        public ActionResult Registration(SocietyRegistration register, HttpPostedFileBase Image)
        {
            using (var db = new SocietyContext())
            {
                if (Image != null && Image.ContentLength > 0)
                {

                    try
                    {
                        string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(Image.FileName);
                        string uploadUrl = Server.MapPath("~/Picture");
                        Image.SaveAs(Path.Combine(uploadUrl, fileName));
                        register.Image = "Picture/" + fileName;
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = "ERROR:" + ex.Message.ToString();
                    }
                }
                register.Email = register.Email;
                var e = db.SocietyRegistrations.Where(c => c.Email == register.Email).ToList().Count;
                if (e == 0)
                {
                    register.Password = register.Password;
                    register.Name = register.Name;
                    register.Address = register.Address;
                    register.Mobile = register.Mobile;
                    db.SocietyRegistrations.Add(register);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.Error = "Already Registered";
                }
            }
            ViewBag.Message = '1';
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Login = "active";
            return View();
        }
        [HttpPost]
        public ActionResult Login(LogIn logIn)
        {
            string password = logIn.Password;
            string email = logIn.Email;
            using (var ctx = new SocietyContext())
            {

                var p = ctx.SocietyRegistrations.Where(c => c.Email == email && c.Password == password).Select(c => new { c.Id, c.Email }).ToList();
                if (p.Any())
                {
                    foreach (var k in p)
                    {
                        Session["SocietyId"] = k.Id;
                        Session["SocietyEmail"] = k.Email;

                    }
                    return RedirectToAction("Index", "Society");
                }
                else
                {
                    ViewBag.Error = "Login Failed";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["SocietyId"] = null;
            Session["SocietyEmail"] = null;
            return RedirectToAction("Login", "Authentication");
        }

      

//****************************************************Admin***************************************************//


        public ActionResult Admin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Admin(Admin admin)
        {
            string password = admin.Password;
            string email = admin.Email;
            using (var ctx = new SocietyContext())
            {

                var p = ctx.Admins.Where(c => c.Email == email && c.Password == password).Select(c => new { c.Id, c.Email }).ToList();
                if (p.Any())
                {
                    foreach (var k in p)
                    {
                        Session["AdminId"] = k.Id;
                        Session["AdminEmail"] = k.Email;

                    }
                    return RedirectToAction("Index", "Society");
                }
                else
                {
                    ViewBag.Error = "Login Failed";
                }



            }
            return View();
        }
        public ActionResult AdminLogout()
        {
            Session["AdminId"] = null;
            Session["AdminEmail"] = null;
            return RedirectToAction("Admin", "Authentication");
        }
	}
}