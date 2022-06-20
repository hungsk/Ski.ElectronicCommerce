using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ski.Demo1.Domain;

namespace Ski.Demo1.WebService.Controllers
{
    [ApiController]
    public class ArticlesController : Controller
    {
        private readonly IArticlesLogic _articlesLogic;

        public ArticlesController(IArticlesLogic articlesService)
        {
            _articlesLogic = articlesService;
        }

        [Route("api/hung/admin/articles/all")]
        [HttpGet]
        public ArticlesListResponse GetArticlesList(int page)
        {
            var productList = _articlesLogic.GetList(page);
            return productList;
        }

        [Route("api/hung/admin/article")]
        [Authorize]
        [HttpPost]
        public EditResponse AddArticles(ArticlesRequest request)
        {
            return _articlesLogic.Create(request);
        }

        [Route("api/hung/admin/article/{id}")]
        [HttpGet]
        public ArticlesResponse GetArticles(string id)
        {
            return _articlesLogic.Get(id);
        }

        [Route("api/hung/admin/article/{id}")]
        [Authorize]
        [HttpPut]
        public EditResponse UpdateArticles(ArticlesRequest request, string id)
        {
            return _articlesLogic.Update(request, id);
        }

        [Route("api/hung/admin/article/{id}")]
        [Authorize]
        [HttpDelete]
        public EditResponse DeleteArticles(string id)
        {
            return _articlesLogic.Delete(id);
        }

        protected override void Dispose(bool disposing)
        {
            _articlesLogic.Dispose();
            base.Dispose(disposing);
        }
    }
}