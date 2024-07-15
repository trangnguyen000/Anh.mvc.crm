using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Data.Helper
{
    public static class Permissions
    {
        public const string Admin_User = "Admin.User";
        public const string Admin_User_View = "Admin.User.View";
        public const string Admin_User_Create = "Admin.User.Create";
        public const string Admin_User_Edit = "Admin.User.Edit";
        public const string Admin_User_DeLete = "Admin.User.DeLete";

        public const string Admin_Role = "Admin.Role";
        public const string Admin_Role_View = "Admin.Role.View";
        public const string Admin_Role_Create = "Admin.Role.Create";
        public const string Admin_Role_Edit = "Admin.Role.Edit";
        public const string Admin_Role_DeLete = "Admin.Role.DeLete";

        public const string Admin_TinTuc = "Admin.TinTuc";
        public const string Admin_TinTuc_View = "Admin.TinTuc.View";
        public const string Admin_TinTuc_Create = "Admin.TinTuc.Create";
        public const string Admin_TinTuc_Edit = "Admin.TinTuc.Edit";
        public const string Admin_TinTuc_DeLete = "Admin.TinTuc.DeLete";

        public const string Admin_Contact_Support = "Admin.Contact.Support";
        public const string Admin_Contact_Support_View = "Admin.Contact.Support.View";
        public const string Admin_Contact_Support_Create = "Admin.Contact.Support.Create";
        public const string Admin_Contact_Support_Edit = "Admin.Contact.Support.Edit";
        public const string Admin_Contact_Support_DeLete = "Admin.Contact.Support.DeLete";

        public const string Admin_Slider = "Admin.Slider";
        public const string Admin_Slider_View = "Admin.Slider.View";
        public const string Admin_Slider_Create = "Admin.Slider.Create";
        public const string Admin_Slider_Edit = "Admin.Slider.Edit";
        public const string Admin_Slider_DeLete = "Admin.Slider.DeLete";


        public const string Admin_Dictionary = "Admin.Dictionary";
        public const string Admin_Dictionary_View = "Admin.Dictionary.View";
        public const string Admin_Dictionary_Create = "Admin.Dictionary.Create";
        public const string Admin_Dictionary_Edit = "Admin.Dictionary.Edit";
        public const string Admin_Dictionary_DeLete = "Admin.Dictionary.DeLete";



        public static List<RolePermissions> ArrayPermissions = new List<RolePermissions>()
        {
           new RolePermissions(){
               KeyValue = Admin_User,
               DisplayName = "Nhân sự",
                Children = new List<RolePermissions>()
                {
                    new RolePermissions(){ KeyValue = Admin_User_View,ParentValue =Admin_User , DisplayName = "Xem"},
                    new RolePermissions(){ KeyValue = Admin_User_Create,ParentValue =Admin_User , DisplayName = "Thêm"},
                    new RolePermissions(){ KeyValue = Admin_User_Edit,ParentValue =Admin_User , DisplayName = "Sửa"},
                    new RolePermissions(){ KeyValue = Admin_User_DeLete,ParentValue =Admin_User , DisplayName = "Xóa"}
                }
           },
             new RolePermissions(){
                 KeyValue = Admin_Role,
                 DisplayName = "Vai trò",
                  Children = new List<RolePermissions>()
                    {
                        new RolePermissions(){ KeyValue = Admin_Role_View,ParentValue =Admin_Role , DisplayName = "Xem"},
                        new RolePermissions(){ KeyValue = Admin_Role_Create,ParentValue =Admin_Role , DisplayName = "Thêm"},
                        new RolePermissions(){ KeyValue = Admin_Role_Edit,ParentValue =Admin_Role , DisplayName = "Sửa"},
                        new RolePermissions(){ KeyValue = Admin_Role_DeLete,ParentValue =Admin_Role , DisplayName = "Xóa"}
                    }
             },
               new RolePermissions(){
                 KeyValue = Admin_Contact_Support,
                 DisplayName = "Liên hệ hỗ trợ",
                  Children = new List<RolePermissions>()
                    {
                        new RolePermissions(){ KeyValue = Admin_Contact_Support_View,ParentValue =Admin_Contact_Support , DisplayName = "Xem"},
                        new RolePermissions(){ KeyValue = Admin_Contact_Support_Create,ParentValue =Admin_Contact_Support , DisplayName = "Thêm"},
                        new RolePermissions(){ KeyValue = Admin_Contact_Support_Edit,ParentValue =Admin_Contact_Support , DisplayName = "Sửa"},
                        new RolePermissions(){ KeyValue = Admin_Contact_Support_DeLete,ParentValue =Admin_Contact_Support , DisplayName = "Xóa"}
                    }
             },
             new RolePermissions(){
                 KeyValue = Admin_Slider,
                 DisplayName = "Quản lý Banner",
                  Children = new List<RolePermissions>()
                    {
                        new RolePermissions(){ KeyValue = Admin_Slider_View,ParentValue =Admin_Slider , DisplayName = "Xem"}
                    }
             },

             new RolePermissions(){
                 KeyValue = Admin_TinTuc,
                 DisplayName = "Tin tức",
                  Children = new List<RolePermissions>()
                    {
                        new RolePermissions(){ KeyValue = Admin_TinTuc_View,ParentValue =Admin_TinTuc , DisplayName = "Xem"}
                    }
             },

        };
    }
    public class RolePermissions
    {
        public string KeyValue { get; set; }
        public string ParentValue { get; set; }
        public string DisplayName { get; set; }
        public List<RolePermissions> Children { get; set; }
    }
}
