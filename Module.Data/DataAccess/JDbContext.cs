using Module.Data.DataAccess.Domain;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Module.Data.DataAccess
{
    public partial class JDbContext : DbContext
    {
        public JDbContext()
            : base("name=DbConnection")
        {
        }

        public virtual DbSet<AbpPermission> AbpPermissions { get; set; }
        public virtual DbSet<AbpRole> AbpRoles { get; set; }
        public virtual DbSet<AbpUserRole> AbpUserRoles { get; set; }
        public virtual DbSet<AbpUser> AbpUsers { get; set; }
        public virtual DbSet<AppDictionary> AppDictionaries { get; set; }
        public virtual DbSet<AppTinTuc> AppTinTucs { get; set; }
        public virtual DbSet<AppEmployee> AppEmployees { get; set; }
        public virtual DbSet<AppContactSupport> AppContactSupport { get; set; }
        public virtual DbSet<AppEmployeeExperience> AppEmployeeExperience { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppDictionary>()
                .Property(e => e.ValueKey)
                .IsUnicode(false);
        }
    }
}
