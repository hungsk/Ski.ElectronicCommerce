namespace Ski.Demo1.Domain
{
    public class UserDTO
    {
        public string name { get; set; }
        public string email { get; set; }
        public string tel { get; set; }
        public string address { get; set; }
    }

    public class OrderRequest
    {
        public Data data { get; set; }

        public class Data
        {
            public UserDTO user { get; set; }
            public string message { get; set; }
        }
    }

    public class OrderResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public decimal total { get; set; }
        public decimal create_at { get; set; }
        public string orderId { get; set; }
    }

    public class OrderDTO
    {
        public int create_at { get; set; }
        public string id { get; set; }
        public bool is_paid { get; set; }
        public string message { get; set; }
        public string payment_method { get; set; }
        public List<ProductInfo> products { get; set; }
        public decimal total { get; set; }
        public UserDTO user { get; set; }
        public int num { get; set; }

        public class ProductInfo
        {
            public CouponDTO coupon { get; set; }
            public decimal final_total { get; set; }
            public string id { get; set; }
            public ProductDTO product { get; set; }
            public string product_id { get; set; }
            public int qty { get; set; }
            public decimal total { get; set; }
        }
    }

    public class OrderGetResponse
    {
        public bool success { get; set; }
        public OrderDTO order { get; set; }
    }

    public class OrderListResponse
    {
        public bool success { get; set; }
        public List<OrderDTO> orders { get; set; }
        public Pagination pagination { get; set; }
        public List<string> messages { get; set; }
    }

    public class OrderUpdateRequest
    {
        public OrderDTO data { get; set; }
    }
}