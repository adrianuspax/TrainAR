using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserDataButtons : MonoBehaviour
{
    [Header("BUTTONS", order = 0)]
    [Space(-10, order = 1)]
    [Header("Canvas Buttons", order = 2)]
    [SerializeField] private Button nextButton;
    [SerializeField] private Button closeButton;
    [Space(-10, order = 0)]
    [Header("Page Two Buttons", order = 1)]
    [SerializeField] private Button maleButton;
    [SerializeField] private Button femaleButton;
    [Space(-10, order = 0)]
    [Header("Page three Buttons", order = 1)]
    [SerializeField] private Button looseWeightButton;
    [SerializeField] private Button moreMusclesButton;
    [SerializeField] private Button keepShapeButton;
    [Space(-10, order = 0)]
    [Header("Page Four Buttons", order = 1)]
    [SerializeField] private Transform levelButtonsHeader;
    [Space(-10, order = 0)]
    [Header("Page Five Buttons", order = 1)]
    [SerializeField] private Transform focusButtonsHeader;
    [Space(-10, order = 0)]
    [Header("Page Six Edit Buttons", order = 1)]
    [SerializeField] private Button editAge;
    [SerializeField] private Button editCurrentWeight;
    [SerializeField] private Button editWeightGoal;
    [SerializeField] private Button editHeight;
    [Space(-10, order = 0)]
    [Header("Footer Buttons", order = 1)]
    [SerializeField] private Button saveButton;
    [SerializeField] private Button cancelButton;

    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("Variables", order = 2)]
    [Space(-10, order = 3)]
    [Header("Page2_(Genre)", order = 4)]
    [SerializeField, ReadOnly] private Button[] genreButtonByIndex;
    [SerializeField, ReadOnly] private bool[] genreIsPressed;
    [SerializeField, ReadOnly] private bool isMale;
    [SerializeField, ReadOnly] private Color[] genreColorPressed;
    [Space(-10, order = 0)]
    [Header("Page3_(Goal)", order = 1)]
    [SerializeField, ReadOnly] private Button[] goalButtonByIndex;
    [SerializeField, ReadOnly] private bool[] goalIsPressed;
    [SerializeField, ReadOnly] private string chosenGoal;
    [Space(-10, order = 0)]
    [Header("Page4_(Level)", order = 1)]
    [SerializeField, ReadOnly] private Button[] levelButton;
    [SerializeField, ReadOnly] private bool[] levelIsPressed;
    [SerializeField, ReadOnly] private string[] levelByIndex;
    [Space(-10, order = 0)]
    [Header("Page5_(Focus)", order = 1)]
    [SerializeField, ReadOnly] private Button[] focusButton;
    [SerializeField, ReadOnly] private bool[] focusIsPressed;
    [SerializeField, ReadOnly] private string[] focusByIndex;
    [SerializeField, ReadOnly] private string chosenFocus;
    [Header("Scripts", order = 0)]
    [SerializeField, ReadOnly] private ButtonsManager Buttons;
    [SerializeField, ReadOnly] private PagesAndFields PagesAndFields;
    [SerializeField, ReadOnly] private AnimationsManager Animations;
    [SerializeField, ReadOnly] private DataManagement Data;
    [SerializeField, ReadOnly] private PalleteColors PalleteColors;

    private enum ChosenGoal
    {
        Lose_Weight = PagesAndFields.LOSEWEIGHT,
        More_Muscles = PagesAndFields.MOREMUSCLES,
        Keep_Shape = PagesAndFields.KEEPSHAPE
    };

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
        genreButtonByIndex = new Button[2] { maleButton, femaleButton };
        genreIsPressed = new bool[2] { false, false };
        genreButtonByIndex[PagesAndFields.M].onClick.AddListener(() => GenreButtonBehaviour(PagesAndFields.M));
        genreButtonByIndex[PagesAndFields.F].onClick.AddListener(() => GenreButtonBehaviour(PagesAndFields.F));
        #endregion
        #region GOAL
        goalButtonByIndex = new Button[3] { looseWeightButton, moreMusclesButton, keepShapeButton };
        goalIsPressed = new bool[3] { false, false, false };
        genreColorPressed[0] = PalleteColors.Triad(3);
        genreColorPressed[1] = PalleteColors.Main;
        goalButtonByIndex[PagesAndFields.LOSEWEIGHT].onClick.AddListener(() => GoalButtonsBehaviour(PagesAndFields.LOSEWEIGHT));
        goalButtonByIndex[PagesAndFields.MOREMUSCLES].onClick.AddListener(() => GoalButtonsBehaviour(PagesAndFields.MOREMUSCLES));
        goalButtonByIndex[PagesAndFields.KEEPSHAPE].onClick.AddListener(() => GoalButtonsBehaviour(PagesAndFields.KEEPSHAPE));
        #endregion
        #region LEVEL
        levelByIndex = new string[5] { PagesAndFields.LEVEL1, PagesAndFields.LEVEL2, PagesAndFields.LEVEL3, PagesAndFields.LEVEL4, PagesAndFields.LEVEL5 };
        (levelButton, levelIsPressed) = Buttons.Assignment(levelButtonsHeader, LevelButtonsBehaviour);
        #endregion
        #region FOCUS
        focusByIndex = new string[4] { PagesAndFields.FOCUSNAME0, PagesAndFields.FOCUSNAME1, PagesAndFields.FOCUSNAME2, PagesAndFields.FOCUSNAME3 };
        (focusButton, focusIsPressed) = Buttons.Assignment(focusButtonsHeader, FocusButtonsBehaviour);
        #endregion
        #region MORE
        editAge.onClick.AddListener(() => CallFooterField(0));
        editCurrentWeight.onClick.AddListener(() => CallFooterField(1));
        editWeightGoal.onClick.AddListener(() => CallFooterField(2));
        editHeight.onClick.AddListener(() => CallFooterField(3));
        #endregion
        #region FOOTER
        saveButton.onClick.AddListener(() => SaveButtonFunction());
        cancelButton.onClick.AddListener(() => CancelButtonFunction());        
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

        Genre = (index == 0);
        NextButtonInteraction = genreIsPressed[index];
    }

    public bool Genre
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
    #region PAGE4_(LEVEL)
    public void LevelButtonsBehaviour(int index)
    {
        for (int i = 0; i < levelButton.Length; i++)
        {
            levelButton[i].image.color = (i <= index) ? PalleteColors.Main : PalleteColors.BlueGray_WhiteBlue;
            levelIsPressed[i] = (i <= index);
        }

        Level = levelByIndex[index];
        NextButtonInteraction = levelIsPressed[index];
    }

    public string Level
    {
        get => PagesAndFields.LevelName;
        private set => PagesAndFields.LevelName = value;
    }

    #endregion
    #region PAGE5_(FOCUS)
    public void FocusButtonsBehaviour(int index)
    {
        focusIsPressed[index] = !focusIsPressed[index];

        TextMeshProUGUI tmp;

        for (int i = 0; i < focusButton.Length; i++)
        {
            if (i == index && focusIsPressed[index])
            {
                tmp = focusButton[index].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                focusButton[index].image.color = PalleteColors.Main;
                tmp.color = PalleteColors.BlueGray_White;
                tmp.fontStyle = FontStyles.Bold;
            }
            else
            {
                tmp = focusButton[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                focusButton[i].image.color = PalleteColors.BlueGray_White;
                tmp.color = PalleteColors.BlueGrey(4);
                tmp.fontStyle = FontStyles.Normal;
                focusIsPressed[i] = false;
            }
        }
        Focus = focusByIndex[index];
        NextButtonInteraction = focusIsPressed[index];
    }

    public string Focus
    {
        get => chosenFocus;
        private set => chosenFocus = value;
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
            NextButtonInteraction = false;
            Data.PagesSavePreferences(PagesAndFields.CurrentPage);
            PagesAndFields.PageActivation(PagesAndFields.CurrentPage + 1);
        }
    }

    public bool NextButtonInteraction
    {
        get => nextButton.IsInteractable();
        set => Buttons.Interaction(value, nextButton);
    }
    #endregion
}