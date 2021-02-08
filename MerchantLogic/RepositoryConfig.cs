namespace MerchantLogic
{
    public static class RepositoryConfig
    {
        private static string _apiKey = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322"; // Default

        private static string _host = "https://api-dev.channelengine.net/api/v2/"; // Default
        public static void Configure(string host, string apiKey)
        {
            _host = host;
            _apiKey = apiKey;
        }

        public static string ApiKey { get { return _apiKey; } }
        public static string Host { get { return _host; } }
    }
}
