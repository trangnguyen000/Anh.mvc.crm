using Anh.mvc.crm.Authentication;
using Anh.mvc.crm.Models;
using Hangfire;
using Module.BusinessLogic.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anh.mvc.crm.Api
{
    [RoutePrefix("api/ApiBase")]
    public class ApiBaseController : ApiController
    {
        [CustomAuthentication(Roles = "User")]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            return Ok(1);
        }

        [HttpGet]
        [Route("CallJob")]
        public void CrallJob()
        {
            RecurringJob.AddOrUpdate(Guid.NewGuid().ToString(), () => XuLyJob(), "* * * * *", TimeZoneInfo.Local);
        }

        [HttpGet]
        [Route("GetStatus")]
        public IHttpActionResult GetStatus(string key)
        {
            var result = new ResultDto() { Success = true };
            switch (key)
            {
                case KeyCodeDictionary.StudyProgram:
                    result.Data = Common.StatusContactSupports.ToList();
                    break;
            }
            return Ok(result);
        }

        public void XuLyJob()
        {
        }

    }
}
