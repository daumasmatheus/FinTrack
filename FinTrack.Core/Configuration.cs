using static System.Net.WebRequestMethods;

namespace FinTrack.Core
{
    public static class Configuration
    {
        public const int DefaultStatusCode = 200;
        public const int DefaultPageNumber = 1;
        public const int DefaultPageSize = 25;
        
        public static string BackEndUrl { get; set; } = "http://localhost:5211";
        public static string FrontEndUrl { get; set; } = "http://localhost:5052";
    }
}
