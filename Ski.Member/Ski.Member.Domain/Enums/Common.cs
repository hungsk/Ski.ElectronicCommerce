namespace Ski.Member.Domain.Enums
{
    /// <summary>
    /// OTP型態
    /// </summary>
    public enum OTPType
    {
        /// <summary>
        /// 首次締約作網路投保OTP
        /// </summary>
        FirstOTP = 0,

        /// <summary>
        /// 投保前OTP
        /// </summary>
        BeforeInsure = 1,

        /// <summary>
        /// 網路保險服務OTP
        /// </summary>
        OnlineService = 2,

        /// <summary>
        /// 網路投保+保險服務OTP
        /// </summary>
        Synthesize = 3,

        /// <summary>
        /// 保險服務申請前OTP
        /// </summary>
        BeforeApply = 4,

        /// <summary>
        /// 會員登入OTP
        /// </summary>
        MemberSignIn = 5,

        /// <summary>
        /// 非會員OTP
        /// </summary>
        NonMemberOTP = 6,

        /// <summary>
        /// 電商後台登入OTP
        /// </summary>
        ECLogonOTP = 7
    }

    /// <summary>
    /// CardsType
    /// </summary>
    public enum CardsType
    {
        /// <summary>
        /// 身分證
        /// </summary>
        IdentityCard = 1,

        /// <summary>
        /// 居留證
        /// </summary>
        ResidentPermit = 2,

        /// <summary>
        /// 護照號
        /// </summary>
        PassPort = 3
    }

    /// <summary>
    /// zipCode
    /// </summary>
    public class zipCode
    {
        ///<summary>0</summary>
        public string ZipCode { get; set; }

        ///<summary>1</summary>
        public string City { get; set; }

        ///<summary>1</summary>
        public string Area { get; set; }

        ///<summary>1</summary>
        public string iCity { get; set; }
    }

    /// <summary>
    /// Base64字串加解密動作
    /// </summary>
    public enum Base64Encrypt
    {
        /// <summary>
        /// 加密
        /// </summary>
        Encode,

        /// <summary>
        /// 解密
        /// </summary>
        Decode
    }

    /// <summary>
    /// 英文數字驗證規則
    /// </summary>
    public enum PasswordRule
    {
        /// <summary>
        /// 包含英文字母與數字
        /// </summary>
        AlphabetAndNumeric = 0,

        /// <summary>
        /// 包含英文字母
        /// </summary>
        ContainAlphabet = 1,

        /// <summary>
        /// 包含數字
        /// </summary>
        ContainNumeric = 2,

        /// <summary>
        /// 有大寫英文字母
        /// </summary>
        AlphabetUpperCase = 3,

        /// <summary>
        /// 有小寫英文字母
        /// </summary>
        AlphabetLowerCase = 4,
    }

    /// <summary>
    /// 連線字串
    /// </summary>
    public enum ConnectionString
    {
        /// <summary>
        /// SKI 主要資料庫
        /// </summary>
        SkiDb,

        /// <summary>
        /// 會員資料庫
        /// </summary>
        SkiMemberDb,

        /// <summary>
        /// SKIWeb資料庫
        /// </summary>
        SkiWebDB,

        /// <summary>
        /// SkiB2CWebDb資料庫
        /// </summary>
        SkiB2CWebDb
    }
}
