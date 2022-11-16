using UnityEngine;
public class Calculation : MonoBehaviour
{
    public enum Goal
    {
        loseWeight = 0,
        moreMuscles = 1,
        keepShape = 2
    }

    public enum Level
    {
        lvl1 = 1,
        lvl2 = 2,
        lvl3 = 3,
        lvl4 = 4,
        lvl5 = 5
    }

    public enum StatusIMC
    {
        lean = 0,
        normal = 1,
        overweight = 2,
        obese = 3
    }

    [Header("Input", order = 0)]
    [SerializeField] private bool _isMale;
    [SerializeField] private int _age;
    [SerializeField] private float _height;
    [SerializeField] private float _currentWeight;
    [SerializeField] private float _targetWeight;
    [SerializeField] private Goal _goal;
    [SerializeField] private Level _level;

    [Header("Output", order = 0)]
    [Space(-10, order = 1)]
    [Header("Current", order = 2)]
    [SerializeField, ReadOnly] private float imc;
    [SerializeField, ReadOnly] private StatusIMC statusIMC;
    [SerializeField, ReadOnly] private float bodyFat;
    [SerializeField, ReadOnly] private float currentFat;
    [Space(-10, order = 0)]
    [Header("Target", order = 1)]
    [SerializeField, ReadOnly] private Level targetLevel;
    [SerializeField, ReadOnly] private float targetFat;
    [SerializeField, ReadOnly] private float targetBodyFat;
    [SerializeField, ReadOnly] private float loseFat;
    [SerializeField, ReadOnly] private float burnCalories;

    public static bool isMale;
    public static int age;
    public static float height;
    public static float currentWeight;
    public static float targetWeight;
    public static Goal goal;
    public static Level level;

    private void Awake()
    {
        isMale = _isMale;
        age = _age;
        height = _height;
        currentWeight = _currentWeight;
        targetWeight = _targetWeight;
        goal = _goal;
        level = _level;
    }

    private void Start()
    {
        imc = IMC;
        statusIMC = CalcStatusIMC(IMC);
        bodyFat = BodyFat;
        currentFat = CurrentFat;
        targetFat = TargetFat;
        targetBodyFat = TargetBodyFat;
        loseFat = LoseFat;
        burnCalories = BurnCalories;
        targetLevel = TargetLevel;
    }

    private static float CalcIMC(float height, float weight)
    {
        float imc, heightInMeters;

        heightInMeters = height / 100f;
        imc = weight / Mathf.Pow(heightInMeters, 2);

        return imc;
    }

    private static StatusIMC CalcStatusIMC(float imc)
    {
        if (imc < 18.5f)
        {
            return StatusIMC.lean;
        }
        else if (imc < 25f)
        {
            return StatusIMC.normal;
        }
        else if (imc < 30f)
        {
            return StatusIMC.overweight;
        }
        else
        {
            return StatusIMC.obese;
        }
    }

    private static float CalcBodyFat(int age, bool isMale, Level level)
    {
        if(age <= 29) // 0 ~ 29
        {
            return level switch
            {
                Level.lvl1 => isMale ? 0.23f : 0.31f,
                Level.lvl2 => isMale ? 0.22f : 0.30f,
                Level.lvl3 => isMale ? 0.17f : 0.24f,
                Level.lvl4 => isMale ? 0.12f : 0.175f,
                Level.lvl5 => isMale ? 0.11f : 0.16f,
                _ => isMale ? 0.17f : 0.24f,
            };
        }
        else if(age <= 39) // 30 ~ 39
        {
            return level switch
            {
                Level.lvl1 => isMale ? 0.24f : 0.32f,
                Level.lvl2 => isMale ? 0.23f : 0.31f,
                Level.lvl3 => isMale ? 0.18f : 0.25f,
                Level.lvl4 => isMale ? 0.13f : 0.185f,
                Level.lvl5 => isMale ? 0.12f : 0.17f,
                _ => isMale ? 0.18f : 0.25f,
            };
        }
        else if(age <= 49) // 40 ~ 49
        {
            return level switch
            {
                Level.lvl1 => isMale ? 0.26f : 0.33f,
                Level.lvl2 => isMale ? 0.25f : 0.32f,
                Level.lvl3 => isMale ? 0.20f : 0.26f,
                Level.lvl4 => isMale ? 0.15f : 0.195f,
                Level.lvl5 => isMale ? 0.14f : 0.18f,
                _ => isMale ? 0.20f : 0.26f,
            };
        }
        else // 50 ~ infinity
        {
            return level switch
            {
                Level.lvl1 => isMale ? 0.27f : 0.34f,
                Level.lvl2 => isMale ? 0.26f : 0.33f,
                Level.lvl3 => isMale ? 0.21f : 0.27f,
                Level.lvl4 => isMale ? 0.16f : 0.205f,
                Level.lvl5 => isMale ? 0.15f : 0.19f,
                _ => isMale ? 0.21f : 0.27f,
            };
        }
    }

    private static float CalcFat(float weight, float bodyFat)
    {
        return weight * bodyFat;
    }

    private static float CalcLoseFat(float currentFat, float targetFat)
    {
        return currentFat - targetFat;
    }

    public static float IMC => CalcIMC(height, currentWeight);
    public static float BodyFat => CalcBodyFat(age, isMale, level);
    public static Level TargetLevel
    {
        get
        {
            Level targetLevel = StatusBMI switch
            {
                StatusIMC.lean => goal switch
                {
                    Goal.loseWeight => (level == Level.lvl5) ? Level.lvl5 : (Level)((int)level + 1),
                    Goal.moreMuscles => (level == Level.lvl5) ? Level.lvl5 : (Level)((int)level + 1),
                    Goal.keepShape => ((int)level > 3) ? level : Level.lvl3,
                    _ => Level.lvl3,
                },
                StatusIMC.normal => goal switch
                {
                    Goal.loseWeight => (level == Level.lvl5) ? Level.lvl5 : (Level)((int)level + 1),
                    Goal.moreMuscles => ((int)level > 3) ? Level.lvl5 : (Level)((int)level + 2),
                    Goal.keepShape => ((int)level > 3) ? level : Level.lvl3,
                    _ => Level.lvl3,
                },
                StatusIMC.overweight => goal switch
                {
                    Goal.loseWeight => ((int)level > 3) ? level : Level.lvl3,
                    Goal.moreMuscles => ((int)level > 3) ? level : Level.lvl4,
                    Goal.keepShape => ((int)level > 3) ? level : Level.lvl3,
                    _ => Level.lvl3,
                },
                StatusIMC.obese => goal switch
                {
                    Goal.loseWeight => ((int)level > 3) ? level : Level.lvl3,
                    Goal.moreMuscles => ((int)level > 3) ? level : Level.lvl3,
                    Goal.keepShape => ((int)level > 3) ? level : Level.lvl3,
                    _ => Level.lvl3,
                },
                _ => Level.lvl3,
            };

            return targetLevel;
        }
    }

    public static StatusIMC StatusBMI => CalcStatusIMC(IMC);
    public static float TargetBodyFat => CalcBodyFat(age, isMale, TargetLevel);
    public static float CurrentFat => CalcFat(currentWeight, BodyFat);
    public static float TargetFat => CalcFat(targetWeight, TargetBodyFat);
    public static float LoseFat => CalcLoseFat(CurrentFat, TargetFat);
    public static float BurnCalories => LoseFat * 7000;
}
