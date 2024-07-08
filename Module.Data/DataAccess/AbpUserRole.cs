namespace Module.Data.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AbpUserRole
    {
        public int Id { get; set; }

        public int? RoleId { get; set; }

        public int? UserId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreationTime { get; set; }

        public int? CreatorUserId { get; set; }
    }
}
