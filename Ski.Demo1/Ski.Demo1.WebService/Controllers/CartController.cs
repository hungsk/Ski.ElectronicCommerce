using Microsoft.AspNetCore.Mvc;
using Ski.Demo1.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ski.Demo1.WebService.Controllers
{
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartLogic _cartLogic;

        public CartController(ICartLogic cartService)
        {
            _cartLogic = cartService;
        }

        private string GetJwtToken()
        {
            //todo 待優化成USER物件傳遞
            var authStr = HttpContext.Request.Headers["Authorization"]
            .ToString().Replace("Bearer ", "");

            var username = "";
            if (authStr != "" && authStr != "Bearer")
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(authStr);
                username = token.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            }

            return username;
        }

        [Route("api/hung/cart")]
        [HttpPost]
        public CartResponse AddCart(CartRequest request)
        {
            string username = GetJwtToken();
            return _cartLogic.Create(request, username);
        }

        [Route("api/hung/cart")]
        [HttpGet]
        public CartListResponse GetCartList()
        {
            string username = GetJwtToken();
            return _cartLogic.GetList(username);
        }

        [Route("api/hung/cart/{id}")]
        [HttpDelete]
        public EditResponse DeleteCart(string id)
        {
            string username = GetJwtToken();
            return _cartLogic.Delete(id, username);
        }

        [Route("api/hung/carts")]
        [HttpDelete]
        public EditResponse DeleteCart()
        {
            string username = GetJwtToken();
            return _cartLogic.Delete(username);
        }

        [Route("api/hung/cart/{id}")]
        [HttpPut]
        public CartResponse UpdateCart(CartRequest request, string id)
        {
            string username = GetJwtToken();
            return _cartLogic.Update(request, id, username);
        }

        protected override void Dispose(bool disposing)
        {
            _cartLogic.Dispose();
            base.Dispose(disposing);
        }
    }
}