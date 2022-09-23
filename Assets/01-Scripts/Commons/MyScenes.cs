using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyScenes : MonoBehaviour
{
    private static readonly string[] scene = new string[]
    {
        "UserData",
        "TrainAR"
    };

    public static void LoadUserData()
    {
        SceneManager.LoadScene(scene[0]);
    }

    public static void LoadTrainAR()
    {
        SceneManager.LoadScene(scene[1]);
    }
}