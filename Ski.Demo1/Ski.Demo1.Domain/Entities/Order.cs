using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ski.Demo1.Domain
{
    public class Order
    {
        [Key]
        [Column(TypeName = "nvarchar(200)")]
        public string id { get; set; }

        public DateTime create_at { get; set; }

        public int is_paid { get; set; }

        public string payment_method { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string message { get; set; }

        public decimal total { get; set; }
        //public decimal final_total { get; set; }

        public List<OrderItem> orderItem { get; set; } //Foreign Key

        public string userid { get; set; }
        public User user { get; set; } //Foreign Key
    }

    public class OrderItem
    {
        [Key]
        [Column(TypeName = "nvarchar(200)")]
        public string id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string orderid { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string productid { get; set; }

        public int qty { get; set; }

        public decimal total { get; set; }

        public decimal final_total { get; set; }
    }
}