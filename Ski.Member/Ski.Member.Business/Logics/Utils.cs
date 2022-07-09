//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Newtonsoft.Json;
//using Ski.Base.Util.Services;
//using Ski.Member.Domain.Entities;
//using Ski.Member.Domain.Interfaces;

//namespace Ski.Member.Business.Logics
//{
//    public class Utils
//    {
//        public bool CheckIDNumber(string id)
//        {
//            var result = false;
//            if (id.Length == 10)
//            {
//                id = id.ToUpper();
//                if (id[0] >= 0x41 && id[0] <= 0x5A)
//                {
//                    var a = new[] { 10, 11, 12, 13, 14, 15, 16, 17, 34, 18, 19, 20, 21, 22, 35, 23, 24, 25, 26, 27, 28, 29, 32, 30, 31, 33 };
//                    var b = new int[11];
//                    b[1] = a[id[0] - 65] % 10;
//                    var c = b[0] = a[id[0] - 65] / 10;
//                    for (var i = 1; i <= 9; i++)
//                    {
//                        b[i + 1] = id[i] - 48;
//                        c += b[i] * (10 - i);
//                    }
//                    if ((c % 10 + b[10]) % 10 == 0)
//                    {
//                        result = true;
//                    }
//                }
//            }
//            return result;
//        }

//        public int mobileCountQry(string mobile)
//        {
//            return 4;
//        }
//    }
//}
