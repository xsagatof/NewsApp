using System.Text.RegularExpressions;

namespace NewsApp.Helpers
{
    public static class PhoneNumberValidator
    {
        // Basic phone number validation pattern (adjust based on your needs)
        private static readonly Regex PhoneRegex = new Regex(@"^\+?[1-9]\d{1,14}$");

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            return PhoneRegex.IsMatch(phoneNumber);
        }

        public static string NormalizePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return string.Empty;

            // Remove spaces, dashes, parentheses
            var normalized = Regex.Replace(phoneNumber, @"[\s\-\(\)]", "");
            
            // Ensure it starts with +
            if (!normalized.StartsWith("+"))
            {
                // Add country code if missing (adjust default as needed)
                normalized = "+998" + normalized;
            }

            return normalized;
        }

        public static bool TryNormalizePhoneNumber(string phoneNumber, out string normalized)
        {
            normalized = NormalizePhoneNumber(phoneNumber);
            return IsValidPhoneNumber(normalized);
        }
    }
}