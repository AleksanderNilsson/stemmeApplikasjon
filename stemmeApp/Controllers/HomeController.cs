using System.Web;
using System.Web.Mvc;
using stemmeApp.Models;
using stemmeApp.Data;
using Microsoft.AspNet.Identity;
using System.IO;


namespace stemmeApp.Controllers
{
    //[Authorize] 
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DbQuery db = new DbQuery();
            
            return View(db.getElectionInfo());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Vote()
        {
            DbQuery db = new DbQuery();
            var Model = new VoteModel();
            string Voter = User.Identity.GetUserName();
            if (db.CheckIfVotedOn(Voter))
            {
                TempData["VotedOn"] = "yahoo";
            }
            else
            {
                TempData["VotedOn"] = null;
            }
            Model.ElectionInformation = db.getElectionInfo();
            Model.Candidates = db.GetAllCandidates();
            Model.Votes = db.getVotes();
            return View(Model);
        }

       

        [HttpPost]
        public ActionResult Vote(string username)
        {
            //ViewBag.Message = "Vote for a candidate";
            DbQuery db = new DbQuery();
            db.VoteForUser(username, User.Identity.GetUserName());

            return RedirectToAction("Vote");

        }

        [HttpPost]
        public ActionResult RemoveVote(string username)
        {
                DbQuery db = new DbQuery();
                db.RemoveVote(User.Identity.GetUserName());

                TempData["RemoveVote"] = "You have revoked your vote!";

                return RedirectToAction("Vote");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize]
        public ActionResult Candidate()
        {
            ViewBag.Message = "Nominate a user!";

            return View();   
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Candidate(CandidateModel Model, HttpPostedFileBase file)
        {
            DbQuery db = new DbQuery();

            //Checks if the inserted username belongs to a user
            if (!db.CheckIfUserExists(Model.Email))
            {
                ModelState.AddModelError("email", "Nobody with that username exists, the person you want to add as a candidate need to register as an user");
            }


            //Checks if the inserted username already is a candidate
            if (db.CheckIfCandidateExists(Model.Email))
            {
                ModelState.AddModelError("email", "This person is already a nominated");
            }


            //Checks if file is an image
            if (!file.ContentType.Contains("image"))
            {             
                ModelState.AddModelError("picture", "File is not an image, you can only upload images");
            }


            if (ModelState.IsValid) 
            {                            
                var extension = Path.GetExtension(file.FileName);               
                var fileName = Model.Email + extension;
                var path = Path.Combine(Server.MapPath("~/content/Pictures/"), fileName);
                file.SaveAs(path);
                int PictureId = db.CheckForAvailableImageId();
                string dbPath = "content/Pictures/" + fileName;
                db.InsertNewCandidate(Model.Email, Model.Faculty, Model.Institute, Model.Info, PictureId);
                db.InsertNewImage(PictureId, dbPath, Model.PictureText);
                TempData["CandidteSuccess"] = "Successfully added candidate";

                return RedirectToAction("Vote");
            }

            return View();
        }

        public ActionResult Results()
        {
            ViewBag.Message = "Results";

            DbQuery db = new DbQuery();
            var Model = new ResultsViewModel();
            Model.CandidateVotes = db.getCandidateVotes();
            Model.ElectionInformation = db.getElectionInfo();

            return View(Model);
        }

        
    }

}