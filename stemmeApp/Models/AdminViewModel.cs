using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace stemmeApp.Models
{
    public class AdminUserDetailsViewModel
    {
        [Display(Name = "User ID")]
        public string Id { get; set; }

        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

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
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        //public IEnumerable<SelectListItem> RolesList { get; set; }

    }
    public class AdminUserViewModel
    {
        [Display(Name = "User ID")]
        public string Id { get; set; }

        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class AdminRolesViewModel
    {
        public IEnumerable<string> RoleNames { get; set; }
        public string UserName { get; set; }
    }
}