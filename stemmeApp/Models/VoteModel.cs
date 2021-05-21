using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNet.Identity.MySQL;

namespace stemmeApp.Models
{
    public class VoteModel:MySQLDatabase
    {
        public string username { get; set; }
        public string faculty { get; set; }

        public string institute { get; set; }
        public string info { get; set; }
    }
}