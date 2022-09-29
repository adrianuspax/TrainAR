using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BodyPart
{
    Chest = 0,
    Abdomen = 1,
    Legs = 2,
    Arms = 3,
    All = 4
}

[RequireComponent(typeof(AnimationsManager))]
public class FloatMenu : MonoBehaviour
{
    [Header("BUTTONS", order = 0)]
    [Space(-10, order = 1)]
    [Header("Header", order = 2)]
    [SerializeField] private Transform buttonsFloatHeader;

    [Header("READY ONLY", order = 0)]
    [Space(-10, order = 1)]
    [Header("Variables", order = 2)]
    [NonReorderable][SerializeField, ReadOnly] private Button[] buttonFloatButton;
    [NonReorderable][SerializeField, ReadOnly] private Transform[] buttonFloatPivot;
    [NonReorderable][SerializeField, ReadOnly] private Canvas[] buttonFloatCanvas;
    [NonReorderable][SerializeField, ReadOnly] private Animator[] buttonFloatAnimator;

    [Header("Scripts", order = 0)]
    [SerializeField, ReadOnly] private ButtonsManager Buttons;

    private const string animationParameter = "floatIsOpen";

    private void Awake()
    {
        Buttons = this.GetComponent<ButtonsManager>();

        buttonFloatAnimator = AssignAnimators(FloatHeader);
        buttonFloatPivot = AssignPivots(AnimatorFloat);
        buttonFloatCanvas = AssignCanvas(PivotFloat);
        buttonFloatButton = AssignButtons(CanvasFloat);

        ButtonFloat[(int)BodyPart.Chest].onClick.AddListener(() => ButtonsFloatFunction(BodyPart.Chest));
        ButtonFloat[(int)BodyPart.Abdomen].onClick.AddListener(() => ButtonsFloatFunction(BodyPart.Abdomen));
        ButtonFloat[(int)BodyPart.Legs].onClick.AddListener(() => ButtonsFloatFunction(BodyPart.Legs));
        ButtonFloat[(int)BodyPart.Arms].onClick.AddListener(() => ButtonsFloatFunction(BodyPart.Arms));

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
        Button[] button;
        button = new Button[TotalFloats];

        for (int i = 0; i < TotalFloats; i++)
        {
            button[i] = canvas[i].transform.GetChild(0).GetComponent<Button>();
        }

        return button;
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
            return AnimatorFloat[index].GetBool(animationParameter);
        }

        set
        {
            foreach (Animator animators in AnimatorFloat)
            {
                animators.SetBool(animationParameter, value);
            }
        }
    }

    public void ButtonsFloatFunction(BodyPart bodyPart)
    {
        FloatsIsOpen = false;
        Buttons.DropDownMenu.ButtonOpenDropDown();

        switch (bodyPart)
        {
            case BodyPart.All:
                foreach (GameObject go in Buttons.DropDownMenu.ButtonGameObject)
                {
                    go.SetActive(true);
                }
                break;
            case BodyPart.Chest:
                Buttons.DropDownMenu.ButtonGameObject[0].SetActive(false);  //Pernas
                Buttons.DropDownMenu.ButtonGameObject[1].SetActive(false);  //Pernas e braços
                Buttons.DropDownMenu.ButtonGameObject[2].SetActive(false);  //Braços
                Buttons.DropDownMenu.ButtonGameObject[3].SetActive(false);  //Pernas
                Buttons.DropDownMenu.ButtonGameObject[4].SetActive(true);   //Tudo
                Buttons.DropDownMenu.ButtonGameObject[5].SetActive(true);   //Peito e pernas
                Buttons.DropDownMenu.ButtonGameObject[6].SetActive(true);   //Peito
                Buttons.DropDownMenu.ButtonGameObject[7].SetActive(false);  //braço e pernas
                Buttons.DropDownMenu.ButtonGameObject[8].SetActive(false);  //Pernas
                Buttons.DropDownMenu.ButtonGameObject[9].SetActive(false);  //braços e pernas
                Buttons.DropDownMenu.ButtonGameObject[10].SetActive(true);  //Peito e braços
                Buttons.DropDownMenu.ButtonGameObject[11].SetActive(true);  //Braços e peito
                Buttons.DropDownMenu.ButtonGameObject[12].SetActive(false); //Pernas
                Buttons.DropDownMenu.ButtonGameObject[13].SetActive(true);  //Peito
                break;
            case BodyPart.Abdomen:
                Buttons.DropDownMenu.ButtonGameObject[0].SetActive(false);
                Buttons.DropDownMenu.ButtonGameObject[1].SetActive(false);
                Buttons.DropDownMenu.ButtonGameObject[2].SetActive(false);
                Buttons.DropDownMenu.ButtonGameObject[3].SetActive(false);
                Buttons.DropDownMenu.ButtonGameObject[4].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[5].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[6].SetActive(false);
                Buttons.DropDownMenu.ButtonGameObject[7].SetActive(false);
                Buttons.DropDownMenu.ButtonGameObject[8].SetActive(false);
                Buttons.DropDownMenu.ButtonGameObject[9].SetActive(false);
                Buttons.DropDownMenu.ButtonGameObject[10].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[11].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[12].SetActive(false);
                Buttons.DropDownMenu.ButtonGameObject[13].SetActive(true);
                break;
            case BodyPart.Legs:
                Buttons.DropDownMenu.ButtonGameObject[0].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[1].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[2].SetActive(false);
                Buttons.DropDownMenu.ButtonGameObject[3].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[4].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[5].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[6].SetActive(false);
                Buttons.DropDownMenu.ButtonGameObject[7].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[8].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[9].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[10].SetActive(false);
                Buttons.DropDownMenu.ButtonGameObject[11].SetActive(false);
                Buttons.DropDownMenu.ButtonGameObject[12].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[13].SetActive(false);
                break;
            case BodyPart.Arms:
                Buttons.DropDownMenu.ButtonGameObject[0].SetActive(false);
                Buttons.DropDownMenu.ButtonGameObject[1].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[2].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[3].SetActive(false);
                Buttons.DropDownMenu.ButtonGameObject[4].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[5].SetActive(false);
                Buttons.DropDownMenu.ButtonGameObject[6].SetActive(false);
                Buttons.DropDownMenu.ButtonGameObject[7].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[8].SetActive(false);
                Buttons.DropDownMenu.ButtonGameObject[9].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[10].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[11].SetActive(true);
                Buttons.DropDownMenu.ButtonGameObject[12].SetActive(false);
                Buttons.DropDownMenu.ButtonGameObject[13].SetActive(false);
                break;
            default:
                foreach (GameObject go in Buttons.DropDownMenu.ButtonGameObject)
                {
                    go.SetActive(true);
                }
                break;
        }
    }
    #endregion
}