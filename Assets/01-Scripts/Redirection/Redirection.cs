using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASP;
using Debug = ASP.Debug;

public class Redirection : MonoBehaviour
{
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