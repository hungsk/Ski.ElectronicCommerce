using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ski.Demo1.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ski.Demo1.WebService.Controllers
{
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderLogic _orderLogic;

        public OrderController(IOrderLogic orderService)
        {
            _orderLogic = orderService;
        }

        [HttpGet("api/hung/admin/orders")]
        public OrderListResponse GetOrderList(int page)
        {
            return _orderLogic.GetList(page);
        }

        [Route("api/hung/order/{id}")]
        [HttpGet]
        public OrderGetResponse Get(string id)
        {
            return _orderLogic.Get(id);
        }

        [Route("api/hung/order")]
        [HttpPost]
        public OrderResponse AddOrder(OrderRequest request)
        {
            string username = GetJwtToken();
            return _orderLogic.Create(request, username);
        }

        [Route("api/hung/admin/order/{id}")]
        [HttpPut]
        public EditResponse UpdateOrder(OrderUpdateRequest request, string id)
        {
            return _orderLogic.Update(request, id);
        }

        [Route("api/hung/admin/order/{id}")]
        [Authorize]
        [HttpDelete]
        public EditResponse DeleteOrder(string id)
        {
            return _orderLogic.Delete(id);
        }

        private string GetJwtToken()
        {
            //todo 待優化成USER物件傳遞
            var authStr = HttpContext.Request.Headers["Authorization"]
            .ToString().Replace("Bearer ", "");

            var username = "";
            if (authStr != "")
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(authStr);
                username = token.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            }

            return username;
        }

        protected override void Dispose(bool disposing)
        {
            _orderLogic.Dispose();
            base.Dispose(disposing);
        }
    }
}