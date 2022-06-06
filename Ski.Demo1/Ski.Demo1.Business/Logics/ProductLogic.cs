using Ski.Base.Util.Services;
using Ski.Demo1.Domain;
using System.Text.Json;

namespace Ski.Demo1.Business
{
    public class ProductLogic : IProductLogic
    {
        private readonly IUnitOfWorks _unitOfWork;

        public ProductLogic(IUnitOfWorks unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public EditResponse Create(ProductRequest request)
        {
            var req = request.data;
            var productEntity = new Product
            {
                id = Guid.NewGuid().ToString("N"),
                title = req.title,
                category = req.category,
                origin_price = req.origin_price,
                price = req.price,
                unit = req.unit,
                imageUrl = req.imageUrl,
                description = req.description,
                content = req.content,
                is_enabled = req.is_enabled
            };
            if (req.imagesUrl != null)
            {
                List<ImagesUrl> imagesUrl = new List<ImagesUrl>();
                foreach (string i in req.imagesUrl)
                {
                    imagesUrl.Add(
                        new ImagesUrl()
                        {
                            url = i,
                            productid = productEntity.id
                        });
                }
                productEntity.imagesUrl = imagesUrl;
            }

            _unitOfWork.ProductRepository.Create(productEntity);

            var key = _Redis.RedisTypeEstr.BaoList + "ProductAll";
            _Redis.DeleteKeyAsync(key);

            var result = new EditResponse()
            {
                success = true,
                message = "已建立產品",
            };

            return result;
        }

        public EditResponse Update(ProductRequest request, string id)
        {
            var req = request.data;
            var productEntity = _unitOfWork.ProductRepository.Get(filter: item => item.id == id, includeProperties: product => product.imagesUrl).FirstOrDefault();

            productEntity.id = req.id;
            productEntity.title = req.title;
            productEntity.category = req.category;
            productEntity.origin_price = req.origin_price;
            productEntity.price = req.price;
            productEntity.unit = req.unit;
            productEntity.imageUrl = req.imageUrl;
            productEntity.description = req.description;
            productEntity.content = req.content;
            productEntity.is_enabled = req.is_enabled;

            // Mark an existing table as Deleted
            productEntity.imagesUrl = null;

            // Insert a new table
            //foreach (string i in p.imagesUrl)
            //{
            //    if (i != null)
            //    {
            //        productEntity.imagesUrl.Add(
            //            new ImagesUrl()
            //            {
            //                url = i,
            //                productid = productEntity.id
            //            }
            //        );
            //    }
            //}

            List<ImagesUrl> imagesUrl = new List<ImagesUrl>();
            foreach (string i in req.imagesUrl)
            {
                if (i != null)
                {
                    imagesUrl.Add(
                        new ImagesUrl()
                        {
                            url = i,
                            productid = productEntity.id
                        });
                }
            }
            productEntity.imagesUrl = imagesUrl;

            _unitOfWork.ProductRepository.Update(productEntity);

            var key = _Redis.RedisTypeEstr.BaoList + "ProductAll";
            _Redis.DeleteKeyAsync(key);

            var result = new EditResponse();
            result.success = true;
            result.message = "更新成功";

            return result;
        }

        public EditResponse Delete(string id)
        {
            var productEntity = _unitOfWork.ProductRepository.Get(item => item.id == id).FirstOrDefault();
            _unitOfWork.ProductRepository.Delete(productEntity);

            var result = new EditResponse();
            if (productEntity == null)
            {
                result.success = false;
                result.message = "查無此商品";
            }
            else
            {
                var key = _Redis.RedisTypeEstr.BaoList + "ProductAll";
                _Redis.DeleteKeyAsync(key);

                result.success = true;
                result.message = "刪除成功";
            }
            return result;
        }

        public ProductResponse Get(string id)
        {
            var productEntity = _unitOfWork.ProductRepository.Get(filter: item => item.id == id, includeProperties: product => product.imagesUrl).FirstOrDefault();

            var result = new ProductResponse();
            if (productEntity == null)
            {
                result.success = false;
                result.product = null;
            }
            else
            {
                result.success = true;
                result.product = Mapper(productEntity);
            }

            return result;
            //return _mapper.Map<EmployeeDTO>(employeeEntity);
        }

        public ProductAllResponse GetList()
        {
            //return result;
            //return _mapper.Map<List<EmployeeDTO>>(productList);

            //1.get redis key: BaoList + query condition
            var key = _Redis.RedisTypeEstr.BaoList + "ProductAll";
            //JsonSerializer.Serialize(productList)

            //2.check redis has data or not
            var value = _Redis.GetStrAsync(key).Result;

            //3.return redis data if existed
            if (value != null)
                return JsonSerializer.Deserialize<ProductAllResponse>(value);

            //4.read db
            var productList = _unitOfWork.ProductRepository.Get(includeProperties: product => product.imagesUrl).ToList();
            var result = new ProductAllResponse();
            result.success = true;
            result.products = productList.Select(x => Mapper(x)).ToList();

            //5.write redis & return data
            _Redis.SetStrAsync(key, JsonSerializer.Serialize(result));
            return result;
        }

        public ProductListResponse GetList(int page)
        {
            var productList = _unitOfWork.ProductRepository.Get(includeProperties: product => product.imagesUrl).ToList();

            var result = new ProductListResponse();
            result.success = true;
            result.products = productList.Select(x => Mapper(x)).ToList();

            var count = productList.Count;
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

        public static ProductDTO Mapper(Product x)
        {
            List<string> imagesUrlArry = new List<string>();
            if (x.imagesUrl != null)
            {
                foreach (ImagesUrl i in x.imagesUrl) { imagesUrlArry.Add(i.url); }
            }

            var result = new ProductDTO()
            {
                id = x.id,
                category = x.category,
                content = x.content,
                description = x.description,
                imageUrl = x.imageUrl,
                imagesUrl = imagesUrlArry,
                is_enabled = x.is_enabled,
                origin_price = x.origin_price,
                price = x.price,
                title = x.title,
                unit = x.unit
            };

            return result;
        }
    }
}