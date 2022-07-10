using Newtonsoft.Json;
using RulesEngine.Models;
using Ski.Base.Util.Services;
using Ski.Member.Business.MemberLogics;
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
        public List<RuleResultTree> CreateInspect(MemberDTO req , MemberLogic logic)
        {
            var re = GetRulesEngine("Member.json");

            dynamic datas = new ExpandoObject();
            datas.Uid = req.Uid;
            datas.Password = req.Password;
            datas.Name = req.Name;
            datas.Birthday = req.Birthday;
            datas.Mobile = req.Mobile;
            datas.PostalCode = req.PostalCode;
            datas.Address = req.Address;
            datas.Email = req.Email;

            var utils = new Logics.Utils();
            var result = re.ExecuteAllRulesAsync("inputWorkflow", new RuleParameter("datas", datas), new RuleParameter("logic", logic), new RuleParameter("utils", utils)).Result;

            return result;
        }

        private RulesEngine.RulesEngine GetRulesEngine(string filename)
        {
            var data = GetFileContent(filename);
            return new RulesEngine.RulesEngine(new string[] { data });
        }

        private string GetFileContent(string filename)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory() as string, "Workflows", filename);
            return File.ReadAllText(filePath);
        }

    }
}
