using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using qlcdvien.Models;

namespace qlcdvien.Controllers
{
    [Authorize]
    public class KhenthuongsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult DownloadFile(string file = "")
        {

            file = HostingEnvironment.MapPath("~" + file);

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = Path.GetFileName(file);
            return File(file, contentType, fileName);

        }
        // GET: Khenthuongs
        public ActionResult Index()
        {
            //var khenthuongs = db.Khenthuongs.Include(k => k.ApplicationUser).Include(k => k.CapCongDoan);
            var khenthuongs = db.Khenthuongs.Include(k => k.ApplicationUser);
            return View(khenthuongs.ToList());
        }

        // GET: Khenthuongs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Khenthuong khenthuong = db.Khenthuongs.Find(id);
            if (khenthuong == null)
            {
                return HttpNotFound();
            }
            return View(khenthuong);
        }

        // GET: Khenthuongs/Create
        public ActionResult Create()
        {
            ViewBag.cdv_id = new SelectList(db.Users, "Id", "name");
            ViewBag.tochuc_id = new SelectList(db.CapCongDoans, "Capcongdoan_id", "name");
            return View();
        }

        // POST: Khenthuongs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Khenthuong_id,ngaykhenthuong,hinhthuc,soquyetdinh,capkhenthuong,cdv_id,tochuc_id,scanurl,doituongKhenthuong")] Khenthuong khenthuong, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                  
                    
                    //nap anh moi
                    //string filename = Path.GetFileNameWithoutExtension(uploadImage.FileName);
                    string extension = Path.GetExtension(uploadImage.FileName);
                    string filename = khenthuong.Khenthuong_id + DateTime.Now.ToString("yymmssfff") + extension;
                    khenthuong.scanurl = "/Image/Khenthuong/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Image/Khenthuong/"), filename);
                    uploadImage.SaveAs(filename);
                }

                db.Khenthuongs.Add(khenthuong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cdv_id = new SelectList(db.Users, "Id", "name", khenthuong.cdv_id);
            //ViewBag.tochuc_id = new SelectList(db.CapCongDoans, "Capcongdoan_id", "name", khenthuong.tochuc_id);
            return View(khenthuong);
        }

        // GET: Khenthuongs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Khenthuong khenthuong = db.Khenthuongs.Find(id);
            if (khenthuong == null)
            {
                return HttpNotFound();
            }
            ViewBag.cdv_id = new SelectList(db.Users, "Id", "name", khenthuong.cdv_id);
            //ViewBag.tochuc_id = new SelectList(db.CapCongDoans.OrderBy(s=>s.motaphancap), "Capcongdoan_id", "namephancap", khenthuong.tochuc_id);
            return View(khenthuong);
        }

        // POST: Khenthuongs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Khenthuong_id,ngaykhenthuong,hinhthuc,soquyetdinh,capkhenthuong,cdv_id,tochuc_id,scanurl,doituongKhenthuong")] Khenthuong khenthuong, HttpPostedFileBase uploadImage)
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
                    string filename = khenthuong.Khenthuong_id + DateTime.Now.ToString("yymmssfff") + extension;
                    khenthuong.scanurl = "/Image/Khenthuong/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Image/Khenthuong/"), filename);
                    uploadImage.SaveAs(filename);
                }
                else
                {
                    khenthuong.scanurl = Request["oldurl"];
                }
                db.Entry(khenthuong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cdv_id = new SelectList(db.Users, "Id", "name", khenthuong.cdv_id);
            //ViewBag.tochuc_id = new SelectList(db.CapCongDoans, "Capcongdoan_id", "name", khenthuong.tochuc_id);
            return View(khenthuong);
        }

        // GET: Khenthuongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Khenthuong khenthuong = db.Khenthuongs.Find(id);
            if (khenthuong == null)
            {
                return HttpNotFound();
            }
            return View(khenthuong);
        }

        // POST: Khenthuongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Khenthuong khenthuong = db.Khenthuongs.Find(id);

            //xoa anh cu
            var filePath = Server.MapPath(khenthuong.scanurl);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            db.Khenthuongs.Remove(khenthuong);
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
