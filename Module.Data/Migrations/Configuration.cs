namespace Module.Data.Migrations
{
    using Module.Data.Helper;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Module.Data.DataAccess.JDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Module.Data.DataAccess.JDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            if (!context.AbpRoles.Any(o => o.DisplayName == "admin") && !context.AbpUsers.Any(o => o.UserName == "admin"))
            {

                var idKey = Guid.NewGuid().ToString();
                context.AbpRoles.Add(new DataAccess.AbpRole
                {
                    CreationTime = DateTime.Now,
                    DisplayName = "admin",
                    IsDefault = false,
                    IsDeleted = false,
                    Name = idKey,
                    NormalizedName = idKey.ToUpper()
                });
                context.AbpUsers.Add(new DataAccess.AbpUser
                {
                    CreationTime = DateTime.Now,
                    LastName = "admin",
                    UserName = "admin",
                    FullName = " admin",
                    IsActive = true,
                    IsDeleted = false,
                    Password = "46F94C8DE14FB36680850768FF1B7F2A",
                    PasswordResetCode = "46F94C8DE14FB36680850768FF1B7F2A",
                });
                context.SaveChanges();
                var role = context.AbpRoles.FirstOrDefault(o => o.Name == idKey);
                var user = context.AbpUsers.FirstOrDefault(o => o.UserName == "admin");
                if (role != null && user != null)
                {
                    foreach (var item in Permissions.ArrayPermissions.ToList())
                    {
                        foreach (var child in item.Children)
                        {
                            context.AbpPermissions.Add(new DataAccess.AbpPermission
                            {
                                CreationTime = DateTime.Now,
                                CreatorUserId = user.Id,
                                Name = child.KeyValue,
                                RoleId = role.Id
                            });

                        }
                    }
                    context.AbpUserRoles.Add(new DataAccess.AbpUserRole
                    {
                        CreationTime = DateTime.Now,
                        CreatorUserId = user.Id,
                        RoleId = role.Id,
                        UserId = user.Id
                    });
                    context.SaveChanges();
                }
            }


            //  to avoid creating duplicate seed data.
        }
    }
}
