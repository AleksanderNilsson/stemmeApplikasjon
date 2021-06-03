
using System.Collections.Generic;


namespace stemmeApp.Models
{
    public class VoteModel
    {
        public List<Candidates> Candidates { get; set; } = new List<Candidates>();

        public List<ElectionInformation> ElectionInformation { get; set; } = new List<ElectionInformation>();

        public List<Votes> Votes { get; set; } = new List<Votes>();

    }

    public class Candidates
    {
        public string username { get; set; }
        public string faculty { get; set; }

        public string institute { get; set; }
        public string info { get; set; }
        public string picture { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
    }
    

}