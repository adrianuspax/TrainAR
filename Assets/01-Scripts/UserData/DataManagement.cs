using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagement : MonoBehaviour
{
    [Header("READY ONLY", order = 0)]
    [Space(-10, order = 1)]
    [Header("SAVED DATA", order = 2)]
    [ReadOnly, SerializeField] private string userName;
    [ReadOnly, SerializeField] private string genre;
    [Space(-10, order = 0)]
    [Header("Scripts", order = 1)]
    [ReadOnly, SerializeField] private ButtonsManager Buttons;
    [ReadOnly, SerializeField] private PagesAndFields PagesAndFields;

    private const string DATAPAGE1 = "name";
    private const string DATAPAGE2 = "genre";

    public void Awake()
    {
        Buttons = this.GetComponent<ButtonsManager>();
        PagesAndFields = this.GetComponent<PagesAndFields>();
    }

    public void Start()
    {
        Buttons.UserData.NextButtonInteraction = false;
        userName = UserName;
        genre = IsMale ? "M" : "F";
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
                genre = IsMale ? "M" : "F";
                Debug.Log("Page2: " + "Genre = " + genre);
                break;
            case PagesAndFields.PAGE3:

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
        private set => GenreByIndex = value ? 0 : 1;
    }

    public int GenreByIndex
    {
        get => PlayerPrefs.GetInt(DATAPAGE2);
        private set => PlayerPrefs.SetInt(DATAPAGE2, value);
    }
}