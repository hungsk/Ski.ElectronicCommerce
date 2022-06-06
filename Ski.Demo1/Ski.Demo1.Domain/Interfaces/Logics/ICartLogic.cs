namespace Ski.Demo1.Domain
{
    public interface ICartLogic
    {
        CartResponse Create(CartRequest request, string username);

        CartResponse Update(CartRequest request, string id, string username);

        EditResponse Delete(string id, string username);

        EditResponse Delete(string username);

        CartListResponse GetList(string username);

        void Dispose();
    }
}