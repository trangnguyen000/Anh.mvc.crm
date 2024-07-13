using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BusinessLogic.Dto.view
{
    public class ContactSuportViewDto
    {
        public int? Id { get; set; }

        public string CustomerName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        public int? FromDataSourceId { get; set; }

        public string StudyProgramName { get; set; }

        public string SupportEmployeeName { get; set; }

        public string Note { get; set; }

        public short? Status { get; set; }
        public string StatusName { get; set; }
        public DateTime? CreationTime { get; set; }
    }
}
