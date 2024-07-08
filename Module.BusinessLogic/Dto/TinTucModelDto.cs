using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BusinessLogic.Dto
{
    public class TinTucModelDto
    {
        public int Id { get; set; }
        public int? ChuyenMucId { get; set; }
        public string Anh { get; set; }
        public string Link { get; set; }
        public string TieuDe { get; set; }
        public string MoTa { get; set; }
        public string NoiDung { get; set; }
        public bool? IsViewHome { get; set; }

        public int? TypePage { get; set; }
    }
}
