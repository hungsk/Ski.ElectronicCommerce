using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RulesEngine.Models;
using Ski.Member.Business.Rules;
using Ski.Member.Data;
using Ski.Member.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ski.Member.Business.Rules.Tests
{
    [TestClass()]
    public class MemberLogicRuleTests
    {
        ShinkongDbContext dbContext;
        ShinkongDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ShinkongDbContext>()
                .UseInMemoryDatabase(databaseName: "Shinkong")
                .Options;
            return new ShinkongDbContext(options);
        }

        [TestInitialize]
        public void Init()
        {
            dbContext = GetDbContext();

            string[] ids = { "test1", "test2", "test3", "test4", "test5" };
            foreach (string id in ids)
            {
                var datas = new MemberModel()
                {
                    me_id = id,
                    me_pw = "1234567890",
                    me_name = "test",
                    me_birth = DateTime.Now,
                    me_mobile = "1234567890",
                    me_add1_post = "104",
                    me_add1 = "台北市中山區",
                    me_email = "test@gmail.com"
                };
                dbContext.Member.Add(datas);
            }

            dbContext.SaveChanges();
        }
        [TestMethod]
        public void CreateDbContext_New_ReturnsNotNull()
        {
            var rec = dbContext.Member.FirstOrDefault();
            Assert.IsNotNull(rec);
        }

        [TestMethod()]
        [DataRow("member.json")]
        public void CreateInspect_New_ReturnsNotNull(string ruleFileName)
        {
            var re = GetRulesEngine(ruleFileName);
            Assert.IsNotNull(re);
        }

        [TestMethod()]
        [DataRow("member.json", "InputWorkflow")]
        public void CreateInspect_InputWithVariable_ReturnsTrue(string ruleFileName, string workflowName)
        {
            // arrange
            var re = GetRulesEngine(ruleFileName);
            var utils = new Logics.Utils();
            var logic = new MemberLogics.MemberLogic(new UnitOfWork(GetDbContext()));
            dynamic datas = InputWorkflow();

            // act
            var result = re.ExecuteAllRulesAsync(workflowName, new RuleParameter("datas", datas), new RuleParameter("logic", logic), new RuleParameter("utils", utils)).Result;

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Where(i => i.IsSuccess == false).Count());
        }

        [TestMethod()]
        [DataRow("member.json", "InputWorkflow", "CheckIsExists")]
        [DataRow("member.json", "InputWorkflow", "CheckUid")]
        [DataRow("member.json", "InputWorkflow", "CheckMobile")]
        [DataRow("member.json", "InputWorkflow", "RequireUid")]
        [DataRow("member.json", "InputWorkflow", "RequireName")]
        [DataRow("member.json", "InputWorkflow", "RequireBirthday")]
        [DataRow("member.json", "InputWorkflow", "RequireMobile")]
        [DataRow("member.json", "InputWorkflow", "RequirePostalCode")]
        [DataRow("member.json", "InputWorkflow", "RequireAddress")]
        [DataRow("member.json", "InputWorkflow", "RequireEmail")]
        public void CreateInspect_InputWithRuleName_ReturnsFalse(string ruleFileName, string workflowName, string ruleName)
        {
            // arrange
            var re = GetRulesEngine(ruleFileName);
            var utils = new Logics.Utils();
            var logic = new MemberLogics.MemberLogic(new UnitOfWork(GetDbContext()));
            dynamic datas;
            switch (ruleName)
            {
                case "CheckIsExists":
                    datas = CheckIsExists();
                    break;

                case "CheckUid":
                    datas = CheckUid();
                    break;

                case "CheckMobile":
                    datas = CheckMobile();
                    break;

                case "RequireUid":
                    datas = RequireUid();
                    break;

                case "RequireName":
                    datas = RequireName();
                    break;

                case "RequireBirthday":
                    datas = RequireBirthday();
                    break;

                case "RequireMobile":
                    datas = RequireMobile();
                    break;

                case "RequirePostalCode":
                    datas = RequirePostalCode();
                    break;

                case "RequireAddress":
                    datas = RequireAddress();
                    break;

                case "RequireEmail":
                    datas = RequireEmail();
                    break;

                default:
                    datas = InputWorkflow();
                    break;
            }

            // act
            var result = re.ExecuteAllRulesAsync(workflowName, new RuleParameter("datas", datas), new RuleParameter("logic", logic), new RuleParameter("utils", utils)).Result;
            var ruleResult = result?.FirstOrDefault(r => string.Equals(r.Rule.RuleName, ruleName, StringComparison.OrdinalIgnoreCase));

            // assert
            Assert.IsFalse(ruleResult.IsSuccess);
        }

        private dynamic InputWorkflow()
        {
            dynamic datas = new ExpandoObject();
            datas.Uid = "F125003281";
            datas.Password = "1234567890";
            datas.Name = "test";
            datas.Birthday = "1980/01/01";
            datas.Mobile = "0999999999";
            datas.PostalCode = "test";
            datas.Address = "test";
            datas.Email = "test";
            return datas;
        }

        private dynamic CheckIsExists()
        {
            dynamic datas = new ExpandoObject();
            datas.Uid = "test1";
            datas.Password = "1234567890";
            datas.Name = "test";
            datas.Birthday = "1980/01/01";
            datas.Mobile = "0999999999";
            datas.PostalCode = "test";
            datas.Address = "test";
            datas.Email = "test";
            return datas;
        }

        private dynamic CheckUid()
        {
            dynamic datas = new ExpandoObject();
            datas.Uid = "test6";
            datas.Password = "1234567890";
            datas.Name = "test";
            datas.Birthday = "1980/01/01";
            datas.Mobile = "0999999999";
            datas.PostalCode = "test";
            datas.Address = "test";
            datas.Email = "test";
            return datas;
        }

        private dynamic CheckMobile()
        {
            dynamic datas = new ExpandoObject();
            datas.Uid = "test6";
            datas.Password = "1234567890";
            datas.Name = "test";
            datas.Birthday = "1980/01/01";
            datas.Mobile = "1234567890";
            datas.PostalCode = "test";
            datas.Address = "test";
            datas.Email = "test";
            return datas;
        }

        private dynamic RequireUid()
        {
            dynamic datas = new ExpandoObject();
            datas.Uid = "";
            datas.Password = "1234567890";
            datas.Name = "test";
            datas.Birthday = "1980/01/01";
            datas.Mobile = "0999999999";
            datas.PostalCode = "test";
            datas.Address = "test";
            datas.Email = "test";
            return datas;
        }

        private dynamic RequireName()
        {
            dynamic datas = new ExpandoObject();
            datas.Uid = "test6";
            datas.Password = "1234567890";
            datas.Name = "";
            datas.Birthday = "1980/01/01";
            datas.Mobile = "0999999999";
            datas.PostalCode = "test";
            datas.Address = "test";
            datas.Email = "test";
            return datas;
        }

        private dynamic RequireBirthday()
        {
            dynamic datas = new ExpandoObject();
            datas.Uid = "test6";
            datas.Password = "1234567890";
            datas.Name = "test";
            datas.Birthday = null;
            datas.Mobile = "0999999999";
            datas.PostalCode = "test";
            datas.Address = "test";
            datas.Email = "test";
            return datas;
        }

        private dynamic RequireMobile()
        {
            dynamic datas = new ExpandoObject();
            datas.Uid = "test6";
            datas.Password = "1234567890";
            datas.Name = "test";
            datas.Birthday = "1980/01/01";
            datas.Mobile = "";
            datas.PostalCode = "test";
            datas.Address = "test";
            datas.Email = "test";
            return datas;
        }

        private dynamic RequirePostalCode()
        {
            dynamic datas = new ExpandoObject();
            datas.Uid = "test6";
            datas.Password = "1234567890";
            datas.Name = "test";
            datas.Birthday = "1980/01/01";
            datas.Mobile = "0999999999";
            datas.PostalCode = "";
            datas.Address = "test";
            datas.Email = "test";
            return datas;
        }

        private dynamic RequireAddress()
        {
            dynamic datas = new ExpandoObject();
            datas.Uid = "test6";
            datas.Password = "1234567890";
            datas.Name = "test";
            datas.Birthday = "1980/01/01";
            datas.Mobile = "0999999999";
            datas.PostalCode = "test";
            datas.Address = "";
            datas.Email = "test";
            return datas;
        }

        private dynamic RequireEmail()
        {
            dynamic datas = new ExpandoObject();
            datas.Uid = "test6";
            datas.Password = "1234567890";
            datas.Name = "test";
            datas.Birthday = "1980/01/01";
            datas.Mobile = "0999999999";
            datas.PostalCode = "test";
            datas.Address = "test";
            datas.Email = "";
            return datas;
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