namespace Ski.Demo1.Domain
{
    public class ArticlesDTO
    {
        public string ?id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string ?tag { get; set; }
        public List<string> ?tags { get; set; }
        public string ?image { get; set; }
        public string author { get; set; }
        public string content { get; set; }
        public Boolean isPublic { get; set; }
    }

    public class ArticlesRequest
    {
        public ArticlesDTO data { get; set; }
    }

    public class ArticlesListResponse
    {
        public bool success { get; set; }
        public List<ArticlesDTO> articles { get; set; }

        public Pagination pagination { get; set; }
        public List<string> messages { get; set; }
    }

    public class ArticlesAllResponse
    {
        public bool success { get; set; }
        public List<ArticlesDTO> articles { get; set; }
    }

    public class ArticlesResponse
    {
        public bool success { get; set; }
        public ArticlesDTO articles { get; set; }
    }
}