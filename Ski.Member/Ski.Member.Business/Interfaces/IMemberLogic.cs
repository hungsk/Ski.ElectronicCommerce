using Ski.Member.Domain.Entities;

namespace Ski.Member.Business.Interfaces
{
    public interface IMemberLogic
    {
        EditResponse Create(MemberRequest request);

        EditResponse Update(MemberRequest request, string id);

        EditResponse Delete(string id);

        MemberResponse Get(string id);

        bool IsExists(string id);

        int GetMobileCount(string mobile);
        //ProductAllResponse GetList();

        //ProductListResponse GetList(int page);

        void Dispose();
    }
}
