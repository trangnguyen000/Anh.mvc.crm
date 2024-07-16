using Module.Data.DataAccess;
using Module.Data.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Repository.Shared
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<AbpUser> Users { get; }
        IRepository<AbpRole> Roles { get; }
        IRepository<AbpUserRole> UserRoles { get; }
        IRepository<AbpPermission> Permissions { get; }
        IRepository<AppTinTuc> TinTucs { get; }
        IRepository<AppDictionary> Dictionarys { get; }
        IRepository<AppEmployee> AppEmployees { get; }
        IRepository<AppContactSupport> AppContactSupports { get; }
        int Complete();
        Database GetDatabase();
        int Complete(bool usingTransaction);

        IUnitOfWork New();
    }
}
