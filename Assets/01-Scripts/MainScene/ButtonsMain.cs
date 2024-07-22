using UnityEngine;
using UnityEngine.UI;
using ASP;
using Debug = ASP.Debug;
[RequireComponent(typeof(AugmentedReality), typeof(ImagesManager))]
public class ButtonsMain : MonoBehaviour
{
    [Header("BUTTONS", order = 0)]
    [Space(-10, order = 1)]
    [Header("Top Area", order = 2)]
    [SerializeField] private Button btnTopL;
    [SerializeField] private Button btnTopR;
    [Space(-10, order = 0)]
    [Header("Top Area", order = 1)]
    [SerializeField] private Button btnBarL;
    [SerializeField] private Button btnBarC;
    [SerializeField] private Button btnBarR;
    [Header("READY ONLY", order = 0)]
    [Space(-10, order = 1)]
    [Header("Variables", order = 2)]
    [SerializeField, ReadOnly] private StateButtonAR stateButtonAR;
    [SerializeField, ReadOnly] private bool btnBarRIsPressed;

    [Space(-10, order = 1)]
    [Header("Scripts", order = 2)]
    [SerializeField, ReadOnly] private AugmentedReality AR;
    [SerializeField, ReadOnly] private ImagesManager Sprites;
    [SerializeField, ReadOnly] private PanelResult panelResult;
    [SerializeField, ReadOnly] private PanelCoach panelCoach;
    [SerializeField, ReadOnly] private AnimationsMain animationsMain;
    [SerializeField, ReadOnly] private FloatMenu floatMenu;
    [SerializeField, ReadOnly] private CoachAnimations coachAnimations;

    private void Awake()
    {
        AR = GetComponent<AugmentedReality>();
        Sprites = GetComponent<ImagesManager>();
        panelResult = GetComponent<PanelResult>();
        panelCoach = GetComponent<PanelCoach>();
        animationsMain = GetComponent<AnimationsMain>();
        floatMenu = GetComponent<FloatMenu>();
        coachAnimations = GetComponent<CoachAnimations>();

        btnTopR.onClick.AddListener(ButtonsManager.CloseApplication);
        btnTopL.onClick.AddListener(() => TrainARSceneManager.LoadMenu());
        btnBarC.onClick.AddListener(() => AR.PlaceCoachAR());
        btnBarR.onClick.AddListener(() => ButtonPanelCoach(btnBarRIsPressed));
        btnBarL.onClick.AddListener(() => panelResult.PanelResultBehaviour());
    }

    public void ButtonARBehaviour(StateButtonAR state, bool isInteractable)
    {
        bool condition;

        switch (state)
        {
            case StateButtonAR.ReadyToSet:
                btnBarC.image.sprite = Sprites.setCoachAR;
                break;
            case StateButtonAR.Refresh:
                btnBarC.image.sprite = Sprites.replaceCoachAR;
                break;
        }

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
        isPressed = !isPressed;

        btnBarR.image.sprite = isPressed ? Sprites.closePanel : Sprites.openPanel;
        floatMenu.SetBoolFloatAnimators(!isPressed);

        panelCoach.PanelCoachBehaviour(isPressed);

        btnBarRIsPressed = isPressed;
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