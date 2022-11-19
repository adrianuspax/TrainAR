using System;
using UnityEngine;

public class Calculation : MonoBehaviour
{
    private static
        (
        bool isMale,
        int age,
        float height,
        float currentWeight,
        float targetWeight,
        Goal goal,
        Level level,
        Focus focus
        ) InputedData
    {
        get
        {
            return
                (
                AccountData.IsMale,
                AccountData.Age,
                AccountData.Height,
                AccountData.CurrentWeight,
                AccountData.TargetWeight,
                (Goal)Enum.Parse(typeof(Goal), AccountData.Goal),
                (Level)AccountData.Level,
                (Focus)Enum.Parse(typeof(Focus), AccountData.Focus)
                );
        }
    }

    private static float CalcBMI(float height, float weight)
    {
        float imc, heightInMeters;

        heightInMeters = height / 100f;
        imc = weight / Mathf.Pow(heightInMeters, 2);

        return imc;
    }

    private static StatusBMI SetStatusBMI(float bmi)
    {
        if (bmi < 18.5f)
        {
            return StatusBMI.lean;
        }
        else if (bmi < 25f)
        {
            return StatusBMI.normal;
        }
        else if (bmi < 30f)
        {
            return StatusBMI.overweight;
        }
        else
        {
            return StatusBMI.obese;
        }
    }

    private static float CalcBodyFat(int age, bool isMale, Level level)
    {
        if(age <= 29) // 0 ~ 29
        {
            return level switch
            {
                Level.I => isMale ? 0.23f : 0.31f,
                Level.II => isMale ? 0.22f : 0.30f,
                Level.III => isMale ? 0.17f : 0.24f,
                Level.IV => isMale ? 0.12f : 0.175f,
                Level.V => isMale ? 0.11f : 0.16f,
                _ => isMale ? 0.17f : 0.24f,
            };
        }
        else if(age <= 39) // 30 ~ 39
        {
            return level switch
            {
                Level.I => isMale ? 0.24f : 0.32f,
                Level.II => isMale ? 0.23f : 0.31f,
                Level.III => isMale ? 0.18f : 0.25f,
                Level.IV => isMale ? 0.13f : 0.185f,
                Level.V => isMale ? 0.12f : 0.17f,
                _ => isMale ? 0.18f : 0.25f,
            };
        }
        else if(age <= 49) // 40 ~ 49
        {
            return level switch
            {
                Level.I => isMale ? 0.26f : 0.33f,
                Level.II => isMale ? 0.25f : 0.32f,
                Level.III => isMale ? 0.20f : 0.26f,
                Level.IV => isMale ? 0.15f : 0.195f,
                Level.V => isMale ? 0.14f : 0.18f,
                _ => isMale ? 0.20f : 0.26f,
            };
        }
        else // 50 ~ infinity
        {
            return level switch
            {
                Level.I => isMale ? 0.27f : 0.34f,
                Level.II => isMale ? 0.26f : 0.33f,
                Level.III => isMale ? 0.21f : 0.27f,
                Level.IV => isMale ? 0.16f : 0.205f,
                Level.V => isMale ? 0.15f : 0.19f,
                _ => isMale ? 0.21f : 0.27f,
            };
        }
    }

    private static float CalcFat(float weight, float bodyFat) => weight * bodyFat;
    private static float CalcLoseFat(float currentFat, float targetFat) => currentFat - targetFat;
    public static float BMI => CalcBMI(InputedData.height, InputedData.currentWeight);
    public static float BodyFat => CalcBodyFat(InputedData.age, InputedData.isMale, InputedData.level);
    public static Level TargetLevel
    {
        get
        {
            Level targetLevel = StatusBMI switch
            {
                StatusBMI.lean => InputedData.goal switch
                {
                    Goal.loseWeight => (InputedData.level == Level.V) ? Level.V : (Level)((int)InputedData.level + 1),
                    Goal.moreMuscles => (InputedData.level == Level.V) ? Level.V : (Level)((int)InputedData.level + 1),
                    Goal.keepShape => ((int)InputedData.level > 3) ? InputedData.level : Level.III,
                    _ => Level.III,
                },
                StatusBMI.normal => InputedData.goal switch
                {
                    Goal.loseWeight => (InputedData.level == Level.V) ? Level.V : (Level)((int)InputedData.level + 1),
                    Goal.moreMuscles => ((int)InputedData.level > 3) ? Level.V : (Level)((int)InputedData.level + 2),
                    Goal.keepShape => ((int)InputedData.level > 3) ? InputedData.level : Level.III,
                    _ => Level.III,
                },
                StatusBMI.overweight => InputedData.goal switch
                {
                    Goal.loseWeight => ((int)InputedData.level > 3) ? InputedData.level : Level.III,
                    Goal.moreMuscles => ((int)InputedData.level > 3) ? InputedData.level : Level.IV,
                    Goal.keepShape => ((int)InputedData.level > 3) ? InputedData.level : Level.III,
                    _ => Level.III,
                },
                StatusBMI.obese => InputedData.goal switch
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
    public static StatusBMI StatusBMI => SetStatusBMI(BMI);
    public static float TargetBodyFat => CalcBodyFat(InputedData.age, InputedData.isMale, TargetLevel);
    public static float CurrentFat => CalcFat(InputedData.currentWeight, BodyFat);
    public static float TargetFat => CalcFat(InputedData.targetWeight, TargetBodyFat);
    public static float ToLoseFat => CalcLoseFat(CurrentFat, TargetFat);
    public static float ToBurnCalories => ToLoseFat * 7000;

    public static int IndexGoal => (int)InputedData.goal;
    public static int IndexFocus => (int)InputedData.focus;
    public static int IndexLevel => (int)InputedData.level;
}
