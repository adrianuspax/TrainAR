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
    [SerializeField] private TextMeshProUGUI age;
    [SerializeField] private TextMeshProUGUI currentWeight;
    [SerializeField] private TextMeshProUGUI weightGoal;
    [SerializeField] private TextMeshProUGUI height;

    [Header("FOOTERS", order = 0)]
    [Space(-10, order = 1)]
    [Header("Footer 1", order = 2)]
    [SerializeField] private TMP_InputField ageInput;
    [Space(-10, order = 1)]
    [Header("Footer 2", order = 2)]
    [SerializeField] private TMP_InputField currentWeightInput;
    [Space(-10, order = 1)]
    [Header("Footer 3", order = 2)]
    [SerializeField] private TMP_InputField weightGoalInput;
    [Space(-10, order = 1)]
    [Header("Footer 4", order = 2)]
    [SerializeField] private TMP_InputField heightInput;

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
    [ReadOnly, SerializeField] private bool ageInputIsEmpty;
    [ReadOnly, SerializeField] private string ageValue;
    [ReadOnly, SerializeField] private bool currentWeightInputIsEmpty;
    [ReadOnly, SerializeField] private string currentWeightValue;
    [ReadOnly, SerializeField] private bool weightGoalInputIsEmpty;
    [ReadOnly, SerializeField] private string weightGoalValue;
    [ReadOnly, SerializeField] private bool heightInputIsEmpty;
    [ReadOnly, SerializeField] private string heightValue;

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

        ageInput.onSelect.AddListener(OnSelectAge);
        ageInput.onDeselect.AddListener(OnDeselectAge);
        ageInput.onValueChanged.AddListener(OnValueChangedAge);
        ageInput.onEndEdit.AddListener(OnEndEditAge);

        currentWeightInput.onSelect.AddListener(OnSelectCurrentWeight);
        currentWeightInput.onDeselect.AddListener(OnDeselectCurrentWeight);
        currentWeightInput.onValueChanged.AddListener(OnValueChangedCurrentWeight);
        currentWeightInput.onEndEdit.AddListener(OnEndEditCurrentWeight);

        weightGoalInput.onSelect.AddListener(OnSelectWeightGoal);
        weightGoalInput.onDeselect.AddListener(OnDeselectWeightGoal);
        weightGoalInput.onValueChanged.AddListener(OnValueChangedWeightGoal);
        weightGoalInput.onEndEdit.AddListener(OnEndEditWeightGoal);

        heightInput.onSelect.AddListener(OnSelectHeight);
        heightInput.onDeselect.AddListener(OnDeselectHeight);
        heightInput.onValueChanged.AddListener(OnValueChangedHeight);
        heightInput.onEndEdit.AddListener(OnEndEditHeight);

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
        ageInputIsEmpty = true;
        currentWeightInputIsEmpty = true;
        weightGoalInputIsEmpty = true;
        heightInputIsEmpty = true;
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
    #endregion
    #region FOOTER1
    public void OnSelectAge(string age)
    {
        ageInput.placeholder.GetComponent<TextMeshProUGUI>().text = "";
        ageInputIsEmpty = true;
    }

    public void OnDeselectAge(string age)
    {
        ageInputIsEmpty = (age == "");
        ageInput.placeholder.GetComponent<TextMeshProUGUI>().text = "value";
        Buttons.UserData.SaveButtonInteraction = !ageInputIsEmpty;
    }

    public void OnValueChangedAge(string age)
    {
        ageInputIsEmpty = (age == "");
        Buttons.UserData.SaveButtonInteraction = !ageInputIsEmpty;
    }

    public void OnEndEditAge(string age)
    {
        Buttons.UserData.SaveButtonInteraction = !ageInputIsEmpty;
        Age = age;
    }

    public string Age
    {
        get => ageValue + " years";
        private set => ageValue = value;
    }
    #endregion
    #region FOOTER2
    public void OnSelectCurrentWeight(string currentWeight)
    {
        currentWeightInput.placeholder.GetComponent<TextMeshProUGUI>().text = "";
        currentWeightInputIsEmpty = true;
    }

    public void OnDeselectCurrentWeight(string currentWeight)
    {
        currentWeightInputIsEmpty = (currentWeight == "");
        currentWeightInput.placeholder.GetComponent<TextMeshProUGUI>().text = "value";
        Buttons.UserData.SaveButtonInteraction = !currentWeightInputIsEmpty;
    }

    public void OnValueChangedCurrentWeight(string currentWeight)
    {
        //CONTINUAR AQUI
    }

    public void OnEndEditCurrentWeight(string currentWeight)
    {

    }

    public string CurrentWeight
    {
        get => currentWeightValue + " kg";
        private set => currentWeightValue = value;
    }
    #endregion
    #region FOOTER3
    public void OnSelectWeightGoal(string weightGoal)
    {

    }

    public void OnDeselectWeightGoal(string weightGoal)
    {

    }

    public void OnValueChangedWeightGoal(string weightGoal)
    {

    }

    public void OnEndEditWeightGoal(string weightGoal)
    {

    }

    public string WeightGoal
    {
        get => weightGoalValue + " kg";
        private set => weightGoalValue = value;
    }
    #endregion
    #region FOOTER4
    public void OnSelectHeight(string height)
    {

    }

    public void OnDeselectHeight(string height)
    {

    }

    public void OnValueChangedHeight(string height)
    {

    }

    public void OnEndEditHeight(string height)
    {

    }

    public string Height
    {
        get => heightValue + " cm";
        private set => heightValue = value;
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