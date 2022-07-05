using Ski.Member.Domain.Entities;

namespace Ski.Member.Domain.Interfaces
{
    public interface IMemberLogic
    {
        EditResponse Create(MemberRequest request);

        EditResponse Update(MemberRequest request, string id);

        EditResponse Delete(string id);

        MemberResponse Get(string id);

        //ProductAllResponse GetList();

        //ProductListResponse GetList(int page);

        void Dispose();
    }
}
