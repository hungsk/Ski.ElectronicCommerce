using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using Ski.Member.Domain.Entities.MemberModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Ski.Member.Data;
using Ski.Member.Business.MemberLogics;
using Ski.Member.Domain.Entities;
using Ski.Member.Domain.Interfaces;

namespace Ski.Member.WebService.Controllers
{
    [ApiController]
    //[Route("[controller]/[action]")]
    public class MemberController : Controller
    {
        private readonly IMemberLogic _memberLogic;

        public MemberController(IMemberLogic memberService)
        {
            _memberLogic = memberService;
        }

        //private readonly IMemberRepository _memberRepository;
        //public MemberController(IMemberRepository memberRepository)
        //{
        //    _memberRepository = memberRepository;
        //}

        //[HttpGet]
        //public async Task<IEnumerable<MemberModel>> GetMembers()
        //{
        //    return await _memberRepository.GetAllAsync();
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<MemberModel>> GetMember(int id)
        //{
        //    var member = await _memberRepository.GetAsync(id);

        //    if (member == null)
        //    {
        //        return NotFound();
        //    }

        //    return member;
        //}

        //[HttpPost]
        //public async Task<object> Create([BindRequired] MemberCreateRequest row)
        //{
        //    return await new MemberLogic().CreateAsync(row);
        //}

        [Route("api/admin/member")]
        [HttpPost]
        public EditResponse AddCreate(MemberRequest request)
        {
            return _memberLogic.Create(request);
        }

        [Route("api/admin/member/{id}")]
        [HttpPut]
        public EditResponse UpdateMember(MemberRequest request, string id)
        {
            return _memberLogic.Update(request, id);
        }

        [Route("api/admin/member/{id}")]
        [HttpDelete]
        public EditResponse DeleteMember(string id)
        {
            return _memberLogic.Delete(id);
        }

        [Route("api/member/{id}")]
        [HttpGet]
        public MemberResponse GetMember(string id)
        {
            return _memberLogic.Get(id);
        }

        //[HttpPost]
        //public async Task<object> Login([BindRequired] MemberOTPRequestModel row)
        //{
        //    //TODO
        //    return Task.CompletedTask;
        //}

        //[HttpPost]
        //public async Task<object> Logout([BindRequired] MemberOTPRequestModel row)
        //{
        //    //TODO
        //    return Task.CompletedTask;
        //}

        //[HttpPost]
        //public async Task<object> Otp([BindRequired] MemberOTPRequestModel row)
        //{
        //    return await new MemberLogic().OtpAsync(row);
        //}

        //[HttpPost]
        //public async Task<object> OtpRe([BindRequired] MemberOTPRequestModel row)
        //{
        //    //TODO
        //    return Task.CompletedTask;
        //}

        //[HttpPost]
        //public async Task<object> OTPCheck([BindRequired] MemberOTPRequestModel row)
        //{
        //    //TODO
        //    return Task.CompletedTask;
        //}
        protected override void Dispose(bool disposing)
        {
            _memberLogic.Dispose();
            base.Dispose(disposing);
        }
    }
}
