using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using Ski.Member.Domain.Entities.MemberModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Ski.Member.Data;
using Ski.Member.Business.MemberLogics;

namespace Ski.Member.WebService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        public MemberController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<MemberModel>> GetMembers()
        {
            return await _memberRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberModel>> GetMember(int id)
        {
            var member = await _memberRepository.GetAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        [HttpPost]
        public async Task<object> Create([BindRequired] MemberCreateRequest row)
        {
            return await new MemberLogic().CreateAsync(row);
        }

        [HttpPost]
        public async Task<object> Update([BindRequired] MemberOTPRequestModel row)
        {
            //TODO
            return Task.CompletedTask;
        }

        [HttpPost]
        public async Task<object> Login([BindRequired] MemberOTPRequestModel row)
        {
            //TODO
            return Task.CompletedTask;
        }

        [HttpPost]
        public async Task<object> Logout([BindRequired] MemberOTPRequestModel row)
        {
            //TODO
            return Task.CompletedTask;
        }

        [HttpPost]
        public async Task<object> Otp([BindRequired] MemberOTPRequestModel row)
        {
            return await new MemberLogic().OtpAsync(row);
        }

        [HttpPost]
        public async Task<object> OtpRe([BindRequired] MemberOTPRequestModel row)
        {
            //TODO
            return Task.CompletedTask;
        }

        [HttpPost]
        public async Task<object> OTPCheck([BindRequired] MemberOTPRequestModel row)
        {
            //TODO
            return Task.CompletedTask;
        }
    }
}
