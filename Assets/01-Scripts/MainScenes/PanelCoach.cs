using ASP;
using ASP.Extensions;
using System;
using TMPro;
using TreinAR.DataManager;
using UnityEngine;
using UnityEngine.UI;
using Debug = ASP.Debug;

namespace TreinAR.MainScenes
{
    using Calculation = DataManager.Calculation;

    public class PanelCoach : MonoBehaviour
    {
        [Header(Header.READONLY, order = 0)]
        [Space(-10, order = 1)]
        [Header(Header.components, order = 2)]
        [SerializeField, ReadOnly] private Animator animator;
        [SerializeField, ReadOnly] private ScrollRect scrollView;
        [SerializeField, ReadOnly] private RectTransform canvasRectTransform;
        [SerializeField, ReadOnly] private Transform pivotCorrection;
        [SerializeField, ReadOnly] private Transform pivotPanelCoach;
        [SerializeField, ReadOnly] private Transform objectReference;
        [SerializeField, ReadOnly] private RectMask2D rectMask2D;
        [SerializeField, ReadOnly, NonReorderable] private GameObject[] recomendation;
        [SerializeField, ReadOnly, NonReorderable] private Button[] contentButtons;
        [Space(-10, order = 0)]
        [Header(Header.TMPro, order = 1)]
        [Space(-10, order = 2)]
        [Header("User Data", order = 3)]
        [SerializeField, ReadOnly] private TextMeshProUGUI nameTMP;
        [SerializeField, ReadOnly] private TextMeshProUGUI ageTMP;
        [SerializeField, ReadOnly] private TextMeshProUGUI genderTMP;
        [SerializeField, ReadOnly] private TextMeshProUGUI weightTMP;
        [SerializeField, ReadOnly] private TextMeshProUGUI heightTMP;
        [SerializeField, ReadOnly] private TextMeshProUGUI levelTMP;
        [Space(-10, order = 0)]
        [Header("Calculation", order = 1)]
        [SerializeField, ReadOnly] private TextMeshProUGUI bmiTMP;
        [SerializeField, ReadOnly] private TextMeshProUGUI statusTMP;
        [SerializeField, ReadOnly] private TextMeshProUGUI bodyFatTMP;
        [SerializeField, ReadOnly] private TextMeshProUGUI totalFatTMP;
        [Space(-10, order = 0)]
        [Header("Target", order = 1)]
        [SerializeField, ReadOnly] private TextMeshProUGUI goalTMP;
        [SerializeField, ReadOnly] private TextMeshProUGUI targetLevelTMP;
        [SerializeField, ReadOnly] private TextMeshProUGUI targetWeightTMP;
        [SerializeField, ReadOnly] private TextMeshProUGUI targetBodyFatTMP;
        [SerializeField, ReadOnly] private TextMeshProUGUI toLoseFatTMP;
        [SerializeField, ReadOnly] private TextMeshProUGUI toBurnCalTMP;
        [Space(-10, order = 0)]
        [Header(Header.variables, order = 1)]
        [SerializeField, ReadOnly] private Vector3 vectorCanvasPos;
        [SerializeField, ReadOnly] private float distance;
        [SerializeField, NonReorderable, ReadOnly] private string[] statusBMI;
        [SerializeField, NonReorderable, ReadOnly] private string[] levelPhrase;
        [SerializeField, NonReorderable, ReadOnly] private string[] goalPhrase;

        [Header(Header.scripts, order = 0)]
        [SerializeField, ReadOnly] private AugmentedReality AR;
        [SerializeField, ReadOnly] private TrainingSystem trainingSystem;
        [SerializeField, ReadOnly] private ButtonsFloat floatMenu;
        [SerializeField, ReadOnly] private ButtonsMain buttonsMain;
        [SerializeField, ReadOnly] private Calculation calculation;
        [SerializeField, NonReorderable, ReadOnly] private InfoData[] infoDatas;

        public const string PARAMETER = "isOpen";

        private void Awake()
        {
            ComponentsAssignment();
        }

        private void Start()
        {
            rectMask2D.enabled = true;

            ProvisionalRecomendationsActivations();

            Quaternion localRotation = Quaternion.Euler(new Vector3(0f, -180f, 0f));
            pivotCorrection.SetLocalPositionAndRotation(Vector3.up, localRotation);
            int length;
            length =  Enum.GetValues(typeof(Calculation.BMI_Status)).Length;
            statusBMI = new string[length];

            for (int i = 0; i < length; i++)
            {
                int index = i;
                LocalizationManager.SetLocalizationString(((Calculation.BMI_Status)index).ToString(), "StringTableMain", (result) => statusBMI[index] = result);
            }

            length = Enum.GetValues(typeof(Calculation.Level)).Length;
            levelPhrase = new string[length];
            string[] entry = new string[] { "Sedentary", "Walker", "Runner", "Athletic", "BodyBuilder"};


            for (int i = 0; i < length; i++)
            {
                int index = i;
                LocalizationManager.SetLocalizationString(entry[index], "StringTableMain", (result) => levelPhrase[index] = result);
            }

            length = Enum.GetValues(typeof(Calculation.Goal)).Length;
            goalPhrase = new string[length];

            for (int i = 0; i < length; i++)
            {
                int index = i;
                LocalizationManager.SetLocalizationString(((Calculation.Goal)index).ToString(), "StringTableMain", (result) => goalPhrase[index] = result);
            }

            if (ContentData.TryLoad(out ContentData contentData) && UserData.TryLoad(out UserData userData))
                calculation = new(userData, contentData);

            nameTMP.text = contentData.name;
            ageTMP.text = contentData.age;
            genderTMP.text = contentData.gender;
            weightTMP.text = contentData.currentWeight;
            heightTMP.text = contentData.height;
            levelTMP.text = contentData.level;
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
                "Level: " + levelTMP.text + " || Index Level: " + calculation.Data.user.levelIndex + "\n" +
                "BMI: " + bmiTMP.text + "\n" +
                "Status: " + statusTMP.text + "\n" +
                "Body Fat: " + bodyFatTMP.text + "\n" +
                "Total Fat: " + totalFatTMP.text + "\n" +
                "Goal: " + goalTMP.text + " || Index Goal: " + calculation.Data.user.goalIndex + "\n" +
                "Target Level: " + targetLevelTMP.text + "\n" +
                "Target Weight: " + targetWeightTMP.text + "\n" +
                "Target Body Fat: " + targetBodyFatTMP.text + "\n" +
                "To Lose Weight: " + toLoseFatTMP.text + "\n" +
                "To Burn Cal: " + toBurnCalTMP.text + "\n");

            Debug.Log("Index Focus: " + calculation.Data.user.focusIndex);
        }

        private void Update()
        {
            if (AR.CoachIsActivated)
            {
                distance = Vector3.Distance(Camera.main.transform.localPosition, pivotPanelCoach.localPosition);

                float scale = (0.0018f * distance + 0.001f);

                vectorCanvasPos = (pivotPanelCoach.position + Camera.main.transform.position) * 0.5f;
                canvasRectTransform.localScale = new Vector3(scale, scale, scale);

                pivotPanelCoach.position = Vector3.Lerp(pivotPanelCoach.position, objectReference.position, 5f * Time.deltaTime);
                pivotPanelCoach.LookAt(Camera.main.transform);
                canvasRectTransform.position = new Vector3(vectorCanvasPos.x, 0f, vectorCanvasPos.z);
            }
        }
        /// <summary>
        /// Assignment of components and variables
        /// </summary>
        [ContextMenu("Components Assignment")]
        private void ComponentsAssignment()
        {
            this.GetComponentIfNull(ref animator, 0);
            animator.GetComponentIfNull(ref scrollView, 0);
            if (canvasRectTransform.IsNull())
                canvasRectTransform = transform.parent.GetComponent<RectTransform>();
            if (pivotCorrection.IsNull())
                pivotCorrection = canvasRectTransform.parent.transform;
            if (pivotPanelCoach.IsNull())
                pivotPanelCoach = pivotCorrection.parent.transform;
            if (objectReference.IsNull())
                objectReference = GameObject.FindGameObjectWithTag(Tag.Coach).transform.GetChild(0);
            scrollView.GetComponentIfNull(ref rectMask2D, 0);
            if (AR.IsNull())
                AR = FindAnyObjectByType<AugmentedReality>();
            if (trainingSystem.IsNull())
                trainingSystem = FindAnyObjectByType<TrainingSystem>();
            if (floatMenu.IsNull())
                floatMenu = FindAnyObjectByType<ButtonsFloat>();
            if (buttonsMain.IsNull())
                buttonsMain = FindAnyObjectByType<ButtonsMain>();
            scrollView.GetComponentsInAllChildrenIfNull(ref infoDatas, 0, 0);

            if (recomendation.IsNullOrEmpty())
            {
                recomendation = new GameObject[infoDatas[3].Cells.Length];

                for (int i = 0; i < recomendation.Length; i++)
                {
                    recomendation[i] = infoDatas[3].Cells[i].gameObject;
                }
            }

            if (contentButtons.IsNullOrEmpty())
            {
                contentButtons = new Button[infoDatas.Length];

                for (int i = 0; i < contentButtons.Length; i++)
                {
                    contentButtons[i] = infoDatas[i].Button;
                }
            }

            if (nameTMP.IsNull())
                nameTMP = infoDatas[0].InfoTMPs[0];
            if (ageTMP.IsNull())
                ageTMP = infoDatas[0].InfoTMPs[1];
            if (genderTMP.IsNull())
                genderTMP = infoDatas[0].InfoTMPs[2];
            if (weightTMP.IsNull())
                weightTMP = infoDatas[0].InfoTMPs[3];
            if (heightTMP.IsNull())
                heightTMP = infoDatas[0].InfoTMPs[4];
            if (levelTMP.IsNull())
                levelTMP = infoDatas[0].InfoTMPs[5];
            if (bmiTMP.IsNull())
                bmiTMP = infoDatas[1].InfoTMPs[0];
            if (statusTMP.IsNull())
                statusTMP = infoDatas[1].InfoTMPs[1];
            if (bodyFatTMP.IsNull())
                bodyFatTMP = infoDatas[1].InfoTMPs[2];
            if (totalFatTMP.IsNull())
                totalFatTMP = infoDatas[1].InfoTMPs[3];
            if (goalTMP.IsNull())
                goalTMP = infoDatas[2].InfoTMPs[0];
            if (targetLevelTMP.IsNull())
                targetLevelTMP = infoDatas[2].InfoTMPs[1];
            if (targetWeightTMP.IsNull())
                targetWeightTMP = infoDatas[2].InfoTMPs[2];
            if (targetBodyFatTMP.IsNull())
                targetBodyFatTMP = infoDatas[2].InfoTMPs[3];
            if (toLoseFatTMP.IsNull())
                toLoseFatTMP = infoDatas[2].InfoTMPs[4];
            if (toBurnCalTMP.IsNull())
                toBurnCalTMP = infoDatas[2].InfoTMPs[5];

            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }

        public void PanelCoachBehaviour(bool isOpen)
        {
            animator.SetBool(PARAMETER, isOpen);
        }

        public void ButtonsPanelBehaviour(int index)
        {
            if (index == 3)
                _buttonPlay();
            else
                scrollView.horizontalNormalizedPosition = (index + 1f) / 3f;

            void _buttonPlay()
            {
                buttonsMain.ButtonPanelCoach(true);
                trainingSystem.WorkoutSequence();
                floatMenu.SetBoolFloatAnimators(false);
            }
        }

        private void RecomendationsActivations(Calculation.Focus focus)
        {
            bool linesToActivate;

            for (int i = 0; i < recomendation.Length; i++)
            {
                switch (focus)
                {
                    case Calculation.Focus.chest:
                        linesToActivate =
                            i == 0 ||
                            i == DropDownMenuIndex.BURPEE + 1 ||
                            i == DropDownMenuIndex.BICYCLECRUNCH + 1 ||
                            i == DropDownMenuIndex.CIRCLECRUNCH + 1 ||
                            i == DropDownMenuIndex.PIKEWALK + 1 ||
                            i == DropDownMenuIndex.PUSHUP + 1 ||
                            i == DropDownMenuIndex.SITUPS + 1 ||
                            i == 15;
                        recomendation[i].SetActive(linesToActivate);
                        break;
                    case Calculation.Focus.abdomen:
                        linesToActivate =
                            i == 0 ||
                            i == DropDownMenuIndex.BURPEE + 1 ||
                            i == DropDownMenuIndex.BICYCLECRUNCH + 1 ||
                            i == DropDownMenuIndex.PIKEWALK + 1 ||
                            i == DropDownMenuIndex.PUSHUP + 1 ||
                            i == DropDownMenuIndex.SITUPS + 1 ||
                            i == 15;
                        recomendation[i].SetActive(linesToActivate);
                        break;
                    case Calculation.Focus.legs:
                        linesToActivate =
                            i == 0 ||
                            i == DropDownMenuIndex.SQUAT + 1 ||
                            i == DropDownMenuIndex.SQUATBENTARMS + 1 ||
                            i == DropDownMenuIndex.BOXJUMP + 1 ||
                            i == DropDownMenuIndex.BURPEE + 1 ||
                            i == DropDownMenuIndex.BICYCLECRUNCH + 1 ||
                            i == DropDownMenuIndex.JUMPINGJACKS + 1 ||
                            i == DropDownMenuIndex.PISTOL + 1 ||
                            i == DropDownMenuIndex.PLANK + 1 ||
                            i == DropDownMenuIndex.PUSHUP + 1 ||
                            i == 15;
                        recomendation[i].SetActive(linesToActivate);
                        break;
                    case Calculation.Focus.arms:
                        linesToActivate =
                            i == 0 ||
                            i == DropDownMenuIndex.SQUATBENTARMS + 1 ||
                            i == DropDownMenuIndex.BICEPCURL + 1 ||
                            i == DropDownMenuIndex.BURPEE + 1 ||
                            i == DropDownMenuIndex.JUMPINGJACKS + 1 ||
                            i == DropDownMenuIndex.PLANK + 1 ||
                            i == DropDownMenuIndex.PIKEWALK + 1 ||
                            i == DropDownMenuIndex.PUSHUP + 1 ||
                            i == 15;
                        recomendation[i].SetActive(linesToActivate);
                        break;
                }
            }
        }

        private void ProvisionalRecomendationsActivations()
        {
            bool linesToActivate;

            for (int i = 0; i < recomendation.Length; i++)
            {
                linesToActivate =
                    i == 0 ||
                    i == DropDownMenuIndex.SQUAT + 1 ||
                    i == DropDownMenuIndex.BURPEE + 1 ||
                    i == DropDownMenuIndex.PISTOL + 1 ||
                    i == 15;
                recomendation[i].SetActive(linesToActivate);
            }
        }

        private (string bmi,
                 string statusBMI,
                 string bodyFat,
                 string totalFat)
        Calc
        {
            get
            {
                return ($"{calculation.ValueBMI:F2} kg/m²",
                        statusBMI[(int)calculation.StatusBMI],
                        $"{calculation.BodyFat * 100f:F2}%",
                        $"{calculation.CurrentFat:F2} kg");
            }
        }

        private (string goal,
                 string level,
                 string weight,
                 string BodyFat,
                 string toLoseFat,
                 string toBurnCal)
        Target
        {
            get
            {
                return (calculation.Data.content.goal,
                        levelPhrase[((int)calculation.TargetLevel - 1)],
                        calculation.Data.content.goalWeight,
                       $"{(calculation.TargetBodyFat * 100f):F2}%",
                        toLoseFatTMP.text = $"{ calculation.ToLoseWeight:F2} kg",
                        toBurnCalTMP.text = $"{(calculation.ToBurnCalories * 0.0001f):F2} kcal");
            }
        }
    }
}
