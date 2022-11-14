using UnityEngine;

public static class AccountData
{
    public const string USERNAME = "username";
    public const string PASSWORD = "newPassword";
    public const string LANGUAGE = "language";
    public const string SIGNED = "signed";

    public static string Username
    {
        get => PlayerPrefs.GetString(USERNAME);
        set => PlayerPrefs.SetString(USERNAME, value);
    }

    public static string Password
    {
        get => PlayerPrefs.GetString(PASSWORD);
        set => PlayerPrefs.SetString(PASSWORD, value);
    }

    public static bool HasUsername => PlayerPrefs.HasKey(USERNAME);
    public static bool HasPassword => PlayerPrefs.HasKey(PASSWORD);
    public static bool HasAccount => HasUsername && HasPassword;
    public static bool HasLanguage => PlayerPrefs.HasKey(LANGUAGE);

    public static int Language
    {
        get => PlayerPrefs.GetInt(LANGUAGE);
        set => PlayerPrefs.SetInt(LANGUAGE, value);
    }

    public static bool Signed
    {
        get => PlayerPrefs.HasKey(SIGNED) && PlayerPrefs.GetInt(SIGNED) == 1;
        set => PlayerPrefs.SetInt(SIGNED, value ? 1 : 0);
    }
}
