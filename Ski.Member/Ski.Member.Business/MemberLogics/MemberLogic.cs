using Ski.Base.Util.Models;
using Ski.Base.Util.Services;
using Newtonsoft.Json;
using Ski.Member.Business.CommonLogics;
using Ski.Member.Domain.Enums;
using Ski.Member.Domain.Entities.MemberModels;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Web;
using System.Dynamic;

namespace Ski.Member.Business.MemberLogics
{
    public class MemberLogic
    {
        #region 通用工具列
        public CommonLogic commonHelper = new CommonLogic();
        private string sql = string.Empty;
        //private List<SqlParameter> sqlParam = new List<SqlParameter>();

        /// <summary>
        /// 忽略5次手機註冊的控管號碼
        /// </summary>
        public List<string> MobileByPass { get; } = new List<string>()
        {
            "0937977710",
            "0918100403",
            "0932328670",
            "0914192979",
            "0932718903",
            "0916267097",
            "0984102103",
            "0938074309",
            "0985531787",
            "0911273683",
            "0918418699"
        };

        #endregion 通用工具列

        #region GetPubEmailContent - 取得信件下方共用資訊

        /// <summary>
        /// 取得信件下方共用資訊
        /// </summary>
        /// <returns></returns>
        public string GetPubEmailContent()
        {
            var pubContent = string.Empty;
            pubContent += "若您任何疑問，可於服務時間內來信或來電洽詢，謝謝您的配合。<br>";
            pubContent += "<font color=red>※本信件為系統直接發送，請勿直接回覆</font><br><br>";
            pubContent += "網路投保諮詢專線：（02）2517-5725<br>";
            pubContent += "網路投保傳真專線：（02）2507-6810<br>";
            pubContent += "服務信箱：skservice@skinsurance.com.tw<br>";
            pubContent += "服務時間：週一至週五AM8:30~12:00，PM1:30~6:00 (國定例假日除外)<br>";
            pubContent += "公司地址：台北市中山區建國北路2段15號8樓<br>";
            pubContent += "　　祝　　順心<br>";
            pubContent += "<b>處處新光  讓愛發光<br>";
            pubContent += "新光產險  敬  上</b>";

            return pubContent;
        }

        #endregion GetPubEmailContent - 取得信件下方共用資訊

        #region GetMemberData - 取得會員資料

        /// <summary>
        /// 取得會員資料
        /// </summary>
        /// <param name="uid">會員帳號</param>
        /// <returns></returns>
        //public  Task<Member> GetMemberData(string uid, Db db)
        //{
        //    var sql = " Select Top 1 * From member Where  me_pid = @me_pid ";
        //    return db.GetJsonAsync(sql, new List<Member>() { "me_pid", uid });

        //    //var connStr = ConnectionString.SkiMemberDb;
        //    //var sqlStr = " Select Top 1 * From member Where  me_pid = @me_pid ";
        //    //var sqlParam = new List<SqlParameter>();
        //    //sqlParam.Add(new SqlParameter("@me_pid", uid.ToUpper().Trim()));

        //    //var memberQry = commonHelper.DapperSQLQuery<Model.MemberApi.DB.Member>(connStr, sqlStr, sqlParam) ?? new List<Model.MemberApi.DB.Member>();

        //    //return memberQry.FirstOrDefault();
        //}
        public async Task<MemberModel> GetMemberDataAsync(string uid)
        {
            var sql = " Select Top 1 * From member Where  me_pid = @me_pid ";

            var jobj = await _Db.GetJsonAsync(sql, new List<object>() { "me_pid", uid });

            //return await _Db.GetJsonAsync(sql, new List<object>() { "me_pid", uid });

            return JsonConvert.DeserializeObject<MemberModel>(jobj.ToString());
        }

        #endregion GetMemberData - 取得會員資料

        #region MemberCreateRequestCheck - 會員建檔參數檢查

        /// <summary>
        /// 會員建檔參數檢查
        /// </summary>
        /// <param name="request">會員建檔參數</param>
        /// <returns></returns>
        public string MemberCreateRequestCheck(MemberCreateRequest request)
        {
            dynamic datas = new ExpandoObject();
            datas.Uid = request.Uid;
            datas.Password = request.Password;
            datas.Name = request.Name;
            datas.Birthday = request.Birthday;
            datas.Mobile = request.Mobile;
            datas.PostalCode = request.PostalCode;
            datas.Address = request.Address;
            datas.Email = request.Email;
            var inputs = new dynamic[]
            {
                datas
            };

            var resultList = _Rule.Run("Member.json", "MemberCreate", inputs);


            var errorMsg = string.Empty;
            if (string.IsNullOrWhiteSpace(request.Uid) ||
                string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.Name) ||
                string.IsNullOrWhiteSpace(request.Birthday) ||
                string.IsNullOrWhiteSpace(request.Mobile) ||
                string.IsNullOrWhiteSpace(request.PostalCode) ||
                string.IsNullOrWhiteSpace(request.Address) ||
                string.IsNullOrWhiteSpace(request.Email))
            {
                errorMsg = "資料提供不完整";
            }
            else if (request.Birthday.Length != 8)
            {
                errorMsg = "生日格式異常";
            }
            else if (request.Uid.ToUpper() == request.Password.ToUpper())
            {
                errorMsg = "密碼不可為註冊帳號";
            }
            else if (request.Password.Length < 8)
            {
                errorMsg = "密碼長度須至少需８碼";
            }
            else if (!commonHelper.VerityStringRule(request.Password, PasswordRule.AlphabetAndNumeric))
            {
                errorMsg = "密碼必須是英數字";
            }
            else if (!commonHelper.VerityStringRule(request.Password, PasswordRule.ContainNumeric) ||
                     !commonHelper.VerityStringRule(request.Password, PasswordRule.AlphabetUpperCase) ||
                     !commonHelper.VerityStringRule(request.Password, PasswordRule.AlphabetLowerCase))
            {
                errorMsg = "密碼內容需含英文字母大寫、小寫與數字";
            }
            return errorMsg;
        }

        #endregion MemberCreateRequestCheck - 會員建檔參數檢查

        #region GetOTP - 取得OTP密碼

        /// <summary>
        /// 取得OTP密碼並寄簡訊及發送Email
        /// </summary>
        /// <param name="uid">會員帳號</param>
        /// <param name="mobile">會員手機</param>
        /// <param name="otpType">OTP種類</param>
        /// <returns></returns>
        public async Task<string> GetOTPAsync(string uid, string mobile, string otpType, string ip)
        {
            var rand = new Random();
            var randnum = "0000000" + Convert.ToString(Convert.ToInt64((9999999 * rand.Next()) + 1));
            var optnum = randnum.Substring(randnum.Length - 7, 7);

            #region OPT密碼存到OTPLog

            var connStr = ConnectionString.SkiMemberDb;
            var sql = string.Empty;
            sql = "insert into OTPLog (";
            sql += " me_id, ";
            sql += " me_mobile, ";
            sql += " BGNDT, ";
            sql += " ENDDT, ";
            sql += " otp, ";
            sql += " sIP, ";
            sql += " Dcreate, ";
            sql += " PEstatus, ";
            sql += " sType, ";
            sql += " ReCount ) ";
            sql += " values(";
            sql += " @me_id, ";
            sql += " @me_mobile, ";
            sql += " DATEADD (minute, -2 , getdate()), ";
            sql += " DATEADD (minute,  5 , getdate()), ";
            sql += " @otp, ";
            sql += " @sIP, ";
            sql += " {fn now()}, ";
            sql += " 'Y', ";
            sql += " @sType, ";
            sql += " '0' )";
            //var sqlParam = new List<SqlParameter>();
            //sqlParam.Add(new SqlParameter("@me_id", uid));
            //sqlParam.Add(new SqlParameter("@me_mobile", mobile));
            //sqlParam.Add(new SqlParameter("@otp", optnum));
            //sqlParam.Add(new SqlParameter("@sIP", ip));
            //sqlParam.Add(new SqlParameter("@sType", otpType));
            //commonHelper.DapperSQLExecute(connStr, sqlStr, sqlParam);

            var args = new List<object>()
                    {
                        "me_id", uid,
                        "me_mobile", mobile,
                        "otp", optnum,
                        "sIP",ip,
                        "sType", otpType
                    };
            await _Db.ExecSqlAsync(sql, args);

            #endregion OPT密碼存到OTPLog

            return optnum;
        }

        #endregion GetOTP - 取得OTP密碼

        #region SendSmsAndEmail - 發送簡訊及Email

        /// <summary>
        /// 發送簡訊及Email
        /// </summary>
        /// <param name="mobile">會員帳號</param>
        /// <param name="mobile">會員手機</param>
        /// <param name="smsMsg">簡訊內容</param>
        /// <param name="email">會員信箱</param>
        /// <param name="emailTitle">信件標題</param>
        /// <param name="emaailContent">信件內容</param>
        /// <param name="otp">OTP密碼</param>
        public async Task<bool> SendSmsAndEmailAsync(string uid, string mobile, string smsMsg, List<string> mailto, string emailTitle, string emailContent, string otp, string ip)
        {
            #region 寄簡訊

            try
            {
                var url = $"http://imsp.emome.net:8008/imsp/sms/servlet/SubmitSM?account=10267&password=10267&to_addr={mobile}&msg_type=0&msg={smsMsg}";
                var response = HTMLGET(url);

                //紀錄簡訊內容Log
                commonHelper.SaveLogAsync("SendSmsAndEmail", "R", response, ip, url);
                if (string.IsNullOrEmpty(response))
                {
                    throw new ArgumentException($"中華電信OTP簡訊服務發生錯誤[url:{url}][Response:{response}]");
                }

                //response格式:"<html>\n<header>\n</header>\n<body>\n09XXXXXXXX|X|XXXXXXXX|Success<br>\n</body>\n</html>\n";
                response = response.Substring(response.IndexOf("<body>") + 7, (response.IndexOf("</body>") - 5) - (response.IndexOf("<body>") + 7));
                if (!response.Split('|').ToList().Any())
                {
                    throw new ArgumentException($"中華電信OTP簡訊服務發生錯誤[url:{url}][Response:{response}]");
                }
                PaserRetSMSAsync(response.Split('|').ToList(), uid, mobile, otp);
            }
            catch (Exception ex)
            {
                commonHelper.SaveLogAsync("SendSmsAndEmail - Sms", "N", $"MemberOtpApiHelper/SendSmsAndEmail", ip, $"寄簡訊失敗，錯誤訊息: <br><br>{ex.Message}");
                return false;
            }

            #endregion 寄簡訊

            #region 發Email

            try
            {
                var email = new EmailDto()
                {
                    Subject = emailTitle,
                    ToUsers = mailto,
                    Body = emailContent
                };
                await _Email.SendByDtoAsync(email);

                //var mailBasicSet = new MailBasicSet()
                //{
                //    Subject = emailTitle,
                //    Body = emailContent,
                //    Recipients = email,
                //    Bbcs = commonHelper.AdminEmail
                //};
                //var mail = new MailAction(mailBasicSet);
                //mail.Send();
            }
            catch (Exception ex)
            {
                commonHelper.SaveLogAsync("SendSmsAndEmail - Email", "N", $"MemberOtpApiHelper/SendSmsAndEmail", ip, $"寄Email失敗，錯誤訊息: <br<br>{ex.Message}");
                return false;
            }

            #endregion 發Email

            return true;
        }

        #endregion SendSmsAndEmail - 發送簡訊及Email

        #region HTMLGET - 取得寄簡訊結果

        /// <summary>
        /// 取得寄簡訊結果
        /// </summary>
        /// <param name="url">寄信網址</param>
        /// <returns></returns>
        public string HTMLGET(string url)
        {
            string strContent = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
            strContent = sr.ReadToEnd();
            sr.Close();
            request = null;
            response = null;
            return strContent;
        }

        #endregion HTMLGET - 取得寄簡訊結果

        #region PaserRetSMS - 紀錄中華電信回傳的簡訊發送結果

        /// <summary>
        /// 紀錄中華電信回傳的簡訊發送結果
        /// </summary>
        /// <param name="response">寄簡訊結果</param>
        /// <param name="uid">會員帳號</param>
        /// <param name="mobile">會員手機</param>
        /// <param name="otp">OTP密碼</param>
        public async Task PaserRetSMSAsync(List<string> response, string uid, string mobile, string otp)
        {
            var connStr = ConnectionString.SkiMemberDb;
            var sql = "Update OTPLog Set ";
            sql += " RetCode = @RetCode, ";
            sql += " RetStatus = @RetStatus ";
            sql += " Where 1=1 ";
            sql += " And me_id = @me_id ";
            sql += " And me_mobile = @me_mobile ";
            sql += " And otp = @otp ";
            sql += " And pkey = (Select Max(pkey) From OTPLog Where  1=1 And  me_id = @me_id And me_mobile = @me_mobile And otp = @otp ) ";

            //var sqlParam = new List<SqlParameter>();
            //sqlParam.Add(new SqlParameter("@RetCode", response[2]));
            //sqlParam.Add(new SqlParameter("@RetStatus", response[3]));
            //sqlParam.Add(new SqlParameter("@me_id", uid));
            //sqlParam.Add(new SqlParameter("@me_mobile", mobile));
            //sqlParam.Add(new SqlParameter("@otp", otp));

            //commonHelper.DapperSQLExecute(connStr, sqlStr, sqlParam);

            var args = new List<object>()
                    {
                        "RetCode", response[2],
                        "RetStatus", response[3],
                        "me_id", uid,
                        "me_mobile", mobile,
                        "otp", otp
                    };
            await _Db.ExecSqlAsync(sql, args);
        }

        #endregion PaserRetSMS - 紀錄中華電信回傳的簡訊發送結果





        /// <summary>
        /// 會員帳號建檔
        /// </summary>
        /// <param name="MemberCreateRequest">MemberCreateRequest</param>
        /// <returns>MemberCreateResponse</returns>
        public async Task<object> CreateAsync(MemberCreateRequest request)
        {
            try
            {
                //記錄傳入參數Log
                //var logUrl = Url.Request.RequestUri.AbsoluteUri;
                //memberOtpApiHelper.SaveLog(request.Uid, "L", logUrl, getClientIP(), $"Request:{JsonConvert.SerializeObject(request)}");

                //會員填寫資料驗證
                var checkResult = MemberCreateRequestCheck(request);
                if (!string.IsNullOrWhiteSpace(checkResult))
                {
                    return new MemberCreateResponse()
                    {
                        Result = false,
                        ResultMsg = checkResult
                    };
                }
                //身分證號驗證
                var verityIDResult = commonHelper.VerityIDNumberRule(request.Uid);
                if (!string.IsNullOrWhiteSpace(verityIDResult))
                {
                    return new MemberCreateResponse()
                    {
                        Result = false,
                        ResultMsg = verityIDResult
                    };
                }

                //查詢會員帳號是否存在 不存在才建檔
                var memberData = await GetMemberDataAsync(request.Uid);
                if (memberData == null)
                {
                    #region 行動電話被註冊總次數
                    sql = "Select Count(*) count From member Where me_mobile = @me_mobile and otpchk = 'Y' ";
                    var mobileCountQry = await _Db.GetJsonAsync(sql, new List<object>() { "me_mobile", request.Mobile });


                    //connStr = ConnectionString.SkiMemberDb;
                    //sqlStr = "Select Count(*) From member Where me_mobile = @me_mobile and otpchk = 'Y' ";
                    //sqlParam.Clear();
                    //sqlParam.Add(new SqlParameter("@me_mobile", request.Mobile));

                    //var mobileCountQry = commonHelper.DapperSQLQuery<string>(connStr, sqlStr, sqlParam) ?? new List<string>();
                    if (commonHelper.CInt(mobileCountQry["count"].Value<string>()) > 4 &&
                        MobileByPass.IndexOf(request.Mobile) == -1)
                    {
                        return new MemberCreateResponse()
                        {
                            Result = false,
                            ResultMsg = "行動電話已被註冊5次"
                        };
                    }
                    #endregion

                    #region 用郵遞區號抓 城市和區域
                    sql = "Select * From ZipCode where rtrim(ZipCode) = @ZipCode";
                    var zipCodeQry = await _Db.GetJsonAsync(sql, new List<object>() { "ZipCode", request.PostalCode });

                    //connStr = ConnectionString.SkiDb;
                    //sqlStr = "Select * From ZipCode where rtrim(ZipCode) = @ZipCode";
                    //sqlParam.Clear();
                    //sqlParam.Add(new SqlParameter("@ZipCode", request.PostalCode));

                    //var zipCodeQry = commonHelper.DapperSQLQuery<zipCode>(connStr, sqlStr, sqlParam) ?? new List<zipCode>();
                    if (zipCodeQry != null)
                    {
                        zipCode zipCodeQryObj = JsonConvert.DeserializeObject<zipCode>(zipCodeQry.ToString());
                        request.City = zipCodeQryObj.City.Trim();
                        request.Area = zipCodeQryObj.Area.Trim();
                    }

                    #endregion

                    #region 會員資料建檔
                    var birthday = DateTime.ParseExact(request.Birthday.Trim(), "yyyyMMdd", CultureInfo.InvariantCulture);
                    var me_nameE = string.Empty;
                    var me_pwE = string.Empty;
                    me_nameE = commonHelper.DESEncrypt(request.Name.Trim());
                    me_pwE = commonHelper.Base64StringProcess(Base64Encrypt.Encode, request.Password.Trim());

                    sql = " Insert Into member (";
                    sql += " me_name ,";
                    sql += " me_id ,";
                    sql += " me_pid ,";
                    sql += " me_pw ,";
                    sql += " me_birth ,";
                    sql += " me_mobile ,";
                    sql += " me_email ,";
                    sql += " me_add1_post ,";
                    sql += " me_add1_city ,";
                    sql += " me_add1_area ,";
                    sql += " me_add1 ,";
                    sql += " me_add2_post ,";
                    sql += " me_add2_city ,";
                    sql += " me_add2_area ,";
                    sql += " me_add2 ,";
                    sql += " adddate ,";
                    sql += " otpchk ,";
                    sql += " m_RegType ,";
                    sql += " bChangePw ,";
                    sql += " iCreate ,";
                    sql += " RememberEmail ,";
                    sql += " iChangeMobile ,";
                    sql += " me_national ,";
                    sql += " me_marital ,";
                    sql += " me_gender ,";
                    sql += " me_tel ,";
                    sql += " LstLoginTime ,";
                    sql += " appLoginTime ,";
                    sql += " me_nameE ,";
                    sql += " me_pwE ,";
                    sql += " sQply) ";
                    sql += " Values(";
                    sql += " @me_name , ";
                    sql += " @me_pid ,";
                    sql += " @me_pid ,";
                    sql += " @sPwd ,";
                    sql += " @me_birth ,";
                    sql += " @me_mobile ,";
                    sql += " @me_email ,";
                    sql += " @me_add_post ,";
                    sql += " @me_add_City ,";
                    sql += " @me_add_area ,";
                    sql += " @me_add ,";
                    sql += " @me_add_post ,";
                    sql += " @me_add_City ,";
                    sql += " @me_add_area ,";
                    sql += " @me_add ,";
                    sql += " {fn now()},";
                    sql += " 'N' ,";
                    sql += " 'W' ,";
                    sql += " 'N' ,";
                    sql += " 'B2C' ,";
                    sql += " @RememberEmail ,";
                    sql += " '0' ,";
                    sql += " ' ' ,";
                    sql += " ' ' ,";
                    sql += " ' ' ,";
                    sql += " ' ' ,";
                    sql += " {fn now()} ,";
                    sql += " {fn now()} ,";
                    sql += " @me_nameE ,";
                    sql += " @me_pwE ,";
                    sql += " 'N' )";

                    var args = new List<object>()
                    {
                        "me_name", "",
                        "me_pid", request.Uid.ToUpper().Trim(),
                        "sPwd", "",
                        "me_birth", birthday.ToString("yyyy/MM/dd"),
                        "me_mobile", request.Mobile.Trim(),
                        "me_email", request.Email.Trim(),
                        "me_add_post", request.PostalCode.Trim(),
                        "me_add_City", request.City.Trim(),
                        "me_add_area", request.Area.Trim(),
                        "me_add", request.Address.Trim(),
                        "RememberEmail", request.RememberEmail.Trim(),
                        "me_nameE", me_nameE,
                        "me_pwE", me_pwE
                    };
                    if (await _Db.ExecSqlAsync(sql, args) == 0)
                    {
                        return new MemberCreateResponse()
                        {
                            Result = false,
                            ResultMsg = "新增資料異常，請稍後再試"
                        };
                    }
                    #endregion

                    #region 新增資料成功發會員通知信
                    var maskUid = commonHelper.MaskInfo(request.Uid.Trim(), 4, 3);
                    var maskMobile = commonHelper.MaskInfo(request.Mobile.Trim(), 4, 2);
                    var maskEmail = commonHelper.MaskInfo(request.Email.Trim(), 0, 1, true);

                    //var email = request.Email.Trim();
                    //var emailTitle = "新光產險網站會員註冊成功通知";
                    //var emailContent = $"親愛的 { request.Name.Trim()} 先生/小姐 您好：<br>感謝您成為新光產險網站會員。請您妥善保存您的帳號資料，會員資料如有異動，請透過 <a href='{ Url.Request.RequestUri.Scheme}://{ Url.Request.RequestUri.Host}/Member/'>會員專區 </a>『會員資料查詢/修改』更新您的資料，確保您資料的正確。<br>帳號：{maskUid.ToUpper()}<br>行動電話：{maskMobile}<br>信箱：{maskEmail}<br><font color=red>※本信件為系統直接發送，請勿直接回覆</font><br><br>{ memberOtpApiHelper.GetPubEmailContent()}";

                    //var sendResult = memberOtpApiHelper.SendMail(new List<string>() { email }, emailTitle, emailContent);

                    var email = new EmailDto()
                    {
                        Subject = "新光產險網站會員註冊成功通知",
                        ToUsers = new() { request.Email.Trim() },
                        Body = $"親愛的 { request.Name.Trim()} 先生/小姐 您好：<br>感謝您成為新光產險網站會員。請您妥善保存您的帳號資料，會員資料如有異動，請透過 <a href='{ "Url.Request.RequestUri.Scheme"}://{ "xxxxxxxxxxxxxxxxxxxxx"}/Member/'>會員專區 </a>『會員資料查詢/修改』更新您的資料，確保您資料的正確。<br>帳號：{maskUid.ToUpper()}<br>行動電話：{maskMobile}<br>信箱：{maskEmail}<br><font color=red>※本信件為系統直接發送，請勿直接回覆</font><br><br>{ GetPubEmailContent()}"
                    };
                    await _Email.SendByDtoAsync(email);

                    #endregion

                    //#region 將帳號密碼清除 只留隱碼過後的
                    //sql = " Update member Set  me_name = '' , me_pw = '' Where  me_pid = @me_pid ";
                    ////var memberUpdate = await db.GetJsonAsync(sql, new List<object>() { "me_pid", request.Uid.ToUpper().Trim() });
                    //if (await _Db.ExecSqlAsync(sql, new List<object>() { "me_pid", request.Uid.ToUpper().Trim() }) == 0)
                    //{
                    //    return new MemberCreateResponseModel()
                    //    {
                    //        Result = false,
                    //        ResultMsg = "新增資料異常，請稍後再試"
                    //    };
                    //}

                    ////connStr = ConnectionString.SkiMemberDb;
                    ////sqlStr = " Update member Set  me_name = '' , me_pw = '' Where  me_pid = @me_pid ";
                    ////sqlParam.Clear();
                    ////sqlParam.Add(new SqlParameter("@me_pid", request.Uid.ToUpper().Trim()));
                    ////var memberUpdate = commonHelper.DapperSQLExecute(connStr, sqlStr, sqlParam);
                    //#endregion
                }
                else
                {
                    return new MemberCreateResponse()
                    {
                        Result = false,
                        ResultMsg = "帳號已註冊"
                    };
                }

            }
            catch (Exception ex)
            {
                //記錄錯誤訊息
                //memberOtpApiHelper.SaveLog("Exception", "N", "/member/api/MemberOTP/MemberCreate", getClientIP(), ex.Message);
                return new MemberCreateResponse()
                {
                    Result = false,
                    ResultMsg = $"發生錯誤，請稍後再試。"
                };
            }
            return new MemberCreateResponse()
            {
                Result = true,
                ResultMsg = "會員帳號註冊成功"
            };
            ;
        }

        /// <summary>
        /// OTP發送簡訊及EMAIL
        /// </summary>
        /// <param name="Uid">會員帳號</param>
        /// <param name="OTPType">OTP類別(0:首次締約作網路投保OTP/1:投保前OTP/2:網路保險服務OTP/3:網路投保+保險服務OTP/4:保險服務申請前OTP/5:會員登入驗證OTP)</param>
        /// <returns></returns>
        public async Task<object> OtpAsync(MemberOTPRequestModel request)
        {
            try
            {
                //記錄傳入參數Log
                //var logUrl = Url.Request.RequestUri.AbsoluteUri;
                //memberOtpApiHelper.SaveLog(request.Uid, "L", logUrl, getClientIP(), $"Request:{JsonConvert.SerializeObject(request)}");

                //查詢帳號是否存在
                var memberData = await GetMemberDataAsync(request.Uid);
                //var memberData = JsonConvert.DeserializeObject<Member>(jobj.ToString());
                if (memberData != null)
                {
                    #region  驗證OTP服務項目是否尚未啟用或已啟用並寄簡訊寄信
                    var otpTypeCheck = true;
                    var errorMsg = string.Empty;
                    var sOtp = string.Empty;
                    var smsMsg = string.Empty;
                    var emailTitle = string.Empty;
                    var emailContent = string.Empty;
                    switch ((OTPType)Convert.ToInt16(request.OTPType))
                    {
                        case OTPType.FirstOTP:
                            if (memberData.otpchk == "Y")
                            {
                                otpTypeCheck = false;
                                errorMsg = "網路投保服務已啟用";
                            }
                            else
                            {
                                sOtp = await GetOTPAsync(memberData.me_pid, memberData.me_mobile, request.OTPType, commonHelper.getClientIP());
                                smsMsg = HttpUtility.UrlEncode($"親愛的客戶您好,請在5分鐘內輸入驗證密碼『{sOtp}』以完成投保身分驗證。新光產險敬上", System.Text.Encoding.GetEncoding(950));
                                emailTitle = "新光產險網路投保服務驗證密碼通知";
                                emailContent += $"請您於確認網頁輸入驗證密碼『<font color=red>{sOtp}</font>』，以開啟網路投保服務並進行身分驗證。提醒您，驗證密碼逾<font color=red>５分鐘將失效</font>。<br>{GetPubEmailContent()}";
                            }
                            break;

                        case OTPType.BeforeInsure:
                            if (memberData.otpchk != "Y")
                            {
                                otpTypeCheck = false;
                                errorMsg = "網路投保服務OTP驗證未完成";
                            }
                            else
                            {
                                sOtp = await GetOTPAsync(memberData.me_pid, memberData.me_mobile, request.OTPType, commonHelper.getClientIP());
                                smsMsg = HttpUtility.UrlEncode($"親愛的客戶您好,請在5分鐘內輸入驗證密碼『{sOtp}』以完成投保身分驗證。新光產險敬上", System.Text.Encoding.GetEncoding(950));
                                emailTitle = "新光產險投保簡訊動態驗證密碼通知";
                                emailContent += $"請您於投保簡訊動態認證網頁輸入驗證密碼『<font color=red>{sOtp}</font>』，以完成投保及身分驗證。提醒您，驗證密碼逾<font color=red>５分鐘將失效</font>。<br>{GetPubEmailContent()}";
                            }
                            break;

                        case OTPType.OnlineService:
                            if (memberData.sQply == "Y")
                            {
                                otpTypeCheck = false;
                                errorMsg = "網路保險服務已啟用";
                            }
                            else
                            {
                                sOtp = await GetOTPAsync(memberData.me_pid, memberData.me_mobile, request.OTPType, commonHelper.getClientIP());
                                smsMsg = HttpUtility.UrlEncode($"親愛的客戶您好,請在5分鐘內輸入驗證密碼『{sOtp}』以完成投保身分驗證。新光產險敬上", System.Text.Encoding.GetEncoding(950));
                                emailTitle = "新光產險網路保險服務驗證密碼通知";
                                emailContent += $"請您於確認網頁輸入驗證密碼『<font color=red>{sOtp}</font>』，以開啟網路保險服務並進行身分驗證。提醒您，驗證密碼逾<font color=red>５分鐘將失效</font>。<br>{GetPubEmailContent()}";
                            }
                            break;

                        case OTPType.Synthesize:
                            if (memberData.sQply == "Y")
                            {
                                otpTypeCheck = false;
                                errorMsg = "網路保險服務已啟用";
                            }
                            else if (memberData.otpchk == "Y")
                            {
                                otpTypeCheck = false;
                                errorMsg = "網路投保服務已啟用";
                            }
                            else
                            {
                                sOtp = await GetOTPAsync(memberData.me_pid, memberData.me_mobile, request.OTPType, commonHelper.getClientIP());
                                smsMsg = HttpUtility.UrlEncode($"親愛的客戶您好,請在5分鐘內於會員註冊網頁輸入驗證密碼『{sOtp}』以開啟網路投保及網路保險服務。新光產險敬上", System.Text.Encoding.GetEncoding(950));
                                emailTitle = "新光產險網路投保及保險服務驗證密碼通知";
                                emailContent += $"請您於確認網頁輸入驗證密碼『<font color=red>{sOtp}</font>』，以開啟網路投保及保險服務並進行身分驗證。提醒您，驗證密碼逾<font color=red>５分鐘將失效</font>。<br>{GetPubEmailContent()}";
                            }
                            break;

                        case OTPType.BeforeApply:
                            if (memberData.sQply != "Y")
                            {
                                otpTypeCheck = false;
                                errorMsg = "網路保險服務未啟用";
                            }
                            else
                            {
                                sOtp = await GetOTPAsync(memberData.me_pid, memberData.me_mobile, request.OTPType, commonHelper.getClientIP());
                                smsMsg = HttpUtility.UrlEncode($"親愛的客戶您好,請在5分鐘內輸入驗證密碼『{sOtp}』以完成投保身分驗證。新光產險敬上", System.Text.Encoding.GetEncoding(950));
                                emailTitle = "新光產險網路投保及保險服務驗證密碼通知";
                                emailContent += $"請您於確認網頁輸入驗證密碼『<font color=red>{sOtp}</font>』，以進行保險資料異動申請。提醒您，驗證密碼逾<font color=red>５分鐘將失效</font>。<br>{GetPubEmailContent()}";
                            }
                            break;

                        case OTPType.MemberSignIn:
                            sOtp = await GetOTPAsync(memberData.me_pid, memberData.me_mobile, request.OTPType, commonHelper.getClientIP());
                            smsMsg = HttpUtility.UrlEncode($"親愛的客戶您好,請在5分鐘內輸入驗證密碼『{sOtp}』以進行會員登入認證。新光產險敬上", System.Text.Encoding.GetEncoding(950));
                            emailTitle = "新光產險網路投保及保險服務驗證密碼通知";
                            emailContent += $"請您於確認網頁輸入驗證密碼『<font color=red>{sOtp}</font>』，以進行會員登入認證。提醒您，驗證密碼逾<font color=red>５分鐘將失效</font>。<br>{GetPubEmailContent()}";
                            break;

                        default:
                            otpTypeCheck = false;
                            errorMsg = "無此OTP服務項目";
                            break;
                    }
                    #endregion

                    if (otpTypeCheck)
                    {
                        //驗證成功寄簡訊及Email並回傳資料
                        var email = new List<string>() { memberData.me_email };
                        await SendSmsAndEmailAsync(memberData.me_pid, memberData.me_mobile, smsMsg, email, emailTitle, emailContent, sOtp, commonHelper.getClientIP());
                        return new MemberOTPResponseModel()
                        {
                            Result = true,
                            OTP = sOtp,
                            Mobile = memberData.me_mobile,
                            Email = memberData.me_email
                        };
                    }
                    else
                    {
                        //驗證失敗回傳錯誤訊息
                        return new MemberOTPResponseModel()
                        {
                            Result = otpTypeCheck,
                            ResultMsg = errorMsg
                        };
                    }
                }
                else
                {
                    //查無會員帳號回傳
                    return new MemberOTPResponseModel()
                    {
                        Result = false,
                        ResultMsg = "查無會員帳號"
                    };
                }
            }
            catch (Exception ex)
            {
                //TODO 記錄錯誤訊息
                //記錄錯誤訊息
                //memberOtpApiHelper.SaveLog("Exception", "N", "/member/api/MemberOTP/MemberOTP", getClientIP(), ex.Message);
                return new MemberOTPResponseModel()
                {
                    Result = false,
                    ResultMsg = "發生錯誤，請稍後再試。",
                    Mobile = ""
                };
            }

        }

    }
}