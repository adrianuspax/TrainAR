using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelResult : MonoBehaviour
{
    [Header("Button", order = 0)]
    [SerializeField] private Button panelButton;

    [Header("Animation", order = 0)]
    [SerializeField] private Animator panelResultAnimator;
    [SerializeField] private AnimationClip panelResultFadeInClip;
    [SerializeField] private AnimationClip panelResultFadeOutClip;

    [Header("TMPro", order = 0)]
    [SerializeField] private TextMeshProUGUI classTMP;

    [Header("Scripts", order = 0)]
    [SerializeField] private UnityEngine.UI.Extensions.RadarPolygon RadarChart;

    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("Values", order = 2)]
    [SerializeField, ReadOnly, Range(0f, 1f)] private float bmi;
    [SerializeField, ReadOnly, Range(0f, 1f)] private float currentLevel;
    [SerializeField, ReadOnly, Range(0f, 1f)] private float bodyFat;
    [SerializeField, ReadOnly, Range(0f, 1f)] private float biotype;
    [SerializeField, ReadOnly, Range(0f, 1f)] private float burnedCal;
    [Space(-10, order = 0)]
    [Header("Variables", order = 1)]
    [SerializeField, ReadOnly] private bool resultsIsOpen;

    private const int LEVEL = 0;
    private const int BMI = 1;
    private const int KCAL = 2;
    private const int BIO = 3;
    private const int BFAT = 4;

    private void Awake()
    {
        panelButton.onClick.AddListener(() => PanelResultBehaviour());
    }

    private void Start()
    {
        bmi = RangingBMI(Calculation.BMI);
        currentLevel = RagingLevel(Calculation.IndexLevel);
        bodyFat = RangingBodyFat(Calculation.BodyFat);
        biotype = RangingBiotype(Calculation.StatusBMI);
        burnedCal = RagingBurnedCal(Calculation.ToBurnCalories * 0.25f); //Falta input

        RadarChart.Value[BMI] = bmi;
        RadarChart.Value[LEVEL] = currentLevel;
        RadarChart.Value[BFAT] = bodyFat;
        RadarChart.Value[BIO] = biotype;
        RadarChart.Value[KCAL] = burnedCal;

        classTMP.text = CalcResultClass;
    }

    public void PanelResultBehaviour()
    {
        resultsIsOpen = !resultsIsOpen;

        panelResultAnimator.Play(resultsIsOpen ? panelResultFadeInClip.name : panelResultFadeOutClip.name);
    }

    public string CalcResultClass
    {
        get
        {
            const float v = 1f / 6f;

            float average;
                  average = (bmi + currentLevel + bodyFat + biotype + burnedCal) / 5f;

            return average switch
            {
                <= v => "E",
                <= 2 * v => "D",
                <= 3 * v => "C",
                <= 4 * v => "B",
                <= 5 * v => "A",
                _ => "S"
            };
        }
    }

    public float RangingBMI(float bmi) => (((-2f * bmi) + 60f) / 23f);
    public float RagingLevel(int level) => (level - 1f) * 0.25f;
    public float RangingBodyFat(float bodyFat) => (Mathf.Pow(bodyFat, 2) / (Mathf.Pow(Calculation.IdealBodyFat, 2) - Calculation.IdealBodyFat)) + ((2 * bodyFat) / (1 - Calculation.IdealBodyFat));
    public float RangingBiotype(StatusBMI status) => (1 - ((int)status / 3f));
    public float RagingBurnedCal(float burnedCal) => burnedCal / Calculation.ToBurnCalories;
}
