using Module.BusinessLogic.Dto;
using Module.BusinessLogic.Helper;
using Module.Data.DataAccess;
using Module.Repository.Shared;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BusinessLogic.SharedCore
{
    public class FrontEndLogic : BaseLogic, Shared.IFrontEndLogic
    {
        public FrontEndLogic(IUnitOfWork uow) : base(uow)
        {
        }

        public TinTucViewHomeDto GetTinTucByHome()
        {
            var data = new TinTucViewHomeDto();
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                data.TinTucHomes = new List<AppTinTuc>();
                var list = uow.TinTucs
                            .Query(o => o.IsDeleted == false && o.TypePage == (int)CommonHelper.TypePage.News)
                            .OrderByDescending(o => o.CreationTime).ToList();
                data.TinTucHomes.AddRange(list);
                var banners = uow.TinTucs
                           .Query(o => o.IsDeleted == false && o.TypePage == (int)CommonHelper.TypePage.Silder)
                           .OrderByDescending(o => o.LastModificationTime).ToList();
                data.Banners = banners;
                return data;
            }

        }


        public IPagedList<AppTinTuc> GetTinTucByChuyenMuc(int? chuyenmucId, int? page)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var data = uow.TinTucs.GetAll().Where(o => o.IsDeleted == false && o.ChuyenMucId == chuyenmucId)
                .OrderByDescending(o => o.CreationTime).ToPagedList(page ?? 1, 10);
                return data;
            }
        }

        public IPagedList<AppTinTuc> GetTinTucPhanTrang(int? page)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var data = uow.TinTucs.GetAll().Where(o => o.IsDeleted == false)
                .OrderByDescending(o => o.CreationTime).ToPagedList(page ?? 1, 8);
                return data;
            }
        }

        public IPagedList<AppTinTuc> SearchTinTuc(string filter, int? page)
        {
            filter = string.IsNullOrWhiteSpace(filter) ? "" : filter.ToUpper();
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var data = uow.TinTucs.GetAll().Where(o => o.IsDeleted == false
                             && o.TieuDe.ToUpper().Contains(filter))
                             .OrderByDescending(o => o.CreationTime).ToPagedList(page ?? 1, 10);
                return data;
            }
        }

        public List<AppTinTuc> GetTopTinTuc(int? top = 3)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var data = uow.TinTucs.GetAll().Where(o => o.IsDeleted == false && o.TypePage == (int)CommonHelper.TypePage.News)
                .OrderByDescending(o => o.CreationTime).Skip(0).Take(top ?? 3).ToList();
                return data;
            }
        }

        public List<AppDictionary> GetChuyenMuc(string type)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                return uow.Dictionarys.Query(o => o.IsDeleted == false && o.GroupCode == type)
                     .OrderBy(o => o.DisplayName).ToList();
            }
        }
        public async Task<string> GetStudyProgramById(int? id)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                var result = await uow.Dictionarys.Query(o => o.Id == id).FirstOrDefaultAsync();
                return result!=null? result.DisplayName : null;
            }
        }

        public AppDictionary GetChuyenMucById(int? id)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                return uow.Dictionarys.Query(o => o.Id == id).FirstOrDefault();
            }
        }

        public AppTinTuc GetTinTucById(int? id)
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                return uow.TinTucs.Query(o => o.Id == id).FirstOrDefault();
            }
        }

        public AppTinTuc GetPageLichSu()
        {
            using (IUnitOfWork uow = base.unitOfWork.New())
            {
                return uow.TinTucs.Query(o => o.IsDeleted == false).FirstOrDefault();
            }
        }


    }
}
