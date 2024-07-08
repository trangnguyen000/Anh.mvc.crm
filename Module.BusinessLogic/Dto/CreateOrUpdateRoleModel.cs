using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BusinessLogic.Dto
{
    public class CreateOrUpdateRoleModel
    {
        public int? Id { get; set; }
        public string DisplayName { get; set; }
        public bool? IsDefault { get; set; }
        public string[] Permissions { get; set; }

    }
}
