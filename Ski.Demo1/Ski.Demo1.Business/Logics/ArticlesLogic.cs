using Ski.Base.Util.Services;
using Ski.Demo1.Domain;

namespace Ski.Demo1.Business
{
    public class ArticlesLogic : IArticlesLogic
    {
        private readonly IUnitOfWorks _unitOfWork;

        public ArticlesLogic(IUnitOfWorks unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public EditResponse Create(ArticlesRequest request)
        {
            var req = request.data;
            var ArticlesEntity = new Articles
            {
                id = Guid.NewGuid().ToString("N"),
                title = req.title,
                description = req.description,
                tag = req.tag,
                image = req.image,
                author = req.author,
                content = req.content,
                isPublic = req.isPublic ? 1 : 0
            };
            if (req.tags != null && req.tags.Count > 0)
            {
                List<Tags> tags = new List<Tags>();
                foreach (string i in req.tags)
                {
                    tags.Add(
                        new Tags()
                        {
                            Name = i,
                            ArticlesId = ArticlesEntity.id
                        });
                }
                ArticlesEntity.tags = tags;
            }

            _unitOfWork.ArticlesRepository.Create(ArticlesEntity);

            var key = _Redis.RedisTypeEstr.BaoList + "ArticlesAll";
            _Redis.DeleteKeyAsync(key);

            var result = new EditResponse()
            {
                success = true,
                message = "已建立文章",
            };

            return result;
        }

        public EditResponse Update(ArticlesRequest request, string id)
        {
            var req = request.data;
            var ArticlesEntity = _unitOfWork.ArticlesRepository.Get(filter: item => item.id == id, includeProperties: Articles => Articles.tags).FirstOrDefault();

            ArticlesEntity.id = req.id;
            ArticlesEntity.title = req.title;
            ArticlesEntity.description = req.description;
            ArticlesEntity.content = req.content;
            ArticlesEntity.author = req.author;
            ArticlesEntity.author = req.author;
            ArticlesEntity.content = req.content;
            ArticlesEntity.isPublic = req.isPublic?1:0;


            // Mark an existing table as Deleted
            ArticlesEntity.tags = null;

            List<Tags> tags = new List<Tags>();
            foreach (string i in req.tags)
            {
                if (i != null)
                {
                    tags.Add(
                        new Tags()
                        {
                            Name = i,
                            ArticlesId = ArticlesEntity.id
                        });
                }
            }
            ArticlesEntity.tags = tags;

            _unitOfWork.ArticlesRepository.Update(ArticlesEntity);

            var key = _Redis.RedisTypeEstr.BaoList + "ArticlesAll";
            _Redis.DeleteKeyAsync(key);

            var result = new EditResponse();
            result.success = true;
            result.message = "更新成功";

            return result;
        }

        public EditResponse Delete(string id)
        {
            var ArticlesEntity = _unitOfWork.ArticlesRepository.Get(item => item.id == id).FirstOrDefault();
            _unitOfWork.ArticlesRepository.Delete(ArticlesEntity);

            var result = new EditResponse();
            if (ArticlesEntity == null)
            {
                result.success = false;
                result.message = "查無此文章";
            }
            else
            {
                var key = _Redis.RedisTypeEstr.BaoList + "ArticlesAll";
                _Redis.DeleteKeyAsync(key);

                result.success = true;
                result.message = "刪除成功";
            }
            return result;
        }

        public ArticlesResponse Get(string id)
        {
            var ArticlesEntity = _unitOfWork.ArticlesRepository.Get(filter: item => item.id == id, includeProperties: Articles => Articles.tags).FirstOrDefault();

            var result = new ArticlesResponse();
            if (ArticlesEntity == null)
            {
                result.success = false;
                result.articles = null;
            }
            else
            {
                result.success = true;
                result.articles = Mapper(ArticlesEntity);
            }

            return result;
        }

        public ArticlesListResponse GetList(int page)
        {
            var ArticlesList = _unitOfWork.ArticlesRepository.Get(includeProperties: Articles => Articles.tags).ToList();

            var result = new ArticlesListResponse();
            result.success = true;
            result.articles = ArticlesList.Select(x => Mapper(x)).ToList();

            var count = ArticlesList.Count;
            var pagination = new Pagination()
            {
                total_pages = (count / 10) <= 0 ? 1 : (count / 10),
                current_page = page,
                has_pre = (page == 1) ? false : true,
                has_next = (page == count) ? false : true,
                category = ""
            };
            result.pagination = pagination;
            result.messages = new List<string> { };

            return result;
        }

        public void Dispose()
        {
            _unitOfWork.SaveChanges();//Saves all unsaved result before disposing
            _unitOfWork.Dispose();
        }

        public static ArticlesDTO Mapper(Articles x)
        {
            List<string> tags = new List<string>();
            if (x.tags != null)
            {
                foreach (Tags i in x.tags) { tags.Add(i.Name); }
            }

            var result = new ArticlesDTO()
            {
                id = x.id,
                title = x.title,
                description = x.description,
                tags = tags,
                image = x.image,
                author = x.author,
                content = x.content,
                isPublic = x.isPublic==0?false:true
            };

            return result;
        }
    }
}