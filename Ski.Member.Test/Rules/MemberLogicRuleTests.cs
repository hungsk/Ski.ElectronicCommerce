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

        ShinkongDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ShinkongDbContext>()
                .UseInMemoryDatabase(databaseName: "Shinkong")
                .Options;
            return new ShinkongDbContext(options);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var dbContext = GetDbContext();
            var datas = new MemberModel()
            {
                me_id = "test",
                me_pw = "1234567890",
                me_name = "test",
                me_birth = DateTime.Now,
                me_mobile = "1234567890",
                me_add1_post = "test",
                me_add1 = "test",
                me_email = "test"
            };
            dbContext.Member.Add(datas);
            dbContext.SaveChanges();
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
        [DataRow("member.json")]
        public void CreateInspect_InputWithVariableProps_ReturnsResult(string ruleFileName)
        {
            var re = GetRulesEngine(ruleFileName);

            dynamic datas = GetInput1();

            // Run previous rules.
            var utils = new Logics.Utils();

            var dbContext = GetDbContext();
            var unitOfWork = new UnitOfWork(dbContext);
            var logic = new MemberLogics.MemberLogic(unitOfWork);

            var result = re.ExecuteAllRulesAsync("inputWorkflow", new RuleParameter("datas", datas), new RuleParameter("logic", logic), new RuleParameter("utils", utils)).Result;

            //List<RuleResultTree> result1 = re.ExecuteAllRulesAsync("inputWorkflow", input1);
            Assert.IsNotNull(result);
        }

        private dynamic GetInput1()
        {
            var converter = new ExpandoObjectConverter();
            var basicInfo = "{\"Uid\": \"F125003281\",\"Password\": \"abc@xyz.com\",\"Name\": \"good\",\"Birthday\": \"1980/01/01\",\"Mobile\": 3,\"PostalCode\": 10000,\"Address\": 10000,\"Email\": 10000}";
            return JsonConvert.DeserializeObject<ExpandoObject>(basicInfo, converter);
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