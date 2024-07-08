using Module.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Anh.mvc.crm.Authentication
{
    public class CustomMembershipUser : MembershipUser
    {
        #region User Properties

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string [] Permissions { get; set; }

        #endregion
        public CustomMembershipUser(AbpUser user) : base("CustomMembership", user.UserName, user.Id, user.EmailAddress, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            Id = user.Id;
            EmailAddress = user.EmailAddress;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
    }
}