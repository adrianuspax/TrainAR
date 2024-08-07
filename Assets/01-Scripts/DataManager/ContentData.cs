using ASP.DataManager;
using System;
using UnityEngine;

namespace TreinAR.DataManager
{
    [Serializable]
    public class ContentData : IDatable
    {
        public string name;
        public string gender;
        public string age;
        public string currentWeight;
        public string height;
        public string level;
        public string goal;
        public string focus;
        public string goalWeight;

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

        public static bool TryLoad(out ContentData contentData)
        {
            return Json.TryLoad(out contentData);
        }
    }
}
