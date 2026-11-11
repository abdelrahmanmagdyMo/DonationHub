//using DonationHub.Domain.Primitives;
//using System.Net.Mail;

//namespace DonationHub.Domain.ValueObjects
//{
//    public sealed class EmailAddress : ValueObject
//    {
//        public const int MaxLength = 255;
//        public string Email { get; }
//        private EmailAddress(string email)
//        {
//            Email = email;
//        }
//        public static EmailAddress Create(string email)
//        {
//            if (email.Length > MaxLength)
//                throw new 
//            if (string.IsNullOrWhiteSpace(email)) return Result.Failure<EmailAddress>(new Error("EmailAddress.IsRequired", "Email Address Is Required"));
//            email = email.Trim();
//            var mailAddress = new MailAddress(email);
//            if (mailAddress.Address != email)
//                return Result.Failure<EmailAddress>(new Error("EmailAddress.InCorrect", "Specified Email Address Is Invalid Format"));
//            return Result.Success<EmailAddress>(new EmailAddress(email));
//        }
//        public override IEnumerable<object> GetEqualityValues()
//        {
//            yield return Email;
//        }
//    }
//}
