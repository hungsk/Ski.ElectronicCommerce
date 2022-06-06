namespace Ski.Member.Domain.Entities.MemberModels
{
    public class MemberOTPCheckRequestModel
    {
        /// <summary>
        /// 會員帳號
        /// </summary>
        public string Uid = "";

        /// <summary>
        /// OTP密碼
        /// </summary>
        public string OTP = "";

        /// <summary>
        /// OTP類型
        /// 0:首次締約作網路投保OTP
        /// 1:投保前OTP
        /// 2:網路保險服務OTP
        /// 3:網路投保+保險服務OTP
        /// 4:保險服務申請前OTP
        /// 5:會員登入OTP
        /// 6:非會員OTP驗證
        /// </summary>
        public string OTPType = "";
    }
}
