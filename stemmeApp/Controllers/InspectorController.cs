using stemmeApp.Data;
using stemmeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace stemmeApp.Controllers
{
    public class InspectorController : Controller
    {
        // GET: Inspector
        public ActionResult Index()
        {
            DbQuery db = new DbQuery();
            var IndexViewModel = new InspectorViewModel();
            IndexViewModel.Votes = db.getVotes();
            IndexViewModel.ElectionInformation = db.getElectionInfo();
            IndexViewModel.Candidates = db.GetAllCandidates();


            return View(IndexViewModel);
        }

        public ActionResult Votes()
        {
            return View();
        }

        public ActionResult Candidates()
        {
            return View();
        }
    }
}