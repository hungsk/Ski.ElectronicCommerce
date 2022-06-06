using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ski.Demo1.Domain
{
    public class Product
    {
        [Key]
        [Column(TypeName = "nvarchar(200)")]
        public string id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string category { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string content { get; set; }

        [Column(TypeName = "nvarchar(2000)")]
        public string description { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string imageUrl { get; set; }

        public List<ImagesUrl> imagesUrl { get; set; }

        public int is_enabled { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public decimal origin_price { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal price { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string title { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string unit { get; set; }
    }

    [Index(nameof(url), nameof(productid), IsUnique = true)]
    public class ImagesUrl
    {
        [Column(TypeName = "nvarchar(2000)")]
        public string url { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string productid { get; set; } //Foreign Key
    }
}