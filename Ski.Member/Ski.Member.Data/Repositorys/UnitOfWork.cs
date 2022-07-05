using Ski.Member.Domain;
using Ski.Member.Domain.Entities;
using Ski.Member.Domain.Interfaces;

namespace Ski.Member.Data
{
    public class UnitOfWork : IUnitOfWorks
    {
        private bool _disposed = false;
        private ShinkongDbContext _context;
        private GenericRepository<MemberModel> _memberRepository;

        public UnitOfWork(ShinkongDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<MemberModel> MemberRepository
        {
            get
            {
                if (this._memberRepository == null)
                {
                    this._memberRepository = new GenericRepository<MemberModel>(_context);
                }
                return _memberRepository;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
    }
}