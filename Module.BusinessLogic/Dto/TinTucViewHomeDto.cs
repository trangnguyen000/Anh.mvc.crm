using Module.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BusinessLogic.Dto
{
   public class TinTucViewHomeDto
    {
        public List<AppTinTuc> TinTucNoiBats { get; set; }
        public List<AppTinTuc> Banners { get; set; }

        public List<AppTinTuc> TinTucHomes { get; set; }

        public List<AppDictionary> ChuyenMuc { get; set; }
    }
}
