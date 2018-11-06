using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using qlcdvien.Models;

namespace qlcdvien.Controllers
{
    public class KhenthuongtapthesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Khenthuongtapthes
        public ActionResult Index()
        {
            var khenthuongtapthes = db.Khenthuongtapthes.Include(k => k.CapCongDoan);
            return View(khenthuongtapthes.ToList());
        }

        // GET: Khenthuongtapthes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Khenthuongtapthe khenthuongtapthe = db.Khenthuongtapthes.Find(id);
            if (khenthuongtapthe == null)
            {
                return HttpNotFound();
            }
            return View(khenthuongtapthe);
        }

        // GET: Khenthuongtapthes/Create
        public ActionResult Create()
        {
            ViewBag.tochuc_id = new SelectList(db.CapCongDoans.OrderBy(s=>s.motaphancap), "Capcongdoan_id", "namephancap");
            return View();
        }

        // POST: Khenthuongtapthes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Khenthuong_id,ngaykhenthuong,hinhthuc,soquyetdinh,capkhenthuong,tochuc_id,scanurl")] Khenthuongtapthe khenthuongtapthe, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {


                    //nap anh moi
                    //string filename = Path.GetFileNameWithoutExtension(uploadImage.FileName);
                    string extension = Path.GetExtension(uploadImage.FileName);
                    string filename = khenthuongtapthe.Khenthuong_id + DateTime.Now.ToString("yymmssfff") + extension;
                    khenthuongtapthe.scanurl = "/Image/Khenthuong/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Image/Khenthuong/"), filename);
                    uploadImage.SaveAs(filename);
                }

                db.Khenthuongtapthes.Add(khenthuongtapthe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tochuc_id = new SelectList(db.CapCongDoans, "Capcongdoan_id", "name", khenthuongtapthe.tochuc_id);
            return View(khenthuongtapthe);
        }

        // GET: Khenthuongtapthes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Khenthuongtapthe khenthuongtapthe = db.Khenthuongtapthes.Find(id);
            if (khenthuongtapthe == null)
            {
                return HttpNotFound();
            }
            ViewBag.tochuc_id = new SelectList(db.CapCongDoans.OrderBy(s => s.motaphancap), "Capcongdoan_id", "namephancap", khenthuongtapthe.tochuc_id);
            return View(khenthuongtapthe);
        }

        // POST: Khenthuongtapthes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Khenthuong_id,ngaykhenthuong,hinhthuc,soquyetdinh,capkhenthuong,tochuc_id,scanurl")] Khenthuongtapthe khenthuongtapthe, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    //xoa anh cu

                    var filePath = Server.MapPath(Request["oldurl"]);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    //nap anh moi
                    //string filename = Path.GetFileNameWithoutExtension(uploadImage.FileName);
                    string extension = Path.GetExtension(uploadImage.FileName);
                    string filename = khenthuongtapthe.Khenthuong_id + DateTime.Now.ToString("yymmssfff") + extension;
                    khenthuongtapthe.scanurl = "/Image/Khenthuong/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Image/Khenthuong/"), filename);
                    uploadImage.SaveAs(filename);
                }
                else
                {
                    khenthuongtapthe.scanurl = Request["oldurl"];
                }
                db.Entry(khenthuongtapthe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tochuc_id = new SelectList(db.CapCongDoans, "Capcongdoan_id", "name", khenthuongtapthe.tochuc_id);
            return View(khenthuongtapthe);
        }

        // GET: Khenthuongtapthes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Khenthuongtapthe khenthuongtapthe = db.Khenthuongtapthes.Find(id);
            if (khenthuongtapthe == null)
            {
                return HttpNotFound();
            }
            return View(khenthuongtapthe);
        }

        // POST: Khenthuongtapthes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Khenthuongtapthe khenthuongtapthe = db.Khenthuongtapthes.Find(id);
            //xoa anh cu
            var filePath = Server.MapPath(khenthuongtapthe.scanurl);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            db.Khenthuongtapthes.Remove(khenthuongtapthe);
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
