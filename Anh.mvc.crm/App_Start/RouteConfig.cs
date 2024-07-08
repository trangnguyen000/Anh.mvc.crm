using Anh.mvc.crm.Helper;
using Module.BusinessLogic.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Anh.mvc.crm
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "D2Visa",
                url: "visa-D2",
                defaults: new { controller = "Home", action = "D2Visa", id = UrlParameter.Optional },
                 new[] { "Anh.mvc.crm.Controllers" }
             );
            routes.MapRoute(
               name: "About",
               url: "ve-chung-toi",
               defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional },
                new[] { "Anh.mvc.crm.Controllers" }
            );
            routes.MapRoute(
             name: "CompanyKorea",
             url: "ve-chung-toi-korea",
             defaults: new { controller = "Home", action = "CompanyKorea", id = UrlParameter.Optional },
              new[] { "Anh.mvc.crm.Controllers" }
          );
            routes.MapRoute(
               name: "Contact",
               url: "lien-he",
               defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional },
                new[] { "Anh.mvc.crm.Controllers" }
            );

            routes.MapRoute(
               name: "Master",
               url: "du-hoc-thac-sy",
               defaults: new { controller = "Home", action = "Master", id = UrlParameter.Optional },
                new[] { "Anh.mvc.crm.Controllers" }
            );
            routes.MapRoute(
              name: "ActivitiesPage",
              url: CommonHelper.PageUrl + "/{id}/{title}",
              defaults: new { controller = "Home", action = "ActivitiesPage", id = UrlParameter.Optional },
               new[] { "Anh.mvc.crm.Controllers" }
           );
            routes.MapRoute(
             name: "D4Visa",
             url: "visa-d4",
             defaults: new { controller = "Home", action = "D4Visa", id = UrlParameter.Optional },
              new[] { "Anh.mvc.crm.Controllers" }
          );
            routes.MapRoute(
              name: "D10Visa",
              url: "visa-d10",
              defaults: new { controller = "Home", action = "D10Visa", id = UrlParameter.Optional },
               new[] { "Anh.mvc.crm.Controllers" }
           );
            routes.MapRoute(
              name: "E7Visa",
              url: "visa-e7",
              defaults: new { controller = "Home", action = "E7Visa", id = UrlParameter.Optional },
               new[] { "Anh.mvc.crm.Controllers" }
           );
            routes.MapRoute(
            name: "HighSchool",
            url: "du-hoc-cap-3",
            defaults: new { controller = "Home", action = "HighSchool", id = UrlParameter.Optional },
             new[] { "Anh.mvc.crm.Controllers" }
         );
            routes.MapRoute(
            name: "E9",
            url: "e9-ve-nuoc",
            defaults: new { controller = "Home", action = "E9", id = UrlParameter.Optional },
             new[] { "Anh.mvc.crm.Controllers" }
         );
            routes.MapRoute(
            name: "BusinessKorean",
            url: "tieng-han-doanh-nghiep",
            defaults: new { controller = "Home", action = "BusinessKorean", id = UrlParameter.Optional },
             new[] { "Anh.mvc.crm.Controllers" }
         );
            routes.MapRoute(
            name: "University",
            url: "du-hoc-dai-hoc",
            defaults: new { controller = "Home", action = "University", id = UrlParameter.Optional },
             new[] { "Anh.mvc.crm.Controllers" }
         );
            routes.MapRoute(
            name: "Handbook",
            url: "cam-nang-du-hoc",
            defaults: new { controller = "Home", action = "Handbook", id = UrlParameter.Optional },
             new[] { "Anh.mvc.crm.Controllers" }
         );
            routes.MapRoute(
        name: "LearnFree",
        url: "dao-tao-tieng-han-mien-phi",
        defaults: new { controller = "Home", action = "LearnFree", id = UrlParameter.Optional },
         new[] { "Anh.mvc.crm.Controllers" }
     );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 new[] { "Anh.mvc.crm.Controllers" }
            );
            routes.MapRoute(
                "Admin",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Anh.mvc.crm.Areas.Admin.Controllers" }
            );
            

        }
    }
}
