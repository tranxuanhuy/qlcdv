using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using qlcdvien.Models;

namespace qlcdvien.Controllers
{
    [Authorize]
    public class YkiensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Doixuly()
        {
            var ykiens = db.Ykiens.Where(s => s.daDuyet != true).Include(y => y.ApplicationUser);
            return View(ykiens.ToList());
        }
        // GET: Ykiens
        public ActionResult Index()
        {
            var ykiens = db.Ykiens.Where(s => s.daDuyet == true).Include(y => y.ApplicationUser);
            return View(ykiens.ToList());
        }

        // GET: Ykiens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ykien ykien = db.Ykiens.Find(id);
            if (ykien == null)
            {
                return HttpNotFound();
            }
            return View(ykien);
        }

        // GET: Ykiens/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Ykiens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Hoatdong_Id,NoiDung,Tieude,ngaydang,nguoidang_id,daDuyet")] Ykien ykien)
        {
            if (ModelState.IsValid)
            {
                ykien.nguoidang_id = System.Web.HttpContext.Current.User.Identity.GetUserId();
                ykien.ngaydang = DateTime.Now;
                ykien.daDuyet = false;
                db.Ykiens.Add(ykien);
                db.SaveChanges(System.Web.HttpContext.Current.User.Identity.GetUserName());
                return RedirectToAction("Index");
            }

            
            return View(ykien);
        }

        // GET: Ykiens/Edit/5
        [Authorize(Roles = "admin,mod")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ykien ykien = db.Ykiens.Find(id);
            if (ykien == null)
            {
                return HttpNotFound();
            }
            //phan quyen mod cap tren tro len co quyen
            var loggedInUser = System.Web.HttpContext.Current.User.Identity.GetUserId();

            if ((!db.CapCongDoans.Find(db.Users.Find(db.Ykiens.Find(id).nguoidang_id).capcongdoan_id).motaphancap.Contains(db.Users.Include(x => x.CapCongDoan).SingleOrDefault(x => x.Id == loggedInUser).CapCongDoan.motaphancap) && User.IsInRole("mod")))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(ykien);
        }

        // POST: Ykiens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Hoatdong_Id,NoiDung,Tieude,ngaydang,daDuyet")] Ykien ykien)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(ykien).State = EntityState.Modified;
                db.Entry(ykien).Property("nguoidang_id").IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(ykien);
        }

        // GET: Ykiens/Delete/5
        [Authorize(Roles = "admin,mod")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ykien ykien = db.Ykiens.Find(id);
            if (ykien == null)
            {
                return HttpNotFound();
            }
            //phan quyen mod cap tren tro len co quyen
            var loggedInUser = System.Web.HttpContext.Current.User.Identity.GetUserId();

            if ((!db.CapCongDoans.Find(db.Users.Find(db.Ykiens.Find(id).nguoidang_id).capcongdoan_id).motaphancap.Contains(db.Users.Include(x => x.CapCongDoan).SingleOrDefault(x => x.Id == loggedInUser).CapCongDoan.motaphancap) && User.IsInRole("mod")))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View(ykien);
        }

        // POST: Ykiens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ykien ykien = db.Ykiens.Find(id);
            db.Ykiens.Remove(ykien);
            db.SaveChanges(System.Web.HttpContext.Current.User.Identity.GetUserName());
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
