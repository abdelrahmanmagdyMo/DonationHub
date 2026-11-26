using DonationHub.Application.Contracts;

namespace DonationHub.API.Helpers
{
    public class LanguageHelper(IHttpContextAccessor accessor) : ILanguageHelper
    {
        private const string DefaultLanguage = "En";

        public static string Resolve(HttpRequest httpRequest)
        {
            var top = httpRequest.GetTypedHeaders().AcceptLanguage
                 .OrderByDescending(l => l.Quality ?? 1.0)
                 .Select(c => c.Value.Value)
                 .FirstOrDefault();
            return top != null && top.StartsWith("ar", StringComparison.OrdinalIgnoreCase)
                ? "ar" : "en";
        }

        public string GetCurrentLanguage()
        {
            var request = accessor.HttpContext!.Request;
            return request is null ? DefaultLanguage : Resolve(request);
        }
    }
}
