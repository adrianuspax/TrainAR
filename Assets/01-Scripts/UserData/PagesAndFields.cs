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

    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("Pages", order = 2)]
    [ReadOnly, SerializeField] private GameObject[] page;
    [Space(-10, order = 0)]
    [Header("Fields", order = 1)]
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

    public const int PAGE1 = 1;
    public const int PAGE2 = 2;
    public const int PAGE3 = 3;
    public const int PAGE4 = 4;
    public const int PAGE5 = 5;
    public const int PAGE6 = 6;

    public const int FOOTER1 = 1;
    public const int FOOTER2 = 2;
    public const int FOOTER3 = 3;
    public const int FOOTER4 = 4;

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

        PageActivation(PAGE1);

        userNameInputIsEmpty = true;
    }
    #region PAGES
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
    public int TotalPages => page.Length;

    public int CurrentPage
    {
        get => currentPage;
        private set => currentPage = value;
    }
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
}