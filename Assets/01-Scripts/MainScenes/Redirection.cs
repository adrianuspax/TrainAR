using TreinAR.DataManager;
using UnityEngine;

namespace TreinAR.MainScenes
{
    public class Redirection : MonoBehaviour
    {
        private void Start()
        {
            if (AccountData.Signed)
                SceneManager.LoadTrainAR();
            else
                SceneManager.LoadSignIn();
        }
    }
}
