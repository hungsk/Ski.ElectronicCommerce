namespace Ski.Demo1.Domain
{
    public class CartDTO
    {
        public CouponDTO coupon { get; set; }
        public string id { get; set; }
        public Product product { get; set; }
        public string product_id { get; set; }
        public int qty { get; set; }
        public decimal total { get; set; }
        public decimal final_total { get; set; }
    }

    public class CouponDTO
    {
        public string code { get; set; }
        public string due_date { get; set; }
        public string id { get; set; }
        public int is_enabled { get; set; }
        public decimal percent { get; set; }
        public string title { get; set; }
    }

    public class CartRequest
    {
        public CartRequestData data { get; set; }

        public class CartRequestData
        {
            public string product_id { get; set; }
            public int qty { get; set; }
        }
    }

    public class CartResponse
    {
        public bool success { get; set; }
        public CartResponseData data { get; set; }
        public string message { get; set; }

        public class CartResponseData
        {
            public string product_id { get; set; }
            public decimal qty { get; set; }
            public string coupon_code { get; set; }
            public string id { get; set; }
            public decimal total { get; set; }
            public decimal final_total { get; set; }
            public Product product { get; set; }
        }
    }

    public class CartListResponse
    {
        public bool success { get; set; }
        public CartListResponseData data { get; set; }

        public List<string> messages { get; set; }

        public class CartListResponseData
        {
            public List<CartDTO> carts { get; set; }
            public decimal total { get; set; }
            public decimal final_total { get; set; }
        }
    }
}