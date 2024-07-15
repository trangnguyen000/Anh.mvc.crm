using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Data.DataAccess.Domain
{
    public class AppEmployee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(255)]
        public string EmailAddress { get; set; }

        [StringLength(500)]
        public string Image { get; set; }

        [StringLength(500)]
        public string PlaceOfOrigin { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public short? Gender { get; set; }

        [StringLength(25)]
        public string PhoneNumber { get; set; }

        [StringLength(2000)]
        public string Note { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? No { get; set; }

        [StringLength(15)]
        public string Language { get; set; }

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
