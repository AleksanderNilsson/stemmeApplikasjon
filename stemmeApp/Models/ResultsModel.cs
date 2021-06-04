using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stemmeApp.Models
{
    // SKREVET AV ALEKSANDER NILSOON : Sist endret 04.06.2021 

    public class ResultsViewModel
    {
        public List<CandidateVotes> CandidateVotes { get; set; } = new List<CandidateVotes>();
        public List<ElectionInformation> ElectionInformation { get; set; } = new List<ElectionInformation>();
    }
    
}