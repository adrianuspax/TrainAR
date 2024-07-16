using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ASP;
using Debug = ASP.Debug;

[RequireComponent(typeof(ButtonsManager), typeof(AnimationsMain))]
public class DropDownMenu : MonoBehaviour
{
    [Header("DROP-DOWN MENU BEHAVIOUR", order = 0)]
    [Space(-10, order = 1)]
    [Header("Buttons", order = 2)]
    [SerializeField] private Button btnOpenDropDown;
    [SerializeField] private Button btnCloseDropDown;
    [Space(-10, order = 0)]
    [Header("Animations", order = 1)]
    [SerializeField] private Animator aniDropDownMenu;
    [SerializeField] private AnimationClip dropDownCloseClip;
    [SerializeField] private AnimationClip dropDownOpenClip;
    [SerializeField] private AnimationClip dropDownNullClip;
    [Space(-10, order = 0)]
    [Header("Scrow View", order = 1)]
    [SerializeField] private ScrollRect dropDownScrollRect;
    [SerializeField] private GameObject content;

    [Header("READY ONLY", order = 0)]
    [Space(-10, order = 1)]
    [Header("Variables", order = 2)]
    [SerializeField, ReadOnly] private bool panelDropDownOpen;
    [SerializeField, ReadOnly] private Transform buttonsHeader;
    [NonReorderable][SerializeField, ReadOnly] private Button[] gridButtonDDM;
    [NonReorderable][SerializeField, ReadOnly] private bool[] isPressDDM;
    [SerializeField, ReadOnly] private Color pressedColor;
    [SerializeField, ReadOnly] private Color pressedTxtColor;
    [SerializeField, ReadOnly] private Color unpressedColor;
    [SerializeField, ReadOnly] private Color unpressedTxtColor;
    [Space(-10, order = 1)]
    [Header("Scripts", order = 2)]
    [SerializeField, ReadOnly] private ButtonsManager Buttons;
    [SerializeField, ReadOnly] private AnimationsManager Animations;

    private bool singlePressed;

    private void Awake()
    {
        Buttons = this.GetComponent<ButtonsManager>();
        Animations = this.GetComponent<AnimationsManager>();

        btnOpenDropDown.onClick.AddListener(() => ButtonOpenDropDown());
        btnCloseDropDown.onClick.AddListener(() => ButtonCloseDropDown());

        buttonsHeader = content.transform;

        pressedColor = PalleteColors.DoubleSplitComplementary(2);
        pressedTxtColor = PalleteColors.Square(1);
        (gridButtonDDM, isPressDDM) = Buttons.Assignment(buttonsHeader, ButtonsDDMBehaviour);
        unpressedColor = gridButtonDDM[0].image.color;
        unpressedTxtColor = gridButtonDDM[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
    }

    public void ButtonOpenDropDown()
    {
        if (panelDropDownOpen)
        {
            return;
        }
        else
        {
            panelDropDownOpen = true;
            aniDropDownMenu.Play(dropDownOpenClip.name);
            StartCoroutine(Cor_ScrowViewPos(dropDownOpenClip.length));
            Buttons.FloatMenu.ButtonsFloatBehaviour(BodyPart.All);
            Animations.FloatMenu.FloatsIsOpen = false;
        }
    }

    public void ButtonCloseDropDown()
    {
        bool conditional;
             conditional =
            Animations.Coach.CurrentAnimation.isWorkout ||
            Animations.Coach.CurrentAnimation.isEnter ||
            Animations.Coach.NextAnimation.willBeWorkout ||
            Animations.Coach.NextAnimation.willBeEnter;

        if (panelDropDownOpen)
        {
            panelDropDownOpen = false;
            aniDropDownMenu.Play(dropDownCloseClip.name);
            Animations.FloatMenu.FloatsIsOpen = true;
            Buttons.CanvasMain.PanelCoachInteraction = true;

            if (conditional)
            {
                Animations.Coach.StartIdleAnimation();
                AllButtonsDefault();
            }
        }
    }

    private IEnumerator Cor_ScrowViewPos(float time)
    {
        yield return new WaitForSeconds(time);
        dropDownScrollRect.verticalNormalizedPosition = 0.5f;
    }

    private void ButtonsDDMBehaviour(int index)
    {
        isPressDDM[index] = !isPressDDM[index];

        Vector3 deslocation;
        TextMeshProUGUI text;

        for (int i = 0; i < gridButtonDDM.Length; i++)
        {
            if (i == index && isPressDDM[index])
            {
                deslocation = new Vector3(gridButtonDDM[index].transform.localPosition.x, gridButtonDDM[index].transform.localPosition.y, -30);
                text = gridButtonDDM[index].transform.GetChild(0).GetComponent<TextMeshProUGUI>();

                text.color = pressedTxtColor;
                gridButtonDDM[index].image.color = pressedColor;
                gridButtonDDM[index].transform.localPosition = deslocation;

                ButtonsDDMFunction(index);
            }
            else
            {
                deslocation = new Vector3(gridButtonDDM[i].transform.localPosition.x, gridButtonDDM[i].transform.localPosition.y, 0);
                text = gridButtonDDM[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();

                text.color = unpressedTxtColor;
                gridButtonDDM[i].image.color = unpressedColor;
                gridButtonDDM[i].transform.localPosition = deslocation;
                Buttons.Interaction(!isPressDDM[index], gridButtonDDM[i]);
                isPressDDM[i] = false;
            }
        }

        if (!isPressDDM[index])
        {
            Animations.Coach.StartIdleAnimation();
        }

        IsPressed = isPressDDM[index];
    }

    private void ButtonsDDMFunction(int index)
    {
        Animations.Coach.StartAnimation(index + 3);
    }

    public void AllButtonsDefault()
    {
        Vector3 deslocation;
        TextMeshProUGUI text;

        for (int i = 0; i < TotalButtons; i++)
        {
            deslocation = new Vector3(gridButtonDDM[i].transform.localPosition.x, gridButtonDDM[i].transform.localPosition.y, 0);
            text = gridButtonDDM[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            text.color = unpressedTxtColor;
            gridButtonDDM[i].image.color = unpressedColor;
            gridButtonDDM[i].transform.localPosition = deslocation;
            Buttons.Interaction(true, gridButtonDDM);
            isPressDDM[i] = false;
        }
    }

    public bool DDMInterecation
    {
        get => btnOpenDropDown.interactable;
        set => btnOpenDropDown.interactable = value;
    }

    public bool IsPressed
    {
        get => singlePressed;
        private set => singlePressed = value;
    }

    public int TotalButtons => buttonsHeader.childCount;

    public GameObject[] ButtonGameObject
    {
        get
        {
            GameObject[] gameObj;
            gameObj = new GameObject[TotalButtons];

            for (int i = 0; i < TotalButtons; i++)
            { gameObj[i] = gridButtonDDM[i].gameObject; }

            return gameObj;
        }
    }
}