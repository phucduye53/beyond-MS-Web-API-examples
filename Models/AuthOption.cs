namespace DemoApi.Models
{
    public class AuthOption
    {
        public string SecureKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiresInMinutes { get; set; }
    }
}