namespace Ski.Member.Domain.Entities.MemberModels
{
    public class MemberCreateRequest
    {
        /// <summary>
        /// 會員帳號
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// 會員密碼
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 會員姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 會員生日 yyyyMMdd
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// 會員手機
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 郵遞區號
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// 縣市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 地區
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 網路投保開通 Y/N
        /// </summary>
        public string OTPCheck { get; set; }

        /// <summary>
        /// 網路保險服務 Y/N
        /// </summary>
        public string Qply { get; set; }

        /// <summary>
        /// 收Email通知 Y/N
        /// </summary>
        public string RememberEmail { get; set; }
    }
}
