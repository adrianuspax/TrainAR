using TreinAR.MainScenes;
using TreinAR.DataManager.Pages;
using UnityEngine;

namespace TreinAR
{
    public static class Calculation
    {
        public enum BMI_Status
        {
            lean = 0,
            normal = 1,
            overweight = 2,
            obese = 3
        };

        public enum Goal
        {
            loseWeight = 0,
            moreMuscles = 1,
            keepShape = 2
        };

        public enum Level
        {
            I = 1,
            II = 2,
            III = 3,
            IV = 4,
            V = 5
        };

        private static float GetBMI(float height, float weight)
        {
            float bmi, heightInMeters;
            heightInMeters = height * 0.01f;
            bmi = weight / Mathf.Pow(heightInMeters, 2);
            return bmi;
        }

        private static BMI_Status GetStatusBMI(float bmi)
        {
            return bmi switch
            {
                < 18.5f => BMI_Status.lean,
                < 25f => BMI_Status.normal,
                < 30f => BMI_Status.overweight,
                _ => BMI_Status.obese,
            };
        }

        private static float GetBodyFat(int age, bool isMale, Level currentLevel)
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

        private static float GetFat(float weight, float bodyFat) => weight * bodyFat;
        private static float GetLoseWeight(float currentWeight, float targetWeight) => currentWeight - targetWeight;
        public static float ValueBMI => GetBMI(InputedData.height, InputedData.currentWeight);
        public static float BodyFat => GetBodyFat(InputedData.age, InputedData.isMale, InputedData.level);
        public static Level TargetLevel
        {
            get
            {
                Level targetLevel = StatusBMI switch
                {
                    BMI_Status.lean => InputedData.goal switch
                    {
                        Goal.loseWeight => (InputedData.level == Level.V) ? Level.V : (Level)((int)InputedData.level + 1),
                        Goal.moreMuscles => (InputedData.level == Level.V) ? Level.V : (Level)((int)InputedData.level + 1),
                        Goal.keepShape => ((int)InputedData.level > 3) ? InputedData.level : Level.III,
                        _ => Level.III,
                    },
                    BMI_Status.normal => InputedData.goal switch
                    {
                        Goal.loseWeight => (InputedData.level == Level.V) ? Level.V : (Level)((int)InputedData.level + 1),
                        Goal.moreMuscles => ((int)InputedData.level > 3) ? Level.V : (Level)((int)InputedData.level + 2),
                        Goal.keepShape => ((int)InputedData.level > 3) ? InputedData.level : Level.III,
                        _ => Level.III,
                    },
                    BMI_Status.overweight => InputedData.goal switch
                    {
                        Goal.loseWeight => ((int)InputedData.level > 3) ? InputedData.level : Level.III,
                        Goal.moreMuscles => ((int)InputedData.level > 3) ? InputedData.level : Level.IV,
                        Goal.keepShape => ((int)InputedData.level > 3) ? InputedData.level : Level.III,
                        _ => Level.III,
                    },
                    BMI_Status.obese => InputedData.goal switch
                    {
                        Goal.loseWeight => ((int)InputedData.level > 3) ? InputedData.level : Level.III,
                        Goal.moreMuscles => ((int)InputedData.level > 3) ? InputedData.level : Level.III,
                        Goal.keepShape => ((int)InputedData.level > 3) ? InputedData.level : Level.III,
                        _ => Level.III,
                    },
                    _ => Level.III,
                };

                return targetLevel;
            }
        }

        public static BMI_Status StatusBMI => GetStatusBMI(ValueBMI);
        public static float TargetBodyFat => GetBodyFat(InputedData.age, InputedData.isMale, TargetLevel);
        public static float CurrentFat => GetFat(InputedData.currentWeight, BodyFat);
        public static float TargetFat => GetFat(InputedData.targetWeight, TargetBodyFat);
        public static float ToLoseWeight => GetLoseWeight(InputedData.currentWeight, InputedData.targetWeight);
        public static float ToBurnCalories => ToLoseWeight * 7700f;

        public static int IndexGoal => (int)InputedData.goal;
        public static int IndexFocus => (int)InputedData.focus;
        public static int IndexLevel => (int)InputedData.level;

        public static float IdealBodyFat => GetBodyFat(InputedData.age, InputedData.isMale, Level.II);

        private static (bool isMale,
                        int age,
                        float height,
                        float currentWeight,
                        float targetWeight,
                        Goal goal,
                        Level level,
                        PanelCoach.Focus focus)
        InputedData
        {
            get => (PagesControl.UserData.isMale,
                    PagesControl.UserData.age,
                    PagesControl.UserData.height,
                    PagesControl.UserData.currentWeight,
                    PagesControl.UserData.goalWeight,
                    (Goal)PagesControl.UserData.goalIndex,
                    (Level)PagesControl.UserData.levelIndex,
                    (PanelCoach.Focus)PagesControl.UserData.focusIndex);
        }
    }
}
