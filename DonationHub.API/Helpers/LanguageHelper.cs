namespace DonationHub.API.Helpers
{
    public static class LanguageHelper
    {
        public static string Resolve(HttpRequest httpRequest)
        {
            var top = httpRequest.GetTypedHeaders().AcceptLanguage
                 .OrderByDescending(l => l.Quality ?? 1.0)
                 .Select(c => c.Value.Value)
                 .FirstOrDefault();
            return top != null && top.StartsWith("ar", StringComparison.OrdinalIgnoreCase)
                ? "ar" : "en";
        }
    }
}
