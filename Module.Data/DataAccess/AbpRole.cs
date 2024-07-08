namespace Module.Data.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AbpRole
    {
        public int Id { get; set; }

        [StringLength(256)]
        public string DisplayName { get; set; }

        public bool? IsDefault { get; set; }

        public bool? IsDeleted { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(150)]
        public string NormalizedName { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public int? LastModifierUserId { get; set; }

        public DateTime? CreationTime { get; set; }

        public int? CreatorUserId { get; set; }

        public int? DeleterUserId { get; set; }

        public DateTime? DeletionTime { get; set; }
    }
}
