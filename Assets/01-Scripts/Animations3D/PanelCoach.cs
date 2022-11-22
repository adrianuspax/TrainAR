using UnityEngine;
using TMPro;

public class PanelCoach : MonoBehaviour
{
    [Header("Animation", order = 0)]
    [SerializeField] private Animator panelCoachAniamtor;
    [SerializeField] private AnimationClip panelCoachStartClip;
    [SerializeField] private AnimationClip panelCoachEndClip;

    [Header("Informations", order = 0)]
    [Space(-10, order = 1)]
    [Header("Your Data", order = 2)]
    [SerializeField] private TextMeshProUGUI nameTMP;
    [SerializeField] private TextMeshProUGUI ageTMP;
    [SerializeField] private TextMeshProUGUI genderTMP;
    [SerializeField] private TextMeshProUGUI weightTMP;
    [SerializeField] private TextMeshProUGUI heightTMP;
    [SerializeField] private TextMeshProUGUI levelTMP;
    [Space(-10, order = 0)]
    [Header("Calculation", order = 1)]
    [SerializeField] private TextMeshProUGUI bmiTMP;
    [SerializeField] private TextMeshProUGUI statusTMP;
    [SerializeField] private TextMeshProUGUI bodyFatTMP;
    [SerializeField] private TextMeshProUGUI totalFatTMP;
    [Space(-10, order = 0)]
    [Header("Target", order = 1)]
    [SerializeField] private TextMeshProUGUI goalTMP;
    [SerializeField] private TextMeshProUGUI targetLevelTMP;
    [SerializeField] private TextMeshProUGUI targetWeightTMP;
    [SerializeField] private TextMeshProUGUI targetBodyFatTMP;
    [SerializeField] private TextMeshProUGUI toLoseFatTMP;
    [SerializeField] private TextMeshProUGUI toBurnCalTMP;

    [Header("To Translate", order = 0)]
    [Space(-10, order = 1)]
    [Header("Your Data", order = 2)]
    [SerializeField] private TextMeshProUGUI yourDataTMP;
    [SerializeField] private TextMeshProUGUI nameTitleTMP;
    [SerializeField] private TextMeshProUGUI ageTitleTMP;
    [SerializeField] private TextMeshProUGUI genderTitleTMP;
    [SerializeField] private TextMeshProUGUI weightTitleTMP;
    [SerializeField] private TextMeshProUGUI heightTitleTMP;
    [SerializeField] private TextMeshProUGUI levelTitleTMP;
    [Space(-10, order = 0)]
    [Header("Calculation", order = 1)]
    [SerializeField] private TextMeshProUGUI calculationTMP;
    [SerializeField] private TextMeshProUGUI bmiTitleTMP;
    [SerializeField] private TextMeshProUGUI statusTitleTMP;
    [SerializeField] private TextMeshProUGUI bodyTitleFatTMP;
    [SerializeField] private TextMeshProUGUI totalTitleFatTMP;
    [Space(-10, order = 0)]
    [Header("Target", order = 1)]
    [SerializeField] private TextMeshProUGUI targetTMP;
    [SerializeField] private TextMeshProUGUI goalTitleTMP;
    [SerializeField] private TextMeshProUGUI targetTitleLevelTMP;
    [SerializeField] private TextMeshProUGUI targetTitleWeightTMP;
    [SerializeField] private TextMeshProUGUI targetTitleBodyFatTMP;
    [SerializeField] private TextMeshProUGUI toLoseTitleFatTMP;
    [SerializeField] private TextMeshProUGUI toBurnTitleCalTMP;

    [Header("Pivot Rotation Canvas Coach", order = 0)]
    [SerializeField] private Transform pivotPanelCoach;
    [SerializeField, ReadOnly] private Transform objectReference;
    [SerializeField, ReadOnly] private Transform pivotCorrection;

    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("Scripts", order = 2)]
    [SerializeField, ReadOnly] private AugmentedReality AR;

    private void Awake()
    {
        AR = this.GetComponent<AugmentedReality>();
        pivotCorrection = pivotPanelCoach.GetChild(0);
        objectReference = GameObject.FindGameObjectWithTag(Tag.Coach).transform;
    }

    private void Start()
    {
        pivotCorrection.transform.localRotation = Quaternion.Euler(new Vector3(0, -180, 0));
        pivotCorrection.transform.localPosition = Vector3.up;

        yourDataTMP.text = LanguageManager.yourData;
        nameTitleTMP.text = LanguageManager._name;
        ageTitleTMP.text = LanguageManager.age;
        genderTitleTMP.text = LanguageManager.gender;
        weightTitleTMP.text = LanguageManager.weight;
        heightTitleTMP.text = LanguageManager.height;
        levelTitleTMP.text = LanguageManager.level;

        calculationTMP.text = LanguageManager.calculation;
        bodyTitleFatTMP.text = LanguageManager.bodyFat;
        totalTitleFatTMP.text = LanguageManager.totalFat;

        targetTMP.text = LanguageManager.target;
        goalTitleTMP.text = LanguageManager.goal;
        targetTitleLevelTMP.text = LanguageManager.level;
        targetTitleWeightTMP.text = LanguageManager.weight;
        targetTitleBodyFatTMP.text = LanguageManager.bodyFat;
        toLoseTitleFatTMP.text = LanguageManager.toLoseFat;
        toBurnTitleCalTMP.text = LanguageManager.toBurnCal;

        nameTMP.text = DataInputed.completeName;
        ageTMP.text = DataInputed.age;
        genderTMP.text = DataInputed.gender;
        weightTMP.text = DataInputed.weight;
        heightTMP.text = DataInputed.height;
        levelTMP.text = DataInputed.level;
        bmiTMP.text = Calc.bmi;
        statusTMP.text = Calc.statusBMI;
        bodyFatTMP.text = Calc.bodyFat;
        totalFatTMP.text = Calc.totalFat;
        goalTMP.text = Target.goal;
        targetLevelTMP.text = Target.level;
        targetWeightTMP.text = Target.weight;
        targetBodyFatTMP.text = Target.BodyFat;
        toLoseFatTMP.text = Target.toLoseFat;
        toBurnCalTMP.text = Target.toBurnCal;

        Debug.Log
            ("Name: " + nameTMP.text + "\n" +
            "Age: " + ageTMP.text + "\n" +
            "Gender: " + genderTMP.text + "\n" +
            "Weight: " + weightTMP.text + "\n" +
            "Height: " + heightTMP.text + "\n" +
            "Level: " + levelTMP.text + " || Index Level: " + Calculation.IndexLevel + "\n" +
            "BMI: " + bmiTMP.text + "\n" +
            "Status: " + statusTMP.text + "\n" +
            "Body Fat: " + bodyFatTMP.text + "\n" +
            "Total Fat: " + totalFatTMP.text + "\n" +
            "Goal: " + goalTMP.text + " || Index Goal: " + Calculation.IndexGoal + "\n" +
            "Target Level: " + targetLevelTMP.text + "\n" +
            "Target Weight: " + targetWeightTMP.text + "\n" +
            "Target Body Fat: " + targetBodyFatTMP.text + "\n" +
            "To Lose Weight: " + toLoseFatTMP.text + "\n" +
            "To Burn Cal: " + toBurnCalTMP.text + "\n");

        Debug.Log ("Index Focus: " + Calculation.IndexFocus);
    }

    private void Update()
    {
        if (AR.CoachIsActivated)
        {
            pivotPanelCoach.transform.position = Vector3.Lerp(pivotPanelCoach.transform.position, objectReference.position, 5 * Time.deltaTime);
            pivotPanelCoach.LookAt(Camera.main.transform);
        }
    }

    public void PanelCoachBehaviour(bool isOpen)
    {
        panelCoachAniamtor.Play(isOpen ? panelCoachStartClip.name : panelCoachEndClip.name);
    }

    private
        (
        string completeName,
        string age,
        string gender,
        string weight,
        string height,
        string level
        ) DataInputed
    {
        get
        {
            return
                (
                AccountData.CompleteName,
                AccountData.Age.ToString() + " " + LanguageManager.valueUnits[0],
                AccountData.IsMale ? LanguageManager.male : LanguageManager.female,
                AccountData.CurrentWeight.ToString() + " kg",
                (AccountData.Height).ToString() + " cm",
                LanguageManager.levelPhrase[(int)Calculation.IndexLevel]
                );
        }
    }

    private 
        (
        string bmi,
        string statusBMI,
        string bodyFat,
        string totalFat
        ) Calc
    {
        get
        {
            return
                (
                Calculation.BMI.ToString("F2") + " kg/m²",
                LanguageManager.statusBMI[(int)Calculation.StatusBMI],
               (Calculation.BodyFat * 100f).ToString("F2") + "%",
                Calculation.CurrentFat.ToString("F2") + " kg"
                );
        }
    }

    private
        (
        string goal,
        string level,
        string weight,
        string BodyFat,
        string toLoseFat,
        string toBurnCal
        ) Target
    {
        get
        {
            return
                (
                LanguageManager.goalPhrase[Calculation.IndexGoal],
                LanguageManager.levelPhrase[((int)Calculation.TargetLevel - 1)],
                AccountData.TargetWeight.ToString() + " kg",
                (Calculation.TargetBodyFat * 100f).ToString("F2") + "%",
                toLoseFatTMP.text = Calculation.ToLoseWeight.ToString("F2") + " kg",
                toBurnCalTMP.text = (Calculation.ToBurnCalories * 0.0001f).ToString("F2") + " kcal"
                );
        }
    }
}
