using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anh.mvc.crm.Models
{
    public class LoginView
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class CustomSerializeModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public List<string> Permissions { get; set; }

    }

    public class CustomSerializeLichCongTacModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }

        public string IpAddress { get; set; }

    }
}