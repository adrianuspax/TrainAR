using UnityEngine;
using UnityEngine.UI;
using ASP;
using Debug = ASP.Debug;

[RequireComponent(typeof(AnimationsManager))]
public class FloatMenu : MonoBehaviour
{
    public enum BodyPart
    {
        Chest = 0,
        Abdomen = 1,
        Legs = 2,
        Arms = 3,
        All = 4
    }

    [Header(Header.MANAGEABLE, order = 0)]
    [Space(-10, order = 1)]
    [Header(Header.components, order = 2)]
    [Space(-10, order = 3)]
    [Header(Header.buttons, order = 4)]
    [Space(-10, order = 5)]
    [Header("Header", order = 6)]
    [SerializeField] private Transform buttonsFloatHeader;

    [Header(Header.READONLY, order = 0)]
    [Space(-10, order = 1)]
    [Header(Header.components, order = 2)]
    [SerializeField, NonReorderable, ReadOnly] private Button[] buttonFloatButton;
    [SerializeField, NonReorderable, ReadOnly] private Transform[] buttonFloatPivot;
    [SerializeField, NonReorderable, ReadOnly] private Canvas[] buttonFloatCanvas;
    [SerializeField, NonReorderable, ReadOnly] private Animator[] buttonFloatAnimator;

    [Header(Header.scripts, order = 0)]
    [SerializeField, ReadOnly] private ButtonsManager ButtonsManager;

    private void Awake()
    {
        ButtonsManager = GetComponent<ButtonsManager>();

        buttonFloatAnimator = AssignAnimators(FloatHeader);
        buttonFloatPivot = AssignPivots(AnimatorFloat);
        buttonFloatCanvas = AssignCanvas(PivotFloat);
        buttonFloatButton = AssignButtons(CanvasFloat);

        ButtonFloat[(int)BodyPart.Chest].onClick.AddListener(() => ButtonsFloatBehaviour(BodyPart.Chest));
        ButtonFloat[(int)BodyPart.Abdomen].onClick.AddListener(() => ButtonsFloatBehaviour(BodyPart.Abdomen));
        ButtonFloat[(int)BodyPart.Legs].onClick.AddListener(() => ButtonsFloatBehaviour(BodyPart.Legs));
        ButtonFloat[(int)BodyPart.Arms].onClick.AddListener(() => ButtonsFloatBehaviour(BodyPart.Arms));
    }

    public int TotalFloats => FloatHeader.childCount;
    public Transform FloatHeader => buttonsFloatHeader;

    #region ANIMATORS
    private Animator[] AssignAnimators(Transform header)
    {
        Animator[] animator;
        animator = new Animator[TotalFloats];

        for (int i = 0; i < TotalFloats; i++)
        {
            animator[i] = header.GetChild(i).GetChild(0).GetComponent<Animator>();
        }

        return animator;
    }
    public Animator[] AnimatorFloat => buttonFloatAnimator;
    #endregion
    #region PIVOTS
    private Transform[] AssignPivots(Animator[] animator)
    {
        Transform[] pivot;
        pivot = new Transform[TotalFloats];

        for (int i = 0; i < TotalFloats; i++)
        {
            pivot[i] = animator[i].transform.GetChild(0).transform;
        }

        return pivot;
    }
    public Transform[] PivotFloat => buttonFloatPivot;
    #endregion
    #region CANVAS
    private Canvas[] AssignCanvas(Transform[] pivot)
    {
        Canvas[] canvas;
        canvas = new Canvas[TotalFloats];

        for (int i = 0; i < TotalFloats; i++)
        {
            canvas[i] = pivot[i].GetChild(0).GetChild(1).GetComponent<Canvas>();
        }

        return canvas;
    }
    public Canvas[] CanvasFloat => buttonFloatCanvas;
    #endregion
    #region BUTTONS
    private Button[] AssignButtons(Canvas[] canvas)
    {
        Button[] buttons;
        buttons = new Button[TotalFloats];

        for (int i = 0; i < TotalFloats; i++)
        {
            buttons[i] = canvas[i].transform.GetChild(0).GetComponent<Button>();
        }

        return buttons;
    }
    public Button[] ButtonFloat => buttonFloatButton;
    #endregion
    #region MANAGEMENT
    public void LookingAtCam(bool isLooking)
    {
        if (isLooking)
        {
            foreach (Transform pivots in PivotFloat)
            {
                pivots.LookAt(Camera.main.transform);
            }
        }
    }

    public bool FloatsIsOpen
    {
        get
        {
            int index;
            index = Random.Range(0, TotalFloats);
            return AnimatorFloat[index].GetBool(Parameter.floatIsOpen);
        }

        set
        {
            foreach (Animator animators in AnimatorFloat)
            {
                animators.SetBool(Parameter.floatIsOpen, value);
            }
        }
    }

    public void ButtonsFloatBehaviour(BodyPart bodyPart)
    {
        ButtonsFloatFunction();

        bool buttonsToActivate;

        for (int i = 0; i < ButtonsManager.DropDownMenu.ButtonGameObject.Length; i++)
        {
            switch (bodyPart)
            {
                case BodyPart.Chest:
                    buttonsToActivate =
                        i == ButtonDDM.BURPEE ||
                        i == ButtonDDM.BICYCLECRUNCH ||
                        i == ButtonDDM.CIRCLECRUNCH ||
                        i == ButtonDDM.PIKEWALK ||
                        i == ButtonDDM.PUSHUP ||
                        i == ButtonDDM.SITUPS;
                    ButtonsManager.DropDownMenu.ButtonGameObject[i].SetActive(buttonsToActivate);
                    break;
                case BodyPart.Abdomen:
                    buttonsToActivate =
                        i == ButtonDDM.BURPEE ||
                        i == ButtonDDM.BICYCLECRUNCH ||
                        i == ButtonDDM.PIKEWALK ||
                        i == ButtonDDM.PUSHUP ||
                        i == ButtonDDM.SITUPS;
                    ButtonsManager.DropDownMenu.ButtonGameObject[i].SetActive(buttonsToActivate);
                    break;
                case BodyPart.Legs:
                    buttonsToActivate =
                        i == ButtonDDM.SQUAT ||
                        i == ButtonDDM.SQUATBENTARMS ||
                        i == ButtonDDM.BOXJUMP ||
                        i == ButtonDDM.BURPEE ||
                        i == ButtonDDM.BICYCLECRUNCH ||
                        i == ButtonDDM.JUMPINGJACKS ||
                        i == ButtonDDM.PISTOL ||
                        i == ButtonDDM.PLANK ||
                        i == ButtonDDM.PUSHUP;
                    ButtonsManager.DropDownMenu.ButtonGameObject[i].SetActive(buttonsToActivate);
                    break;
                case BodyPart.Arms:
                    buttonsToActivate =
                        i == ButtonDDM.SQUATBENTARMS ||
                        i == ButtonDDM.BICEPCURL ||
                        i == ButtonDDM.BURPEE ||
                        i == ButtonDDM.JUMPINGJACKS ||
                        i == ButtonDDM.PLANK ||
                        i == ButtonDDM.PIKEWALK ||
                        i == ButtonDDM.PUSHUP;
                    ButtonsManager.DropDownMenu.ButtonGameObject[i].SetActive(buttonsToActivate);
                    break;
                case BodyPart.All:
                    ButtonsManager.DropDownMenu.ButtonGameObject[i].SetActive(true);
                    break;
                default:
                    ButtonsManager.DropDownMenu.ButtonGameObject[i].SetActive(false);
                    break;
            }
        }
    }

    public void ButtonsFloatFunction()
    {
        FloatsIsOpen = false;
        ButtonsManager.DropDownMenu.ButtonOpenDropDown();
        ButtonsManager.CanvasMain.PanelCoachInteraction = false;
    }
    #endregion
}