using Ski.Member.Domain.Entities;

namespace Ski.Member.Domain.Interfaces
{
    public interface IUnitOfWorks : IDisposable
    {
        IGenericRepository<MemberModel> MemberRepository { get; }

        void SaveChanges();
    }
}