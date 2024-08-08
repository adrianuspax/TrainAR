using System;
using UnityEngine;

namespace TreinAR.DataManager
{
    using ASP.DataManager;

    [Serializable]
    public class UserData : IDatable
    {
        public string name;
        public bool isMale;
        public int age;
        public float currentWeight;
        public float height;
        public int levelIndex;
        public int goalIndex;
        public int focusIndex;
        public float targetWeight;

        public string ToString(bool prettyPrint = false)
        {
            return JsonUtility.ToJson(this, prettyPrint);
        }

        public void Save()
        {
            Json.Save(this);
        }

        public static bool TryLoad(out UserData userData, ASP.SceneManagement.SceneManager.Scene scene)
        {
            return Json.TryLoad(out userData, scene.name);
        }
    }


}


