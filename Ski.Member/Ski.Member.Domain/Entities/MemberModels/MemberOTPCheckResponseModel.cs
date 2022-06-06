namespace Ski.Member.Domain.Entities.MemberModels
{
    public class MemberOTPCheckResponseModel
    {
        /// <summary>驗證結果</summary>
        public bool Result { get; set; }

        /// <summary>回傳訊息</summary>
        public string ResultMsg { get; set; }
    }
}
