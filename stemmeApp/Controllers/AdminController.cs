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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
       
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
                    TempData["EditUserSuccess"] = "Successfully edited User.";
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
                ViewBag.StatusMessage = ("Username", "Cannot delete a user that is already a registered Candidate. Please remove user from candidate role first.");
                TempData["ErrorCandidate"] = "Cannot delete a user that is already a registered Candidate. Please remove user from candidate role first.";
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            else 
            {
                db.AdminDeleteUser(Model.Username);
                TempData["EditUserSuccess"] = "Successfully deleted user..";
                return RedirectToAction("Index", new { Message = ManageMessageId.AdminSuccess });
            }
            return View();
        }
        [HttpPost]
        public ActionResult AdminRemoveAsCandidate(AdminModel Model, string username)
        {
            DbQuery db = new DbQuery();
            db.removeCandidate(username);
            return RedirectToAction("Edit");
         
        }

        // GET: Admin/ElectionPanel
        public ActionResult ElectionPanel()
        {
            DbQuery db = new DbQuery();
            return View(db.ElectionPanel());
        }

        //POST: Admin/ElectionPanel
        [HttpPost]
        public ActionResult ElectionPanel(ElectionDateInformation Model)
        {
            try
            {
                DbQuery db = new DbQuery();
                db.AdminUpdateElection(Model.Title,
                                       Model.Idelection,
                                       Model.Startelection,
                                       Model.Endelection);
                return RedirectToAction("ElectionPanel");
            }
            catch (Exception e)
            {
                throw e;

            }
            return RedirectToAction("ElectionPanel");

        }
    }
}