using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using stemmeApp.Models;
using System.Net;
using stemmeApp.Data;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using static stemmeApp.Controllers.ManageController;
using AspNet.Identity.MySQL;

namespace stemmeApp.Controllers
{
    public class AdminController : Controller
    {
        MySQLDatabase _database = new MySQLDatabase();

        public AdminController()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_database));
        }

        public AdminController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: Admin
        public ActionResult Index()
        {
            DbQuery db = new DbQuery();
            var data = db.AdminGetUsers();
            return View(db.AdminGetUsers().ToList());
        }

        //
        // GET: Admin/Details/5
        public ActionResult Details(AdminModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DbQuery db = new DbQuery();
            string username = User.Identity.GetUserName();
            return View(db.AdminGetUserDetails(username).ToList());

        }

        //// GET: Admin/Edit/5
        public ActionResult Edit(AdminModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DbQuery db = new DbQuery();
            string username = User.Identity.GetUserName();
            return View(db.AdminGetUserDetails(username).ToList());

        }


        //POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminModel Model, string id)
        {
            DbQuery db = new DbQuery();
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else if(ModelState.IsValid)
            {
                var username =  User.Identity.GetUserName();
                db.AdminEditUser(Model.Username, Model.Email, Model.Firstname, Model.Lastname);
                return RedirectToAction("Index", new { Message = ManageMessageId.AdminSuccess });
            }

            return View();
        }

        //
        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
      
    }
}
