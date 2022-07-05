using Ski.Base.Util.Services;
using Ski.Member.Domain.Enums;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Ski.Member.Business.Logics
{
    public class CommonLogic
    {
        /// <summary>
        /// 管理人信箱
        /// WebConfig先暫時不加入
        /// </summary>
        public List<string> AdminEmail { get; } = new List<string>() { "roywang@skinsurance.com.tw" };

        /// <summary>
        /// 可存取偏量
        /// </summary>
        public string IV { get; } = "03458403";

        /// <summary>
        /// 可存取金鑰
        /// </summary>
        public string Key { get; } = "03458403";

        /// <summary>
        /// 轉字串工具
        /// </summary>
        /// <param name="val">傳入物件</param>
        /// <returns></returns>
        public string CStr(object val)
        {
            if (val == null)
                return "";
            else
                return (string)val;
        }

        /// <summary>
        /// 轉數字工具
        /// </summary>
        /// <param name="val">傳入物件</param>
        /// <returns></returns>
        public int CInt(object val)
        {
            if (val == null)
                return 0;
            else
                return Convert.ToInt16(val);
        }

        /// <summary>
        /// 英文A-Z
        /// </summary>
        public string CharEng = "ABCDEFGHJKLMNPQRSTUVXYWZIO";

        /// <summary>
        /// 數字0~9
        /// </summary>
        public string CharNumber = "0123456789";

        /// <summary>
        /// 身分證字號第一碼轉數字
        /// </summary>
        /// <param name="cityCode"></param>
        /// <returns></returns>
        public int IDCityCodeToInt(char cityCode)
        {
            switch (cityCode)
            {
                case 'A':
                    return 10;
                case 'B':
                    return 11;
                case 'C':
                    return 12;
                case 'D':
                    return 13;
                case 'E':
                    return 14;
                case 'F':
                    return 15;
                case 'G':
                    return 16;
                case 'H':
                    return 17;
                case 'I':
                    return 34;
                case 'J':
                    return 18;
                case 'K':
                    return 19;
                case 'L':
                    return 20;
                case 'M':
                    return 21;
                case 'N':
                    return 22;
                case 'O':
                    return 35;
                case 'P':
                    return 23;
                case 'Q':
                    return 24;
                case 'R':
                    return 25;
                case 'S':
                    return 26;
                case 'T':
                    return 27;
                case 'U':
                    return 28;
                case 'V':
                    return 29;
                case 'W':
                    return 32;
                case 'X':
                    return 30;
                case 'Y':
                    return 31;
                case 'Z':
                    return 33;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// 資料隱碼工具
        /// </summary>
        /// <param name="soureceStr">來源字串</param>
        /// <param name="maskLength">隱碼字元數(Email的話給0即可)</param>
        /// <param name="maskStart">第幾碼開始隱碼</param>
        /// <param name="isEmail">是否用Email方式隱碼</param>
        /// <returns></returns>
        public string MaskInfo(string soureceStr, int maskLength, int maskStart, bool isEmail = false)
        {
            var maskStr = string.Empty;

            //Email隱碼 x******@xxxx.com
            if (isEmail)
            {
                var mailStr = soureceStr.Split('@');

                for (var i = 0; i < mailStr[0].Length - maskStart; i++)
                {
                    maskStr += "*";
                }

                mailStr[0] = mailStr[0].Substring(0, maskStart);
                return string.Join(maskStr + "@", mailStr);
            }
            else
            {
                for (var i = 0; i < maskLength; i++)
                {
                    maskStr += "*";
                }

                var retainStr = new List<string>() {
                    soureceStr.Substring(0, maskStart),
                    soureceStr.Substring(maskStart + maskLength, soureceStr.Length - (maskStart + maskLength))
                };
                return string.Join(maskStr, retainStr);
            }
        }

        #region DESEncrypt - DES字串加密
        /// <summary>
        /// DES字串加密
        /// </summary>
        /// <param name="sourceString">待加密的字符串</param>
        /// <returns>加密后的BASE64的字串</returns>
        public string DESEncrypt(string sourceString)
        {
            byte[] btKey = Encoding.Default.GetBytes(Key);
            byte[] btIV = Encoding.Default.GetBytes(IV);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            try
            {
                byte[] inData = Encoding.Unicode.GetBytes(sourceString);
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write);
                try
                {
                    try
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }
                    finally
                    {
                        cs.Close();
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
                catch
                {
                    return string.Empty;
                }
            }
            finally
            {
                ms.Close();
            }
        }
        #endregion

        #region DESDecrypt - DES字串解密
        /// <summary>
        /// DES字串解密
        /// </summary>
        /// <param name="encryptedString">待解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public string DESDecrypt(string encryptedString)
        {
            byte[] btKey = Encoding.Default.GetBytes(Key);
            byte[] btIV = Encoding.Default.GetBytes(IV);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            try
            {
                byte[] inData = Convert.FromBase64String(encryptedString);
                try
                {
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write);
                    try
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }
                    finally
                    {
                        cs.Close();
                    }
                    return Encoding.Unicode.GetString(ms.ToArray());
                }
                catch
                {
                    return string.Empty;
                }
            }
            finally
            {
                ms.Close();
            }
        }
        #endregion

        /// <summary>
        /// 檢查身分證字號是否正確
        /// </summary>
        /// <param name="identify">身分證字號</param>
        /// <returns></returns>
        public string CheckIDNumber(string identify)
        {
            var res = "身份證字號錯誤";
            if (identify.Length == 10)
            {
                identify = identify.ToUpper();
                if (identify[0] >= 0x41 && identify[0] <= 0x5A)
                {
                    var a = new[] { 10, 11, 12, 13, 14, 15, 16, 17, 34, 18, 19, 20, 21, 22, 35, 23, 24, 25, 26, 27, 28, 29, 32, 30, 31, 33 };
                    var b = new int[11];
                    b[1] = a[identify[0] - 65] % 10;
                    var c = b[0] = a[identify[0] - 65] / 10;
                    for (var i = 1; i <= 9; i++)
                    {
                        b[i + 1] = identify[i] - 48;
                        c += b[i] * (10 - i);
                    }
                    if ((c % 10 + b[10]) % 10 == 0)
                    {
                        res = "";
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// 驗證居留證號碼是否正確
        /// </summary>
        /// <param name="idNumber">居留證號碼</param>
        /// <returns></returns>
        public string CheckResidentPermit(string idNumber)
        {
            if (idNumber.Length < 10 ||
               CharEng.IndexOf(idNumber[0]) < 0 ||
               CharEng.IndexOf(idNumber[1]) < 0)
            {
                return "居留證號錯誤";
            }
            foreach (var c in idNumber.Substring(2))
            {
                if (CharNumber.IndexOf(c) < 0)
                {
                    return "居留證號錯誤";
                }
            }

            var verityNum = 0;
            var verityStr = "";
            verityStr += Convert.ToString(CharEng.IndexOf(idNumber[0]) + 10);
            verityStr += Convert.ToString((CharEng.IndexOf(idNumber[1]) + 10) % 10);
            verityStr += idNumber.Substring(2);

            verityNum += CInt(verityStr[0].ToString());
            verityNum += CInt(verityStr[1].ToString()) * 9;
            verityNum += CInt(verityStr[2].ToString()) * 8;
            verityNum += CInt(verityStr[3].ToString()) * 7;
            verityNum += CInt(verityStr[4].ToString()) * 6;
            verityNum += CInt(verityStr[5].ToString()) * 5;
            verityNum += CInt(verityStr[6].ToString()) * 4;
            verityNum += CInt(verityStr[7].ToString()) * 3;
            verityNum += CInt(verityStr[8].ToString()) * 2;
            verityNum += CInt(verityStr[9].ToString()) * 1;
            verityNum += CInt(verityStr[10].ToString());

            if (verityNum % 10 == 0)
            {
                return "";
            }

            return "居留證號錯誤";
        }

        /// <summary>
        /// 驗證字串規則
        /// </summary>
        /// <returns></returns>
        public bool VerityStringRule(string password, PasswordRule verityRule)
        {
            var result = false;
            switch (verityRule)
            {
                case PasswordRule.AlphabetAndNumeric:
                    if (!string.IsNullOrWhiteSpace(password) && !Regex.IsMatch(password, "[^0-9a-zA-Z]"))
                        result = true;

                    break;
                case PasswordRule.ContainAlphabet:
                    if (!string.IsNullOrWhiteSpace(password) && Regex.IsMatch(password, "[a-zA-Z]"))
                        result = true;

                    break;
                case PasswordRule.ContainNumeric:
                    if (!string.IsNullOrWhiteSpace(password) && Regex.IsMatch(password, "[0-9]"))
                        result = true;

                    break;
                case PasswordRule.AlphabetUpperCase:
                    if (!string.IsNullOrWhiteSpace(password) && Regex.IsMatch(password, "[A-Z]"))
                        result = true;

                    break;
                case PasswordRule.AlphabetLowerCase:
                    if (!string.IsNullOrWhiteSpace(password) && Regex.IsMatch(password, "[a-z]"))
                        result = true;

                    break;
            }
            return result;
        }

        /// <summary>
        /// 驗證身分證字號
        /// </summary>
        /// <param name="idNumber">證件號碼</param>
        /// <param name="cardsType">證件類型</param>
        /// <returns></returns>
        public string VerityIDNumberRule(string idNumber, CardsType cardsType = 0)
        {
            switch (cardsType)
            {

                case CardsType.IdentityCard:
                    return CheckIDNumber(idNumber);

                case CardsType.ResidentPermit:
                    return CheckResidentPermit(idNumber);

                case CardsType.PassPort:
                    if (idNumber.Length < 20)
                    {
                        return "";
                    }
                    else
                    {
                        return "護照號碼有誤";
                    }
                default:
                    if (idNumber.Length >= 10)
                    {
                        switch (idNumber.Substring(1, 1))
                        {
                            case "1":
                            case "2":
                                return CheckIDNumber(idNumber);
                            case "A":
                            case "B":
                            case "C":
                            case "D":
                                return CheckResidentPermit(idNumber);
                            default:
                                return "身分證號碼或居留證號碼有誤，第2碼，應為(1.2或A.C.B.D)";
                        }
                    }
                    return "身分證號碼或居留證號碼長度有誤";
            }
        }

        #region 取得UserIP位址
        /// <summary>
        /// 取得UserIP位址
        /// </summary>
        /// <returns></returns>
        public string getClientIP()
        {
            //TODO getClientIP()
            //if (string.IsNullOrEmpty(commonHelper.CStr(HttpContext.Current.Request.ServerVariables["HTTP_VIA"])))
            //    return commonHelper.CStr(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]).Trim();
            //else if (!string.IsNullOrEmpty(commonHelper.CStr(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"])))
            //    return commonHelper.CStr(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]).Trim();
            //else
            //    return commonHelper.CStr(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]).Trim();
            return "127.0.0.1";
        }
        #endregion

        #region Base64StringProcess - Base64字串加密解密
        /// <summary>
        /// Base64字串加密解密
        /// </summary>
        /// <param name="process"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Base64StringProcess(Base64Encrypt process, string aStr)
        {
            try
            {
                //Base64字串加解密工具
                //var base64Oobj = new BASE64Encrypt.SKIEncrypt.DES();
                //switch (process)
                //{
                //    case Base64Encrypt.Decode:
                //        return base64Oobj.Decrypt(aStr);
                //    case Base64Encrypt.Encode:
                //        return base64Oobj.Encrypt(aStr);
                //    default:
                //        return string.Empty;
                //}
                return string.Empty;
            }
            catch (Exception ex)
            {
                //紀錄Log
                //SaveLog("BASE64Encrypt", "N", $"MemberOtpApiHelper/Base64StringProcess", "::1", $"字串{aStr}{(process == Base64Encrypt.Encode ? "加密" : "解密")}失敗，錯誤訊息: <br<br>{ex.Message}");
                return string.Empty;
            }
        }
        #endregion

        #region SaveLog - 紀錄Log (DB:qryLog)
        /// <summary>
        /// 紀錄Log
        /// </summary>
        /// <param name="uid">會員帳號</param>
        /// <param name="type">登入狀態 Y:成功/N:程式錯誤/D:訊息提示/R:Request參數紀錄</param>
        /// <param name="message">紀錄訊息</param>
        public async Task SaveLogAsync(string uid, string type, string message, string ip, string info = "")
        {
            try
            {
                var connStr = ConnectionString.SkiMemberDb;
                string sql;
                sql = " insert into qryLog (";
                sql += " uid, ";
                sql += " utype, ";
                sql += " qrySql, ";
                sql += " qryinfo, ";
                sql += " sqryip ";
                sql += ")";
                sql += " values( ";
                sql += " @uid, ";
                sql += " @utype, ";
                sql += " @qrySql, ";
                sql += " @qryinfo, ";
                sql += " @sqryip ";
                sql += ")";
                //var sqlParam = new List<SqlParameter>();
                //sqlParam.Add(new SqlParameter("@uid", uid));
                //sqlParam.Add(new SqlParameter("@utype", type));
                //sqlParam.Add(new SqlParameter("@qrySql", message));
                //sqlParam.Add(new SqlParameter("@qryinfo", info));
                //sqlParam.Add(new SqlParameter("@sqryip", ip));

                //commonHelper.DapperSQLExecute(connStr, sqlStr, sqlParam);

                var args = new List<object>()
                    {
                        "uid", uid,
                        "utype", type,
                        "qrySql", message,
                        "qryinfo", info,
                        "sqryip", ip
                    };
                await _Db.ExecSqlAsync(sql, args);


                //不是寄信錯誤的Log 寄信給AdminEmail
                if (uid != "SendSmsAndEmail - Email" || uid != "SendMail")
                {
                    //type = "N"(例外狀況)
                    if (type == "N")
                    {
                        // TODO SendMail
                        //SendMail(commonHelper.AdminEmail, "MemberOTPApi - Exception", $"{message} <br><br>{info}");
                    }
                    //type = "L"(呼叫api參數Log)
                    //else if (type == "L")
                    //{
                    //    SendMail(commonHelper.AdminEmail, "MemberOTPApi - Call", $"{message} <br><br>{info}");
                    //}
                }
            }
            catch (Exception ex)
            {
                string exceptionStr = ex.Message;
            }
        }
        #endregion
    }
}
