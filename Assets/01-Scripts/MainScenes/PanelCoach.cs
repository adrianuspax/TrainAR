using ASP;
using ASP.Extensions;
using TMPro;
using TreinAR.UserData.Pages;
using UnityEngine;
using UnityEngine.UI;
using Debug = ASP.Debug;

namespace TreinAR.MainScenes
{
    public class PanelCoach : MonoBehaviour
    {
        public enum Focus
        {
            chest = 0, //1
            abdomen = 1, //0
            legs = 2, //3
            arms = 3 //2
        }

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

        [Header(Header.scripts, order = 0)]
        [SerializeField, ReadOnly] private AugmentedReality AR;
        [SerializeField, ReadOnly] private TrainingSystem trainingSystem;
        [SerializeField, ReadOnly] private ButtonsFloat floatMenu;
        [SerializeField, ReadOnly] private ButtonsMain buttonsMain;
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

            Debug.Log("Index Focus: " + Calculation.IndexFocus);
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

        private void RecomendationsActivations(Focus focus)
        {
            bool linesToActivate;

            for (int i = 0; i < recomendation.Length; i++)
            {
                switch (focus)
                {
                    case Focus.chest:
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
                    case Focus.abdomen:
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
                    case Focus.legs:
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
                    case Focus.arms:
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

        private (string completeName,
                 string age,
                 string gender,
                 string weight,
                 string height,
                 string level)
        DataInputed
        {
            get
            {
                return (PagesControl.UserData.name,
                        PagesControl.UserData.age.ToString() + " " /*+ LanguageManager.valueUnits[0]*/,
                        /*AccountData.IsMale ? LanguageManager.male : LanguageManager.female*/ default,
                        PagesControl.UserData.currentWeight.ToString() + " kg",
                        PagesControl.UserData.height.ToString() + " cm",
                        /*LanguageManager.levelPhrase[Calculation.IndexLevel]*/ default);
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
                return ($"{Calculation.ValueBMI:F2} kg/m²",
                        /*LanguageManager.statusBMI[(int)Calculation.StatusBMI]*/ default,
                        $"{Calculation.BodyFat * 100f:F2}%",
                        $"{Calculation.CurrentFat:F2} kg");
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
                return (/*LanguageManager.goalPhrase[Calculation.IndexGoal]*/ default,
                        /*LanguageManager.levelPhrase[((int)Calculation.TargetLevel - 1)]*/ default,
                        PagesControl.UserData.goalWeight.ToString() + " kg",
                       (Calculation.TargetBodyFat * 100f).ToString("F2") + "%",
                        toLoseFatTMP.text = Calculation.ToLoseWeight.ToString("F2") + " kg",
                        toBurnCalTMP.text = (Calculation.ToBurnCalories * 0.0001f).ToString("F2") + " kcal");
            }
        }
    }
}
