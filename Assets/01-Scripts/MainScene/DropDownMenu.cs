using ASP;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AnimationsMain))]
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
    [SerializeField, ReadOnly] private FloatMenu floatMenu;
    [SerializeField, ReadOnly] private CoachAnimations coachAnimations;
    [SerializeField, ReadOnly] private ButtonsMain buttonsMain;

    private bool singlePressed;

    private void Awake()
    {
        floatMenu = this.GetComponent<FloatMenu>();
        coachAnimations = this.GetComponent<CoachAnimations>();
        buttonsMain = this.GetComponent<ButtonsMain>();

        btnOpenDropDown.onClick.AddListener(() => ButtonOpenDropDown());
        btnCloseDropDown.onClick.AddListener(() => ButtonCloseDropDown());

        buttonsHeader = content.transform;

        pressedColor = PalleteColors.DoubleSplitComplementary(2);
        pressedTxtColor = PalleteColors.Square(1);
        (gridButtonDDM, isPressDDM) = ButtonsManager.Assignment(buttonsHeader, ButtonsDDMBehaviour);
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
            floatMenu.ButtonsFloatBehaviour(FloatMenu.BodyPart.All);
            floatMenu.SetBoolFloatAnimators(false);
        }
    }

    public void ButtonCloseDropDown()
    {
        bool conditional;
        conditional =
       coachAnimations.CurrentAnimation.isWorkout ||
       coachAnimations.CurrentAnimation.isEnter ||
       coachAnimations.NextAnimation.willBeWorkout ||
       coachAnimations.NextAnimation.willBeEnter;

        if (panelDropDownOpen)
        {
            panelDropDownOpen = false;
            aniDropDownMenu.Play(dropDownCloseClip.name);
            floatMenu.SetBoolFloatAnimators(true);
            buttonsMain.PanelCoachInteraction = true;

            if (conditional)
            {
                coachAnimations.StartIdleAnimation();
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
                ButtonsManager.SetInteraction(!isPressDDM[index], gridButtonDDM[i]);
                isPressDDM[i] = false;
            }
        }

        if (!isPressDDM[index])
        {
            coachAnimations.StartIdleAnimation();
        }

        IsPressed = isPressDDM[index];
    }

    private void ButtonsDDMFunction(int index)
    {
        coachAnimations.StartAnimation(index + 3);
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
            ButtonsManager.SetInteraction(true, gridButtonDDM);
            isPressDDM[i] = false;
        }
    }

    public bool Interactable
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

    public GameObject[] ButtonGameObjects
    {
        get
        {
            GameObject[] gameObjects;
            gameObjects = new GameObject[buttonsHeader.childCount];

            for (int i = 0; i < buttonsHeader.childCount; i++)
            {
                gameObjects[i] = gridButtonDDM[i].gameObject;
            }

            return gameObjects;
        }
    }
}