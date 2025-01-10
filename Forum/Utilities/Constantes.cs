namespace Forum.Utilities
{
    public class Constantes
    {
        public const string GLOBAL_PEPPER = "hGQu3stwq9B3Znv";
        public const string USER_ROLE = "User";
        public const string ADMIN_ROLE = "Administrator";

        public const string USERNAME_REGEX = "^[a-zA-Z0-9_]+$";
        public const int LENGTH_MIN = 8;
        public const string REGEX_UPPERCASE = @"[A-Z]*";
        public const string REGEX_LOWERCASE = @"[a-z]*";
        public const string REGEX_DIGIT = @"[0-9]*";
        public const string REGEX_NO_SPACE = @"[^\s]*";

        public const int PAGE_SIZE = 5;
        public const int TIMEOUT_DURATION_IN_MINUTES = 5;
    }
}
