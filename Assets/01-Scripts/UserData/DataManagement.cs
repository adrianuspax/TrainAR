using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DataManagement : MonoBehaviour
{
    [Header("PAGES", order = 0)]
    [Space(-10, order = 1)]
    [Header("Page 1", order = 2)]
    [SerializeField] private TMP_InputField userNameInput; //Isso tem que ir para o Pages And Field

    [Header("READY ONLY", order = 0)]
    [Space(-10, order = 1)]
    [Header("Variables", order = 2)]
    [ReadOnly, SerializeField] private bool userNameInputIsEmpty;
    [ReadOnly, SerializeField] private string userNameString;
    [Space(-10, order = 0)]
    [Header("Scripts", order = 1)]
    [ReadOnly, SerializeField] private ButtonsManager Buttons;

    private const string DATAPAGE1 = "name";
    private const string DATAPAGE2 = "genre";

    public void Awake()
    {
        Buttons = this.GetComponent<ButtonsManager>();

        userNameInput.onSelect.AddListener(OnSelectUserName);
        userNameInput.onDeselect.AddListener(OnDeselectUserName);
        userNameInput.onValueChanged.AddListener(OnValueChangedUserName);
        userNameInput.onEndEdit.AddListener(OnEditEndUserName);
    }

    public void Start()
    {
        Buttons.UserData.NextButtonInteraction = false;
        userNameInputIsEmpty = true;
    }

    public void OnSelectUserName(string name)
    {
        userNameInput.placeholder.GetComponent<TextMeshProUGUI>().text = "";
        userNameInputIsEmpty = true;
    }

    public void OnDeselectUserName(string name)
    {
        userNameInputIsEmpty = (name == "");
        userNameInput.placeholder.GetComponent<TextMeshProUGUI>().text = "Enter your name here...";
        Buttons.UserData.NextButtonInteraction = !userNameInputIsEmpty;
    }

    public void OnValueChangedUserName(string name)
    {
        userNameInputIsEmpty = (name == "");
        Buttons.UserData.NextButtonInteraction = !userNameInputIsEmpty;
    }

    public void OnEditEndUserName(string name)
    {
        Buttons.UserData.NextButtonInteraction = !userNameInputIsEmpty;
        userNameString = name;
    }

    public void PagesSavePreferences(int page)
    {
        switch (page)
        {
            case PagesAndFields.PAGE1:
                UserName = userNameString;
                Debug.Log("Page1: " + "UserName = " + UserName);
                break;
            case PagesAndFields.PAGE2:
                IsMale = Buttons.UserData.GenreButtonSelected;
                Debug.Log("Page2: " + "IsMale = " + IsMale);
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
        set => GenreByIndex = value ? 0 : 1;
    }

    public int GenreByIndex
    {
        set => PlayerPrefs.SetInt(DATAPAGE2, value);
        get => PlayerPrefs.GetInt(DATAPAGE2);
    }
}