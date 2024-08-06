using ASP;

namespace TreinAR.DataManager
{
    public static class AccountData
    {
        public readonly static PlayerPrefs.String username = new("username");
        public readonly static PlayerPrefs.String password = new("password");
        public readonly static PlayerPrefs.Integer language = new("language");
        public readonly static PlayerPrefs.Boolean signed = new("signed");
        public readonly static PlayerPrefs.Integer levelValue = new("levelValue");

        public static string Username
        {
            get => PlayerPrefs.String.Get(username);
            set => PlayerPrefs.String.Set(username, value);
        }

        public static string Password
        {
            get => PlayerPrefs.String.Get(password);
            set => PlayerPrefs.String.Set(password, value);
        }

        public static int Language
        {
            get => PlayerPrefs.Integer.Get(language);
            set => PlayerPrefs.Integer.Set(language, value);
        }

        public static bool Signed
        {
            get => PlayerPrefs.Boolean.HasSaved(signed) && PlayerPrefs.Boolean.Get(signed);
            set => PlayerPrefs.Boolean.Set(signed, value);
        }

        public static bool HasUsername => PlayerPrefs.String.HasSaved(username);
        public static bool HasPassword => PlayerPrefs.String.HasSaved(password);
        public static bool HasAccount => PlayerPrefs.String.HasSaved(username) && PlayerPrefs.String.HasSaved(password);
        public static bool HasLanguage => PlayerPrefs.Integer.HasSaved(language);
    }
}
