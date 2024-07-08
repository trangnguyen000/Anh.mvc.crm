using Module.BusinessLogic.Dto;
using Module.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BusinessLogic.Shared
{
    public interface IUserLogic
    {
        Task<List<AbpUser>> GetUsers();
        Task<ResultUserDto> GetDanhSachUser(string filter, int page);
        Task<ResultRoleDto> GetDanhSachRole(string filter  ,int page);
        Task<ResultRoleDto> GetRoleAll();
        ResultDto CreateOfUpdateRole(CreateOrUpdateRoleModel model,int? userId);
        ResultDto UpdatePassWord(CreateOrUpdateUserModel model, int? userId);
        ResultDto DeleteRole(int id, int? userId);
        ResultDto CreateOfUpdateUser(CreateOrUpdateUserModel model, int? userId);
        ResultDto DeleteUser(int id, int? userId);
        Task<object> GetRoles();
    }
}
