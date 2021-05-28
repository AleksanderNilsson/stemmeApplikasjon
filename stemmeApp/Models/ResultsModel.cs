using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stemmeApp.Models
{
    

        public class ResultsViewModel
        {
            public List<CandidateVotes> CandidateVotes { get; set; } = new List<CandidateVotes>();
            public List<ElectionInformation> ElectionInformation { get; set; } = new List<ElectionInformation>();
        }
    
}