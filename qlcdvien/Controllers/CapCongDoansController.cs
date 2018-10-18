using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;
using qlcdvien.Models;

namespace qlcdvien.Controllers
{
    public class CapCongDoansController : Controller
    {
        private Model1 db = new Model1();

        // GET: CapCongDoans
        public ActionResult Index()
        {
            return View(db.CapCongDoans.ToList());
        }
        public ActionResult Treeview()
        {
            return View();
        }
        [WebMethod]
        public string GetChartData()
        {

            List<object> chartData = new List<object>();
            var content = db.CapCongDoans.Select(s => new
            {
                s.Capcongdoan_id,
                s.name,
                s.parent
            });
            foreach (var item in content)
            {
                CapCongDoan fh = db.CapCongDoans.Find(item.parent);
                string currentName1 = fh == null ? "" : db.Entry(fh).Property(u => u.name).CurrentValue;
                chartData.Add(new object[]
                    {
                        item.name,currentName1,""
                    });
            }


            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(chartData);
            return json;
        }
        // GET: CapCongDoans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CapCongDoan capCongDoan = db.CapCongDoans.Find(id);
            if (capCongDoan == null)
            {
                return HttpNotFound();
            }
            return View(capCongDoan);
        }

        // GET: CapCongDoans/Create
        public ActionResult Create()
        {
            var categories = from c in db.CapCongDoans select c;
            ViewBag.categoryID = new SelectList(categories, "Capcongdoan_id", "name"); // danh sách Category

            return View();
        }

        // POST: CapCongDoans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Capcongdoan_id,name")] CapCongDoan capCongDoan, int categoryID = 0)
        {
            capCongDoan.parent = categoryID;
            if (ModelState.IsValid)
            {
                db.CapCongDoans.Add(capCongDoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(capCongDoan);
        }

        // GET: CapCongDoans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CapCongDoan capCongDoan = db.CapCongDoans.Find(id);
            if (capCongDoan == null)
            {
                return HttpNotFound();
            }
            return View(capCongDoan);
        }

        // POST: CapCongDoans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Capcongdoan_id,name,parent")] CapCongDoan capCongDoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(capCongDoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(capCongDoan);
        }

        // GET: CapCongDoans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CapCongDoan capCongDoan = db.CapCongDoans.Find(id);
            if (capCongDoan == null)
            {
                return HttpNotFound();
            }
            return View(capCongDoan);
        }

        // POST: CapCongDoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CapCongDoan capCongDoan = db.CapCongDoans.Find(id);
            db.CapCongDoans.Remove(capCongDoan);
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
