using Microsoft.AspNetCore.Mvc;
using Ski.Demo1.Domain;

namespace Ski.Demo1.WebService.Controllers
{
    [ApiController]
    public class PayController : BaseController
    {
        private readonly IPayLogic _payLogic;

        public PayController(IPayLogic payService)
        {
            _payLogic = payService;
        }

        [Route("api/hung/pay")]
        [HttpGet]
        public object Payment(string orderNum)
        {
            var jwt = GetJwtToken();            
            return _payLogic.Payment(orderNum, jwt);
        }
    }
}
