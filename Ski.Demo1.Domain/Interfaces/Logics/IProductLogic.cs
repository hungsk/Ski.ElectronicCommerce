namespace Ski.Demo1.Domain
{
    public interface IProductLogic
    {
        EditResponse Create(ProductRequest request);

        EditResponse Update(ProductRequest request, string id);

        EditResponse Delete(string id);

        ProductResponse Get(string id);

        ProductAllResponse GetList();

        ProductListResponse GetList(int page);

        void Dispose();
    }
}