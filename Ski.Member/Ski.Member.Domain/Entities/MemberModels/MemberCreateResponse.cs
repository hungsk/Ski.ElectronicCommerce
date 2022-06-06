namespace Ski.Member.Domain.Entities.MemberModels
{
    public class MemberCreateResponse
    {
        /// <summary>
        /// 回傳結果
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// 回傳訊息
        /// </summary>
        public string ResultMsg { get; set; }
    }
}
