using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace stemmeApp.Models
{
    public class AdminModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        [Display(Name = "Firstname")]
        public string FirstName { get; set; }
        [Display(Name = "Lastname")]
        public string LastName { get; set; }
        [Display(Name = "Faculty")]
        public string Faculty { get; set; }
        [Display(Name = "Institute")]
        public string Institute { get; set; }
        [Display(Name = "Info")]
        public string Info { get; set; }
        public int EmailChangeLimit { get; set; } = 10;
    }
}