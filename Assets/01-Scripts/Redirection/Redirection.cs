using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASP;
using Debug = ASP.Debug;

[RequireComponent(typeof(AnimationsManager))]
public class Redirection : MonoBehaviour
{
    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    
    [Header("Scripts", order = 1)]
    [SerializeField, ReadOnly] private AnimationsManager Animations;

    private void Awake()
    {
        Animations = this.GetComponent<AnimationsManager>();
    }

    private void Start()
    {
        RedirectionScene();
    }

    public void RedirectionScene()
    {
        if (AccountData.HasAccount)
        {
            if (AccountData.Signed)
            { TrainARSceneManager.LoadTrainAR(); }
            else
            {
                if (AccountData.HasLanguage)
                { TrainARSceneManager.LoadSignIn(); }
                else
                { TrainARSceneManager.LoadLanguage(); }
            }
        }
        else
        { TrainARSceneManager.LoadLanguage(); }
    }
}