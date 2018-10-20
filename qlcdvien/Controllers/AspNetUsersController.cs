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
    public class ApplicationUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicationUsers
        public ActionResult Index()
        {
            var ApplicationUsers = db.Users.Include(a => a.CapCongDoan);
            return View(ApplicationUsers.ToList());
        }
        public ActionResult Index1()
        {
            var ApplicationUsers = db.Users.Include(a => a.CapCongDoan);
            return View(ApplicationUsers.ToList());
        }
        // GET: ApplicationUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser ApplicationUser = db.Users.Find(id);
            if (ApplicationUser == null)
            {
                return HttpNotFound();
            }
            return View(ApplicationUser);
        }

        // GET: ApplicationUsers/Create
        public ActionResult Create()
        {
            ViewBag.capcongdoan_id = new SelectList(db.CapCongDoans, "Capcongdoan_id", "name");
            return View();
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,name,DOB,sex,noisinh,quequan,HKTT,tamtru,chucvuChinhquyen,chucvuDoanthe,vanhoa,chuyenmon,hocvi,hocham,tinhoc,ngoaingu,imageurl,tongiao,dantoc,cmnd,noicapcmnd,ngaycapcmnd,truongcongdoanbophan,truonglopdaotao,nangkhieu,hanche,capcongdoan_id")] ApplicationUser ApplicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.ToList().Add(ApplicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.capcongdoan_id = new SelectList(db.CapCongDoans, "Capcongdoan_id", "name", ApplicationUser.capcongdoan_id);
            return View(ApplicationUser);
        }

        // GET: ApplicationUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser ApplicationUser = db.Users.Find(id);
            if (ApplicationUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.capcongdoan_id = new SelectList(db.CapCongDoans, "Capcongdoan_id", "name", ApplicationUser.capcongdoan_id);
            return View(ApplicationUser);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,name,DOB,sex,noisinh,quequan,HKTT,tamtru,chucvuChinhquyen,chucvuDoanthe,vanhoa,chuyenmon,hocvi,hocham,tinhoc,ngoaingu,imageurl,tongiao,dantoc,cmnd,noicapcmnd,ngaycapcmnd,truongcongdoanbophan,truonglopdaotao,nangkhieu,hanche,capcongdoan_id")] ApplicationUser ApplicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ApplicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.capcongdoan_id = new SelectList(db.CapCongDoans, "Capcongdoan_id", "name", ApplicationUser.capcongdoan_id);
            return View(ApplicationUser);
        }

        // GET: ApplicationUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser ApplicationUser = db.Users.Find(id);
            if (ApplicationUser == null)
            {
                return HttpNotFound();
            }
            return View(ApplicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser ApplicationUser = db.Users.Find(id);
            db.Users.ToList().Remove(ApplicationUser);
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
