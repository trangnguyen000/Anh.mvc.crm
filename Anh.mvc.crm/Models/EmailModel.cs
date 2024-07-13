using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace Anh.mvc.Models
{
    public class ContactFormModel
    {
        [Required]
        public string name { get; set; }

        public string phoneNumber { get; set; }
        public int? country { get; set; }
        public string subject { get; set; }
        public string Email { get; set; }
    }
}