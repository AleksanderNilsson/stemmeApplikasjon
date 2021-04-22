using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using stemmeApp.Models;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using stemmeApp.Data;
using Microsoft.AspNet.Identity;

namespace stemmeApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Candidate()
        {
            ViewBag.Message = "Nominer en bruker.";

            return View();   
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Candidate(CandidateModel Model)
        {
            if (ModelState.IsValid) 
            {
                DbQuery db = new DbQuery();
                db.InsertNewNominee(Model.Epost, Model.info);
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Test()
        {
            ViewBag.Message = "Test side.";




                string currentUser = User.Identity.GetUserName();
                DbQuery db = new DbQuery();
                ViewBag.fornavn = db.GetFirstName("123@test.no");
                ViewBag.username = currentUser;

            return View();
        }
    }
}