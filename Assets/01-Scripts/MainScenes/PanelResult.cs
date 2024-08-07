using ASP;
using ASP.Extensions;
using TMPro;
using TreinAR.DataManager;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace TreinAR.MainScenes
{
    using Calculation = DataManager.Calculation;

    public class PanelResult : MonoBehaviour
    {
        [Header(Header.READONLY, order = 0)]
        [Space(-10, order = 1)]
        [Header(Header.components, order = 2)]
        [SerializeField, ReadOnly] private Animator animator;
        [SerializeField, ReadOnly] private Button button;
        [SerializeField, ReadOnly] private TextMeshProUGUI classTMP;
        [Space(20, order = 0)]
        [SerializeField, ReadOnly] private bool isOpen;
        [Space(-10, order = 0)]
        [Header(Header.variables, order = 1)]
        [Space(10, order = 2)]
        [SerializeField, ReadOnly, Range(0f, 1f, order = 3)] private float bmi;
        [SerializeField, ReadOnly, Range(0f, 1f, order = 4)] private float currentLevel;
        [SerializeField, ReadOnly, Range(0f, 1f, order = 5)] private float bodyFat;
        [SerializeField, ReadOnly, Range(0f, 1f, order = 6)] private float biotype;
        [SerializeField, ReadOnly, Range(0f, 1f, order = 7)] private float burnedCal;

        [Space(-10, order = 0)]
        [Header(Header.scripts, order = 1)]
        [SerializeField, ReadOnly] private RadarPolygon radarPolygon;
        [SerializeField, ReadOnly] private Calculation calculation;

        private const int LVL = 0;
        private const int BMI = 1;
        private const int KCAL = 2;
        private const int BIO = 3;
        private const int BFAT = 4;

        public const string PARAMETER = "isOpen";

        private void Awake()
        {
            ComponentsAssignment();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(() => PanelResultBehaviour(false));
        }

        private void Start()
        {
            if (ContentData.TryLoad(out ContentData contentData) && UserData.TryLoad(out UserData userData))
                calculation = new(userData, contentData);

            bmi = RangingBMI(calculation.ValueBMI);
            currentLevel = RangingLevel(calculation.Data.user.levelIndex);
            bodyFat = RangingBodyFat(calculation.BodyFat);
            biotype = RangingBiotype(calculation.StatusBMI);
            burnedCal = RangingBurnedCal(calculation.ToBurnCalories * 0.25f); //Falta input

            radarPolygon.Value[BMI] = bmi;
            radarPolygon.Value[LVL] = currentLevel;
            radarPolygon.Value[BFAT] = bodyFat;
            radarPolygon.Value[BIO] = biotype;
            radarPolygon.Value[KCAL] = burnedCal;

            classTMP.text = GetResultClass();
        }
        /// <summary>
        /// Assignment of components and variables
        /// </summary>
        [ContextMenu("Components Assignment")]
        private void ComponentsAssignment()
        {
            this.GetComponentInChildrenIfNull(ref animator);
            this.GetComponentInChildrenIfNull(ref button);
            button.GetComponentIfNull(ref classTMP, 2, 0, 1);
            button.GetComponentInChildrenIfNull(ref radarPolygon);
            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }

        public void PanelResultBehaviour()
        {
            PanelResultBehaviour(isOpen = !isOpen);
        }

        public void PanelResultBehaviour(bool isOpen)
        {
            animator.SetBool(PARAMETER, isOpen);
            isOpen.ComparativeAssignment(ref this.isOpen);
        }

        public string GetResultClass()
        {
            const float value = 1f / 6f;
            float average = (bmi + currentLevel + bodyFat + biotype + burnedCal) / 5f;

            return average switch
            {
                <= value => "E",
                <= 2 * value => "D",
                <= 3 * value => "C",
                <= 4 * value => "B",
                <= 5 * value => "A",
                _ => "S"
            };
        }

        public float RangingBMI(float bmi)
        {
            return ((-2f * bmi) + 60f) / 23f;
        }

        public float RangingLevel(int level)
        {
            return (level - 1f) * 0.25f;
        }

        public float RangingBodyFat(float bodyFat)
        {
            float bodyFatPow2, idealFatPow2;
            bodyFatPow2 = Mathf.Pow(bodyFat, 2);
            idealFatPow2 = Mathf.Pow(calculation.IdealBodyFat, 2);
            return (bodyFatPow2 / (idealFatPow2 - calculation.IdealBodyFat)) + (2f * bodyFat / (1f - calculation.IdealBodyFat));
        }

        public float RangingBiotype(Calculation.BMI_Status status)
        {
            return 1 - ((int)status / 3f);
        }

        public float RangingBurnedCal(float burnedCal)
        {
            return burnedCal / calculation.ToBurnCalories;
        }
    }
}

