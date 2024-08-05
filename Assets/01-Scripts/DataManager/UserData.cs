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
        public float weight;
        public float height;
        public int levelIndex;

        public string ToString(bool prettyPrint = false)
        {
            return JsonUtility.ToJson(this, prettyPrint);
        }

        public void Save()
        {
            Json.Save(this);
        }

        public static UserData Load()
        {
            return Json.Load<UserData>();
        }
    }
}


