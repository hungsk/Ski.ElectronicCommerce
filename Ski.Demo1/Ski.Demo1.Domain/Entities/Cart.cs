using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ski.Demo1.Domain
{
    public class Cart
    {
        [Key]
        [Column(TypeName = "nvarchar(200)")]
        public string id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string username { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string productid { get; set; }

        public int qty { get; set; }
        public decimal final_total { get; set; }
        public decimal total { get; set; }
        public int is_enabled { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string coupon_code { get; set; }
    }
}