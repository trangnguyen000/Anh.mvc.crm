namespace Module.Data.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppDictionary")]
    public partial class AppDictionary
    {
        public int Id { get; set; }

        public int? ValueId { get; set; }

        [StringLength(150)]
        public string ValueKey { get; set; }

        [StringLength(250)]
        public string DisplayName { get; set; }

        [StringLength(150)]
        public string GroupCode { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreationTime { get; set; }

        public long? CreatorUserId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastModificationTime { get; set; }

        public long? LastModifierUserId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletionTime { get; set; }

        public long? DeleterUserId { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
