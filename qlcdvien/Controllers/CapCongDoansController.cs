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
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CapCongDoans
        public ActionResult Index()
        {
            return View(db.CapCongDoans.ToList());
        }
        
        public ActionResult IndexTreeViewWithPersonList()
        {
            TempData["data"] = "[{'text':'Parent1','nodes':[{'text':'Child1','nodes':[{'text':'Grandchild1'},{'text':'Grandchild2'}]},{'text':'Child2'}]},{'text':'Parent2'},{'text':'Parent3'},{'text':'Parent4'},{'text':'Parent5'}]";
            return View();
            
        }
        public string GetAllCategoriesForTree()
        {
            List<CapCongDoan> categories = new List<CapCongDoan>();
            categories = db.CapCongDoans.ToList();

            
            {
            
                List<TreeNode> headerTree = FillRecursive(categories, 0);

                #region BindingHeaderMenus  

                string root_li = "[{'text':'Parent1','nodes':[{'text':'Child1','nodes':[{'text':'Grandchild1'},{'text':'Grandchild2'}]},{'text':'Child2'}]},{'text':'Parent2'},{'text':'Parent3'},{'text':'Parent4'},{'text':'Parent5'}]";
                string down1_names = string.Empty;
                string down2_names = string.Empty;

                //foreach (var item in headerTree)
                //{
                //    root_li += "{'text':'" + item.CategoryName + "'";

                //    down1_names = "";
                //    foreach (var down1 in item.Children)
                //    {
                //        down2_names = "";
                //        foreach (var down2 in down1.Children)
                //        {
                //            down2_names += "<li><a href=\"/Product/ListProduct?cat=" + down2.CategoryId + "\">" + down2.CategoryName + "</a></li>";
                //        }
                //        down1_names += "<div class=\"col-md-2 col-sm-6\">"
                //                        + "<h3 class=\"mega-menu-heading\"><a href=\"/Product/ListProduct?cat=" + down1.CategoryId + "\">" + down1.CategoryName + "</a></h3>"
                //                        + "<ul class=\"list-unstyled style-list\">"
                //                        + down2_names
                //                        + "</ul>"
                //                        + "</div>";
                //    }
                //    root_li += "<ul class=\"dropdown-menu\">"
                //                + "<li>"
                //                    + "<div class=\"mega-menu-content\">"
                //                        + "<div class=\"container\">"
                //                            + "<div class=\"row\">"
                //                                + down1_names
                //                            + "</div>"
                //                        + "</div>"
                //                    + "<div>"
                //                + "</li>"
                //                + "</ul>"
                //            + "</li>";
                //}
                #endregion

                return root_li;
            }
            return "Record Not Found!!";
        }

        private static List<TreeNode> FillRecursive(List<CapCongDoan> flatObjects, int? parentId = null)
        {
            return flatObjects.Where(x => x.parent.Equals(parentId)).Select(item => new TreeNode
            {
                CategoryName = item.name,
                CategoryId = item.Capcongdoan_id,
                Children = FillRecursive(flatObjects, item.Capcongdoan_id)
            }).ToList();
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
            ViewBag.CapCongDoanID = new SelectList(categories, "Capcongdoan_id", "name"); // danh sách CapCongDoan

            return View();
        }

        // POST: CapCongDoans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Capcongdoan_id,name")] CapCongDoan capCongDoan, int CapCongDoanID = 0)
        {
            capCongDoan.parent = CapCongDoanID;
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
