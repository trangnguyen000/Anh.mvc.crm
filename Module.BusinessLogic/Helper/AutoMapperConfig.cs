using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BusinessLogic.Helper
{
    public static class AutoMapperConfig
    {
        private static MapperConfiguration mapperConfiguration;

        public static void Configure()
        {
            mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
                // Add other profiles if needed
            });
        }

        public static IMapper GetMapper()
        {
            return mapperConfiguration.CreateMapper();
        }
    }
}
