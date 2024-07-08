using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Anh.mvc.crm.Models
{
    public class ResultDto
    {
        public bool Success { get; set; } = true;
        public object Data { get; set; }

        public int Total { get; set; } = 0;
    }
}