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
    public class SecurityController : Controller
    {
        private SocietyContext db = new SocietyContext();

        // GET: /Security/
        public ActionResult SecuIndex()
        {
            ViewBag.Index = "active";
            return View(db.Securities.ToList());
        }

        // GET: /Security/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Security/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Moobile,Image")] Security security, HttpPostedFileBase Image)
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
                        security.Image = "Picture/" + fileName;
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                }
                security.Name = security.Name;
                security.Email = security.Email;
                security.Moobile = security.Moobile;
                db.Securities.Add(security);
                db.SaveChanges();

            }

            return RedirectToAction("Create", new { message = "Security added Successfully" });
        }

        // GET: /Security/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Security security = db.Securities.Find(id);
            if (security == null)
            {
                return HttpNotFound();
            }
            return View(security);
        }

        // POST: /Security/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Moobile,Image")] Security security, HttpPostedFileBase Image, string pastImage)
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
                        security.Image = "Picture/" + fileName;
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                }
                else
                {
                    security.Image = pastImage;
                }
                db.Entry(security).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SecuIndex");
            }
            return View(security);
        }

        // GET: /Security/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Security security = db.Securities.Find(id);
            if (security == null)
            {
                return HttpNotFound();
            }
            return View(security);
        }

        // POST: /Security/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Security security = db.Securities.Find(id);
            db.Securities.Remove(security);
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
