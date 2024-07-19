using ASP.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using PlayerPrefs = ASP.PlayerPrefs;

public class TrainARSceneManager : MonoBehaviour
{
    public static readonly SceneManager.Scene redirection = new("Redirection");
    public static readonly SceneManager.Scene language = new("Language");
    public static readonly SceneManager.Scene signIn = new("SignIn");
    public static readonly SceneManager.Scene signOn = new("SignOn");
    public static readonly SceneManager.Scene userData = new("UserData");
    public static readonly SceneManager.Scene trainAR = new("TrainAR");
    public static readonly SceneManager.Scene menu = new("Menu");

    public static readonly PlayerPrefs.String currentScene = new ("currentScene");
    public static event UnityAction<SceneManager.Scene> Load;

    public static void LoadRedirection()
    {
        SceneManager.Load(redirection);
    }

    public static void LoadLanguage()
    {
        PlayerPrefs.String.Set(currentScene, SceneManager.CurrentScene.name);
        Load?.Invoke(language);
    }

    public static void LoadSignIn()
    {
        PlayerPrefs.String.Set(currentScene, SceneManager.CurrentScene.name);
        Load?.Invoke(signIn);
    }

    public static void LoadSignOn()
    {
        PlayerPrefs.String.Set(currentScene, SceneManager.CurrentScene.name);
        Load?.Invoke(signOn);
    }

    public static void LoadUserData()
    {
        PlayerPrefs.String.Set(currentScene, SceneManager.CurrentScene.name);
        Load?.Invoke(userData);
    }

    public static void LoadTrainAR()
    {
        PlayerPrefs.String.Set(currentScene, SceneManager.CurrentScene.name);
        Load?.Invoke(trainAR);
    }

    public static void LoadMenu()
    {
        PlayerPrefs.String.Set(currentScene, SceneManager.CurrentScene.name);
        Load?.Invoke(menu);
    }

    public static SceneManager.Scene GetPreviousSceneSaved()
    {
        return new(PlayerPrefs.String.Get(currentScene));
    }
}