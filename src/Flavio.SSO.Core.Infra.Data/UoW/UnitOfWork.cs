using Flavio.SSO.Core.Infra.Data.Context;
using Flavio.SSO.Core.Infra.Data.Interfaces;
using System;

namespace Flavio.SSO.Core.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoreContext _context;
        private bool _disposed;

        public UnitOfWork(CoreContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _disposed = false;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}