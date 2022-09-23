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
    [Space(-10, order = 0)]
    [Header("Page 6", order = 1)]
    [SerializeField] private Transform blocksAreaHeader;

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
    [ReadOnly, SerializeField] private TextMeshProUGUI[] moreValueTMP;
    [ReadOnly, SerializeField] private TMP_InputField[] footerInput;
    [ReadOnly, SerializeField] private bool[] footerInputIsEmpty;
    [ReadOnly, SerializeField] private string[] footerValue;

    [Header("Scripts", order = 0)]
    [ReadOnly, SerializeField] private AnimationsManager Animations;
    [ReadOnly, SerializeField] private ButtonsManager Buttons;

    private void Awake()
    {
        page = AssignGameObjects(pagesHeader);
        footer = AssignGameObjects(footerHeader);

        userNameInput.onSelect.AddListener(OnSelectUserName);
        userNameInput.onDeselect.AddListener(OnDeselectUserName);
        userNameInput.onValueChanged.AddListener(OnValueChangedUserName);
        userNameInput.onEndEdit.AddListener(OnEndEditUserName);

        moreValueTMP = new TextMeshProUGUI[footerHeader.childCount];
        footerInput = new TMP_InputField[footerHeader.childCount];
        footerInputIsEmpty = new bool[footerHeader.childCount];
        footerValue = new string[footerHeader.childCount];

        for (int i = 0; i < footerHeader.childCount; i++)
        {
            AssignFootersArrays(i);
            AssignFootersListeners(i);
        }

        Animations = this.GetComponent<AnimationsManager>();
        Buttons = this.GetComponent<ButtonsManager>();
    }

    private void Start()
    {
        Animations.UserData.AnimatorsPage = Animations.UserData.AssignAnimatorsPages(page);

        foreach (GameObject pages in page) { pages.SetActive(false); }
        foreach (GameObject footers in footer) { footers.SetActive(false); }

        PageActivation(DataManagement.PAGE1NUM);

        userNameInputIsEmpty = true;
        SetAllFooterInputIsEmpty(true);
        SetTextAllMoreValueTMP("edit");
    }
    #region PAGES_BEHAVIOURS
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

    public void OnEndEditUserName(string name)
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
    #region PAGE5
    public void SetTextAllMoreValueTMP(string setText)
    {
        foreach (TextMeshProUGUI tmp in moreValueTMP)
        {
            tmp.text = setText;
        }
    }
    #endregion
    #region FOOTERS_BEHAVIOURS
    public void FooterActivation(int footerIndexToActive)
    {
        if (footerIndexToActive < TotalFooters && footerIndexToActive >= 0)
        {
            foreach (GameObject footers in footer)
            {
                footers.SetActive(false);
            }

            footer[footerIndexToActive].SetActive(true);
            Animations.UserData.PlayFooterEnterClip();

            CurrentFooter = footerIndexToActive + 1;
        }
        else
        {
            Debug.LogError("Footer Activation: Parameter value out of range. You moke!" + "// footerIndexToActive = " + footerIndexToActive);
        }
    }

    public int TotalFooters => footer.Length;

    public int CurrentFooter
    {
        get => currentFooter;
        private set => currentFooter = value;
    }

    public void SetAllFooterInputIsEmpty(bool allIsEmpty)
    {
        for (int i = 0; i < footerInputIsEmpty.Length; i++)
        {
            footerInputIsEmpty[i] = allIsEmpty;
        }
    }
    #endregion
    #region FOOTERS
    public void OnSelectFooter(string data)
    {
        footerInput[CurrentFooterIndex].placeholder.GetComponent<TextMeshProUGUI>().text = "";
        footerInputIsEmpty[CurrentFooterIndex] = true;
    }

    public void OnDeselectFooter(string data)
    {
        footerInputIsEmpty[CurrentFooterIndex] = (data == "");
        footerInput[CurrentFooterIndex].placeholder.GetComponent<TextMeshProUGUI>().text = "value";
        Buttons.UserData.SaveButtonInteraction = !footerInputIsEmpty[CurrentFooterIndex];
    }

    public void OnValueChangedFooter(string data)
    {
        footerInputIsEmpty[CurrentFooterIndex] = (data == "");
        Buttons.UserData.SaveButtonInteraction = !footerInputIsEmpty[CurrentFooterIndex];
    }

    public void OnEndEditFooter(string data)
    {
        Buttons.UserData.SaveButtonInteraction = !footerInputIsEmpty[CurrentFooterIndex];
        DataFooter = data;
    }

    public string DataFooter
    {
        get => footerValue[CurrentFooterIndex];
        private set => footerValue[CurrentFooterIndex] = value;
    }

    private int CurrentFooterIndex => CurrentFooter - 1;

    public void ClearAllInputFooter()
    {
        foreach (TMP_InputField inputs in footerInput)
        {
            inputs.text = "";
        }
    }

    public void ClearAllInputFooter(float delay)
    {
        StartCoroutine(Cor_ClearAllInputFooter(delay));
    }

    private IEnumerator Cor_ClearAllInputFooter(float time)
    {
        yield return new WaitForSeconds(time);
        ClearAllInputFooter();
    }
    #endregion
    #region ASSIGNMENTS
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

    private void AssignFootersListeners(int index)
    {
        footerInput[index].onSelect.AddListener(OnSelectFooter);
        footerInput[index].onDeselect.AddListener(OnDeselectFooter);
        footerInput[index].onValueChanged.AddListener(OnValueChangedFooter);
        footerInput[index].onEndEdit.AddListener(OnEndEditFooter);
    }

    private void AssignFootersArrays(int index)
    {
        moreValueTMP[index] = blocksAreaHeader.GetChild(index).GetChild(0).GetComponent<TextMeshProUGUI>();
        footerInput[index] = footerHeader.GetChild(index).GetChild(0).GetComponent<TMP_InputField>();
    }
    #endregion
}