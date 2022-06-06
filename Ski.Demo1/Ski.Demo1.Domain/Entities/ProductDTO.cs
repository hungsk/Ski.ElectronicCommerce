namespace Ski.Demo1.Domain
{
    public class ProductDTO
    {
        public string? id { get; set; }
        public string category { get; set; }
        public string content { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
        public List<string> imagesUrl { get; set; }
        public int is_enabled { get; set; }
        public decimal origin_price { get; set; }
        public decimal price { get; set; }
        public string title { get; set; }
        public string unit { get; set; }
    }

    public class ProductRequest
    {
        public ProductDTO data { get; set; }
    }

    public class EditResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
    }

    public class ProductListResponse
    {
        public bool success { get; set; }
        public List<ProductDTO> products { get; set; }

        public Pagination pagination { get; set; }
        public List<string> messages { get; set; }
    }

    public class Pagination
    {
        public int total_pages { get; set; }
        public int current_page { get; set; }
        public bool has_pre { get; set; }
        public bool has_next { get; set; }
        public object category { get; set; }
    }

    public class ProductAllResponse
    {
        public bool success { get; set; }
        public List<ProductDTO> products { get; set; }
    }

    public class ProductResponse
    {
        public bool success { get; set; }
        public ProductDTO product { get; set; }
    }
}