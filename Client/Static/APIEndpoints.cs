namespace Client.Static
{
    internal static class APIEndpoints
    {

#if DEBUG
        internal const string ServerBaseUrl = "https://localhost:5003";
#else
        internal const string ServerBaseUrl = "https://oriolblogserver.azurewebsites.net";
#endif

        // Endpoint for category
        internal readonly static string s_categories = $"{ServerBaseUrl}/api/categories";

    }
}
