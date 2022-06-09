using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RulesEngine.Extensions;
using RulesEngine.Models;
//using RulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
//using static RulesEngine.Extensions.ListofRuleResultTreeExtension;

namespace Ski.Base.Util.Services
{
    public class _Rule
    {
        public static List<RuleResultTree> Run(string fileName , string workflowName, dynamic[] inputs)
        {

            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), fileName, SearchOption.AllDirectories);
            if (files == null || files.Length == 0)
                throw new Exception("Rules not found.");

            var fileData = File.ReadAllText(files[0]);
            var workflow = JsonConvert.DeserializeObject<List<Workflow>>(fileData);

            var bre = new RulesEngine.RulesEngine(workflow.ToArray(), null);

            List<RuleResultTree> resultList = bre.ExecuteAllRulesAsync(workflowName, inputs).Result;

            return resultList;
        }
    }
}
