using UnityEngine;

public static class AccountData
{
    public static string Username
    {
        get => PlayerPrefs.GetString(Key.username);
        set => PlayerPrefs.SetString(Key.username, value);
    }

    public static string Password
    {
        get => PlayerPrefs.GetString(Key.password);
        set => PlayerPrefs.SetString(Key.password, value);
    }

    public static bool HasUsername => PlayerPrefs.HasKey(Key.username);
    public static bool HasPassword => PlayerPrefs.HasKey(Key.password);
    public static bool HasAccount => HasUsername && HasPassword;
    public static bool HasLanguage => PlayerPrefs.HasKey(Key.language);

    public static int Language
    {
        get => PlayerPrefs.GetInt(Key.language);
        set => PlayerPrefs.SetInt(Key.language, value);
    }

    public static bool Signed
    {
        get => PlayerPrefs.HasKey(Key.signed) && PlayerPrefs.GetInt(Key.signed) == 1;
        set => PlayerPrefs.SetInt(Key.signed, value ? 1 : 0);
    }

    public static string CompleteName => PlayerPrefs.GetString(DataManagement.pageKey[0]);
    public static bool IsMale => PlayerPrefs.GetInt(DataManagement.pageKey[1]) == 0;
    public static string Goal => PlayerPrefs.GetString(DataManagement.pageKey[2]);
    public static int Level => PlayerPrefs.GetInt(Key.levelValue);
    public static string Focus => PlayerPrefs.GetString(DataManagement.pageKey[4]);
    public static int Age => int.Parse(PlayerPrefs.GetString(DataManagement.footerKey[0]));
    public static float CurrentWeight => float.Parse(PlayerPrefs.GetString(DataManagement.footerKey[1]));
    public static float TargetWeight => float.Parse(PlayerPrefs.GetString(DataManagement.footerKey[2]));
    public static float Height => float.Parse(PlayerPrefs.GetString(DataManagement.footerKey[3]));
}
