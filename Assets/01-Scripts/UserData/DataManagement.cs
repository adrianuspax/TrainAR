#region USED_NAMESPACES
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
#region CLASS
public class DataManagement : MonoBehaviour
{
    #region READ_INSPECTOR
    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("SAVED DATA", order = 2)]
    [ReadOnly, SerializeField] private string userName;
    [ReadOnly, SerializeField] private string genre;
    [ReadOnly, SerializeField] private string goal;
    [ReadOnly, SerializeField] private string level;
    [ReadOnly, SerializeField] private string focus;
    [ReadOnly, SerializeField] private string age;
    [ReadOnly, SerializeField] private string currentWeight;
    [ReadOnly, SerializeField] private string weightGoal;
    [ReadOnly, SerializeField] private string height;

    [Header("Scripts", order = 0)]
    [ReadOnly, SerializeField] private ButtonsManager Buttons;
    [ReadOnly, SerializeField] private PagesAndFields PagesAndFields;
    #endregion
    #region DATA_MANAGEMENT
    public const string MALE = "Male";
    public const string FEMALE = "Female";

    public readonly string[] pageKey = new string[] { "name", "genre", "goal", "level", "focus"};
    public readonly string[] footerKey = new string[] { "age", "currentWeight", "weightGoal", "height" };

    public const int PAGE1 = 1;
    public const int PAGE2 = 2;
    public const int PAGE3 = 3;
    public const int PAGE4 = 4;
    public const int PAGE5 = 5;
    public const int PAGE6 = 6;

    public const int FOOTER1 = 0;
    public const int FOOTER2 = 1;
    public const int FOOTER3 = 2;
    public const int FOOTER4 = 3;
    #endregion
    #region DATA_VALUES_MANAGEMENT
    public readonly string[] goalName = new string[] { "Lose Weight", "More Muscles", "Keep Shape" };
    public readonly string[] levelPhrase = LanguageManager.levelPhrase;
    public readonly string[] focusName = new string[] { "Abdomen", "Chest", "Arms", "Legs" };
    #endregion
    #region UNITY_MONOBEHAVIOUR
    public void Awake()
    {
        Buttons = this.GetComponent<ButtonsManager>();
        PagesAndFields = this.GetComponent<PagesAndFields>();

        PagesAndFields.PagesQuestionString = LanguageManager.pageQuestion;
        Buttons.UserData.NextButtonString = LanguageManager.nextButton;
        PagesAndFields.Page1PlaceHolder = LanguageManager.page1PlaceHolder;
        Buttons.UserData.GoalButtonsName = LanguageManager.buttonGoal;
        Buttons.UserData.FocusButtonsName = LanguageManager.buttonFocus;
        PagesAndFields.BlockAreaName = LanguageManager.blockAreaName;
        PagesAndFields.MoreValuesStringEdit = LanguageManager.valueDefaultButtonsEdit;
        PagesAndFields.FooterTitle = LanguageManager.footerTitle;
        PagesAndFields.FooterYerarsString = LanguageManager.valueUnits[0];
        PagesAndFields.FootersPlaceholer = LanguageManager.footerPlaceholder;
        Buttons.UserData.SaveButtonString = LanguageManager.saveButton;
        Buttons.UserData.CancelButtonString = LanguageManager.cancelButton;
    }

    public void Start()
    {
        Buttons.UserData.NextButtonInteraction = false;
        userName = UserName;
        genre = (IsMale ? MALE : FEMALE);
        goal = Goal;
        level = Level;
        focus = Focus;
        age = MoreEdit[0];
        currentWeight = MoreEdit[1];
        weightGoal = MoreEdit[2];
        height = MoreEdit[3];
    }
    #endregion
    #region SAVE_DATA
    public void PagesSavePreferences(int page)
    {
        switch (page)
        {
            case PAGE1:
                UserName = PagesAndFields.UserName;
                Debug.Log("Page1: UserName = " + UserName);
                break;
            case PAGE2:
                IsMale = Buttons.UserData.Genre;
                Debug.Log("Page2: Genre = " + (IsMale ? MALE : FEMALE));
                break;
            case PAGE3:
                Goal = Buttons.UserData.Goal;
                Buttons.UserData.LevelButtonsBehaviour(2);
                Debug.Log("Page3: Goal = " + Goal);
                break;
            case PAGE4:
                Level = Buttons.UserData.Level;
                Debug.Log("Page4: Level = " + Level);
                break;
            case PAGE5:
                Focus = Buttons.UserData.Focus;
                Debug.Log("Page5: Focus = " + Focus);
                break;
            case PAGE6:
                MoreEdit = PagesAndFields.FooterArray;
                Debug.Log("Page6: Age = " + MoreEdit[0]);
                Debug.Log("Page6: Current Weight = " + MoreEdit[1]);
                Debug.Log("Page6: Weight Goal = " + MoreEdit[2]);
                Debug.Log("Page6: Height = " + MoreEdit[3]);
                break;
        }
    }
    #endregion
    #region GET_SET_FUNCTIONS
    public string UserName
    {
        get  => PlayerPrefs.GetString(pageKey[0]);
        private set
        {
            PlayerPrefs.SetString(pageKey[0], value);
            userName = PlayerPrefs.GetString(pageKey[0]);
        }
    }

    public bool IsMale
    {
        get => (GenreByIndex == 0);
        private set
        {
            GenreByIndex = value ? 0 : 1;
            genre = (GenreByIndex == 0) ? MALE : FEMALE;
        }
    }

    private int GenreByIndex
    {
        get => PlayerPrefs.GetInt(pageKey[1]);
        set => PlayerPrefs.SetInt(pageKey[1], value);
    }

    public string Goal
    {
        get => PlayerPrefs.GetString(pageKey[2]);
        private set
        {
            PlayerPrefs.SetString(pageKey[2], value);
            goal = PlayerPrefs.GetString(pageKey[2]);
        }
    }

    public string Level
    {
        get => PlayerPrefs.GetString(pageKey[3]);
        private set
        {
            PlayerPrefs.SetString(pageKey[3], value);
            level = PlayerPrefs.GetString(pageKey[3]);
        }
    }

    public string Focus
    {
        get => PlayerPrefs.GetString(pageKey[4]);
        private set
        {
            PlayerPrefs.SetString(pageKey[4], value);
            focus = PlayerPrefs.GetString(pageKey[4]);
        }
    }

    public string[] MoreEdit
    {
        get
        {
            string[] data;
            data = new string[footerKey.Length];

            for (int i = 0; i < footerKey.Length; i++)
            {
                data[i] = PlayerPrefs.GetString(footerKey[i]);
            }

            return data;
        }

        set
        {
            for (int i = 0; i < footerKey.Length; i++)
            {
                PlayerPrefs.SetString(footerKey[i], value[i]);
            }

            age = MoreEdit[0];
            currentWeight = MoreEdit[1];
            weightGoal = MoreEdit[2];
            height = MoreEdit[3];
        }
    }
    #endregion
}
#endregion