namespace Ski.Demo1.Domain
{
    public interface IArticlesLogic
    {
        EditResponse Create(ArticlesRequest request);

        EditResponse Update(ArticlesRequest request, string id);

        EditResponse Delete(string id);

        ArticlesResponse Get(string id);

        //ArticlesAllResponse GetList();

        ArticlesListResponse GetList(int page);

        void Dispose();
    }
}