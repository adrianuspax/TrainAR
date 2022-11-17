using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManagement : MonoBehaviour
{
    public delegate void CoroutineDelegate(string scene);
    public static CoroutineDelegate coroutineDelegate;

    public static void LoadRedirection() => SceneManager.LoadScene(SceneName.Redirection);
    public static void LoadLanguage() => coroutineDelegate(SceneName.Language);
    public static void LoadSignIn() => coroutineDelegate(SceneName.SignIn);
    public static void LoadSignOn() => coroutineDelegate(SceneName.SignOn);
    public static void LoadUserData() => coroutineDelegate(SceneName.UserData);
    public static void LoadTrainAR() => coroutineDelegate(SceneName.TrainAR);
    public static void LoadMenu() => coroutineDelegate(SceneName.Menu);

    public static string PreviousScene
    {
        get => PlayerPrefs.GetString(Key.CurrentScene);
        set => PlayerPrefs.SetString(Key.CurrentScene, value);
    }
}