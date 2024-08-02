using ASP;
using ASP.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace TreinAR.MainScenes
{
    public class ButtonsMain : MonoBehaviour
    {
        public enum StateButtonAR
        {
            ReadyToSet = 0,
            Refresh = 1
        };

        [Header(Header.MANAGEABLE, order = 0)]
        [Space(-10, order = 1)]
        [Header(Header.variables, order = 2)]
        [SerializeField] private Sprite setCoachAR;
        [SerializeField] private Sprite replaceCoachAR;
        [SerializeField] private Sprite openPanel;
        [SerializeField] private Sprite closePanel;

        [Header(Header.READONLY, order = 0)]
        [Space(-10, order = 1)]
        [Header(Header.variables, order = 2)]
        [SerializeField, ReadOnly] private StateButtonAR stateButtonAR;
        [SerializeField, ReadOnly] private bool isPressedButtonBar;
        [Header(Header.buttons, order = 0)]
        [Space(-10, order = 1)]
        [Header("Top Area", order = 2)]
        [SerializeField, ReadOnly] private Button btnTopL;
        [SerializeField, ReadOnly] private Button btnTopR;
        [Space(-10, order = 0)]
        [Header("Top Area", order = 1)]
        [SerializeField, ReadOnly] private Button btnBarL;
        [SerializeField, ReadOnly] private Button btnBarC;
        [SerializeField, ReadOnly] private Button btnBarR;

        [Header(Header.scripts, order = 0)]
        [SerializeField, ReadOnly] private AugmentedReality AR;
        [SerializeField, ReadOnly] private PanelResult panelResult;
        [SerializeField, ReadOnly] private PanelCoach panelCoach;
        [SerializeField, ReadOnly] private AnchorAnimation animationsMain;
        [SerializeField, ReadOnly] private ButtonsFloat floatMenu;
        [SerializeField, ReadOnly] private Coach coachAnimations;

        private void Awake()
        {
            ComponentsAssignment();
        }
        /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        private void OnEnable()
        {
            btnTopR.onClick.AddListener(ButtonsManager.CloseApplication);
            btnTopL.onClick.AddListener(TrainARSceneManager.LoadMenu);
            btnBarC.onClick.AddListener(AR.PlaceCoachAR);
            btnBarL.onClick.AddListener(panelResult.PanelResultBehaviour);
            btnBarR.onClick.AddListener(() => ButtonPanelCoach(isPressedButtonBar = !isPressedButtonBar));
        }
        /// <summary>
        /// Assignment of components and variables
        /// </summary>
        [ContextMenu("Components Assignment")]
        private void ComponentsAssignment()
        {
            if (AR.IsNull())
                AR = FindAnyObjectByType<AugmentedReality>();
            if (panelResult.IsNull())
                panelResult = FindAnyObjectByType<PanelResult>();
            if (panelCoach.IsNull())
                panelCoach = FindAnyObjectByType<PanelCoach>();
            if (animationsMain.IsNull())
                animationsMain = FindAnyObjectByType<AnchorAnimation>();
            if (floatMenu.IsNull())
                floatMenu = FindAnyObjectByType<ButtonsFloat>();
            if (coachAnimations.IsNull())
                coachAnimations = FindAnyObjectByType<Coach>();

            this.GetComponentIfNull(ref btnTopL, 0, 0);
            this.GetComponentIfNull(ref btnTopR, 0, 1);
            this.GetComponentIfNull(ref btnBarL, 1, 0);
            this.GetComponentIfNull(ref btnBarC, 1, 1);
            this.GetComponentIfNull(ref btnBarR, 1, 2);

            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }

        public void ButtonARBehaviour(StateButtonAR state, bool isInteractable)
        {
            bool condition;

            Sprite sprite = state switch
            {
                StateButtonAR.ReadyToSet => setCoachAR,
                StateButtonAR.Refresh => replaceCoachAR,
                _ => default,
            };

            btnBarC.image.sprite = sprite;

            condition = (CurrentStateButtonAR == StateButtonAR.Refresh) || isInteractable;
            ButtonSetCoachInteraction = condition && !coachAnimations.HasTriggered;

            if (ButtonSetCoachInteraction)
            {
                btnBarC.image.rectTransform.localScale = Vector3.one;
                btnBarC.image.rectTransform.localRotation = Quaternion.Euler(Vector3.zero);
                btnBarC.image.rectTransform.anchoredPosition = Vector3.zero;
            }

            animationsMain.MovePhoneButton(!condition);
            stateButtonAR = state;
        }

        public void ButtonPanelCoach(bool isPressed)
        {
            btnBarR.image.sprite = isPressed ? closePanel : openPanel;
            floatMenu.SetBoolFloatAnimators(!isPressed);
            panelCoach.PanelCoachBehaviour(isPressed);
            isPressed.ComparativeAssignment(ref isPressedButtonBar);
        }

        public bool ButtonSetCoachInteraction
        {
            get => btnBarC.IsInteractable();
            set => btnBarC.interactable = value;
        }

        public bool PanelCoachInteraction
        {
            get => btnBarR.IsInteractable();
            set => btnBarR.interactable = value;
        }

        public StateButtonAR CurrentStateButtonAR => stateButtonAR;
    }
}
