using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using stemmeApp.Models;
using System.Net;
using System.Web.Helpers;
using stemmeApp.Data;
using System.Threading;
using System.Runtime.Remoting.Contexts;
using Microsoft.Ajax.Utilities;
using System.Data.Entity;
using AspNet.Identity.MySQL;

namespace stemmeApp.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;
        public AdminController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
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
        public AdminController()
        {
        }
        //
        // GET: Admin
        public ActionResult Index()
        {
            DbQuery db = new DbQuery();
            var data = db.AdminGetUsers();
            return View(db.AdminGetUsers().ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        //
        // GET: Admin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DbQuery db = new DbQuery();
            string userDetails = User.Identity.GetUserName();
            var data = db.AdminGetUserDetails(userDetails);
            return View(db.AdminGetUserDetails(userDetails).ToList());
        }

        //// GET: Admin/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminGetUserDetails Model)
        {
            DbQuery db = new DbQuery();
            if (ModelState.IsValid)
            {
                //Response.TrySkipIisCustomErrors = true;
                if (Model.Email != User.Identity.GetUserName())
                {
                    ModelState.AddModelError("email", "You cannot change your email");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Something Went Baaaad");
                }
                //db.AdminUserDetails(Model.UserName, Model.Email, Model.FirstName, Model.LastName, Model.PhoneNumber);
                return RedirectToAction("Index");
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
