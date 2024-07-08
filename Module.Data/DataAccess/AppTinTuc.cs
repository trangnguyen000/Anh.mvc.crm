namespace Module.Data.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppTinTuc")]
    public partial class AppTinTuc
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string TieuDe { get; set; }

        [StringLength(500)]
        public string Mota { get; set; }

        public string NoiDung { get; set; }

        [StringLength(250)]
        public string Link { get; set; }

        [StringLength(500)]
        public string Anh { get; set; }

        public int? ChuyenMucId { get; set; }

        public int? TypePage { get; set; }

        public byte? TrangThai { get; set; }

        public bool? IsViewHome { get; set; }

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
