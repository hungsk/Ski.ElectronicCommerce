using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ski.Demo1.WebService.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 取得JWTToken
        /// </summary>
        /// <returns></returns>
        protected string GetJwtToken()
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
    }
}
