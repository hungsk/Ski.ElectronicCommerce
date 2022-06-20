using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ski.Demo1.Domain
{
    public class Articles
    {
        [Key]
        [Column(TypeName = "nvarchar(200)")]
        public string id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string title { get; set; }

        [Column(TypeName = "nvarchar(2000)")]
        public string description { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string ?tag { get; set; }
        public List<Tags> tags { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string ?image { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string author { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string content { get; set; }

        public int isPublic { get; set; }
    }

    [Index(nameof(Name), nameof(ArticlesId), IsUnique = true)]
    public class Tags
    {
        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string ArticlesId { get; set; } //Foreign Key
    }
}