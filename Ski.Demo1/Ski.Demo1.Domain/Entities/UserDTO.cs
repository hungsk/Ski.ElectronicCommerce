namespace Ski.Demo1.Domain
{
    public class UserRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class UserResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string uid { get; set; }
        public string token { get; set; }
        public string expired { get; set; }
    }
}