using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ASP;
using Debug = ASP.Debug;

[RequireComponent(typeof(ButtonsManager))]
public class UserDataButtons : MonoBehaviour
{
    [Header("BUTTONS", order = 0)]
    [Space(-10, order = 1)]
    [Header("Canvas Buttons", order = 2)]
    [SerializeField] private Button nextButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button returnButton;
    [Space(-10, order = 0)]
    [Header("Page Two Header", order = 1)]
    [SerializeField] private Transform genreButtonsHeader;
    [Space(-10, order = 0)]
    [Header("Page three Header", order = 1)]
    [SerializeField] private Transform goalButtonsHeader;
    [Space(-10, order = 0)]
    [Header("Page Four Buttons", order = 1)]
    [SerializeField] private Transform levelButtonsHeader;
    [Space(-10, order = 0)]
    [Header("Page Five Buttons", order = 1)]
    [SerializeField] private Transform focusButtonsHeader;
    [Space(-10, order = 0)]
    [Header("Footer Buttons", order = 1)]
    [SerializeField] private Button saveButton;
    [SerializeField] private Button cancelButton;
    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("Variables", order = 2)]
    [Space(-10, order = 3)]
    [Header("Page2_(Genre)", order = 4)]
    [NonReorderable][SerializeField, ReadOnly] private Button[] genreButton;
    [NonReorderable][SerializeField, ReadOnly] private bool[] genreIsPressed;
    [SerializeField, ReadOnly] private bool isMale;
    [NonReorderable][SerializeField, ReadOnly] private Color[] genreColorPressed;
    [Space(-10, order = 0)]
    [Header("Page3_(Goal)", order = 1)]
    [NonReorderable][SerializeField, ReadOnly] private Button[] goalButton;
    [NonReorderable][SerializeField, ReadOnly] private bool[] goalIsPressed;
    [NonReorderable][SerializeField, ReadOnly] private string[] goalbyIndex;
    [SerializeField, ReadOnly] private string chosenGoal;
    [Space(-10, order = 0)]
    [Header("Page4_(Level)", order = 1)]
    [NonReorderable][SerializeField, ReadOnly] private Button[] levelButton;
    [NonReorderable][SerializeField, ReadOnly] private bool[] levelIsPressed;
    [NonReorderable][SerializeField, ReadOnly] private string[] levelByIndex;
    [SerializeField, ReadOnly] private string chosenLevel;
    [SerializeField, ReadOnly] private int levelValue;
    [Space(-10, order = 0)]
    [Header("Page5_(Focus)", order = 1)]
    [NonReorderable][SerializeField, ReadOnly] private Button[] focusButton;
    [NonReorderable][SerializeField, ReadOnly] private bool[] focusIsPressed;
    [NonReorderable][SerializeField, ReadOnly] private string[] focusByIndex;
    [SerializeField, ReadOnly] private string chosenFocus;
    [Space(-10, order = 0)]
    [Header("Page6_(More)", order = 1)]
    [NonReorderable][SerializeField, ReadOnly] private Button[] moreEditButton;
    [NonReorderable][SerializeField, ReadOnly] private bool[] moreValueIsEdited;

    [Header("Scripts", order = 0)]
    [SerializeField, ReadOnly] private ButtonsManager Buttons;
    [SerializeField, ReadOnly] private PagesAndFields PagesAndFields;
    [SerializeField, ReadOnly] private DataManagement Data;
    [SerializeField, ReadOnly] private UserDataAnimations userDataAnimations;

    private void Awake()
    {
        #region SCRIPTS
        Buttons = this.GetComponent<ButtonsManager>();
        PagesAndFields = this.GetComponent<PagesAndFields>();
        Data = this.GetComponent<DataManagement>();
        userDataAnimations = this.GetComponent<UserDataAnimations>();
        #endregion
        #region CANVAS_MAIN
        closeButton.onClick.AddListener(ButtonsManager.CloseApplication);
        nextButton.onClick.AddListener(() => NextButtonFunction());
        returnButton.onClick.AddListener(() => TrainARSceneManager.LoadMenu());
        #endregion
        #region GENRE
        genreColorPressed = new Color[2] { PalleteColors.Triad(3), PalleteColors.Main };
        (genreButton, genreIsPressed) = Buttons.Assignment(genreButtonsHeader, GenreButtonBehaviour);
        #endregion
        #region GOAL
        goalbyIndex = Data.goalName;
        (goalButton, goalIsPressed) = Buttons.Assignment(goalButtonsHeader, GoalButtonsBehaviour);
        #endregion
        #region FOCUS
        focusByIndex = Data.focusName;
        (focusButton, focusIsPressed) = Buttons.Assignment(focusButtonsHeader, FocusButtonsBehaviour);
        #endregion
        #region MORE
        (moreEditButton, moreValueIsEdited) = PagesAndFields.AssignmentEditMore(CallFooterField);
        #endregion
        #region FOOTER
        saveButton.onClick.AddListener(() => SaveButtonFunction());
        cancelButton.onClick.AddListener(() => CancelButtonFunction());
        #endregion
        #region LEVEL
        (levelButton, levelIsPressed) = Buttons.Assignment(levelButtonsHeader, LevelButtonsBehaviour);
        #endregion
    }

    private void Start()
    {
        returnButton.gameObject.SetActive(TrainARSceneManager.GetPreviousSceneSaved() == TrainARSceneManager.menu);
        levelByIndex = Data.levelPhrase;
    }

    #region PAGE2_(GENRE)
    public void GenreButtonBehaviour(int index)
    {
        genreIsPressed[index] = !genreIsPressed[index];

        Image icon;

        for (int i = 0; i < genreButton.Length; i++)
        {
            if (i == index && genreIsPressed[index])
            {
                icon = genreButton[index].transform.GetChild(0).GetComponent<Image>();
                genreButton[index].image.color = genreColorPressed[index];
                icon.color = PalleteColors.BlueGray_White;
            }
            else
            {
                icon = genreButton[i].transform.GetChild(0).GetComponent<Image>();
                genreButton[i].image.color = PalleteColors.BlueGray_White;
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
        Transform child;

        for (int i = 0; i < goalButton.Length; i++)
        {
            if (i == index && goalIsPressed[index])
            {
                child = goalButton[index].transform;

                for (int j = 0; j < child.childCount; j++)
                {
                    if (child.GetChild(j).GetComponent<Image>() != null)
                    {
                        icon = child.GetChild(j).GetComponent<Image>();
                        icon.color = PalleteColors.BlueGray_White;
                    }

                    if (child.GetChild(j).GetComponent<TextMeshProUGUI>() != null)
                    {
                        tmp = child.GetChild(j).GetComponent<TextMeshProUGUI>();
                        tmp.color = PalleteColors.BlueGray_White;
                        tmp.fontStyle = FontStyles.Bold;
                    }
                }

                goalButton[index].image.color = PalleteColors.Main;
            }
            else
            {
                child = goalButton[i].transform;

                for (int j = 0; j < child.childCount; j++)
                {
                    if (child.GetChild(j).GetComponent<Image>() != null)
                    {
                        icon = child.GetChild(j).GetComponent<Image>();
                        icon.color = PalleteColors.Main;
                    }

                    if (child.GetChild(j).GetComponent<TextMeshProUGUI>() != null)
                    {
                        tmp = child.GetChild(j).GetComponent<TextMeshProUGUI>();
                        tmp.color = PalleteColors.BlueGrey(4);
                        tmp.fontStyle = FontStyles.Normal;
                    }
                }

                goalButton[i].image.color = PalleteColors.BlueGray_White;
                goalIsPressed[i] = false;
            }
        }

        Goal = goalbyIndex[index];
        NextButtonInteraction = goalIsPressed[index];
    }

    public string Goal
    {
        get => chosenGoal;
        private set => chosenGoal = value;
    }
    public string[] GoalButtonsName
    {
        set
        {
            Transform child;

            for (int i = 0; i < value.Length; i++)
            {
                child = goalButton[i].transform;

                for (int j = 0; j < child.childCount; j++)
                {
                    if (child.GetChild(j).GetComponent<TextMeshProUGUI>() != null)
                    { child.GetChild(j).GetComponent<TextMeshProUGUI>().text = value[i]; }
                }
            }
        }
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
        LevelValue = index;
    }

    public string Level
    {
        get => chosenLevel;
        private set
        {
            chosenLevel = value;
            PagesAndFields.LevelName = chosenLevel;
        }
    }

    public int LevelValue
    {
        get => levelValue;
        set => levelValue = value + 1;
    }

    #endregion
    #region PAGE5_(FOCUS)
    public void FocusButtonsBehaviour(int index)
    {
        focusIsPressed[index] = !focusIsPressed[index];

        Transform child;
        TextMeshProUGUI tmp;

        for (int i = 0; i < focusButton.Length; i++)
        {
            if (i == index && focusIsPressed[index])
            {
                child = focusButton[index].transform;

                for (int j = 0; j < child.childCount; j++)
                {
                    if (child.GetChild(j).GetComponent<TextMeshProUGUI>() != null)
                    {
                        tmp = child.GetChild(j).GetComponent<TextMeshProUGUI>();
                        tmp.color = PalleteColors.BlueGray_White;
                        tmp.fontStyle = FontStyles.Bold;
                    }
                }

                focusButton[index].image.color = PalleteColors.Main;
            }
            else
            {
                child = focusButton[i].transform;

                for (int j = 0; j < child.childCount; j++)
                {
                    if (child.GetChild(j).GetComponent<TextMeshProUGUI>() != null)
                    {
                        tmp = child.GetChild(j).GetComponent<TextMeshProUGUI>();
                        tmp.color = PalleteColors.BlueGrey(4);
                        tmp.fontStyle = FontStyles.Normal;
                    }
                }

                focusButton[i].image.color = PalleteColors.BlueGray_White;
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
    public string[] FocusButtonsName
    {
        set
        {
            Transform child;

            for (int i = 0; i < value.Length; i++)
            {
                child = focusButton[i].transform;

                for (int j = 0; j < child.childCount; j++)
                {
                    if (child.GetChild(j).GetComponent<TextMeshProUGUI>() != null)
                    { child.GetChild(j).GetComponent<TextMeshProUGUI>().text = value[i]; }
                }
            }
        }
    }
    #endregion
    #region FOOTERS
    public void SaveButtonFunction()
    {
        int index;
        index = PagesAndFields.CurrentFooter - 1;
        moreValueIsEdited[index] = true;
        PagesAndFields.SetMoreValueTMP(index, PagesAndFields.DataFooter);
        userDataAnimations.PlayFooterSaveClip();
        PagesAndFields.ClearAllInputFooter(userDataAnimations.EnterClipTime);
        Buttons.UserData.NextButtonInteraction = AllFooterValuesItsEdited;
    }

    public bool AllFooterValuesItsEdited
    {
        get
        {
            int totalFootersEdited = 0;

            foreach (bool edited in moreValueIsEdited)
            { if (edited) { totalFootersEdited++; } }

            return totalFootersEdited == 4;
        }
    }

    public void CancelButtonFunction()
    {
        userDataAnimations.PlayFooterCancelClip();
        PagesAndFields.ClearAllInputFooter();
    }

    public void CallFooterField(int index)
    {
        PagesAndFields.FooterActivation(index);
        SaveButtonInteraction = false;
    }

    public bool SaveButtonInteraction
    {
        get => saveButton.IsInteractable();
        set => Buttons.Interaction(value, saveButton);
    }

    public string SaveButtonString
    { set => saveButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value; }

    public string CancelButtonString
    { set => cancelButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value; }

    #endregion
    #region NEXT_BUTTON
    public void NextButtonFunction()
    {
        Data.PagesSavePreferences(PagesAndFields.CurrentPage);

        if (PagesAndFields.CurrentPage >= PagesAndFields.TotalPages)
        {
            if (TrainARSceneManager.GetPreviousSceneSaved() == TrainARSceneManager.menu)
                TrainARSceneManager.LoadMenu();
            else
                TrainARSceneManager.LoadTrainAR();
        }
        else
        {
            NextButtonInteraction = false;
            PagesAndFields.PageActivation(PagesAndFields.CurrentPage + 1);
        }
    }

    public bool NextButtonInteraction
    {
        get => nextButton.IsInteractable();
        set => Buttons.Interaction(value, nextButton);
    }

    public string NextButtonString
    { set =>  nextButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value; }
    #endregion
}