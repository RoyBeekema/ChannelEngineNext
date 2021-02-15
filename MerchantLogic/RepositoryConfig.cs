namespace MerchantLogic
{
    /// <summary>
    /// Configure the repository to use a certain API server
    /// </summary>
    public static class RepositoryConfig
    {
        private static string _apiKey = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322"; // Default

        private static string _host = "https://api-dev.channelengine.net/api/v2/"; // Default

        /// <summary>
        /// Configure the repository to use a certain server
        /// </summary>
        /// <param name="host">The full URI of the API</param>
        /// <param name="apiKey">The secure Api Key to use for calling the API</param>
        public static void Configure(string host, string apiKey)
        {
            _host = host;
            _apiKey = apiKey;
        }

        /// <summary>
        /// The full URI of the API
        /// </summary>
        public static string ApiKey { get { return _apiKey; } }

        /// <summary>
        /// The secure Api Key to use for calling the API
        /// </summary>
        public static string Host { get { return _host; } }
    }
}
