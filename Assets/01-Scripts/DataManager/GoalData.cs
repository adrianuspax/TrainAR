using System;
using UnityEngine;

namespace TreinAR.DataManager
{
    using ASP.DataManager;

    [Serializable]
    public class GoalData : IDatable
    {
        public int goalIndex;
        public int focusIndex;
        public float weight;

        public string ToString(bool prettyPrint = false)
        {
            return JsonUtility.ToJson(this, prettyPrint);
        }

        public void Save()
        {
            Json.Save(this);
        }

        public static GoalData Load()
        {
            return Json.Load<GoalData>();
        }
    }
}