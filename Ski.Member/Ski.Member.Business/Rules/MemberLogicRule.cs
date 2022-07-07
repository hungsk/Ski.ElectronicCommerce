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
        //public List<RuleResultTree> CreateInspect(MemberDTO req, Logics.CommonLogic commonHelper)
        //{
        //    dynamic datas = new ExpandoObject();
        //    datas.Uid = req.Uid;
        //    datas.Password = req.Password;
        //    datas.Name = req.Name;
        //    datas.Birthday = req.Birthday;
        //    datas.Mobile = req.Mobile;
        //    datas.PostalCode = req.PostalCode;
        //    datas.Address = req.Address;
        //    datas.Email = req.Email;
        //    var inputs = new dynamic[]
        //    {
        //        datas
        //    };

        //    var resultList = Run("Member.json", "MemberCreate", inputs , commonHelper);

        //    return resultList;
        //}

        public List<RuleResultTree> CreateInspect(MemberDTO req)
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
            var result = re.ExecuteAllRulesAsync("inputWorkflow", new RuleParameter("datas", datas), new RuleParameter("utils", utils)).Result;

            return result;
        }

        private RulesEngine.RulesEngine GetRulesEngine(string filename)
        {
            var data = GetFileContent(filename);

            //var injectWorkflow = new Workflow
            //{
            //    WorkflowName = "inputWorkflowReference",
            //    WorkflowsToInject = new List<string> { "inputWorkflow" }
            //};

            //var injectWorkflowStr = JsonConvert.SerializeObject(injectWorkflow);
            return new RulesEngine.RulesEngine(new string[] { data });
        }

        private string GetFileContent(string filename)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory() as string, "Workflows", filename);
            return File.ReadAllText(filePath);
        }





        //public static List<RuleResultTree> Run(string fileName, string workflowName, dynamic[] inputs, Logics.CommonLogic commonHelper)
        //{

        //    var files = Directory.GetFiles(Directory.GetCurrentDirectory(), fileName, SearchOption.AllDirectories);
        //    if (files == null || files.Length == 0)
        //        throw new Exception("Rules not found.");

        //    var fileData = File.ReadAllText(files[0]);
        //    var workflow = JsonConvert.DeserializeObject<List<Workflow>>(fileData);

        //    var bre = new RulesEngine.RulesEngine(workflow.ToArray(), null);

        //    //List<RuleResultTree> resultList = bre.ExecuteAllRulesAsync(workflowName, new RuleParameter("inputs", inputs), new RuleParameter("commonHelper", commonHelper)).Result;
        //    List<RuleResultTree> resultList = bre.ExecuteAllRulesAsync(workflowName, inputs, new RuleParameter("commonHelper", commonHelper)).Result;
        //    //List<RuleResultTree> resultList = bre.ExecuteAllRulesAsync(workflowName, inputs).Result;

        //    return resultList;
        //}


    }
}
