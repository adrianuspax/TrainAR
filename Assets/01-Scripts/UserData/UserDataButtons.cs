using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UserDataButtons;

public class UserDataButtons : MonoBehaviour
{
    private enum ChosenGoal
    {
        Lose_Weight = LOSEWEIGHT,
        More_Muscles = MOREMUSCLES,
        Keep_Shape = KEEPSHAPE
    };

    [Header("BUTTONS", order = 0)]
    [Space(-10, order = 1)]
    [Header("Canvas Buttons", order = 2)]
    [SerializeField] private Button nextButton;
    [SerializeField] private Button closeButton;
    [Space(-10, order = 1)]
    [Header("Page Two Buttons", order = 2)]
    [SerializeField] private Button maleButton;
    [SerializeField] private Button femaleButton;
    [Space(-10, order = 1)]
    [Header("Page three Buttons", order = 2)]
    [SerializeField] private Button looseWeightButton;
    [SerializeField] private Button moreMusclesButton;
    [SerializeField] private Button keepShapeButton;
    [Space(-10, order = 1)]
    [Header("Page Six Edit Buttons", order = 2)]
    [SerializeField] private Button editAge;
    [SerializeField] private Button editCurrentWeight;
    [SerializeField] private Button editWeightGoal;
    [SerializeField] private Button editHeight;
    [Space(-10, order = 1)]
    [Header("Footer Buttons", order = 2)]
    [SerializeField] private Button saveButton;
    [SerializeField] private Button cancelButton;

    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("Variables", order = 2)]
    [Space(-10, order = 0)]
    [Header("Page2_(genre)", order = 1)]
    [SerializeField, ReadOnly] private Button[] genreButtonByIndex;
    [SerializeField, ReadOnly] private bool[] genreIsPressed;
    [SerializeField, ReadOnly] private bool isMale;
    [SerializeField, ReadOnly] private Color[] genreColorPressed;
    [Space(-10, order = 0)]
    [Header("Page3_(goal)", order = 1)]
    [SerializeField, ReadOnly] private Button[] goalButtonByIndex;
    [SerializeField, ReadOnly] private bool[] goalIsPressed;
    [SerializeField, ReadOnly] private string chosenGoal;

    [Header("Scripts", order = 0)]
    [SerializeField, ReadOnly] private ButtonsManager Buttons;
    [SerializeField, ReadOnly] private PagesAndFields PagesAndFields;
    [SerializeField, ReadOnly] private AnimationsManager Animations;
    [SerializeField, ReadOnly] private DataManagement Data;
    [SerializeField, ReadOnly] private PalleteColors PalleteColors;

    public const int M = 0;
    public const int F = 1;

    public const int LOSEWEIGHT = 0;
    public const int MOREMUSCLES = 1;
    public const int KEEPSHAPE = 2;

    private void Awake()
    {
        #region SCRIPTS
        PalleteColors = GameObject.FindGameObjectWithTag(PalleteColors.TAG).GetComponent<PalleteColors>();
        Buttons = this.GetComponent<ButtonsManager>();
        PagesAndFields = this.GetComponent<PagesAndFields>();
        Animations = this.GetComponent<AnimationsManager>();
        Data = this.GetComponent<DataManagement>();
        #endregion
        #region CANVAS_MAIN
        closeButton.onClick.AddListener(() => CloseApplication());
        nextButton.onClick.AddListener(() => NextButtonFunction());
        #endregion
        #region GENRE
        genreButtonByIndex = new Button[] { maleButton, femaleButton };
        genreIsPressed = new bool[] { false, false };
        genreButtonByIndex[M].onClick.AddListener(() => GenreButtonBehaviour(M));
        genreButtonByIndex[F].onClick.AddListener(() => GenreButtonBehaviour(F));
        #endregion
        #region GOAL
        goalButtonByIndex = new Button[] { looseWeightButton, moreMusclesButton, keepShapeButton };
        goalIsPressed = new bool[] { false, false, false };
        genreColorPressed[0] = PalleteColors.Triad(3);
        genreColorPressed[1] = PalleteColors.Main;
        goalButtonByIndex[LOSEWEIGHT].onClick.AddListener(() => GoalButtonsBehaviour(LOSEWEIGHT));
        goalButtonByIndex[MOREMUSCLES].onClick.AddListener(() => GoalButtonsBehaviour(MOREMUSCLES));
        goalButtonByIndex[KEEPSHAPE].onClick.AddListener(() => GoalButtonsBehaviour(KEEPSHAPE));
        #endregion
        #region FOOTER
        saveButton.onClick.AddListener(() => SaveButtonFunction());
        cancelButton.onClick.AddListener(() => CancelButtonFunction());
        editAge.onClick.AddListener(() => CallFooterField(0));
        editCurrentWeight.onClick.AddListener(() => CallFooterField(1));
        editWeightGoal.onClick.AddListener(() => CallFooterField(2));
        editHeight.onClick.AddListener(() => CallFooterField(3));
        #endregion
    }

    #region CLOSE_BUTTON
    public void CloseApplication()
    {
#if UNITY_EDITOR
        Debug.Log("CloseApplication() called!");
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    #endregion
    #region PAGE2_(GENRE)
    public void GenreButtonBehaviour(int index)
    {
        genreIsPressed[index] = !genreIsPressed[index];

        Image icon;

        for (int i = 0; i < genreButtonByIndex.Length; i++)
        {
            if (i == index && genreIsPressed[index])
            {
                icon = genreButtonByIndex[index].transform.GetChild(0).GetComponent<Image>();
                genreButtonByIndex[index].image.color = genreColorPressed[index];
                icon.color = PalleteColors.BlueGray_White;
            }
            else
            {
                icon = genreButtonByIndex[i].transform.GetChild(0).GetComponent<Image>();
                genreButtonByIndex[i].image.color = PalleteColors.BlueGray_White;
                icon.color = genreColorPressed[i];
                genreIsPressed[i] = false;
            }
        }

        GenreButtonSelected = (index == 0);
        NextButtonInteraction = genreIsPressed[index];
    }

    public bool GenreButtonSelected
    {
        get => isMale;
        private set => isMale = value;
    }
    #endregion
    #region PAGE3_(GOAL)
    public void GoalButtonsBehaviour(int index)
    {
        goalIsPressed[index] = !goalIsPressed[index];

        Image icon;
        TextMeshProUGUI tmp;

        for (int i = 0; i < goalButtonByIndex.Length; i++)
        {
            if (i == index && goalIsPressed[index])
            {
                icon = goalButtonByIndex[index].transform.GetChild(0).GetComponent<Image>();
                tmp = goalButtonByIndex[index].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                goalButtonByIndex[index].image.color = PalleteColors.Main;
                icon.color = PalleteColors.BlueGray_White;
                tmp.color = PalleteColors.BlueGray_White;
                tmp.fontStyle = FontStyles.Bold;
            }
            else
            {
                icon = goalButtonByIndex[i].transform.GetChild(0).GetComponent<Image>();
                tmp = goalButtonByIndex[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                goalButtonByIndex[i].image.color = PalleteColors.BlueGray_White;
                icon.color = PalleteColors.Main;
                tmp.color = PalleteColors.BlueGrey(4);
                tmp.fontStyle = FontStyles.Normal;
                goalIsPressed[i] = false;
            }
        }

        Goal = ((ChosenGoal)index).ToString();
        NextButtonInteraction = goalIsPressed[index];
    }

    public string Goal
    {
        get => chosenGoal;
        private set => chosenGoal = value;
    }

    #endregion
    #region FOOTER
    public void SaveButtonFunction()
    {
        Animations.UserData.PlayFooterSaveClip();
    }

    public void CancelButtonFunction()
    {
        Animations.UserData.PlayFooterCancelClip();
    }

    public void CallFooterField(int index)
    {
        PagesAndFields.FooterActivation(index + 1);
    }
    #endregion
    #region NEXT_BUTTON
    public void NextButtonFunction()
    {
        if (PagesAndFields.CurrentPage >= PagesAndFields.TotalPages)
        {
            Debug.Log("Finished");
            return;
        }
        else
        {
            Data.PagesSavePreferences(PagesAndFields.CurrentPage);
            PagesAndFields.PageActivation(PagesAndFields.CurrentPage + 1);
            NextButtonInteraction = false;
        }
    }

    public bool NextButtonInteraction
    {
        get => nextButton.IsInteractable();
        set => Buttons.Interaction(value, nextButton);
    }
    #endregion
}