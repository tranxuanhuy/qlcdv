using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
    [Authorize]
    public class UsersController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public UsersController()
        {
        }

        public UsersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
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

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        [Authorize(Roles = "admin")]
        public ActionResult AIndex()
        {
            var aspNetUsers = UserManager.Users.Include(a => a.CapCongDoan);
            return View(aspNetUsers.ToList());
        }

        [Authorize(Roles = "admin")]
        // GET: AspNetUsers/Details/5
        public ActionResult ADetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser aspNetUser = UserManager.FindById(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }


        [Authorize(Roles = "admin")]
        // GET: AspNetUsers/Edit/5
        public ActionResult AEdit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser aspNetUser = UserManager.FindById(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.capcongdoan_id = new SelectList(new ApplicationDbContext().CapCongDoans, "Capcongdoan_id", "name", aspNetUser.capcongdoan_id);

            List<SelectListItem> list = new List<SelectListItem>();
            int i = 1;
            string temp="";
            foreach (var role in RoleManager.Roles)
            {
                SelectListItem sli = new SelectListItem() { Value = role.Name, Text = role.Name };

                if (i.ToString() == aspNetUser.Roles.FirstOrDefault().RoleId)
                    temp = role.Name;
                i++;
                list.Add(sli);
            }
            ViewBag.roles = new SelectList(list, "Value", "Text", temp);
            

            return View(aspNetUser);
        }

        [Authorize(Roles = "admin")]
        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AEdit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,name,DOB,sex,noisinh,quequan,HKTT,tamtru,chucvuChinhquyen,chucvuDoanthe,vanhoa,chuyenmon,hocvi,hocham,tinhoc,ngoaingu,imageurl,tongiao,dantoc,cmnd,noicapcmnd,ngaycapcmnd,truongcongdoanbophan,truonglopdaotao,nangkhieu,hanche,capcongdoan_id")] ApplicationUser aspNetUser,string roles,string pass="")
        {
            if (ModelState.IsValid)
            {
                ApplicationUser originUser = UserManager.FindById(aspNetUser.Id);

                originUser.Email = aspNetUser.Email;
                originUser.EmailConfirmed = aspNetUser.EmailConfirmed;
                originUser.PasswordHash = aspNetUser.PasswordHash;
                originUser.SecurityStamp = aspNetUser.SecurityStamp;
                originUser.PhoneNumber = aspNetUser.PhoneNumber;
                originUser.PhoneNumberConfirmed = aspNetUser.PhoneNumberConfirmed;
                originUser.TwoFactorEnabled = aspNetUser.TwoFactorEnabled;
                originUser.LockoutEndDateUtc = aspNetUser.LockoutEndDateUtc;
                originUser.LockoutEnabled = aspNetUser.LockoutEnabled;
                originUser.AccessFailedCount = aspNetUser.AccessFailedCount;
                originUser.UserName = aspNetUser.UserName;
                originUser.name = aspNetUser.name;
                originUser.DOB = aspNetUser.DOB;
                originUser.sex = aspNetUser.sex;
                originUser.noisinh = aspNetUser.noisinh;
                originUser.quequan = aspNetUser.quequan;
                originUser.HKTT = aspNetUser.HKTT;
                originUser.tamtru = aspNetUser.tamtru;
                originUser.chucvuChinhquyen = aspNetUser.chucvuChinhquyen;
                originUser.chucvuDoanthe = aspNetUser.chucvuDoanthe;
                originUser.vanhoa = aspNetUser.vanhoa;
                originUser.chuyenmon = aspNetUser.chuyenmon;
                originUser.hocvi = aspNetUser.hocvi;
                originUser.hocham = aspNetUser.hocham;
                originUser.tinhoc = aspNetUser.tinhoc;
                originUser.ngoaingu = aspNetUser.ngoaingu;
                originUser.imageurl = aspNetUser.imageurl;
                originUser.tongiao = aspNetUser.tongiao;
                originUser.dantoc = aspNetUser.dantoc;
                originUser.cmnd = aspNetUser.cmnd;
                originUser.noicapcmnd = aspNetUser.noicapcmnd;
                originUser.ngaycapcmnd = aspNetUser.ngaycapcmnd;
                originUser.truongcongdoanbophan = aspNetUser.truongcongdoanbophan;
                originUser.truonglopdaotao = aspNetUser.truonglopdaotao;
                originUser.nangkhieu = aspNetUser.nangkhieu;
                originUser.hanche = aspNetUser.hanche;
                originUser.capcongdoan_id = aspNetUser.capcongdoan_id;

                var roleid = originUser.Roles.FirstOrDefault().RoleId;
                UserManager.RemoveFromRole(aspNetUser.Id, new ApplicationDbContext().Roles.Find(roleid).Name);
                UserManager.AddToRole(aspNetUser.Id, roles);

                if (!String.IsNullOrEmpty(pass))
                {
                    UserManager.RemovePassword(aspNetUser.Id);

                    UserManager.AddPassword(aspNetUser.Id, "12QWaszx!@"); 
                }

                UserManager.Update(originUser);
                return RedirectToAction("AIndex");
            }
            ViewBag.capcongdoan_id = new SelectList(new ApplicationDbContext().CapCongDoans, "Capcongdoan_id", "name", aspNetUser.capcongdoan_id);
            return View(aspNetUser);
        }

        public ActionResult Index(string listItem,string gender, string searchString, string currentFilter, string searchString1, string currentFilter1,
            string searchString2, string currentFilter2,
            string searchString3, string currentFilter3)
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
            var categories = from c in new ApplicationDbContext().CapCongDoans orderby c.motaphancap select c;
            ViewBag.ListItem = new SelectList(categories, "Capcongdoan_id", "namephancap"); // danh sách CapCongDoan
            ViewBag.Gender = gender;

            var userstemp = users.AsQueryable();
            if (!String.IsNullOrEmpty(listItem))
            {
                
                userstemp = userstemp.Where(s => s.CapCongDoan.motaphancap.Contains("-"+listItem+"-"));
                
            }
            if (gender=="Male")
            {
               
                userstemp = userstemp.Where(s => s.sex==true);
                
            }
            else if (gender == "Female")
            {

                userstemp = userstemp.Where(s => s.sex == false);

            }
            

            if (searchString != null)
            {
                
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                userstemp = userstemp.Where(s => s.vanhoa.ToLower().Contains(searchString.ToLower()));
            }


            if (searchString1 != null)
            {

            }
            else
            {
                searchString1 = currentFilter1;
            }
            ViewBag.CurrentFilter1 = searchString1;

            if (!String.IsNullOrEmpty(searchString1))
            {
                userstemp = userstemp.Where(s => s.vanhoa.ToLower().Contains(searchString1.ToLower()));
            }


            if (searchString2 != null)
            {

            }
            else
            {
                searchString2 = currentFilter2;
            }
            ViewBag.CurrentFilter2 = searchString2;

            if (!String.IsNullOrEmpty(searchString2))
            {
                userstemp = userstemp.Where(s => (DateTime.Now.Year - s.DOB.Value.Year)>=int.Parse(searchString2));
            }


            if (searchString3 != null)
            {

            }
            else
            {
                searchString3 = currentFilter3;
            }
            ViewBag.CurrentFilter3 = searchString3;

            if (!String.IsNullOrEmpty(searchString3))
            {
                userstemp = userstemp.Where(s => (DateTime.Now.Year - s.DOB.Value.Year) <= int.Parse(searchString3));
            }


            users = userstemp.ToList();
            return View(users);

            
        }
        // GET: Users
        public ActionResult Index2()
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

            var loggedInUser = System.Web.HttpContext.Current.User.Identity.GetUserId();

            if (id != loggedInUser && User.IsInRole("user"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (id != loggedInUser && User.IsInRole("mod"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
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
            var categories = from c in new ApplicationDbContext().CapCongDoans orderby c.motaphancap select c;
            ViewBag.CapCongDoanID = new SelectList(categories, "Capcongdoan_id", "namephancap",user.CapCongDoan.Capcongdoan_id); // danh sách CapCongDoan
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,DOB,sex,noisinh,quequan,HKTT,tamtru,chucvuChinhquyen,chucvuDoanthe,vanhoa,chuyenmon,hocvi,hocham,tinhoc,ngoaingu,imageurl,tongiao,dantoc,cmnd,noicapcmnd,ngaycapcmnd,truongcongdoanbophan,truonglopdaotao,nangkhieu,hanche,capcongdoan_id")] User model, HttpPostedFileBase uploadImage, int CapCongDoanID = 0 )
        {
            
            if (ModelState.IsValid)
            {
              

                //db.Entry(user).State = EntityState.Modified;
                ApplicationUser user = UserManager.FindById(model.Id);

                if (uploadImage!=null)
                {
                    //xoa anh cu
                    var filePath = Server.MapPath(user.imageurl);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    user.imageurl = model.imageurl;
                    //nap anh moi
                    //string filename = Path.GetFileNameWithoutExtension(uploadImage.FileName);
                    string extension = Path.GetExtension(uploadImage.FileName);
                    string filename = user.UserName + DateTime.Now.ToString("yymmssfff") + extension;
                    user.imageurl = "/Image/Profile/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Image/Profile/"), filename);
                    uploadImage.SaveAs(filename); 
                }

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

        [Authorize(Roles = "admin")]
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

            //xoa anh cu
            var filePath = Server.MapPath(user.imageurl);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

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
