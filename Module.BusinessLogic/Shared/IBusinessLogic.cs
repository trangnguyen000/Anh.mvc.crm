using Module.BusinessLogic.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BusinessLogic.Shared
{
    public interface IBusinessLogic
    {
        Task<ResultDto> GetDanhSachTinTuc(int? typePage,string filter, int page, int pageSize);


        Task<object> GetSuggestDanhMuc(string key);

        Task<ResultDto> GetDanhMuc(string key, string filter, int? page);

        Task<object> GetConfig(string key);


        Task<object> GetChiTietTinTucById(int id);



        Task<object> GetChiTietTinTucPageById();


        Task<int> SaveTinTuc(TinTucModelDto model, long? userId);

   

        Task DeleteTinTuc(int id, long? userId);

     

        Task<int> SaveDanhMuc(CreateOrUpdateDictionaryModel model, long? userId);

        Task DeleteDanhMuc(int id, long? userId);


        Task BackUpDatabase(string filePath, string databaseName);

    }
}
