using Anh.mvc.crm.Authentication;
using Anh.mvc.crm.Helper;
using Module.BusinessLogic.Dto;
using Module.BusinessLogic.Dto.Filter;
using Module.BusinessLogic.Shared;
using Module.BusinessLogic.SharedCore;
using Module.Repository;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;


namespace Anh.mvc.crm.Api
{
    [RoutePrefix("api/ApiBusiness")]
    [CustomAuthentication]
    public class ApiBusinessController : ApiBaseController
    {
        private readonly IBusinessLogic _businessLogic;

        private readonly ConfigCurent _config;
        public ApiBusinessController()
        {
            _businessLogic = new BusinessLogic(new UnitOfWork());
            _config = new ConfigCurent();
        }

        [HttpGet]
        [Route("GetDanhSachTinTuc")]
        public async Task<IHttpActionResult> GetDanhSachTinTuc(int? typePage,string filter, int? page, int? pageSize )
        {
            var data = await _businessLogic.GetDanhSachTinTuc(typePage,filter ?? "", page ?? 1, pageSize??10);
            return Ok(data);
        }

        [HttpPost]
        [Route("GetDanhSachLienHe")]
        public async Task<IHttpActionResult> GetDanhSachLienHe([FromBody] ContactSupportFIlterDto filter)
        {
            var data = await _businessLogic.GetDanhSachLienHe( filter);
            return Ok(data);
        }

        [HttpGet]
        [Route("GetSuggestDanhMuc")]
        public async Task<IHttpActionResult> GetSuggestDanhMuc(string key)
        {
            var data = await _businessLogic.GetSuggestDanhMuc(key);
            return Ok(data);
        }


        [HttpGet]
        [Route("GetDanhMuc")]
        public async Task<IHttpActionResult> GetDanhMuc(string key, string filter, int? page)
        {
            var data = await _businessLogic.GetDanhMuc(key,filter ?? "", page);
            return Ok(data);
        }
        [HttpGet]
        [Route("GetConfig")]
        public async Task<IHttpActionResult> GetConfig(string key)
        {
            var data = await _businessLogic.GetConfig(key);
            return Ok(data);
        }
        [HttpGet]
        [Route("GetChiTietTinTucById")]
        public async Task<IHttpActionResult> GetChiTietTinTucById(int id)
        {
            var data = await _businessLogic.GetChiTietTinTucById(id);
            return Ok(data);
        }

      
        [HttpGet]
        [Route("GetChiTietTinTucPageById")]
        public async Task<IHttpActionResult> GetChiTietTinTucPageById()
        {
            var data = await _businessLogic.GetChiTietTinTucPageById();
            return Ok(data);
        }
        
      
        [HttpPost]
        [Route("SaveTinTuc")]
        public async Task<IHttpActionResult> SaveTinTuc(TinTucModelDto model)
        {
            model.TieuDe = model.TieuDe ?? string.Empty;
            model.Link = Common.ConvertUrl(model.TieuDe);
            var data = await _businessLogic.SaveTinTuc(model, _config.CurrentUser.Id);
            return Ok(data);
        }


        [HttpPost]
        [Route("DeleteTinTuc")]
        public async Task DeleteTinTuc(TinTucModelDto model)
        {
            await _businessLogic.DeleteTinTuc(model.Id, _config.CurrentUser.Id);
        }

        [HttpPost]
        [Route("DeleteDanhMuc")]
        public async Task DeleteDanhMuc(CreateOrUpdateDictionaryModel model)
        {
            await _businessLogic.DeleteDanhMuc(model.Id, _config.CurrentUser.Id);
        }

      
        [HttpPost]
        [Route("SaveDanhMuc")]
        public async Task<IHttpActionResult> SaveDanhMuc(CreateOrUpdateDictionaryModel model)
        {
            var data = await _businessLogic.SaveDanhMuc(model, _config.CurrentUser.Id);
            return Ok(data);
        }
        [HttpPost]
        [Route("SaveContractSupport")]
        public async Task<IHttpActionResult> SaveContractSupport(CreateOrUpdateContactSuportDto model )
        {
            var data = await _businessLogic.SaveContractSupport(model, _config.CurrentUser.Id);
            return Ok(data);
        }

        [HttpPost]
        [Route("DeleteContractSupport")]
        public async Task DeleteContractSupport(CreateOrUpdateContactSuportDto model)
        {
            await _businessLogic.DeleteContractSupport(model.Id, _config.CurrentUser.Id);
        }

        [HttpPost]
        [Route("BackUpData")]
        public async Task<IHttpActionResult> BackUp(CreateOrUpdateDictionaryModel model)
        {
            try
            {
                string folderName = model.GhiChu;
                string folfer = DateTime.Now.ToString("yyyy-MM-dd.HH-mm");
                string pathString = System.IO.Path.Combine(folderName, folfer);
                System.IO.Directory.CreateDirectory(pathString);
                string fileName = "SoureCode.zip";
                string zipPath = System.IO.Path.Combine(pathString, fileName);
                string startPath = System.IO.Path.Combine(WebConfigurationManager.AppSettings["ConfigSoureCode"]);
                ZipFile.CreateFromDirectory(startPath, zipPath);
                await _businessLogic.BackUpDatabase(pathString, WebConfigurationManager.AppSettings["ConfigDatabase"]);
                return Ok();
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

    }
}