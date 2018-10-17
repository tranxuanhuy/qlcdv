using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using qlcdvien.Models;

namespace qlcdvien.Controllers
{
    public class HoatdongCongdoansController : Controller
    {
        private Model1 db = new Model1();

        // GET: HoatdongCongdoans
        public ActionResult Index(string firstdate,string enddate)
        {
            
            var hoatdongCongdoans = db.HoatdongCongdoans.Include(h => h.AspNetUser);            
            if (!String.IsNullOrEmpty(firstdate)&& !String.IsNullOrEmpty(enddate)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                DateTime d1 = DateTime.ParseExact(firstdate, "dd/MM/yyyy", null);
                DateTime d2 = DateTime.ParseExact(enddate, "dd/MM/yyyy", null);
                hoatdongCongdoans = hoatdongCongdoans
       .Where(n => n.ngaydang >= d1)
       .Where(n => n.ngaydang <=d2);
            
        }
            return View(hoatdongCongdoans.ToList());
        }

        // GET: HoatdongCongdoans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoatdongCongdoan hoatdongCongdoan = db.HoatdongCongdoans.Find(id);
            if (hoatdongCongdoan == null)
            {
                return HttpNotFound();
            }
            return View(hoatdongCongdoan);
        }

        // GET: HoatdongCongdoans/Create
        public ActionResult Create()
        {
            ViewBag.nguoidang_id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: HoatdongCongdoans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Hoatdong_Id,NoiDung,Tieude,Anhhoatdong,ngaydang,nguoidang_id,daDuyet")] HoatdongCongdoan hoatdongCongdoan)
        {
            if (ModelState.IsValid)
            {
                db.HoatdongCongdoans.Add(hoatdongCongdoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.nguoidang_id = new SelectList(db.AspNetUsers, "Id", "Email", hoatdongCongdoan.nguoidang_id);
            return View(hoatdongCongdoan);
        }

        // GET: HoatdongCongdoans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoatdongCongdoan hoatdongCongdoan = db.HoatdongCongdoans.Find(id);
            if (hoatdongCongdoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.nguoidang_id = new SelectList(db.AspNetUsers, "Id", "Email", hoatdongCongdoan.nguoidang_id);
            return View(hoatdongCongdoan);
        }

        // POST: HoatdongCongdoans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Hoatdong_Id,NoiDung,Tieude,Anhhoatdong,ngaydang,nguoidang_id,daDuyet")] HoatdongCongdoan hoatdongCongdoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoatdongCongdoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nguoidang_id = new SelectList(db.AspNetUsers, "Id", "Email", hoatdongCongdoan.nguoidang_id);
            return View(hoatdongCongdoan);
        }

        // GET: HoatdongCongdoans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoatdongCongdoan hoatdongCongdoan = db.HoatdongCongdoans.Find(id);
            if (hoatdongCongdoan == null)
            {
                return HttpNotFound();
            }
            return View(hoatdongCongdoan);
        }

        // POST: HoatdongCongdoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoatdongCongdoan hoatdongCongdoan = db.HoatdongCongdoans.Find(id);
            db.HoatdongCongdoans.Remove(hoatdongCongdoan);
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
