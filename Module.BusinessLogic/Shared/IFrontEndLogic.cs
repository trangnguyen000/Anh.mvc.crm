using Module.BusinessLogic.Dto;
using Module.Data.DataAccess;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BusinessLogic.Shared
{
    public interface IFrontEndLogic
    {
        TinTucViewHomeDto GetTinTucByHome();


        IPagedList<AppTinTuc> GetTinTucByChuyenMuc(int? chuyenmucId, int? page);

        IPagedList<AppTinTuc> GetTinTucPhanTrang(int? page);

        IPagedList<AppTinTuc> SearchTinTuc(string filter, int? page);

        List<AppTinTuc> GetTopTinTuc(int? top = 3);

        List<AppDictionary> GetChuyenMuc(string type);

        AppDictionary GetChuyenMucById(int? id);

        AppTinTuc GetTinTucById(int? id);

        AppTinTuc GetPageLichSu();


    }
}
