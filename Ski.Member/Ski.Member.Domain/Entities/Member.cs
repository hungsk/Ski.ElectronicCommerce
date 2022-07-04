using System;
using System.ComponentModel.DataAnnotations;

namespace Ski.Member.Domain
{
    public class Member
    {
        [Key]
        ///<summary>會員流水號</summary>
        public int me_sn { get; set; }

        ///<summary>會員姓名</summary>
        public string? me_name { get; set; }

        ///<summary>會員帳號</summary>
        public string? me_id { get; set; }

        ///<summary>會員密碼</summary>
        public string? me_pw { get; set; }

        ///<summary>會員性別</summary>
        public string? me_gender { get; set; }

        ///<summary>會員身分證</summary>
        public string? me_pid { get; set; }

        ///<summary>會員生日</summary>
        public DateTime? me_birth { get; set; }

        ///<summary>會員家電</summary>
        public string? me_tel { get; set; }

        ///<summary>會員家電分機</summary>
        public string? me_tel_ext { get; set; }

        ///<summary>會員傳真電話</summary>
        public string? me_fax { get; set; }

        ///<summary>會員行動電話</summary>
        public string? me_mobile { get; set; }

        ///<summary>會員戶籍郵遞區號</summary>
        public string? me_add1_post { get; set; }

        ///<summary>會員戶籍縣市</summary>
        public string? me_add1_city { get; set; }

        ///<summary>會員戶籍地區</summary>
        public string? me_add1_area { get; set; }

        ///<summary>會員戶籍通訊地址</summary>
        public string? me_add1 { get; set; }

        ///<summary>會員未婚/已婚</summary>
        public string? me_marital { get; set; }

        ///<summary>會員國籍</summary>
        public string? me_national { get; set; }

        ///<summary>會員Email信箱</summary>
        public string? me_email { get; set; }

        ///<summary>會員地址郵遞區號</summary>
        public string? me_add2_post { get; set; }

        ///<summary>會員居住縣市</summary>
        public string? me_add2_city { get; set; }

        ///<summary>會員居住地區</summary>
        public string? me_add2_area { get; set; }

        ///<summary>會員通訊地址</summary>
        public string? me_add2 { get; set; }

        ///<summary></summary>
        public string? me_receiver { get; set; }

        ///<summary>註冊建立日期時間判斷用 Unicode解密 or ansi解密 支援舊資料</summary>
        public DateTime? adddate { get; set; }

        ///<summary>會員資料異動時間</summary>
        public DateTime? moddate { get; set; }

        ///<summary>OTP驗證否 Y=驗證/N=還沒</summary>
        public string? otpchk { get; set; }

        ///<summary>Email驗證碼</summary>
        public string? authcode { get; set; }

        ///<summary>Email驗證否 Y=驗證/N=還沒</summary>
        public string? authflag { get; set; }

        ///<summary>OPT驗證時間</summary>
        public DateTime? dOTP { get; set; }

        ///<summary>註冊方式 W=網站/P=紙本</summary>
        public string? m_RegType { get; set; }

        ///<summary>是否有更換過密碼 Y=變更/N=還沒</summary>
        public string? bChangePw { get; set; }

        ///<summary>會員帳號種類 B2C</summary>
        public string? iCreate { get; set; }

        ///<summary>記憶卡號否Y/N</summary>
        public string? RememberCC { get; set; }

        ///<summary>是否記得信箱</summary>
        public string? RememberEmail { get; set; }

        ///<summary>變更行動電話次數</summary>
        public string? iChangeMobile { get; set; }

        ///<summary>最後登入時間</summary>
        public DateTime? LstLoginTime { get; set; }

        ///<summary>會員登入時間</summary>
        public DateTime? appLoginTime { get; set; }

        ///<summary>會員密碼(加密過)</summary>
        public string? me_pwE { get; set; }

        ///<summary>會員姓名(加密過)</summary>
        public string? me_nameE { get; set; }

        ///<summary>是否鎖定</summary>
        public string? bLock { get; set; }

        ///<summary>連續錯誤次數</summary>
        public int? ilock { get; set; }

        ///<summary>網路保險服務 Y=驗證/N=還沒</summary>
        public string? sQply { get; set; }

        ///<summary>網路保險服務啟用日期</summary>
        public DateTime? dQply { get; set; }
    }
}

