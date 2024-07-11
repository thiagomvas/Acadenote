using System.Text.RegularExpressions;

namespace Acadenode.Core
{
    public static class Utils
    {
        public const string AtLeastOneNumberRegex = "^(?=.*[0-9])";
        public const string AtLeastOneSpecialCharacterRegex = "^(?=.*[!@#\\$%\\^&\\*])";
        public const string AtLeastOneLetterRegex = "^(?=.*[a-zA-Z])";
        public const string NoSpacesRegex = "^(?=\\S+$)";

        public static bool IsValidPassword(string password)
        {
            if (Regex.IsMatch(password, AtLeastOneNumberRegex)
                && Regex.IsMatch(password, AtLeastOneLetterRegex)
                && Regex.IsMatch(password, NoSpacesRegex)
                && password.Length >= 8)
            {
                return true;
            }
            return false;
        }

    }
}
