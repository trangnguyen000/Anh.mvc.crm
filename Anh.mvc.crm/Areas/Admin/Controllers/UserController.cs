using Anh.mvc.crm.Authentication;
using Module.BusinessLogic.Dto;
using Module.BusinessLogic.Shared;
using Module.BusinessLogic.SharedCore;
using Module.Data.Helper;
using Module.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Anh.mvc.crm.Areas.Admin.Controllers
{
    [CustomAuthorizeAttribute]
    public class UserController : BaseController
    {
        private readonly IUserLogic _userLogic;

        private readonly ConfigCurent _config;
        public UserController()
        {
            _userLogic = new UserLogic(new UnitOfWork());
            _config = new ConfigCurent();
        }
        // GET: Admin/User

        [CustomAuthorizeAttribute(Roles = Permissions.Admin_User_View)]
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorizeAttribute(Roles = Permissions.Admin_Role_View)]
        public ActionResult VaiTro()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetPermissions()
        {
            return Json(Permissions.ArrayPermissions, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetRoleAll()
        {
            var data = await _userLogic.GetRoleAll();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetDanhSachUser( string filter, int? page)
        {
            if (filter == null)
            {
                filter = "";
            }
            var data = await _userLogic.GetDanhSachUser(filter, page ??1);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public async Task<ActionResult> GetDanhSachRole(string filter, int?  page )
        {
            if(filter ==null)
            {
                filter = "";
            }    
            var data = await _userLogic.GetDanhSachRole(filter, page??1);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateOfUpdateUser(CreateOrUpdateUserModel model)
        {
            if ( model.Id == 0)
            {
                return CreateUser(model);
            }
            else
            {
                return UpdateUser(model);
            }
        }

        [CustomAuthorizeAttribute(Roles = Permissions.Admin_User_Create)]
        private ActionResult CreateUser(CreateOrUpdateUserModel model)
        {
            var data = _userLogic.CreateOfUpdateUser(model, _config.CurrentUser.Id);
            return Json(data);
        }

        [CustomAuthorizeAttribute(Roles = Permissions.Admin_User_Edit)]
        private ActionResult UpdateUser(CreateOrUpdateUserModel model)
        {
            var data = _userLogic.CreateOfUpdateUser(model, _config.CurrentUser.Id);
            return Json(data);
        }

        [HttpPost]
        public ActionResult CreateOfUpdateRole(CreateOrUpdateRoleModel model)
        {
            if(model.Id == null || model.Id == 0)
            {
                return CreateRole(model);
            }
            else
            {
                return UpdateRole(model);
            }
        }
      
        [HttpPost]
        public ActionResult UpdatePassWord(CreateOrUpdateUserModel model)
        {
            var data = _userLogic.UpdatePassWord(model, _config.CurrentUser.Id);
            return Json(data);
        }

        [CustomAuthorizeAttribute(Roles = Permissions.Admin_Role_Create)]
        private ActionResult CreateRole(CreateOrUpdateRoleModel model)
        {
            var data = _userLogic.CreateOfUpdateRole(model, _config.CurrentUser.Id);
            return Json(data);
        }

        [CustomAuthorizeAttribute(Roles = Permissions.Admin_Role_Edit)]
        private ActionResult UpdateRole(CreateOrUpdateRoleModel model)
        {
            var data = _userLogic.CreateOfUpdateRole(model, _config.CurrentUser.Id);
            return Json(data);
        }

        [CustomAuthorizeAttribute(Roles = Permissions.Admin_Role_DeLete)]
        [HttpPost]
        public ActionResult DeleteRole(int id)
        {
            var data = _userLogic.DeleteRole(id, _config.CurrentUser.Id);
            return Json(data);
        }

        [CustomAuthorizeAttribute(Roles = Permissions.Admin_User_DeLete)]
        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            var data = _userLogic.DeleteUser(id, _config.CurrentUser.Id);
            return Json(data);
        }
    }
}