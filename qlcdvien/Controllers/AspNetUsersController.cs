using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using qlcdvien.Models;

namespace qlcdvien.Controllers
{
    public class AspNetUsersController : Controller
    {
        private Model1 db = new Model1();

        // GET: AspNetUsers
        public ActionResult Index()
        {
            var aspNetUsers = db.AspNetUsers.Include(a => a.CapCongDoan);
            return View(aspNetUsers.ToList());
        }

        // GET: AspNetUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // GET: AspNetUsers/Create
        public ActionResult Create()
        {
            ViewBag.capcongdoan_id = new SelectList(db.CapCongDoans, "Capcongdoan_id", "name");
            return View();
        }

        // POST: AspNetUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,name,DOB,sex,noisinh,quequan,HKTT,tamtru,chucvuChinhquyen,chucvuDoanthe,vanhoa,chuyenmon,hocvi,hocham,tinhoc,ngoaingu,imageurl,tongiao,dantoc,cmnd,noicapcmnd,ngaycapcmnd,truongcongdoanbophan,truonglopdaotao,nangkhieu,hanche,capcongdoan_id")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(aspNetUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.capcongdoan_id = new SelectList(db.CapCongDoans, "Capcongdoan_id", "name", aspNetUser.capcongdoan_id);
            return View(aspNetUser);
        }

        // GET: AspNetUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.capcongdoan_id = new SelectList(db.CapCongDoans, "Capcongdoan_id", "name", aspNetUser.capcongdoan_id);
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,name,DOB,sex,noisinh,quequan,HKTT,tamtru,chucvuChinhquyen,chucvuDoanthe,vanhoa,chuyenmon,hocvi,hocham,tinhoc,ngoaingu,imageurl,tongiao,dantoc,cmnd,noicapcmnd,ngaycapcmnd,truongcongdoanbophan,truonglopdaotao,nangkhieu,hanche,capcongdoan_id")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.capcongdoan_id = new SelectList(db.CapCongDoans, "Capcongdoan_id", "name", aspNetUser.capcongdoan_id);
            return View(aspNetUser);
        }

        // GET: AspNetUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUser);
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
