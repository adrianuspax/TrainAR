using UnityEngine.Events;
using PlayerPrefs = ASP.PlayerPrefs;

namespace TreinAR
{
    public static class SceneManager
    {
        public static readonly ASP.SceneManagement.SceneManager.Scene redirection = new("Redirection");
        public static readonly ASP.SceneManagement.SceneManager.Scene language = new("Language");
        public static readonly ASP.SceneManagement.SceneManager.Scene signIn = new("SignIn");
        public static readonly ASP.SceneManagement.SceneManager.Scene signUp = new("SignUp");
        public static readonly ASP.SceneManagement.SceneManager.Scene userData = new("UserData");
        public static readonly ASP.SceneManagement.SceneManager.Scene trainAR = new("TrainAR");
        public static readonly ASP.SceneManagement.SceneManager.Scene menu = new("Menu");

        public static readonly PlayerPrefs.String currentScene = new ("currentScene");
        public static event UnityAction<ASP.SceneManagement.SceneManager.Scene> Load;

        public static void LoadRedirection()
        {
            ASP.SceneManagement.SceneManager.Load(redirection);
        }

        public static void LoadLanguage()
        {
            PlayerPrefs.String.Set(currentScene, ASP.SceneManagement.SceneManager.CurrentScene.name);
            Load?.Invoke(language);
        }

        public static void LoadSignIn()
        {
            PlayerPrefs.String.Set(currentScene, ASP.SceneManagement.SceneManager.CurrentScene.name);
            Load?.Invoke(signIn);
        }

        public static void LoadSignUp()
        {
            PlayerPrefs.String.Set(currentScene, ASP.SceneManagement.SceneManager.CurrentScene.name);
            Load?.Invoke(signUp);
        }

        public static void LoadUserData()
        {
            PlayerPrefs.String.Set(currentScene, ASP.SceneManagement.SceneManager.CurrentScene.name);
            Load?.Invoke(userData);
        }

        public static void LoadTrainAR()
        {
            PlayerPrefs.String.Set(currentScene, ASP.SceneManagement.SceneManager.CurrentScene.name);
            Load?.Invoke(trainAR);
        }

        public static void LoadMenu()
        {
            PlayerPrefs.String.Set(currentScene, ASP.SceneManagement.SceneManager.CurrentScene.name);
            Load?.Invoke(menu);
        }

        public static ASP.SceneManagement.SceneManager.Scene GetPreviousSceneSaved()
        {
            return new(PlayerPrefs.String.Get(currentScene));
        }
    }
}
