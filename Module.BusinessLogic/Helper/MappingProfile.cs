using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BusinessLogic.Helper
{
    using AutoMapper;
    using Module.BusinessLogic.Dto;
    using Module.Data.DataAccess.Domain;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateOrUpdateContactSuportDto, AppContactSupport > ();
        }
    }

}
