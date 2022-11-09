using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManagement : MonoBehaviour
{
    public const string Redirection = "Redirection";
    public const string Language = "Language";
    public const string UserData = "UserData";
    public const string TrainAR = "TrainAR";
    public const string Menu = "Menu";
    public const string SignOn = "SignOn";
    public const string SignIn = "SignIn";

    public delegate void CoroutineDelegate(float time, string scene);
    public static CoroutineDelegate coroutineDelegate;

    public static void LoadLanguage() => SceneManager.LoadScene(Language);
    public static void LoadLanguage(float delay) => coroutineDelegate(delay, Language);
    public static void LoadUserData() => SceneManager.LoadScene(UserData);
    public static void LoadUserData(float delay) => coroutineDelegate(delay, UserData);
    public static void LoadTrainAR() => SceneManager.LoadScene(TrainAR);
    public static void LoadTrainAR(float delay) => coroutineDelegate(delay, TrainAR);
    public static void LoadMenu() => SceneManager.LoadScene(Menu);
    public static void LoadMenu(float delay) => coroutineDelegate(delay, Menu);
    public static void LoadRedirection() => SceneManager.LoadScene(Redirection);
    public static void LoadRedirection(float delay) => coroutineDelegate(delay, Redirection);
}