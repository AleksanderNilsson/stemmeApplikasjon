using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace stemmeApp.Models
{
    // SKREVET AV ALEKSANDER NILSOON : Sist endret 04.06.2021 
    public class CandidateModel
    {
        [Required(ErrorMessage = "The candidate needs to have a username")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "The faculty the candidate belongs to")]
        public string Faculty { get; set; }
        [Required(ErrorMessage = "The institute the candidate belongs to")]
        public string Institute { get; set; }
        [Display(Name = "Write about the candidate")]
        [Required(ErrorMessage = "You need to write about the candidate")]
        public string Info { get; set; }
        public string Picture { get; set; }
        [Display(Name = "Picture description")]
        [Required(ErrorMessage = "The picture needs a description")]
        public string PictureText { get; set; }
    }
}