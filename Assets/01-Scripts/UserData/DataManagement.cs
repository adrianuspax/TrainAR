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


    [Header("Scripts", order = 0)]
    [ReadOnly, SerializeField] private ButtonsManager Buttons;
    [ReadOnly, SerializeField] private PagesAndFields PagesAndFields;

    private const string DATAPAGE1 = "name";
    private const string DATAPAGE2 = "genre";
    private const string DATAPAGE3 = "goal";

    private const string M = "Male";
    private const string F = "Female";


    public void Awake()
    {
        Buttons = this.GetComponent<ButtonsManager>();
        PagesAndFields = this.GetComponent<PagesAndFields>();
    }

    public void Start()
    {
        Buttons.UserData.NextButtonInteraction = false;
        userName = UserName;
        genre = IsMale ? M : F;
        goal = Goal;
    }

    public void PagesSavePreferences(int page)
    {
        switch (page)
        {
            case PagesAndFields.PAGE1:
                UserName = PagesAndFields.UserName;
                userName = UserName;
                Debug.Log("Page1: " + "UserName = " + userName);
                break;
            case PagesAndFields.PAGE2:
                IsMale = Buttons.UserData.GenreButtonSelected;
                genre = IsMale ? M : F;
                Debug.Log("Page2: " + "Genre = " + genre);
                break;
            case PagesAndFields.PAGE3:
                Goal = Buttons.UserData.Goal;
                goal = Goal;
                Debug.Log("Page3: " + "Goal = " + goal);
                break;
            case PagesAndFields.PAGE4:

                break;
            case PagesAndFields.PAGE5:

                break;
            case PagesAndFields.PAGE6:

                break;
        }
    }

    public string UserName
    {
        get => PlayerPrefs.GetString(DATAPAGE1);
        private set => PlayerPrefs.SetString(DATAPAGE1, value);
    }

    public bool IsMale
    {
        get => GenreByIndex == 0;
        private set => GenreByIndex = value ? UserDataButtons.M : UserDataButtons.F;
    }

    public int GenreByIndex
    {
        get => PlayerPrefs.GetInt(DATAPAGE2);
        private set => PlayerPrefs.SetInt(DATAPAGE2, value);
    }

    public string Goal
    {
        get => PlayerPrefs.GetString(DATAPAGE3);
        private set => PlayerPrefs.SetString(DATAPAGE3, value);
    }
}