using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonsManager))]
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
    [SerializeField, ReadOnly] private ButtonsManager Buttons;
    [SerializeField, ReadOnly] private AnimationsManager Animations;
    [SerializeField, ReadOnly] private ImagesManager Sprites;

    private void Awake()
    {
        AR = this.GetComponent<AugmentedReality>();
        Animations = this.GetComponent<AnimationsManager>();
        Buttons = this.GetComponent<ButtonsManager>();
        Sprites = this.GetComponent<ImagesManager>();

        btnTopR.onClick.AddListener(ButtonsManager.CloseApplication);
        btnTopL.onClick.AddListener(() => AR.ChangeCamera()); //ScenesManagement.LoadMenu());
        btnBarC.onClick.AddListener(() => AR.PlaceCoachAR());
        btnBarR.onClick.AddListener(() => ButtonPanelCoach(btnBarRIsPressed));
        btnBarL.onClick.AddListener(() => Animations.PanelResult.PanelResultBehaviour());
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
        ButtonSetCoachInteraction = condition && !Animations.Coach.HasTriggered;

        if (ButtonSetCoachInteraction)
        {
            btnBarC.image.rectTransform.localScale = Vector3.one;
            btnBarC.image.rectTransform.localRotation = Quaternion.Euler(Vector3.zero);
            btnBarC.image.rectTransform.anchoredPosition = Vector3.zero;
        }

        Animations.Main.MovePhoneButton(!condition);
        stateButtonAR = state;
    }

    public void ButtonPanelCoach(bool isPressed)
    {
        isPressed = !isPressed;

        btnBarR.image.sprite = isPressed ? Sprites.closePanel : Sprites.openPanel;
        Animations.FloatMenu.FloatsIsOpen = !isPressed;

        Animations.PanelCoach.PanelCoachBehaviour(isPressed);

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