using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace stemmeApp.Models
{
    public class AdminModel
    {
        /// <summary>
        /// Data from User Table in DB
        /// </summary>
         
        [Display(Name = "User ID")]
        public string Id { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Email / Username")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Firstname")]
        public string Firstname { get; set; }

        [Display(Name = "Lastname")]
        public string Lastname { get; set; }

        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Data from Candidate table in DB
        /// </summary>
         
        [Display(Name = "Faculty")]
        public string Faculty { get; set; }

        [Display(Name = "Institute")]
        public string Institute { get; set; }

        [Display(Name = "Info")]
        public string Info { get; set; }
        
    }
   
}