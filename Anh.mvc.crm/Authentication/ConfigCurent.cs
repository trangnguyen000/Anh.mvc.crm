using Anh.mvc.crm.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Anh.mvc.crm.Authentication
{
    public class ConfigCurent
    {
        public CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }


        public CustomSerializeLichCongTacModel CurentUserLichHenCongTac
        {
            get
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies["IsUserLichCongTac"];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    var serializeModel = JsonConvert.DeserializeObject<CustomSerializeLichCongTacModel>(authTicket.UserData);
                    return serializeModel;
                }
                return null;
            }
        }

    }
}