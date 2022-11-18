using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Tutorials.Core.Editor;
using System.Runtime.Serialization;

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

    [Header("Pivot Rotation Canvas Coach", order = 0)]
    [SerializeField] private Transform pivotPanelCoach;
    [SerializeField, ReadOnly] private Transform objectReference;
    [SerializeField, ReadOnly] private Transform pivotCorrection;

    /*[Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("Variables", order = 2)]
    [ReadOnly] public bool panelCoachIsOpen;*/

    private void Awake()
    {
        nameTMP.text = AccountData.CompleteName;
        ageTMP.text = AccountData.Age.ToString(); //+ " " + LanguageManager.valueUnits[0];
        genderTMP.text = AccountData.IsMale ? LanguageManager.male : LanguageManager.female;
        weightTMP.text = AccountData.CurrentWeight.ToString() + " kg";
        heightTMP.text = (AccountData.Height * 0.01f).ToString() + " m";
        levelTMP.text = AccountData.LevelPhrase;

        bmiTMP.text = Calculation.BMI.ToString("F2") + " kg/m²";
        statusTMP.text = Calculation.StatusBMI.ToString();
        bodyFatTMP.text = (Calculation.BodyFat * 100f).ToString("F2") + "%";
        totalFatTMP.text = Calculation.CurrentFat.ToString("F2") + " kg" ;

        goalTMP.text = AccountData.Goal;
        targetLevelTMP.text = Calculation.TargetLevel.ToString();
        targetWeightTMP.text = AccountData.TargetWeight.ToString() + " kg";
        targetBodyFatTMP.text = (Calculation.TargetBodyFat * 100f).ToString("F2") + "%";
        toLoseFatTMP.text = Calculation.ToLoseFat.ToString("F2") + " kg";
        toBurnCalTMP.text = (Calculation.ToBurnCalories * 0.0001f).ToString("F2") + " kcal";

        pivotCorrection = pivotPanelCoach.GetChild(0);
        objectReference = GameObject.FindGameObjectWithTag(Tag.Coach).transform;
        pivotCorrection.transform.localRotation =   Quaternion.Euler( new Vector3(0, -180, 0));
        pivotCorrection.transform.localPosition = Vector3.up;
    }

    private void Update()
    {
        pivotPanelCoach.transform.position = Vector3.Lerp(pivotPanelCoach.transform.position, objectReference.position, 5 * Time.deltaTime);
        pivotPanelCoach.LookAt(Camera.main.transform);
    }

    public void PanelCoachBehaviour(bool isOpen)
    {
        panelCoachAniamtor.Play(isOpen ? panelCoachStartClip.name : panelCoachEndClip.name);
    }
}
