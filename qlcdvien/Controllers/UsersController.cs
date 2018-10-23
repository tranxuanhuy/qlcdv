using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using qlcdvien.Models;

namespace qlcdvien.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public UsersController()
        {
        }

        public UsersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index1()
        {
            List<User> users = new List<User>();
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<ApplicationUser, User>();

            });

            IMapper iMapper = config.CreateMapper();
            foreach (var item in UserManager.Users.Include(a => a.CapCongDoan).ToList())
            {
                users.Add(iMapper.Map<ApplicationUser, User>(item));
            }
            return View(users);

           
        }
        // GET: Users
        public ActionResult Index()
        {
            List<User> users = new List<User>();
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<ApplicationUser, User>();

            });

            IMapper iMapper = config.CreateMapper();
            foreach (var item in UserManager.Users.ToList())
            {
                users.Add(iMapper.Map<ApplicationUser, User>(item));
            }
            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<ApplicationUser, User>();

            });

            IMapper iMapper = config.CreateMapper();

            var source = UserManager.FindById(id);



            User user = iMapper.Map<ApplicationUser, User>(source);



            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //// GET: Users/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Users/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "username,name,DOB,sex,noisinh,quequan,HKTT,tamtru,chucvuChinhquyen,chucvuDoanthe,vanhoa,chuyenmon,hocvi,hocham,tinhoc,ngoaingu,imageurl,tongiao,dantoc,cmnd,noicapcmnd,ngaycapcmnd,truongcongdoanbophan,truonglopdaotao,nangkhieu,hanche,capcongdoan_id")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var config = new MapperConfiguration(cfg =>
        //        {

        //            cfg.CreateMap<User, ApplicationUser>();

        //        });

        //        IMapper iMapper = config.CreateMapper();







        //        db.Users.Add(iMapper.Map<User, ApplicationUser>(user));
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(user);
        //}

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<ApplicationUser, User>();

            });

            IMapper iMapper = config.CreateMapper();

            var source = UserManager.FindById(id);



            User user = iMapper.Map<ApplicationUser, User>(source);

            if (user == null)
            {
                return HttpNotFound();
            }
            var categories = from c in new ApplicationDbContext().CapCongDoans select c;
            ViewBag.CapCongDoanID = new SelectList(categories, "Capcongdoan_id", "name",user.CapCongDoan.Capcongdoan_id); // danh sách CapCongDoan
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,DOB,sex,noisinh,quequan,HKTT,tamtru,chucvuChinhquyen,chucvuDoanthe,vanhoa,chuyenmon,hocvi,hocham,tinhoc,ngoaingu,imageurl,tongiao,dantoc,cmnd,noicapcmnd,ngaycapcmnd,truongcongdoanbophan,truonglopdaotao,nangkhieu,hanche,capcongdoan_id")] User model, int CapCongDoanID = 0)
        {
            
            if (ModelState.IsValid)
            {


                //db.Entry(user).State = EntityState.Modified;
                         ApplicationUser user = UserManager.FindById(model.Id);

                user.name = model.name;
                user.DOB = model.DOB;
                user.sex = model.sex;
                user.noisinh = model.noisinh;
                user.quequan = model.quequan;
                user.HKTT = model.HKTT;
                user.tamtru = model.tamtru;
                user.chucvuChinhquyen = model.chucvuChinhquyen;
                user.chucvuDoanthe = model.chucvuDoanthe;
                user.vanhoa = model.vanhoa;
                user.chuyenmon = model.chuyenmon;
                user.hocvi = model.hocvi;
                user.hocham = model.hocham;
                user.tinhoc = model.tinhoc;
                user.ngoaingu = model.ngoaingu;
                user.imageurl = model.imageurl;
                user.tongiao = model.tongiao;
                user.dantoc = model.dantoc;
                user.cmnd = model.cmnd;
                user.noicapcmnd = model.noicapcmnd;
                user.ngaycapcmnd = model.ngaycapcmnd;
                user.truongcongdoanbophan = model.truongcongdoanbophan;
                user.truonglopdaotao = model.truonglopdaotao;
                user.nangkhieu = model.nangkhieu;
                user.hanche = model.hanche;
                user.capcongdoan_id = CapCongDoanID;



                UserManager.Update(user);
                

                return RedirectToAction("Index");
            }
            var categories = from c in new ApplicationDbContext().CapCongDoans select c;
            ViewBag.CapCongDoanID = new SelectList(categories, "Capcongdoan_id", "name", UserManager.FindById(model.Id).CapCongDoan.Capcongdoan_id); // danh sách CapCongDoan
            return View(model);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<ApplicationUser, User>();

            });

            IMapper iMapper = config.CreateMapper();

            var source = UserManager.FindById(id);



            User user = iMapper.Map<ApplicationUser, User>(source);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser user = UserManager.FindById(id);

            UserManager.Delete(user);
            
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }


    }


}
