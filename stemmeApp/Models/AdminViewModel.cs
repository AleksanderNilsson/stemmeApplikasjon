using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace stemmeApp.Models
{
    public class AdminGetUserDetails
    {

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Firstname")]
        public string FirstName { get; set; }
        [Display(Name = "Lastname")]
        public string LastName { get; set; }
       
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Faculty")]
        public string Faculty { get; set; }
        [Display(Name = "Institute")]
        public string Institute { get; set; }
        [Display(Name = "Info")]
        public string Info { get; set; }
    }
    public class AdminGetUsers
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