namespace Ski.Demo1.Domain
{
    public interface IOrderLogic
    {
        OrderResponse Create(OrderRequest request, string username);

        EditResponse Delete(string id);

        EditResponse Update(OrderUpdateRequest request, string id);

        OrderGetResponse Get(string id);

        OrderListResponse GetList(int page);

        void Dispose();
    }
}