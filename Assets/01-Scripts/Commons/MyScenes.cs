using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyScenes : MonoBehaviour
{
    private static readonly string[] scene = new string[]
    {
        "Language",
        "UserData",
        "TrainAR"
    };

    public static void LoadLanguageScene() => SceneManager.LoadScene(scene[0]);
    public static string LanguageScene => scene[0];
    public static void LoadUserData() => SceneManager.LoadScene(scene[1]);
    public static string UserData => scene[1];
    public static void LoadTrainAR() => SceneManager.LoadScene(scene[2]);
    public static string TrainAR => scene[2];
}