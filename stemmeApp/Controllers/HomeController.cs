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
using System.IO;

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
        public ActionResult Candidate(CandidateModel Model, HttpPostedFileBase file)
        {
            DbQuery db = new DbQuery();

            //Checks if username exists
            if (!db.CheckIfUserExists(Model.Username))
            {
                ModelState.AddModelError("username", "Nobody with that username exists, the person you want to add as a candidate need to register as an user");
            }


            //Checks if file is an image
            if (!file.ContentType.Contains("image"))
            {             
                ModelState.AddModelError("", "File is not an image, you can only upload images");
            }


            if (ModelState.IsValid) 
            {                            
                var extension = Path.GetExtension(file.FileName);               
                var fileName = Model.Username + extension;
                var path = Path.Combine(Server.MapPath("~/Pictures/"), fileName);
                file.SaveAs(path);
                int PictureId = db.CheckForAvailableImageId();
                string dbPath = "Pictures/" + fileName;
                db.InsertNewCandidate(Model.Username, Model.Faculty, Model.Institute, Model.Info, PictureId);
                db.InsertNewImage(PictureId, dbPath, "test");

                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Test()
        {
            ViewBag.Message = "Test side.";




                string currentUser = User.Identity.GetUserName();
                DbQuery db = new DbQuery();
                
                ViewBag.username = currentUser;

            return View();
        }
    }
}