using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PagesAndFields
    : MonoBehaviour
{
    [Header("HEADERS", order = 0)]
    [Space(-10, order = 1)]
    [Header("Header Pages", order = 2)]
    [SerializeField] private Transform pagesHeader;
    [Space(-10, order = 0)]
    [Header("Header Fields", order = 1)]
    [SerializeField] private Transform footerHeader;

    [Header("PAGES", order = 0)]
    [Space(-10, order = 1)]
    [Header("Page 1", order = 2)]
    [SerializeField] private TMP_InputField userNameInput;
    [Space(-10, order = 0)]
    [Header("Page 4", order = 1)]
    [SerializeField] private TextMeshProUGUI nameLevel;

    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("Pages", order = 2)]
    [ReadOnly, SerializeField] private GameObject[] page;
    [Space(-10, order = 0)]
    [Header("Footers", order = 1)]
    [ReadOnly, SerializeField] private GameObject[] footer;
    [Space(-10, order = 0)]
    [Header("Variables", order = 1)]
    [ReadOnly, SerializeField] private int currentPage;
    [ReadOnly, SerializeField] private int currentFooter;
    [ReadOnly, SerializeField] private bool userNameInputIsEmpty;
    [ReadOnly, SerializeField] private string userNameString;

    [Header("Scripts", order = 0)]
    [ReadOnly, SerializeField] private AnimationsManager Animations;
    [ReadOnly, SerializeField] private ButtonsManager Buttons;

    public const int PAGE1INDEX = 1;
    public const int PAGE2INDEX = 2;
    public const int PAGE3INDEX = 3;
    public const int PAGE4INDEX = 4;
    public const int PAGE5INDEX = 5;
    public const int PAGE6INDEX = 6;

    public const int FOOTER1INDEX = 1;
    public const int FOOTER2INDEX = 2;
    public const int FOOTER3INDEX = 3;
    public const int FOOTER4INDEX = 4;

    public const int M = 0;
    public const int F = 1;

    public const int LOSEWEIGHT = 0;
    public const int MOREMUSCLES = 1;
    public const int KEEPSHAPE = 2;

    public const string LEVEL1 = "I'm sedentary!";
    public const string LEVEL2 = "I'm walker!";
    public const string LEVEL3 = "I'm runner!";
    public const string LEVEL4 = "I'm athletic!";
    public const string LEVEL5 = "I'm body builder!";

    public const string FOCUSNAME0 = "Abdomen";
    public const string FOCUSNAME1 = "Chest";
    public const string FOCUSNAME2 = "Arms";
    public const string FOCUSNAME3 = "Legs";

    private void Awake()
    {
        page = AssignGameObjects(pagesHeader);
        footer = AssignGameObjects(footerHeader);

        userNameInput.onSelect.AddListener(OnSelectUserName);
        userNameInput.onDeselect.AddListener(OnDeselectUserName);
        userNameInput.onValueChanged.AddListener(OnValueChangedUserName);
        userNameInput.onEndEdit.AddListener(OnEditEndUserName);

        Animations = this.GetComponent<AnimationsManager>();
        Buttons = this.GetComponent<ButtonsManager>();
    }

    private void Start()
    {
        Animations.UserData.AnimatorsPage = Animations.UserData.AssignAnimatorsPages(page);

        foreach (GameObject pages in page) { pages.SetActive(false); }
        foreach (GameObject footers in footer) { footers.SetActive(false); }

        PageActivation(PAGE1INDEX);

        userNameInputIsEmpty = true;
    }
    #region PAGES
    #region BEHAVIOURS
    public void PageActivation(int pageNumberToActive)
    {
        StartCoroutine(Cor_PageActivation(pageNumberToActive));
        CurrentPage = pageNumberToActive;
    }

    private IEnumerator Cor_PageActivation(int pageNumber)
    {
        int index;
        index = pageNumber - 1;

        if (index < TotalPages && index >= 0)
        {
            if (pageNumber > 1)
            {
                Animations.UserData.PlayPageOutAnimation(index);
                yield return new WaitForSeconds(Animations.UserData.OutClipTime * 0.5f);
            }

            page[index].SetActive(true);

            yield return new WaitForSeconds(Animations.UserData.EnterClipTime);

            for (int i = 0; i < index; i++)
            {
                page[i].SetActive(false);
            }

            for (int j = TotalPages - 1; j > index; j--)
            {
                page[j].SetActive(false);
            }
        }
        else
        {
            Debug.LogError("Page Activation: Parameter value out of range. You moke!");
        }
    }

    public int TotalPages => page.Length;
    public int CurrentPage
    {
        get => currentPage;
        private set => currentPage = value;
    }
    #endregion
    #region PAGE1
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
        UserName = name;
    }

    public string UserName
    {
        get => userNameString;
        private set => userNameString = value;
    }
    #endregion
    #region PAGE4
    public string LevelName
    {
        get => nameLevel.text;
        set => nameLevel.text = value;
    }
    #endregion
    #endregion
    #region FOOTERS
    public void FooterActivation(int footerNumberToActive)
    {
        int index;
        index = footerNumberToActive - 1;

        if (index < TotalFooters && index >= 0)
        {
            foreach (GameObject footers in footer)
            {
                footers.SetActive(false);
            }

            footer[index].SetActive(true);
            Animations.UserData.PlayFooterEnterClip();

            CurrentFooter = footerNumberToActive;
        }
        else
        {
            Debug.LogError("Footer Activation: Parameter value out of range. You moke!" + "// footerNumberToActive = " + footerNumberToActive);
        }
    }

    public int TotalFooters => footer.Length;

    public int CurrentFooter
    {
        get => currentFooter;
        private set => currentFooter = value;
    }
    #endregion
    #region ASSIGNMENT
    private GameObject[] AssignGameObjects(Transform header)
    {
        GameObject[] gameObjects;

        gameObjects = new GameObject[header.childCount];

        for (int i = 0; i < header.childCount; i++)
        {
            gameObjects[i] = header.GetChild(i).gameObject;
        }

        return gameObjects;
    }
    #endregion
}