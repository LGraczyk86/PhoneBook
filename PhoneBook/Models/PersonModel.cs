using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc.Html;

namespace PhoneBook.Models
{
    public class PersonModel
    {
        [DisplayName("Lp.")]
        public int ID { get; set; }

        [DisplayName("Imie"), Required(ErrorMessage = "Wymagana wartość"), MaxLength(20)]
        public string FirstName { get; set; }

        [DisplayName("Nazwisko"), Required(ErrorMessage = "Wymagana wartość"), MaxLength(20)]
        public string LastName { get; set; }

        [DisplayName("Telefon"), Range(100000000,999999999, ErrorMessage = "numer musi posiadać 9 liczb"), Required(ErrorMessage = "Wymagana wartość")]
        public int Phone { get; set; }

        [DisplayName("eMail"), Required(ErrorMessage = "Wymagana wartość"), EmailAddress(ErrorMessage = "Proszę wpisać format eMail :)")]
        public string Email { get; set; }

        [DisplayName("Utworzono")]
        public DateTime Created { get; set; }

        [DisplayName("Modyfikacja")]
        public DateTime Updated { get; set; }
    }
}