using Ski.Member.Domain.Entities;

namespace Ski.Member.Data.Interfaces
{
    public interface IUnitOfWorks : IDisposable
    {
        IGenericRepository<MemberModel> MemberRepository { get; }

        void SaveChanges();
    }
}