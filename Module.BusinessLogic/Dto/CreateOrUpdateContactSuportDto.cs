using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Module.BusinessLogic.Helper.CommonHelper;

namespace Module.BusinessLogic.Dto
{
    public class CreateOrUpdateContactSuportDto
    {
        public int? Id { get; set; }

        [StringLength(255)]
        public string CustomerName { get; set; }

        [StringLength(255)]
        public string EmailAddress { get; set; }

        [StringLength(25)]
        public string PhoneNumber { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public int? FromDataSourceId { get; set; }

        public string StudyProgramName { get; set; }

        public string SupportEmployeeName { get; set; }

        public string Note { get; set; }

        public short? Status { get; set; }

    }
}
