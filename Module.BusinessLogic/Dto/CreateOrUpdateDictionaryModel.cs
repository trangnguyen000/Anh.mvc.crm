using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BusinessLogic.Dto
{
    public class CreateOrUpdateDictionaryModel
    {
        public int Id { get; set; }
        public string GroupCode { get; set; }
        public string DisplayName { get; set; }
        public string ValueKey { get; set; }
        public string GhiChu { get; set; }
    }
}
