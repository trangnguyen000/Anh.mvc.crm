using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Data.DataAccess.Domain
{
    public class AppEmployeeExperience
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int? PositionId { get; set; }

        public short? TypeExperience { get; set; }

        public DateTime? EndDateTime { get; set; }

        public DateTime? StartDateTime { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastModificationTime { get; set; }

        public int? LastModifierUserId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreationTime { get; set; }

        public int? CreatorUserId { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsActive { get; set; }
    }
}
