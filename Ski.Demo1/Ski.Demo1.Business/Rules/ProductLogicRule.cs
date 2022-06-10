using RulesEngine.Models;
using Ski.Base.Util.Services;
using Ski.Demo1.Domain;
using System.Dynamic;

namespace Ski.Demo1.Business.Rules
{
    public class ProductLogicRule
    {

        public List<RuleResultTree> CreateInspect(ProductDTO req)
        {
            dynamic datas = new ExpandoObject();
            datas.unit = req.unit;
            datas.price = req.price;
            var inputs = new dynamic[]
            {
                datas
            };

            var resultList = _Rule.Run("Demo1.json", "ProductCreate", inputs);
            return resultList;
        }
    }
}