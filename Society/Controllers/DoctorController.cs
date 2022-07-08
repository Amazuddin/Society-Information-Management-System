using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Society.Models;
using Society.Context;

namespace Society.Controllers
{
    public class DoctorController : Controller
    {
        private SocietyContext db = new SocietyContext();

        // GET: /Doctor/
        public ActionResult DocIndex()
        {
            ViewBag.DocIndex = "active";
            return View(db.Doctors.ToList());
        }

        // GET: /Doctor/Create
        public ActionResult Create()
        {
            ViewBag.Doc = db.Specialists.ToList();
            return View();
        }

        // POST: /Doctor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Image,Degree,Fees,SpecialistId")] Doctor doctor, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.ContentLength > 0)
                {

                    try
                    {
                        string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(Image.FileName);
                        string uploadUrl = Server.MapPath("~/Picture");
                        Image.SaveAs(Path.Combine(uploadUrl, fileName));
                        doctor.Image = "Picture/" + fileName;
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                }
                doctor.Name = doctor.Name;
                doctor.SpecialistId = doctor.SpecialistId;
                doctor.Degree = doctor.Degree;
                doctor.Fees = doctor.Fees;
                db.Doctors.Add(doctor);
                db.SaveChanges();

            }

            return RedirectToAction("Create", new { message = "Doctor added Successfully" });
        }

        // GET: /Doctor/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Doc = db.Specialists.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
           
            return View(doctor);
        }

        // POST: /Doctor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Image,Degree,Fees,SpecialistId")] Doctor doctor, HttpPostedFileBase Image, string pastImage)
        {
            doctor.Image = pastImage;
            if (Image != null && Image.ContentLength > 0)
            {
                string fullPath = Request.MapPath("~/" + pastImage);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                try
                {
                    string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(Image.FileName);
                    string uploadUrl = Server.MapPath("~/Picture");
                    Image.SaveAs(Path.Combine(uploadUrl, fileName));
                    doctor.Image = "Picture/" + fileName;
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "ERROR:" + ex.Message.ToString();
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DocIndex");
            }
            return View(doctor);
        }

        // GET: /Doctor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: /Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doctor doctor = db.Doctors.Find(id);
            db.Doctors.Remove(doctor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
