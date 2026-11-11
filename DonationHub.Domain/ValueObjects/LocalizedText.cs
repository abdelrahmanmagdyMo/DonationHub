using DonationHub.Domain.Exceptions;
using DonationHub.Domain.Primitives;
using System.Text;
namespace DonationHub.Domain.ValueObjects
{
    public sealed class LocalizedText : ValueObject
    {
        public const int MaxLength = 250;
        public string Arabic { get; }
        public string English { get; }
        private LocalizedText(string arabic, string english)
        {
            Arabic = arabic;
            English = english;
        }
        public static LocalizedText Create(string arabic, string english)
        {
            if (string.IsNullOrWhiteSpace(arabic) || !ContainsArabic(arabic))
                throw new LocalizedText_ArabicException();
            if (string.IsNullOrWhiteSpace(english))
                throw new LocalizedText_EnglishException();
            arabic = arabic.Trim();
            english = english.Trim();

            return new LocalizedText(arabic, english);
        }
        private static bool ContainsArabic(string text) =>
            text.EnumerateRunes().All(e => e.Value >= 0x0600 && e.Value <= 0x06FF || Rune.IsDigit(e) || Rune.IsWhiteSpace(e));




        public override IEnumerable<object> GetEqualityValues()
        {
            yield return Arabic;
            yield return English;
        }
    }
}
