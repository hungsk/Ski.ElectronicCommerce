using RulesEngine.Models;
using Ski.Base.Util.Services;
using Ski.Member.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ski.Member.Business.Rules
{
    public class MemberLogicRule
    {
        public List<RuleResultTree> CreateInspect(MemberDTO req)
        {
            dynamic datas = new ExpandoObject();
            datas.Uid = req.Uid;
            datas.Password = req.Password;
            datas.Name = req.Name;
            datas.Birthday = req.Birthday;
            datas.Mobile = req.Mobile;
            datas.PostalCode = req.PostalCode;
            datas.Address = req.Address;
            datas.Email = req.Email;
            var inputs = new dynamic[]
            {
                datas
            };

            var resultList = _Rule.Run("Member.json", "MemberCreate", inputs);

            return resultList;
        }
    }
}
