using Module.BusinessLogic.Dto;
using Module.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BusinessLogic.SharedCore
{
    public abstract class BaseLogic
    {
        protected readonly IUnitOfWork unitOfWork;

        public BaseLogic(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public  ResultDto ActionEroor()
        {
            return new ResultDto() { Success = false, Data = "Đã xảy ra lỗi" };
        }
    }
}
