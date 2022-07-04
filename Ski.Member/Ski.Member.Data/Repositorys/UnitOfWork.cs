using Ski.Member.Domain;

namespace Ski.Member.Data
{
    public class UnitOfWork : IUnitOfWorks
    {
        private bool _disposed = false;
        private SkDbContext _context;
        private GenericRepository<Domain.Member> _memberRepository;

        public UnitOfWork(SkDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Domain.Member> MemberRepository
        {
            get
            {
                if (this._memberRepository == null)
                {
                    this._memberRepository = new GenericRepository<Domain.Member>(_context);
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