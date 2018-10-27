﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using qlcdvien.Models;
using PagedList;
namespace qlcdvien.Controllers
{
    public class HoatdongCongdoansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HoatdongCongdoans
        public ActionResult Index(string daterangepicker, int? page, string currentFilter)
        {
            daterangepicker = Request["date-range-picker"];
            if (daterangepicker != null)
            {
                page = 1;
            }
            else
            {
                daterangepicker = currentFilter;
            }

            

            
            ViewBag.CurrentFilter = daterangepicker;

            var hoatdongCongdoans = db.HoatdongCongdoans.Include(i => i.ApplicationUser);
            if (!String.IsNullOrEmpty(daterangepicker)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                DateTime d1 = DateTime.ParseExact(daterangepicker.Split(' ').First(), "MM/dd/yyyy", null);
                DateTime d2 = DateTime.ParseExact(daterangepicker.Split(' ').Last(), "MM/dd/yyyy", null).AddDays(1);
                hoatdongCongdoans = hoatdongCongdoans
       .Where(n => n.ngaydang >= d1)
       .Where(n => n.ngaydang <=d2);
            
        }
            //return View(hoatdongCongdoans.OrderByDescending(x=>x.ngaydang).ToList());
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(hoatdongCongdoans.OrderByDescending(x => x.ngaydang).ToPagedList(pageNumber, pageSize));
        }

        // GET: HoatdongCongdoans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoatdongCongdoan hoatdongCongdoan = db.HoatdongCongdoans.Include(i => i.ApplicationUser).SingleOrDefault(x => x.Hoatdong_Id == id);
            if (hoatdongCongdoan == null)
            {
                return HttpNotFound();
            }
            return View(hoatdongCongdoan);
        }

        // GET: HoatdongCongdoans/Create
        public ActionResult Create()
        {
            
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
                if (hoatdongCongdoan.NoiDung != null)
                    hoatdongCongdoan.NoiDung = WebUtility.HtmlDecode(hoatdongCongdoan.NoiDung);

                hoatdongCongdoan.nguoidang_id = System.Web.HttpContext.Current.User.Identity.GetUserId();
                hoatdongCongdoan.ngaydang = DateTime.Now;
                db.HoatdongCongdoans.Add(hoatdongCongdoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View(hoatdongCongdoan);
        }

        // GET: HoatdongCongdoans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoatdongCongdoan hoatdongCongdoan = db.HoatdongCongdoans.Include(i => i.ApplicationUser).SingleOrDefault(x => x.Hoatdong_Id == id);
            if (hoatdongCongdoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.nguoidang_id = new SelectList(db.Users.ToList(), "Id", "name", hoatdongCongdoan.ApplicationUser.Id);
            return View(hoatdongCongdoan);
        }

        // POST: HoatdongCongdoans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Hoatdong_Id,NoiDung,Tieude,Anhhoatdong,ngaydang,daDuyet")] HoatdongCongdoan hoatdongCongdoan,string nguoidang_id="")
        {
            if (ModelState.IsValid)
            {
                if (hoatdongCongdoan.NoiDung != null)
                    hoatdongCongdoan.NoiDung = WebUtility.HtmlDecode(hoatdongCongdoan.NoiDung);

                hoatdongCongdoan.nguoidang_id = nguoidang_id;
                db.Entry(hoatdongCongdoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nguoidang_id = new SelectList(db.Users.ToList(), "Id", "name", db.HoatdongCongdoans.Find(hoatdongCongdoan.Hoatdong_Id).ApplicationUser.Id);
            return View(hoatdongCongdoan);
        }

        // GET: HoatdongCongdoans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoatdongCongdoan hoatdongCongdoan = db.HoatdongCongdoans.Include(i => i.ApplicationUser).SingleOrDefault(x => x.Hoatdong_Id == id);
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
