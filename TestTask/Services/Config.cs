namespace TestTask.Services
{
    public class Config
    {
        public static string? ConnectionString {  get; set; }

        public static string? SecretKey { get; set; }

        public static double ExpiryInMinutes { get; set; }

        public static string? Issuer { get; set; }
    }
}
