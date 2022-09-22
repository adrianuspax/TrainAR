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

    private const string PAGE1KEY = "name";
    private const string PAGE2KEY = "genre";
    private const string PAGE3KEY = "goal";
    private const string PAGE4KEY = "level";
    private const string PAGE5KEY = "focus";

    private const string MALE = "Male";
    private const string FEMALE = "Female";


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
            case PagesAndFields.PAGE1INDEX:
                UserName = PagesAndFields.UserName;
                Debug.Log("Page1: " + "UserName = " + userName);
                break;
            case PagesAndFields.PAGE2INDEX:
                IsMale = Buttons.UserData.Genre;
                Debug.Log("Page2: " + "Genre = " + genre);
                break;
            case PagesAndFields.PAGE3INDEX:
                Goal = Buttons.UserData.Goal;
                Buttons.UserData.LevelButtonsBehaviour(2);
                Debug.Log("Page3: " + "Goal = " + goal);
                break;
            case PagesAndFields.PAGE4INDEX:
                Level = Buttons.UserData.Level;
                Debug.Log("Page4: " + "Level = " + level);
                break;
            case PagesAndFields.PAGE5INDEX:
                Focus = Buttons.UserData.Focus;
                Debug.Log("Page4: " + "Level = " + focus);
                break;
            case PagesAndFields.PAGE6INDEX:

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
            GenreByIndex = value ? PagesAndFields.M : PagesAndFields.F;
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