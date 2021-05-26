using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stemmeApp.Models
{


    //Models used for the Inspector>Index page
    public class Votes {
        public String Voter { get; set; }
        public String VotedOn { get; set; }
    }

    public class ElectionInformation
    {
        public int IdElection { get; set; }
        public DateTime ElectionStart { get; set; }
        public DateTime ElectionEnd { get; set; }
        public DateTime Controlled { get; set; }
    }

    
    

    public class InspectorViewModel {
        public List<Votes> Votes {get; set;} = new List<Votes>();
        public List<ElectionInformation> ElectionInformation { get; set; } = new List<ElectionInformation>();
        public List<VoteModel> Candidates { get; set; } = new List<VoteModel>();
    }


    //Model used for the Inspector>Votes page

    public class CandidateVotes
    {
        public String Username { get; set; }
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public int Votes { get; set; }
    }


}

    
