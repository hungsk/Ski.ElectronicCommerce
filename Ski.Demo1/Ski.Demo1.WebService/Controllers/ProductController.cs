using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ski.Demo1.Domain;

namespace Ski.Demo1.WebService.Controllers
{
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductLogic _productLogic;

        public ProductController(IProductLogic productService)
        {
            _productLogic = productService;
        }

        [Route("api/hung/admin/product")]
        [Authorize]
        [HttpPost]
        public EditResponse AddProduct(ProductRequest request)
        {
            return _productLogic.Create(request);
        }

        [HttpGet("api/hung/products")]
        [HttpGet("api/hung/admin/products")]
        public ProductListResponse GetProductList(int page)
        {
            var productList = _productLogic.GetList(page);
            return productList;

            //return _productRepository.GetList(page);
        }

        [Route("api/hung/products/all")]
        [Route("api/hung/admin/products/all")]
        [HttpGet]
        public ProductAllResponse GetProductAll()
        {
            var productList = _productLogic.GetList();
            return productList;
            //return new ApiReponse<List<ProductDTO>>(productList);

            //return _productRepository.GetList();
        }

        [Route("api/hung/admin/product/{id}")]
        [Authorize]
        [HttpDelete]
        public EditResponse DeleteProduct(string id)
        {
            return _productLogic.Delete(id);
        }

        [Route("api/hung/admin/product/{id}")]
        [Authorize]
        [HttpPut]
        public EditResponse UpdateProduct(ProductRequest request, string id)
        {
            return _productLogic.Update(request, id);
        }

        [Route("api/hung/product/{id}")]
        [HttpGet]
        public ProductResponse GetProduct(string id)
        {
            return _productLogic.Get(id);
        }

        protected override void Dispose(bool disposing)
        {
            _productLogic.Dispose();
            base.Dispose(disposing);
        }
    }
}