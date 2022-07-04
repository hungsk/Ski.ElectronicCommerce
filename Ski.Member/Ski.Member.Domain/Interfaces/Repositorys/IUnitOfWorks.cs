namespace Ski.Member.Domain
{
    public interface IUnitOfWorks : IDisposable
    {
        IGenericRepository<Member> MemberRepository { get; }

        void SaveChanges();
    }
}