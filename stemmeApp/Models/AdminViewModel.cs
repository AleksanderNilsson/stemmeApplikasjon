﻿using System;
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
        /// Data from Picture table in DB
        /// </summary>
        public string Picture { get; set; }
        public string PictureID { get; set; }
        /// <summary>
        /// Data from Role table in DB
        /// </summary>
        [StringLength(1, MinimumLength = 1, ErrorMessage = "The RoleID must be a length of only 1 character.")]
        [RegularExpression("0|1|2", ErrorMessage = "(1 = Student, 2 = Inspector, 3 = Admin)")]
        [Required(ErrorMessage = "User must have a role, (1 = Student, 2 = Inspector, 3 = Admin)")]
        [Display(Name = "Role ID")]
        public string RoleId { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
    public class ElectionDateInformation
    {
        [Display(Name = "Election Title: ")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "The Elecction ID must be a length of only 1 character.")]
        public string Title { get; set; }

        [Display(Name = "Election ID: ")]
        [Range(1, 9999, ErrorMessage = "Id must be a positive number")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "The Elecction ID must be a length of only 1 character.")]
        public int Idelection { get; set; }

        [Display(Name = "Start Date: ")]
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "1/6/2021", "1/1/2077", ErrorMessage = "Date not valid..")]
        [DisplayFormat(DataFormatString = "{00:00:00:dd-MMM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Startelection { get; set; }

        [Display(Name = "End Date: ")]
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "1/6/2021", "1/1/2077", ErrorMessage = "Date not valid..")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Endelection { get; set; }

        [Display(Name = "Controlled at Date:")]
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "1/6/2021", "1/1/2077")]
        [DisplayFormat(DataFormatString = "{00:00:00:dd-MMM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Controlled { get; set; }
    }
}