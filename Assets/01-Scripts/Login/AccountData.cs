using ASP;

public static class AccountData
{
    public readonly static PlayerPrefs.String username = new("username");
    public readonly static PlayerPrefs.String password = new("password");
    public readonly static PlayerPrefs.Integer language = new("language");
    public readonly static PlayerPrefs.Boolean signed = new("signed");
    public readonly static PlayerPrefs.Integer levelValue = new("levelValue");

    public readonly static PlayerPrefs.String name = DataManagement.pageKey[0];
    public readonly static PlayerPrefs.Boolean gender = DataManagement.pageKey[1];
    public readonly static PlayerPrefs.String goal = DataManagement.pageKey[2];
    public readonly static PlayerPrefs.String levelPhrase = DataManagement.pageKey[3];
    public readonly static PlayerPrefs.String focus = DataManagement.pageKey[4];

    public readonly static PlayerPrefs.String age = DataManagement.footerKey[0];
    public readonly static PlayerPrefs.String currentWeight = DataManagement.footerKey[1];
    public readonly static PlayerPrefs.String targetWeight = DataManagement.footerKey[2];
    public readonly static PlayerPrefs.String height = DataManagement.footerKey[3];

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

    public static string CompleteName => PlayerPrefs.String.Get(name);
    public static bool IsMale => PlayerPrefs.Boolean.Get(gender);
    public static string Goal => PlayerPrefs.String.Get(goal);
    public static int Level => PlayerPrefs.Integer.Get(levelValue);
    public static string LevelPhrase => PlayerPrefs.String.Get(levelPhrase);
    public static string Focus => PlayerPrefs.String.Get(focus);

    public static int Age => int.Parse(PlayerPrefs.String.Get(age));
    public static float CurrentWeight => float.Parse(PlayerPrefs.String.Get(currentWeight));
    public static float TargetWeight => float.Parse(PlayerPrefs.String.Get(targetWeight));
    public static float Height => float.Parse(PlayerPrefs.String.Get(height));
}
