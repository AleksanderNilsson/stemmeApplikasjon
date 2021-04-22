using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace stemmeApp.Models
{
    public class CandidateModel
    {
        [DataType(DataType.EmailAddress)]
        public string Epost { get; set; }
        [Required(ErrorMessage = "Kandidaten må ha ett fornavn")]
        public string Fornavn { get; set; }
        [Required(ErrorMessage = "Kandidaten må ha ett etternavn")]
        public string Etternavn { get; set; }
        [Display(Name = "Skriv litt om kandidaten")]
        [Required(ErrorMessage = "Du må skrive litt om kandidaten")]
        public string info { get; set; }
        [DataType(DataType.Upload)]
        public long bilde { get; set; }


    }
}