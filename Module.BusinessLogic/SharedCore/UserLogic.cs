using Module.Data.DataAccess;
using Module.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Module.BusinessLogic.Dto;
using Module.Repository.Helper;

namespace Module.BusinessLogic.SharedCore
{
    public class UserLogic : BaseLogic, Shared.IUserLogic
    {
        public UserLogic(IUnitOfWork uow) : base(uow)
        {
        }
        public async Task<List<AbpUser>> GetUsers()
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                return await uow.Users.GetAllAsync().ToListAsync();
            }
        }

        public async Task<ResultRoleDto> GetRoleAll()
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var result = new ResultRoleDto();
                result.Data = await uow.Roles.GetAllAsync()
                                .Where(o => o.IsDeleted == null || o.IsDeleted == false)
                                .Where(o => o.IsDefault == false || o.IsDefault == null)
                               .Select(o => new
                               {
                                   o.Id,
                                   o.DisplayName,
                                   o.IsDefault
                               }).ToListAsync();
                return result;
            }
        }

        public async Task<ResultUserDto> GetDanhSachUser(string filter, int page)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var result = new ResultUserDto();
                result.Total = await uow.Users.Query(o => o.IsDeleted == false && o.FullName.Contains(filter)).CountAsync();
                var data = await uow.Users.Query(o => o.IsDeleted == false && o.FullName.Contains(filter)).OrderBy(o => o.FullName).Skip((page - 1) * 10).Take(10)
                                .Select(o => new
                                {
                                    o.Id,
                                    o.FullName,
                                    o.FirstName,
                                    o.LastName,
                                    o.DateOfBirth,
                                    o.EmailAddress,
                                    o.DonVi,
                                    o.IsActive,
                                    o.PhoneNumber,
                                    o.UserName
                                }).ToListAsync();
                result.Data = data;
                var listRole = data.Select(o => o.Id).ToList();
                result.Roles = await (from p in uow.UserRoles.GetAllAsync()
                                      where listRole.Contains(p.UserId ?? 0)
                                      select new { p.RoleId, p.UserId }).ToListAsync();
                return result;
            }
        }


        public async Task<ResultRoleDto> GetDanhSachRole(string filter, int page)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var result = new ResultRoleDto();
                int skip = (page - 1) * 10;
                result.Total = await uow.Roles.Query(o => o.IsDeleted == false && o.DisplayName.Contains(filter)).CountAsync();
                var data = await uow.Roles.Query(o => o.IsDeleted == false && o.DisplayName.Contains(filter)).OrderBy(o => o.DisplayName).Skip(skip).Take(10)
                                .Select(o => new
                                {
                                    o.Id,
                                    o.DisplayName,
                                    o.LastModificationTime,
                                    o.IsDefault
                                }).ToListAsync();
                result.Data = data;
                var listRole = data.Select(o => o.Id).ToList();
                result.Permissions = await (from p in uow.Permissions.GetAllAsync()
                                            where listRole.Contains(p.RoleId ?? 0)
                                            select new { p.Id, p.RoleId, p.Name }).ToListAsync();
                return result;
            }
        }


        public ResultDto DeleteUser(int id, int? userId)
        {
            var result = new ResultDto();
            result.Success = true;
            if (id == userId)
            {
                result.Success = false;
                result.Data = "Không thể xóa tài khoản của mình";
                return result;
            }
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var user = uow.Users.Query(o => o.Id == id).FirstOrDefault();
                if (user != null)
                {
                    user.IsDeleted = true;
                    user.DeleterUserId = userId;
                    user.DeletionTime = DateTime.Now;
                    uow.Users.Update(user);
                    uow.Complete();
                }
            }
            return result;

        }

        public ResultDto CreateOfUpdateUser(CreateOrUpdateUserModel model, int? userId)
        {
            if (model.Id == 0)
            {
                return CreateUser(model, userId);
            }
            else
            {
                return UpdateUser(model, userId);
            }
        }

        private ResultDto CreateUser(CreateOrUpdateUserModel model, int? userId)
        {
            var result = new ResultDto();
            var idKey = Guid.NewGuid();
            var item = new AbpUser()
            {
                CreationTime = DateTime.Now,
                CreatorUserId = userId,
                LastModificationTime = DateTime.Now,
                LastModifierUserId = userId,
                Password = HashCodeMd5.GetMD5HashData(model.Password),
                PasswordResetCode = HashCodeMd5.GetMD5HashData(model.Password),
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                EmailAddress = model.EmailAddress,
                FullName = model.FirstName + " " + model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName,
                DonVi = model.DonVi,
                IsDeleted = false,
                IsActive = model.IsActive,
            };
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                if (uow.Users.Query(o => o.IsDeleted == false && o.Id != model.Id && o.UserName.ToUpper() == model.UserName.ToUpper()).Any())
                {
                    result.Success = false;
                    result.Data = "Tài khoản đã bị trùng";
                }
                else
                {
                    uow.Users.Add(item);
                    uow.Complete();
                    var itemRoles = model.Roles.Select(o => new AbpUserRole()
                    {
                        CreationTime = DateTime.Now,
                        CreatorUserId = userId,
                        RoleId = o,
                        UserId = item.Id,
                    });
                    uow.UserRoles.AddRange(itemRoles);
                    var check = uow.Complete(true);
                    if (check <= 0)
                    {
                        return ActionEroor();
                    }
                }

            }
            return result;
        }

        private ResultDto UpdateUser(CreateOrUpdateUserModel model, int? userId)
        {
            var result = new ResultDto();
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                if (uow.Users.Query(o => o.IsDeleted == false && o.Id != model.Id && o.UserName.ToUpper() == model.UserName.ToUpper()).Any())
                {
                    result.Success = false;
                    result.Data = "Tài khoản đã bị trùng";
                }
                else
                {
                    var item = uow.Users.Query(o => o.Id == model.Id).FirstOrDefault();
                    if (item != null)
                    {
                        item.FirstName = model.FirstName;
                        item.LastName = model.LastName;
                        item.DateOfBirth = model.DateOfBirth;
                        item.EmailAddress = model.EmailAddress;
                        item.FullName = model.FirstName + " " + model.LastName;
                        item.PhoneNumber = model.PhoneNumber;
                        item.UserName = model.UserName;
                        item.IsActive = model.IsActive;
                        item.PasswordResetCode = item.Password;
                        item.Password = !string.IsNullOrWhiteSpace(model.Password) ? HashCodeMd5.GetMD5HashData(model.Password) : item.Password;
                        item.LastModificationTime = DateTime.Now;
                        item.LastModifierUserId = userId;
                        item.DonVi = model.DonVi;
                        uow.Users.Update(item);

                        var listRoleOld = uow.UserRoles.Query(o => o.UserId == model.Id).ToList();
                        var listDelete = listRoleOld.Where(o => model.Roles.All(c => c != o.RoleId));
                        uow.UserRoles.RemoveRange(listDelete);

                        var itemRoles = model.Roles.Where(o => listRoleOld.All(c => c.RoleId != o)).Select(o => new AbpUserRole()
                        {
                            CreationTime = DateTime.Now,
                            CreatorUserId = userId,
                            RoleId = o,
                            UserId = item.Id,
                        });
                        uow.UserRoles.AddRange(itemRoles);

                        var check = uow.Complete(true);
                        if (check <= 0)
                        {
                            return ActionEroor();
                        }
                    }
                }
            }
            return result;
        }

        public ResultDto DeleteRole(int id, int? userId)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var role = uow.Roles.Query(o => o.Id == id).FirstOrDefault();
                if (role != null)
                {
                    role.IsDeleted = true;
                    role.DeleterUserId = userId;
                    role.DeletionTime = DateTime.Now;
                    uow.Roles.Update(role);
                    uow.Complete();
                }

            }
            return new ResultDto();
        }

        public ResultDto CreateOfUpdateRole(CreateOrUpdateRoleModel model, int? userId)
        {

            if (model.Id == null || model.Id == 0)
            {
                return CreateRole(model, userId);
            }
            else
            {
                return UpdateRole(model, userId);
            }

        }

        private ResultDto CreateRole(CreateOrUpdateRoleModel model, int? userId)
        {
            var result = new ResultDto();
            var idKey = Guid.NewGuid();
            var item = new AbpRole()
            {
                CreationTime = DateTime.Now,
                CreatorUserId = userId,
                LastModificationTime = DateTime.Now,
                LastModifierUserId = userId,
                DisplayName = model.DisplayName,
                IsDeleted = false,
                IsDefault = model.IsDefault,
                Name = idKey.ToString(),
                NormalizedName = idKey.ToString().ToUpper()
            };
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                if (uow.Roles.Query(o => o.Id != model.Id && o.DisplayName.ToUpper() == model.DisplayName.ToUpper()).Any())
                {
                    result.Success = false;
                    result.Data = "Tên vai trò đã bị trùng";
                }
                else
                {
                    uow.Roles.Add(item);
                    uow.Complete();
                    var itemPermissions = model.Permissions.Select(o => new AbpPermission()
                    {
                        CreationTime = DateTime.Now,
                        CreatorUserId = userId,
                        Name = o,
                        RoleId = item.Id,
                    });
                    uow.Permissions.AddRange(itemPermissions);
                    var check = uow.Complete(true);
                    if (check <= 0)
                    {
                        return ActionEroor();
                    }
                }

            }
            return result;
        }

        private ResultDto UpdateRole(CreateOrUpdateRoleModel model, int? userId)
        {
            var result = new ResultDto();
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                if (uow.Roles.Query(o => o.Id != model.Id && o.DisplayName.ToUpper() == model.DisplayName.ToUpper()).Any())
                {
                    result.Success = false;
                    result.Data = "Tên vai trò đã bị trùng";
                }
                else
                {
                    var item = uow.Roles.Query(o => o.Id == model.Id).FirstOrDefault();
                    if (item != null)
                    {
                        item.DisplayName = model.DisplayName;
                        item.IsDefault = model.IsDefault;
                        item.LastModificationTime = DateTime.Now;
                        item.LastModifierUserId = userId;
                        uow.Roles.Update(item);

                        var listPermissionOld = uow.Permissions.Query(o => o.RoleId == model.Id).ToList();
                        var listDelete = listPermissionOld.Where(o => model.Permissions.All(c => c != o.Name));
                        uow.Permissions.RemoveRange(listDelete);

                        var itemPermissions = model.Permissions.Where(o => listPermissionOld.All(c => c.Name != o)).Select(o => new AbpPermission()
                        {
                            CreationTime = DateTime.Now,
                            CreatorUserId = userId,
                            Name = o,
                            RoleId = model.Id,
                        });
                        uow.Permissions.AddRange(itemPermissions);

                        var check = uow.Complete(true);
                        if (check <= 0)
                        {
                            return ActionEroor();
                        }
                    }
                }
            }
            return result;
        }


        public ResultDto UpdatePassWord(CreateOrUpdateUserModel model, int? userId)
        {
            var result = new ResultDto();
            using (IUnitOfWork uow = base.unitOfWork.New())
            {

                var item = uow.Users.Query(o => o.Id == userId).FirstOrDefault();
                if (item != null)
                {

                    item.PasswordResetCode = item.Password;
                    item.Password = !string.IsNullOrWhiteSpace(model.Password) ? HashCodeMd5.GetMD5HashData(model.Password) : item.Password;
                    item.LastModificationTime = DateTime.Now;
                    item.LastModifierUserId = userId;
                    uow.Users.Update(item);
                    var check = uow.Complete(true);
                    if (check <= 0)
                    {
                        return ActionEroor();
                    }
                }
            }
            return result;
        }

        public async Task<object> GetRoles()
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                return await uow.Roles.GetAllAsync().ToListAsync();
            }
        }
    }
}
