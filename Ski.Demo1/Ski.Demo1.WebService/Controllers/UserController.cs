using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Ski.Demo1.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ski.Demo1.WebService.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("admin/testerror")]
        [HttpGet]
        public string TestErrorHandler()
        {
            throw new ArgumentException();
        }

        [Route("admin/signin")]
        [HttpPost]
        public UserResponse AdminLogin(UserRequest request)
        {
            var result = new UserResponse();
            if (request.username == "skiec@skinsurance.com.tw" && request.password == "03458403")
            {
                JwtSecurityToken token = SetJwtToken();

                var userId = token.Claims.First(c => c.Type == ClaimTypes.Name).Value;
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

                result = new UserResponse()
                {
                    success = true,
                    message = "登入成功",
                    uid = userId,
                    token = encodedJwt,
                    expired = token.Claims.ToArray()[1].Value,//todo 待優化應要用name取值
                };
            }
            else
            {
                result = new UserResponse()
                {
                    success = false,
                    message = "登入失敗"
                };
            }

            return result;
        }

        [Route("user/signin")]
        [HttpPost]
        public UserResponse GuestLogin(UserRequest request)
        {
            var result = new UserResponse();
            if (request.username == "guest" && request.password == "guest")
            {
                JwtSecurityToken token = SetJwtToken();

                var userId = token.Claims.First(c => c.Type == ClaimTypes.Name).Value;
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

                result = new UserResponse()
                {
                    success = true,
                    message = "登入成功",
                    uid = userId,
                    token = encodedJwt,
                    expired = token.Claims.ToArray()[1].Value,//todo 待優化應要用name取值
                };
            }
            else
            {
                result = new UserResponse()
                {
                    success = false,
                    message = "登入失敗"
                };
            }

            return result;
        }

        [Route("logout")]
        [Authorize]
        [HttpPost]
        public UserResponse Logout()
        {
            var expired = DateTime.Now.AddMonths(-24);
            var token = new JwtSecurityToken(
                claims: new[]
                {
                    new Claim(ClaimTypes.Name, "delete")
                },
                expires: expired
            );

            var userId = token.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserResponse()
            {
                success = true,
                message = "已登出",
                uid = userId,
                token = encodedJwt,
                expired = token.Claims.ToArray()[1].Value,//todo 待優化應要用name取值
            };
        }

        [Route("api/user/check")]
        [Authorize]
        [HttpPost]
        public UserResponse Check()
        {
            return new UserResponse()
            {
                success = true,
                message = "登入成功"
            };
        }

        [Route("api/hung/afterlogin")]
        [Authorize]
        [HttpPost]
        public string AfterLogin()
        {
            var authStr = HttpContext.Request.Headers["Authorization"]
            .ToString().Replace("Bearer ", "");
            var token = new JwtSecurityTokenHandler().ReadJwtToken(authStr);
            var userId = token.Claims.First(c => c.Type == ClaimTypes.Name).Value;

            return $"After Login OK, your userId={ userId } , token={ token }";
        }

        private JwtSecurityToken SetJwtToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expired = DateTime.Now.AddMinutes(60);
            var token = new JwtSecurityToken(
                claims: new[]
                {
                    new Claim(ClaimTypes.Name, Guid.NewGuid().ToString("N"))
                },
                expires: expired,
                signingCredentials: creds
            );
            return token;
        }
    }
}