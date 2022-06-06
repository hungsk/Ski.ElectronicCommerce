using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ski.Demo1.Domain
{
    public class User
    {
        [Key]
        [Column(TypeName = "nvarchar(200)")]
        public string id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string name { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string email { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string tel { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string address { get; set; }
    }
}