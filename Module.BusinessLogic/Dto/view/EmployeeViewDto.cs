using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Module.BusinessLogic.Helper.CommonHelper;

namespace Module.BusinessLogic.Dto.view
{
    public class EmployeeViewDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public string Image { get; set; }

        public string PlaceOfOrigin { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public short? Gender { get; set; }

        public string GenderName { get; set; }

        public string PhoneNumber { get; set; }

        public string Note { get; set; }

        public short? Status { get; set; }

        public DateTime? CreationTime { get; set; }

        public bool? IsActive { get; set; }

        public int? No { get; set; }

        [StringLength(15)]
        public string Language { get; set; }
    }
}
