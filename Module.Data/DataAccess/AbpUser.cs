namespace Module.Data.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AbpUser
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string LastName { get; set; }

        [StringLength(250)]
        public string FirstName { get; set; }

        [StringLength(250)]
        public string FullName { get; set; }

        [StringLength(255)]
        public string EmailAddress { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsEmailConfirmed { get; set; }

        [StringLength(128)]
        public string Password { get; set; }

        [StringLength(128)]
        public string PasswordResetCode { get; set; }

        [StringLength(32)]
        public string PhoneNumber { get; set; }

        [StringLength(256)]
        public string UserName { get; set; }

        public int? Age { get; set; }

        [StringLength(500)]
        public string DonVi { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastModificationTime { get; set; }

        public int? LastModifierUserId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreationTime { get; set; }

        public int? CreatorUserId { get; set; }

        public int? DeleterUserId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletionTime { get; set; }
    }
}
