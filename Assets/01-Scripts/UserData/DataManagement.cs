using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagement : MonoBehaviour
{
    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("SAVED DATA", order = 2)]
    [ReadOnly, SerializeField] private string userName;
    [ReadOnly, SerializeField] private string genre;
    [ReadOnly, SerializeField] private string goal;
    [ReadOnly, SerializeField] private string level;
    [ReadOnly, SerializeField] private string focus;

    [Header("Scripts", order = 0)]
    [ReadOnly, SerializeField] private ButtonsManager Buttons;
    [ReadOnly, SerializeField] private PagesAndFields PagesAndFields;
    #region DATA_MANAGEMENT
    private const string PAGE1KEY = "name";
    private const string PAGE2KEY = "genre";
    private const string PAGE3KEY = "goal";
    private const string PAGE4KEY = "level";
    private const string PAGE5KEY = "focus";
    private const string PAGEK6EY = "more";

    private const string MALE = "Male";
    private const string FEMALE = "Female";

    public const int PAGE1NUM = 1;
    public const int PAGE2NUM = 2;
    public const int PAGE3NUM = 3;
    public const int PAGE4NUM = 4;
    public const int PAGE5NUM = 5;
    public const int PAGE6NUM = 6;

    private const int FOOTER1INDEX = 1;
    private const int FOOTER2INDEX = 2;
    private const int FOOTER3INDEX = 3;
    private const int FOOTER4INDEX = 4;
    #endregion
    #region DATA_VALUES_MANAGEMENT
    public const string GOAL0 = "Lose Weight";
    public const string GOAL1 = "More Muscles";
    public const string GOAL2 = "Keep Shape";
    public const int TOTALGOALS = 3;

    public const string LEVEL1 = "I'm sedentary!";
    public const string LEVEL2 = "I'm walker!";
    public const string LEVEL3 = "I'm runner!";
    public const string LEVEL4 = "I'm athletic!";
    public const string LEVEL5 = "I'm body builder!";
    public const int TOTALLEVELS = 5;

    public const string FOCUSNAME0 = "Abdomen";
    public const string FOCUSNAME1 = "Chest";
    public const string FOCUSNAME2 = "Arms";
    public const string FOCUSNAME3 = "Legs";
    public const int TOTALFOCUS = 4;
    #endregion

    public void Awake()
    {
        Buttons = this.GetComponent<ButtonsManager>();
        PagesAndFields = this.GetComponent<PagesAndFields>();
    }

    public void Start()
    {
        Buttons.UserData.NextButtonInteraction = false;
        userName = UserName;
        genre = (IsMale ? MALE : FEMALE);
        goal = Goal;
        level = Level;
        focus = Focus;
    }

    public void PagesSavePreferences(int page)
    {
        switch (page)
        {
            case PAGE1NUM:
                UserName = PagesAndFields.UserName;
                Debug.Log("Page1: " + "UserName = " + userName);
                break;
            case PAGE2NUM:
                IsMale = Buttons.UserData.Genre;
                Debug.Log("Page2: " + "Genre = " + genre);
                break;
            case PAGE3NUM:
                Goal = Buttons.UserData.Goal;
                Buttons.UserData.LevelButtonsBehaviour(2);
                Debug.Log("Page3: " + "Goal = " + goal);
                break;
            case PAGE4NUM:
                Level = Buttons.UserData.Level;
                Debug.Log("Page4: " + "Level = " + level);
                break;
            case PAGE5NUM:
                Focus = Buttons.UserData.Focus;
                Debug.Log("Page4: " + "Level = " + focus);
                break;
            case PAGE6NUM:

                break;
        }
    }

    public string UserName
    {
        get  => PlayerPrefs.GetString(PAGE1KEY);
        private set
        {
            PlayerPrefs.SetString(PAGE1KEY, value);
            userName = PlayerPrefs.GetString(PAGE1KEY);
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
        get => PlayerPrefs.GetInt(PAGE2KEY);
        set => PlayerPrefs.SetInt(PAGE2KEY, value);
    }

    public string Goal
    {
        get => PlayerPrefs.GetString(PAGE3KEY);
        private set
        {
            PlayerPrefs.SetString(PAGE3KEY, value);
            goal = PlayerPrefs.GetString(PAGE3KEY);
        }
    }

    public string Level
    {
        get => PlayerPrefs.GetString(PAGE4KEY);
        private set
        {
            PlayerPrefs.SetString(PAGE4KEY, value);
            level = PlayerPrefs.GetString(PAGE4KEY);
        }
    }

    public string Focus
    {
        get => PlayerPrefs.GetString(PAGE5KEY);
        private set
        {
            PlayerPrefs.SetString(PAGE5KEY, value);
            focus = PlayerPrefs.GetString(PAGE5KEY);
        }
    }
}