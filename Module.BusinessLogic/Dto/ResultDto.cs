using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BusinessLogic.Dto
{
    public class ResultDto
    {
        public bool Success { get; set; } = true;
        public object Data { get; set; }
        public int Total { get; set; } = 0;
    }
    public class  ResultRoleDto: ResultDto
    {
        public object Permissions { get; set; }
    }
    public class ResultUserDto : ResultDto
    {
        public object Roles { get; set; }
    }
}
