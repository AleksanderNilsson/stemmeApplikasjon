using AspNet.Identity.MySQL;
using Microsoft.AspNet.Identity;
using stemmeApp.Data;
using stemmeApp.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using static stemmeApp.Controllers.ManageController;

namespace stemmeApp.Controllers
{
    //[Authorize(Roles = "Admin")]
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
            return View(db.AdminGetUsers().ToList());


        }

        //// GET: Admin/Edit/Username
        public ActionResult Edit(String Username)
        {

            DbQuery db = new DbQuery();
            return View(db.AdminGetSingleUser(Username));

        }


        //POST: Admin/Edit/Username
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminModel Model)
        {
            DbQuery db = new DbQuery();

            if (ModelState.IsValid)
            {
                try
                {
                    db.AdminEditUser(Model.Id, Model.Username, Model.Email, Model.Firstname, Model.Lastname, Model.Faculty, Model.Institute, Model.Info, Model.RoleId, Model.Picture);
                    return RedirectToAction("Index", new { Message = ManageMessageId.AdminSuccess });
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return View();
        }

        // POST: Admin/Delete/Username
        [HttpPost]
        public ActionResult Delete(AdminModel Model)
        {
            DbQuery db = new DbQuery();
            if (db.CheckIfUserIsCandidate(Model.Username))
            {
                ModelState.AddModelError("Username", "Cannot delete a user that is already a registered Candidate. Please remove user from candidate role first.");
            }
            else if (ModelState.IsValid && Model.Username != null)
            {
                db.AdminDeleteUser(Model.Username);
                return RedirectToAction("Index", new { Message = ManageMessageId.AdminSuccess });
            }
            else
            {
                ModelState.AddModelError("Username", "Unknown error, could not delete...");
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            return View();
        }
        [HttpPost]
        public ActionResult AdminDeleteUserImage(AdminModel Model, string username)
        {
            DbQuery db = new DbQuery();
            if (db.CheckIfUserIsCandidate(Model.Username))
            {
                    db.AdminDeleteUserImage(username);
                    return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
        }
    }
}