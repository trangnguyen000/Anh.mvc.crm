using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Data.DataAccess.Domain
{
    public class AppContactSupport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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

        [StringLength(500)]
        public string Note { get; set; }

        public short? Status { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastModificationTime { get; set; }

        public int? LastModifierUserId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreationTime { get; set; }

        public int? CreatorUserId { get; set; }

        public bool? IsDeleted { get; set; }

    }
}
