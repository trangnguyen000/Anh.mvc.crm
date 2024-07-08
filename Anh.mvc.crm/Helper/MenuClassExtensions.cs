using Anh.mvc.crm.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anh.mvc.crm.Helper
{
    public static class MenuClassExtensions
    {
        public static char[] delimiterChars = { ',' };
        public static string SetClassActive(this HtmlHelper helper, string url)
        {
            HttpRequest request = HttpContext.Current.Request;
            return url.Split(delimiterChars).Any(o => (request.Url.ToString()+"_").Contains(o+"_")) ? "active" : "";
        }

        public static string SetClassOpen(this HtmlHelper helper, string url)
        {
            HttpRequest request = HttpContext.Current.Request;
            return url.Split(delimiterChars).Any(o => request.Url.ToString().Contains(o)) ? "menu-open" : "";
        }
    }
    public static class CurrentUser
    {
        public static string GetCurrentUser()
        {
            var config = new ConfigCurent();
            return config.CurrentUser.FirstName+' ' + config.CurrentUser.LastName;
        }

        public static string GetCurrentUserLichCongTac()
        {
            var config = new ConfigCurent();
            return config.CurentUserLichHenCongTac.FullName;
        }

        public static bool CheckRoleUser(string role)
        {
            var config = new ConfigCurent();
            return config.CurrentUser.IsInRole(role);
        }

    }

    public static class Webconst
    {
        public static string GetDateNow()
        {
           
            return DateTime.Now.ToString("dddd, dd/MM/yyyy", new System.Globalization.CultureInfo("vi-VN"));
        }

        public static bool CheckRoleUser(string role)
        {
            var config = new ConfigCurent();
            return config.CurrentUser.IsInRole(role);
        }

    }
}