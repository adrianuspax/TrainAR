using UnityEngine;

public static class AccountData
{
    public const string USERNAME = "username";
    public const string PASSWORD = "password";
    public const string LANGUAGECHOSEN = "HasLanguage";

    public static string Username
    {
        get => PlayerPrefs.GetString(USERNAME);
        set => PlayerPrefs.SetString(USERNAME, value);
    }

    public static bool HasUsername => PlayerPrefs.HasKey(USERNAME);

    public static string Password
    {
        get => PlayerPrefs.GetString(PASSWORD);
        set => PlayerPrefs.SetString(PASSWORD, value);
    }

    public static bool HasPassword => PlayerPrefs.HasKey(PASSWORD);

    public static void DeleteAccount()
    {
        PlayerPrefs.DeleteKey(USERNAME);
        PlayerPrefs.DeleteKey(PASSWORD);
        PlayerPrefs.DeleteKey(LANGUAGECHOSEN);
        DataManagement.DeleteAllData();
    }

    public static bool HasAccount => PlayerPrefs.HasKey(USERNAME) && PlayerPrefs.HasKey(PASSWORD);
    public static bool HasLanguage => PlayerPrefs.HasKey(DataManagement.LangKey);
}
