using AutoMapper;
using Module.BusinessLogic.Dto;
using Module.BusinessLogic.Dto.Filter;
using Module.BusinessLogic.Dto.view;
using Module.BusinessLogic.Helper;
using Module.Data.DataAccess;
using Module.Data.DataAccess.Domain;
using Module.Repository.Helper;
using Module.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static Module.BusinessLogic.Helper.Common;

namespace Module.BusinessLogic.SharedCore
{
    public class BusinessLogic : BaseLogic, Shared.IBusinessLogic
    {
        private readonly IMapper _mapper;
        public BusinessLogic(IUnitOfWork uow) : base(uow)
        {
            _mapper = AutoMapperConfig.GetMapper();
        }

        public async Task<ResultDto> GetDanhSachTinTuc(int? typePage, string filter, int page, int pageSize)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var result = new ResultDto();
                typePage = typePage ?? (int)CommonHelper.TypePage.News;
                var data = uow.TinTucs.Query(o => o.IsDeleted == false && o.TypePage == typePage);
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    data = data.Where(o => o.TieuDe.ToUpper().Contains(filter.ToUpper()));
                }
                result.Total = await data.CountAsync();
                var listId = await data.OrderByDescending(o => o.CreationTime)
                                      .Skip((page - 1) * pageSize).Take(pageSize)
                                      .Select(o => o.Id).ToListAsync();
                result.Data = await (from t in uow.TinTucs.Query(o => listId.Any(c => c == o.Id))
                                     join d in uow.Dictionarys.Query(c => c.GroupCode == KeyCodeDictionary.ChuyenMucTinTuc) on t.ChuyenMucId equals d.Id into dis
                                     from di in dis.DefaultIfEmpty()
                                     select new
                                     {
                                         t.Id,
                                         t.TieuDe,
                                         t.Mota,
                                         t.NoiDung,
                                         t.Link,
                                         t.Anh,
                                         t.IsViewHome,
                                         t.TrangThai,
                                         t.CreationTime,
                                         t.ChuyenMucId,
                                         TenChuyenMuc = di.DisplayName
                                     }).ToListAsync();

                return result;
            }
        }

        public async Task<ResultDto> GetEmployeeByPaging(string filter, int page, int pageSize)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var result = new ResultDto();
                var data = uow.AppEmployees.Query(o => o.IsDeleted == false);
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    var textFilter = filter.Trim().ToUpper();
                    data = data.Where(o => (!string.IsNullOrEmpty(o.FullName) && o.FullName.ToUpper().Contains(textFilter))
                                            || (!string.IsNullOrEmpty(o.EmailAddress) && o.EmailAddress.ToUpper().Contains(textFilter))
                                             || (!string.IsNullOrEmpty(o.PhoneNumber) && o.PhoneNumber.ToUpper().Contains(textFilter))
                                             || (!string.IsNullOrEmpty(o.PlaceOfOrigin) && o.PlaceOfOrigin.ToUpper().Contains(textFilter))
                                              || (!string.IsNullOrEmpty(o.PhoneNumber) && o.PhoneNumber.ToUpper().Contains(textFilter)));
                }
                result.Total = await data.CountAsync();
                var listId = await data.OrderByDescending(o => o.CreationTime)
                                      .Skip((page - 1) * pageSize).Take(pageSize)
                                      .Select(o => o.Id).ToListAsync();
                var resultData = await (from t in uow.AppEmployees.Query(o => listId.Any(c => c == o.Id)).OrderByDescending(o => o.CreationTime)
                                        select  t).ToListAsync();

                result.Data = _mapper.Map<List<EmployeeViewDto>>(resultData);
                return result;
            }
        }

        public async Task<ResultDto> GetDanhSachLienHe(ContactSupportFilterDto filter)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var result = new ResultDto();
                var data = uow.AppContactSupports.Query(o => o.IsDeleted == false)
                                                   .Where(c => filter.Status == null || filter.Status == 0 || c.Status == filter.Status);
                if (!string.IsNullOrWhiteSpace(filter.Filter))
                {
                    var textFilter = filter.Filter.Trim().ToUpper();
                    data = data.Where(o => (!string.IsNullOrEmpty(o.CustomerName) && o.CustomerName.ToUpper().Contains(textFilter))
                                            || (!string.IsNullOrEmpty(o.SupportEmployeeName) && o.SupportEmployeeName.ToUpper().Contains(textFilter))
                                             || (!string.IsNullOrEmpty(o.StudyProgramName) && o.StudyProgramName.ToUpper().Contains(textFilter))
                                             || (!string.IsNullOrEmpty(o.EmailAddress) && o.EmailAddress.ToUpper().Contains(textFilter))
                                              || (!string.IsNullOrEmpty(o.PhoneNumber) && o.PhoneNumber.ToUpper().Contains(textFilter)));
                }
                result.Total = await data.CountAsync();
                var listId = await data.OrderByDescending(o => o.CreationTime)
                                      .Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize)
                                      .Select(o => o.Id).ToListAsync();
                var resultData = await (from t in uow.AppContactSupports.Query(o => listId.Any(c => c == o.Id)).OrderByDescending(o => o.CreationTime)
                                        select new ContactSuportViewDto
                                        {
                                            Id = t.Id,
                                            StudyProgramName = t.StudyProgramName,
                                            PhoneNumber = t.PhoneNumber,
                                            CustomerName = t.CustomerName,
                                            EmailAddress = t.EmailAddress,
                                            SupportEmployeeName = t.SupportEmployeeName,
                                            Description = t.Description,
                                            Note = t.Note,
                                            Status = t.Status,
                                            CreationTime = t.CreationTime
                                        }).ToListAsync();
                foreach (var item in resultData)
                {
                    item.StatusName = StatusContactSupports[item.Status ?? (short)StatusContactSupport.New];
                }
                result.Data = resultData;
                return result;
            }
        }

        public async Task<object> GetSuggestDanhMuc(string key)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                return await uow.Dictionarys.Query(c => c.IsDeleted == false && c.GroupCode == key).OrderBy(c => c.DisplayName).ToListAsync();
            }
        }

        public async Task<ResultDto> GetDanhMuc(string key, string filter, int? page)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var result = new ResultDto();

                var data = uow.Dictionarys.Query(o => o.IsDeleted == false && o.GroupCode == key);
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    data = data.Where(o => o.DisplayName.ToUpper().Contains(filter.ToUpper()));
                }
                result.Total = await data.CountAsync();
                result.Data = await data.OrderByDescending(o => o.CreationTime)
                                      .Skip(((page ?? 1) - 1) * 10).Take(10)
                                      .Select(t => new
                                      {
                                          t.Id,
                                          t.DisplayName,
                                          t.GhiChu,
                                          t.CreationTime,
                                      }).ToListAsync();
                return result;
            }
        }

        public async Task<object> GetConfig(string key)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var result = await uow.Dictionarys.Query(c => (c.IsDeleted == false || c.IsDeleted == null) && c.GroupCode == key)
                    .Select(o => new
                    {
                        o.Id,
                        o.ValueId,
                        o.DisplayName,
                        o.GhiChu,
                        o.CreationTime
                    }).ToListAsync();
                return result;
            }
        }

        public async Task<object> GetChiTietTinTucById(int id)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var result = await (from t in uow.TinTucs.Query(c => c.Id == id)
                                    join d in uow.Dictionarys.Query(c => c.IsDeleted == false && c.GroupCode == "KeyChuyenMucTinTuc") on t.ChuyenMucId equals d.Id into dis
                                    from di in dis.DefaultIfEmpty()
                                    select new
                                    {
                                        t.Id,
                                        t.TieuDe,
                                        t.Mota,
                                        t.NoiDung,
                                        t.Link,
                                        t.Anh,
                                        t.IsViewHome,
                                        t.ChuyenMucId,
                                        TenChuyenMuc = di.DisplayName
                                    }).ToListAsync();
                return result;
            }
        }

        public async Task<object> GetChiTietTinTucPageById()
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                return await uow.TinTucs.Query(c => c.TypePage == (int)CommonHelper.TypePage.News && c.IsDeleted == false).OrderByDescending(c => c.CreationTime).Take(1).ToListAsync();
            }
        }

        public async Task<int> SaveTinTuc(TinTucModelDto model, long? userId)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                if (!string.IsNullOrWhiteSpace(model.TieuDe) && await uow.TinTucs.Query(o => o.IsDeleted == false && o.Id != model.Id && o.TieuDe.ToUpper() == model.TieuDe.ToUpper()).AnyAsync())
                {
                    return 2;
                }
                if (model.Id == 0)
                {
                    var entity = new AppTinTuc
                    {
                        TieuDe = model.TieuDe,
                        Anh = model.Anh,
                        ChuyenMucId = model.ChuyenMucId,
                        Mota = model.MoTa,
                        NoiDung = model.NoiDung,
                        TrangThai = 1,
                        IsViewHome = model.IsViewHome,
                        CreationTime = DateTime.Now,
                        CreatorUserId = userId,
                        LastModificationTime = DateTime.Now,
                        LastModifierUserId = userId,
                        TypePage = model.TypePage ?? (int)CommonHelper.TypePage.News,
                        IsDeleted = false,
                    };
                    uow.TinTucs.Add(entity);
                    uow.Complete();

                    entity.Link = "/" + CommonHelper.PageUrl + "/" + entity.Id.ToString() + "/" + model.Link;
                    uow.TinTucs.Update(entity);
                    uow.Complete();
                }
                else
                {
                    var entityUpdate = await uow.TinTucs.Query(o => o.Id == model.Id).FirstOrDefaultAsync();
                    if (entityUpdate != null)
                    {
                        entityUpdate.Link = "/" + CommonHelper.PageUrl + "/" + model.Id.ToString() + "/" + model.Link;
                        entityUpdate.TieuDe = model.TieuDe;
                        entityUpdate.Mota = model.MoTa;
                        entityUpdate.Anh = model.Anh;
                        entityUpdate.NoiDung = model.NoiDung;
                        entityUpdate.ChuyenMucId = model.ChuyenMucId;
                        entityUpdate.IsViewHome = model.IsViewHome;
                        entityUpdate.LastModificationTime = DateTime.Now;
                        entityUpdate.LastModifierUserId = userId;
                        uow.TinTucs.Update(entityUpdate);
                        uow.Complete();
                    }
                }
                return 1;
            }


        }

        public async Task DeleteTinTuc(int id, long? userId)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var model = await uow.TinTucs.Query(o => o.Id == id).FirstOrDefaultAsync();
                if (model != null)
                {
                    model.IsDeleted = true;
                    model.DeletionTime = DateTime.Now;
                    model.DeleterUserId = userId;
                    uow.TinTucs.Update(model);
                    uow.Complete();
                }
            }

        }

        public async Task DeleteDanhMuc(int id, long? userId)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var model = await uow.Dictionarys.Query(o => o.Id == id).FirstOrDefaultAsync();
                if (model != null)
                {
                    model.IsDeleted = true;
                    model.DeletionTime = DateTime.Now;
                    model.DeleterUserId = userId;
                    uow.Dictionarys.Update(model);
                    uow.Complete();
                }
            }
        }

        public async Task<int> SaveDanhMuc(CreateOrUpdateDictionaryModel model, long? userId)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                if (await uow.Dictionarys.Query(o => o.IsDeleted == false
                && o.Id != model.Id
                && o.GroupCode == model.GroupCode
                && o.DisplayName.ToUpper() == model.DisplayName.ToUpper()).AnyAsync())
                {
                    return 2;
                }
                if (model.Id == 0)
                {
                    var entity = new AppDictionary
                    {
                        DisplayName = model.DisplayName,
                        GroupCode = model.GroupCode,
                        ValueKey = model.ValueKey,
                        GhiChu = model.GhiChu,
                        CreationTime = DateTime.Now,
                        CreatorUserId = userId,
                        LastModificationTime = DateTime.Now,
                        LastModifierUserId = userId,
                        IsDeleted = false,
                    };
                    uow.Dictionarys.Add(entity);
                    uow.Complete();

                }
                else
                {
                    var entityUpdate = await uow.Dictionarys.Query(o => o.Id == model.Id).FirstOrDefaultAsync();
                    if (entityUpdate != null)
                    {
                        entityUpdate.DisplayName = model.DisplayName;
                        entityUpdate.ValueKey = model.ValueKey;
                        entityUpdate.GhiChu = model.GhiChu;
                        entityUpdate.LastModificationTime = DateTime.Now;
                        entityUpdate.LastModifierUserId = userId;
                        uow.Dictionarys.Update(entityUpdate);
                        uow.Complete();
                    }
                }
                return 1;
            }
        }

        public async Task<int> SaveContractSupport(CreateOrUpdateContactSuportDto model, int? userId)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                if (model.Id == null)
                {
                    var entity = _mapper.Map<CreateOrUpdateContactSuportDto, AppContactSupport>(model);

                    entity.CreationTime = DateTime.Now;
                    entity.CreatorUserId = userId;
                    entity.LastModificationTime = DateTime.Now;
                    entity.LastModifierUserId = userId;
                    entity.IsDeleted = false;
                    uow.AppContactSupports.Add(entity);
                    uow.Complete();
                }
                else
                {
                    var entityUpdate = await uow.AppContactSupports.Query(o => o.Id == model.Id).FirstOrDefaultAsync();
                    if (entityUpdate != null)
                    {
                        _mapper.Map(model, entityUpdate);
                        entityUpdate.LastModificationTime = DateTime.Now;
                        entityUpdate.LastModifierUserId = userId;
                        uow.AppContactSupports.Update(entityUpdate);
                        uow.Complete();
                    }
                }
                return 1;
            }
        }

        public async Task<int> SaveEmployee(CreateOrUpdateEmployeeDto model, int? userId)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                if (model.Id == null)
                {
                    var entity = _mapper.Map<CreateOrUpdateEmployeeDto, AppEmployee>(model);

                    entity.CreationTime = DateTime.Now;
                    entity.CreatorUserId = userId;
                    entity.LastModificationTime = DateTime.Now;
                    entity.LastModifierUserId = userId;
                    entity.IsDeleted = false;
                    uow.AppEmployees.Add(entity);
                    uow.Complete();
                }
                else
                {
                    var entityUpdate = await uow.AppEmployees.Query(o => o.Id == model.Id).FirstOrDefaultAsync();
                    if (entityUpdate != null)
                    {
                        _mapper.Map(model, entityUpdate);
                        entityUpdate.LastModificationTime = DateTime.Now;
                        entityUpdate.LastModifierUserId = userId;
                        uow.AppEmployees.Update(entityUpdate);
                        uow.Complete();
                    }
                }
                return 1;
            }
        }

        public async Task DeleteContractSupport(int? id, int? userId)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var model = await uow.AppContactSupports.Query(o => o.Id == id).FirstOrDefaultAsync();
                if (model != null)
                {
                    model.IsDeleted = true;
                    model.LastModificationTime = DateTime.Now;
                    model.LastModifierUserId = userId;
                    uow.AppContactSupports.Update(model);
                    uow.Complete();
                }
            }
        }
        public async Task BackUpDatabase(string filePath, string databaseName)
        {
            string fileName = "Database.bak";
            string zipPath = System.IO.Path.Combine(filePath, fileName);
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var query = String.Format("BACKUP DATABASE [{0}] TO  DISK = N'{1}' WITH NOFORMAT, NOINIT,  NAME = N'AbpZeroDbNewsPolice-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10 ", databaseName, zipPath);
                SqlConnection con = new SqlConnection(uow.GetDatabase().Connection.ConnectionString);
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
