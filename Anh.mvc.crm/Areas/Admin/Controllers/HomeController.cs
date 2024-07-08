using Anh.mvc.crm.Authentication;
using Module.Data.Helper;
using System.Web.Mvc;

namespace Anh.mvc.crm.Areas.Admin.Controllers
{
    [CustomAuthorizeAttribute]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorizeAttribute(Roles = Permissions.Admin_TinTuc_View)]
        public ActionResult TinTuc()
        {
            return View();
        }

        [CustomAuthorizeAttribute(Roles = Permissions.Admin_TinTuc_View)]
        public ActionResult EditTinTuc(int? id)
        {
            ViewBag.newsId = id;
            return View();
        }

        public ActionResult Config()
        {
            return View();
        }

       
        public ActionResult DanhMuc(string id)
        {
            ViewBag.KeyDanhMuc = id;
            return View();
        }

     
        public ActionResult PageStatic()
        {
            return View();
        }

        [CustomAuthorizeAttribute(Roles = Permissions.Admin_Slider_View)]
        public ActionResult PageSlider()
        {
            return View();
        }

        [CustomAuthorizeAttribute(Roles = Permissions.Admin_Slider_View)]
        public ActionResult PageSliderEdit(int? id)
        {
            ViewBag.newsId = id;
            return View();
        }
    }
}