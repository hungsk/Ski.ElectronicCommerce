using Ski.Demo1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ski.Demo1.Business
{
    public class PayLogic : IPayLogic
    {
        private IUnitOfWorks _unitOfWork;
        public PayLogic(IUnitOfWorks unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public object Payment(string orderNum, string username)
        {
            
            return $"訂單編號[{orderNum}]繳費成功";
        }

        public void Dispose()
        {
            _unitOfWork.SaveChanges();//Saves all unsaved result before disposing
            _unitOfWork.Dispose();
        }
    }
}
