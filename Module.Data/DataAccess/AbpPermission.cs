namespace Module.Data.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AbpPermission
    {
        public int Id { get; set; }

        public string Discriminator { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        public int? RoleId { get; set; }

        public int? UserId { get; set; }

        public DateTime? CreationTime { get; set; }

        public int? CreatorUserId { get; set; }
    }
}
