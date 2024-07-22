using ASP;
using ASP.Extensions;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AugmentedReality))]
public class ButtonsMain : MonoBehaviour
{
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
    [SerializeField, ReadOnly] private bool btnBarRIsPressed;
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
    [SerializeField, ReadOnly] private AnimationsMain animationsMain;
    [SerializeField, ReadOnly] private FloatMenu floatMenu;
    [SerializeField, ReadOnly] private CoachAnimations coachAnimations;

    private void Awake()
    {
        ComponentsAssignment();
    }
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    private void Start()
    {
        btnTopR.onClick.AddListener(ButtonsManager.CloseApplication);
        btnTopL.onClick.AddListener(() => TrainARSceneManager.LoadMenu());
        btnBarC.onClick.AddListener(() => AR.PlaceCoachAR());
        btnBarR.onClick.AddListener(() => ButtonPanelCoach(btnBarRIsPressed = !btnBarRIsPressed));
        btnBarL.onClick.AddListener(() => panelResult.PanelResultBehaviour());
    }
    /// <summary>
    /// Assignment of components and variables
    /// </summary>
    [ContextMenu("Components Assignment")]
    private void ComponentsAssignment()
    {
        if (AR.IsNull())
            AR = GetComponent<AugmentedReality>();
        if (panelResult.IsNull())
            panelResult = GetComponent<PanelResult>();
        if (panelCoach.IsNull())
            panelCoach = GetComponent<PanelCoach>();
        if (animationsMain.IsNull())
            animationsMain = GetComponent<AnimationsMain>();
        if (floatMenu.IsNull())
            floatMenu = GetComponent<FloatMenu>();
        if (coachAnimations.IsNull())
            coachAnimations = GetComponent<CoachAnimations>();
        if (btnTopL.IsNull())
            btnTopL = GameObject.Find("btnTopL").transform.GetComponent<Button>();
        if (btnTopR.IsNull())
            btnTopR = GameObject.Find("btnTopR").transform.GetComponent<Button>();
        if (btnBarL.IsNull())
            btnBarL = GameObject.Find("btnBarL").transform.GetComponent<Button>();
        if (btnBarC.IsNull())
            btnBarC = GameObject.Find("btnBarC").transform.GetComponent<Button>();
        if (btnBarR.IsNull())
            btnBarR = GameObject.Find("btnBarR").transform.GetComponent<Button>();

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
        isPressed.ComparativeAssignment(ref btnBarRIsPressed);
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