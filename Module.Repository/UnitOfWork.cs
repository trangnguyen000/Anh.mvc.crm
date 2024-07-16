using Module.Data.DataAccess;
using Module.Data.DataAccess.Domain;
using Module.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context = null;
        private readonly Type ContextType;
        public UnitOfWork()
        {
            // Store the type
            ContextType = typeof(JDbContext);
        }

        private DbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public IRepository<AbpUser> Users
        {
            get
            {
                return new Repository<AbpUser>(_context);
            }
        }

        public IRepository<AbpUserRole> UserRoles
        {
            get
            {
                return new Repository<AbpUserRole>(_context);
            }
        }

        public IRepository<AbpRole> Roles
        {
            get
            {
                return new Repository<AbpRole>(_context);
            }
        }

        public IRepository<AbpPermission> Permissions
        {
            get
            {
                return new Repository<AbpPermission>(_context);
            }
        }
        public IRepository<AppTinTuc> TinTucs
        {
            get
            {
                return new Repository<AppTinTuc>(_context);
            }
        }
        public IRepository<AppDictionary> Dictionarys
        {
            get
            {
                return new Repository<AppDictionary>(_context);
            }
        }
        public IRepository<AppEmployee> AppEmployees
        {
            get
            {
                return new Repository<AppEmployee>(_context);
            }
        }
        public IRepository<AppContactSupport> AppContactSupports
        {
            get
            {
                return new Repository<AppContactSupport>(_context);
            }
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public int Complete(bool usingTransaction)
        {
            if (!usingTransaction)
                return Complete();

            int status = -1;
            using (var ts = _context.Database.BeginTransaction())
            {
                try
                {
                    status = _context.SaveChanges();
                    ts.Commit();
                }
                catch (Exception)
                {
                    ts.Rollback();
                    throw;
                }
            }
            return status;
        }

        public Database GetDatabase()
        {
            return _context.Database;
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        public IUnitOfWork New()
        {
            _context = null;
            TryCreateContext();
            return this;
        }

        void TryCreateContext()
        {
            if (ContextType == default(Type))
                throw new Exception("Context type Unknown");

            if (_context == null)
                _context = Activator.CreateInstance(ContextType) as DbContext;
        }
    }
}
