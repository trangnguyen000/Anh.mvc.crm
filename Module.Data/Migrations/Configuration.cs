namespace Module.Data.Migrations
{
    using Module.Data.Helper;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Module.Data.DataAccess.JDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Module.Data.DataAccess.JDbContext context)
        {
            // Khởi tạo dữ liệu mặc định
            DbSeeder.Seed(context);
        }
    }
}
