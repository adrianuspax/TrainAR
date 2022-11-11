using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManagement : MonoBehaviour
{
    public const string Redirection = "Redirection";
    public const string Language = "Language";
    public const string SignIn = "SignIn";
    public const string SignOn = "SignOn";
    public const string UserData = "UserData";
    public const string TrainAR = "TrainAR";
    public const string Menu = "Menu";

    public delegate void CoroutineDelegate(string scene);
    public static CoroutineDelegate coroutineDelegate;

    public static void LoadRedirection() => SceneManager.LoadScene(Redirection);
    public static void LoadLanguage() => coroutineDelegate(Language);
    public static void LoadSignIn() => coroutineDelegate(SignIn);
    public static void LoadSignOn() => coroutineDelegate(SignOn);
    public static void LoadUserData() => coroutineDelegate(UserData);
    public static void LoadTrainAR() => coroutineDelegate(TrainAR);
    public static void LoadMenu() => coroutineDelegate( Menu);

    public static string PreviousScene
    {
        get => PlayerPrefs.GetString("currentScene");
        set => PlayerPrefs.SetString("currentScene", value);
    }
}