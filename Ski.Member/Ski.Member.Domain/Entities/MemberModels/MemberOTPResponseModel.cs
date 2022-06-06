namespace Ski.Member.Domain.Entities.MemberModels
{
    public class MemberOTPResponseModel
    {
        /// <summary>
        /// 驗證結果
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// 回傳訊息
        /// </summary>
        public string ResultMsg { get; set; }

        /// <summary>
        /// OTP密碼
        /// </summary>
        public string OTP { get; set; }

        /// <summary>
        /// 會員手機
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 會員信箱
        /// </summary>
        public string Email { get; set; }
    }
}
