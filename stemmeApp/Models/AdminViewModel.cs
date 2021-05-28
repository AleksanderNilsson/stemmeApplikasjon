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
        [Required(ErrorMessage = "This field cannot be changed")]
        [Display(Name = "User ID")]
        public string Id { get; set; }

        [Required(ErrorMessage = "User must have a Username")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "User must have a Email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "User must have a Firstname")]
        [Display(Name = "Firstname")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "User must have a lastname")]
        [Display(Name = "Lastname")]
        public string Lastname { get; set; }

        /// <summary>
        /// Data from Candidate table in DB
        /// </summary>

        [Display(Name = "Faculty")]
        public string Faculty { get; set; }

        [Display(Name = "Institute")]
        public string Institute { get; set; }

        [Display(Name = "Info")]
        public string Info { get; set; }

        /// <summary>
        /// Data from Role table in DB
        /// </summary>
        [StringLength(3)]
        [Required(ErrorMessage = "User must have a role, (0 = Student, 1 = Inspector, 2 = Admin)")]
        [Display(Name = "Role ID")]
        public string RoleId { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}