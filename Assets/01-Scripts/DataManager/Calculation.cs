using ASP;
using System;
using UnityEngine;

namespace TreinAR.DataManager
{
    [Serializable]
    public class Calculation
    {
        [SerializeField, ReadOnly] private ContentData contentData;
        [SerializeField, ReadOnly] private UserData userData;

        public Calculation(UserData userData, ContentData contentData)
        {
            this.userData = userData;
            this.contentData = contentData;
        }

        public enum BMI_Status
        {
            Lean = 0,
            Normal = 1,
            Overweight = 2,
            Fat = 3
        };

        public enum Goal
        {
            LoseWeight = 0,
            MoreMuscle = 1,
            KeepShape = 2
        };

        public enum Level
        {
            I = 1,
            II = 2,
            III = 3,
            IV = 4,
            V = 5
        };

        public enum Focus
        {
            chest = 0, //1
            abdomen = 1, //0
            legs = 2, //3
            arms = 3 //2
        };

        private float GetBMI(float height, float weight)
        {
            float bmi, heightInMeters;
            heightInMeters = height * 0.01f;
            bmi = weight / Mathf.Pow(heightInMeters, 2);
            return bmi;
        }

        private BMI_Status GetStatusBMI(float bmi)
        {
            return bmi switch
            {
                < 18.5f => BMI_Status.Lean,
                < 25f => BMI_Status.Normal,
                < 30f => BMI_Status.Overweight,
                _ => BMI_Status.Fat,
            };
        }

        private float GetBodyFat(int age, bool isMale, Level currentLevel)
        {
            return age switch
            {
                <= 29 => currentLevel switch
                {
                    Level.I => isMale ? 0.23f : 0.31f,
                    Level.II => isMale ? 0.22f : 0.30f,
                    Level.III => isMale ? 0.17f : 0.24f,
                    Level.IV => isMale ? 0.12f : 0.175f,
                    Level.V => isMale ? 0.11f : 0.16f,
                    _ => isMale ? 0.17f : 0.24f,
                },
                <= 39 => currentLevel switch
                {
                    Level.I => isMale ? 0.24f : 0.32f,
                    Level.II => isMale ? 0.23f : 0.31f,
                    Level.III => isMale ? 0.18f : 0.25f,
                    Level.IV => isMale ? 0.13f : 0.185f,
                    Level.V => isMale ? 0.12f : 0.17f,
                    _ => isMale ? 0.18f : 0.25f,
                },
                <= 49 => currentLevel switch
                {
                    Level.I => isMale ? 0.26f : 0.33f,
                    Level.II => isMale ? 0.25f : 0.32f,
                    Level.III => isMale ? 0.20f : 0.26f,
                    Level.IV => isMale ? 0.15f : 0.195f,
                    Level.V => isMale ? 0.14f : 0.18f,
                    _ => isMale ? 0.20f : 0.26f,
                },
                _ => currentLevel switch
                {
                    Level.I => isMale ? 0.27f : 0.34f,
                    Level.II => isMale ? 0.26f : 0.33f,
                    Level.III => isMale ? 0.21f : 0.27f,
                    Level.IV => isMale ? 0.16f : 0.205f,
                    Level.V => isMale ? 0.15f : 0.19f,
                    _ => isMale ? 0.21f : 0.27f,
                }
            };
        }

        private float GetFat(float weight, float bodyFat) => weight * bodyFat;
        private float GetLoseWeight(float currentWeight, float targetWeight) => currentWeight - targetWeight;
        public float ValueBMI => GetBMI(userData.height, userData.currentWeight);
        public float BodyFat => GetBodyFat(userData.age, userData.isMale, (Level)userData.levelIndex);
        public Level TargetLevel
        {
            get
            {
                Level targetLevel = StatusBMI switch
                {
                    BMI_Status.Lean => (Goal)userData.goalIndex switch
                    {
                        Goal.LoseWeight => ((Level)userData.levelIndex == Level.V) ? Level.V : (Level)(userData.levelIndex + 1),
                        Goal.MoreMuscle => ((Level)userData.levelIndex == Level.V) ? Level.V : (Level)(userData.levelIndex + 1),
                        Goal.KeepShape => (userData.levelIndex > 3) ? (Level)userData.levelIndex : Level.III,
                        _ => Level.III,
                    },
                    BMI_Status.Normal => (Goal)userData.goalIndex switch
                    {
                        Goal.LoseWeight => ((Level)userData.levelIndex == Level.V) ? Level.V : (Level)(userData.levelIndex + 1),
                        Goal.MoreMuscle => (userData.levelIndex > 3) ? Level.V : (Level)(userData.levelIndex + 2),
                        Goal.KeepShape => (userData.levelIndex > 3) ? (Level)userData.levelIndex : Level.III,
                        _ => Level.III,
                    },
                    BMI_Status.Overweight => (Goal)userData.goalIndex switch
                    {
                        Goal.LoseWeight => (userData.levelIndex > 3) ? (Level)userData.levelIndex : Level.III,
                        Goal.MoreMuscle => (userData.levelIndex > 3) ? (Level)userData.levelIndex : Level.IV,
                        Goal.KeepShape => (userData.levelIndex > 3) ? (Level)userData.levelIndex : Level.III,
                        _ => Level.III,
                    },
                    BMI_Status.Fat => (Goal)userData.goalIndex switch
                    {
                        Goal.LoseWeight => (userData.levelIndex > 3) ? (Level)userData.levelIndex : Level.III,
                        Goal.MoreMuscle => (userData.levelIndex > 3) ? (Level)userData.levelIndex : Level.III,
                        Goal.KeepShape => (userData.levelIndex > 3) ? (Level)userData.levelIndex : Level.III,
                        _ => Level.III,
                    },
                    _ => Level.III,
                };

                return targetLevel;
            }
        }

        public BMI_Status StatusBMI => GetStatusBMI(ValueBMI);
        public float TargetBodyFat => GetBodyFat(userData.age, userData.isMale, TargetLevel);
        public float CurrentFat => GetFat(userData.currentWeight, BodyFat);
        public float TargetFat => GetFat(userData.targetWeight, TargetBodyFat);
        public float ToLoseWeight => GetLoseWeight(userData.currentWeight, userData.targetWeight);
        public float ToBurnCalories => ToLoseWeight * 7700f;
        public float IdealBodyFat => GetBodyFat(userData.age, userData.isMale, Level.II);
        public (UserData user, ContentData content) Data => (userData, contentData);
    }
}
