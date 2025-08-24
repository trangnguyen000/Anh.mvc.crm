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
        [Required(ErrorMessage = "Họ và tên là bắt buộc")]
        [RegularExpression(@"^[\p{L}\p{N}\s\.\,\!\?'\-]+$", ErrorMessage = "Họ tên có chứa ký tự không hợp lý")]
        public string name { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [RegularExpression(@"^\+?[0-9]{7,15}$", ErrorMessage = "Số điện thoại không đúng")]
        public string phoneNumber { get; set; }
        [Required(ErrorMessage = "Chương trình quan tâm  là bắt buộc")]
        [RegularExpression(@"^[\p{L}\p{N}\s\.\,\!\?'\-]+$",  ErrorMessage = "Chương trình quan tâm có chứa ký tự không hợp lý")]
        public string country { get; set; }
        [RegularExpression(@"^[\p{L}\p{N}\s\.\,\!\?'\-]+$",  ErrorMessage = "Câu hỏi có chứa ký tự không hợp lý")]
        public string subject { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",  ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }
    }
}