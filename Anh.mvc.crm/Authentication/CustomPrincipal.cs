using Module.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Anh.mvc.crm.Authentication
{
    public class CustomPrincipal : IPrincipal
    {
        #region Identity Properties

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string[] Permissions { get; set; }
        #endregion

        public IIdentity Identity
        {
            get; private set;
        }

        public bool IsInRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                return true;
            }
            using (var dbContext = new JDbContext())
            {
                bool check = (from usr in dbContext.AbpUserRoles
                              join p in dbContext.AbpPermissions on usr.RoleId equals p.RoleId
                              where usr.UserId == Id && role.Contains(p.Name)
                              select p.Id).Any();

                if (check)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public CustomPrincipal(string username)
        {
            Identity = new GenericIdentity(username);
        }
    }
}