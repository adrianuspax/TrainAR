#region USED_NAMESPACES
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
#endregion
#region CLASS
public class PagesAndFields : MonoBehaviour
{
    #region UNITY_INSPECTOR
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
    #endregion
    #region READY_INSPECTOR
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
    #endregion
    public delegate void BehaviourButtons(int index);
    #region UNITY_MONOBEHAVIOUR
    private void Awake()
    {
        page = AssignGameObjects(pagesHeader);
        footer = AssignGameObjects(footerHeader);

        userNameInput.onSelect.AddListener(OnSelectUserName);
        userNameInput.onDeselect.AddListener(OnDeselectUserName);
        userNameInput.onValueChanged.AddListener(OnValueChangedUserName);
        userNameInput.onEndEdit.AddListener(OnEndEditUserName);

        footerInputIsEmpty = new bool[footerHeader.childCount];
        footerValue = new string[footerHeader.childCount];

        footerInput = FooterInputs;
        moreValueTMP = AssignTMPsEditValue;

        for (int i = 0; i < footerHeader.childCount; i++)
        {
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

        PageActivation(DataManagement.PAGE1);

        userNameInputIsEmpty = true;
        SetAllFooterInputIsEmpty(true);
    }
    #endregion
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
    public Transform[] Page
    {
        get
        {
            Transform[] pageT;
            pageT = new Transform[TotalPages];

            for (int i = 0; i < TotalPages; i++)
            {
                pageT[i] = page[i].transform;
            }

            return pageT;
        }
    }
    
    public string[] PagesQuestionString
    {
        set
        {
            for (int i = 0; i < value.Length; i++)
            {
                Page[i].GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = value[i];
            }
        }
    }

    #endregion
    #region PAGE1
    public void OnSelectUserName(string name)
    {
        Page1PlaceHolder = "";
        userNameInputIsEmpty = true;
    }

    public void OnDeselectUserName(string name)
    {
        userNameInputIsEmpty = (name == "");
        Page1PlaceHolder = LanguageManager.page1PlaceHolder;
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
    public string Page1PlaceHolder
    {
        set
        {
            userNameInput.placeholder.GetComponent<TextMeshProUGUI>().text = value;
        }
    }
    #endregion
    #region PAGE4
    public string LevelName
    {
        get => nameLevel.text;
        set => nameLevel.text = value;
    }
    #endregion
    #region PAGE6
    
    public string MoreValuesStringEdit
    {
        set
        {
            foreach (TextMeshProUGUI tmp in moreValueTMP)
            {
                tmp.text = value;
            }
        }
    }

    public int TotalBlocksArea => blocksAreaHeader.childCount;

    public TextMeshProUGUI[] AssignTMPsEditValue
    {
        get
        {
            TextMeshProUGUI[] tmp;
            tmp = new TextMeshProUGUI[blocksAreaHeader.childCount];

            for (int i = 0; i < TotalBlocksArea; i++)
            {
                tmp[i] = blocksAreaHeader.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();
            }

            return tmp;
        }
    }

    public string[] BlockAreaName
    {
        set
        {
            for (int i = 0; i < value.Length; i++)
            {
                blocksAreaHeader.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = value[i];
            }
        }
    }

    public (Button[], bool[]) AssignmentEditMore(params BehaviourButtons[] behaviour)
    {
        Button[] button;
        bool[] itsEdited;
        button = new Button[TotalBlocksArea];
        itsEdited = new bool[TotalBlocksArea];

        for (int index = 0; index < button.Length; index++)
        {
            for (int i = 0; i < behaviour.Length; i++)
            {
                StartCoroutine(Cor_AssignmentEditMore(index, button, behaviour[i]));
            }
        }

        return (button, itsEdited);
    }

    private IEnumerator Cor_AssignmentEditMore(int index, Button[] button, BehaviourButtons behaviour)
    {
        Transform child;
        child = blocksAreaHeader.GetChild(index);

        for (int i = 0; i < child.childCount; i++)
        {
            if (child.GetChild(i).GetComponent<Button>() != null)
            {
                button[index] = child.GetChild(i).GetComponent<Button>();
                button[index].onClick.AddListener(() => behaviour(index));
            }
        }

        yield return null;
    }

    public void SetMoreValueTMP(int index, string data)
    {
        moreValueTMP[index].text = data + LanguageManager.valueUnits[index];
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
        footerInput[CurrentFooterIndex].placeholder.GetComponent<TextMeshProUGUI>().text = LanguageManager.footerPlaceholder;
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

    public string[] FooterArray => footerValue;

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

    public string[] FooterTitle
    {
        set
        {
            for (int i = 0; i < value.Length; i++)
            {
                footer[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = value[i];
            }
        }
    }

    public string FooterYerarsString
    {
        set
        {
            footer[0].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = value;
        }
    }

    public string FootersPlaceholer
    {
        set
        {
            for (int i = 0; i < footerInput.Length; i++)
            {
                footerInput[i].placeholder.GetComponent<TextMeshProUGUI>().text = value;
            }
        }
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

    public TMP_InputField[] FooterInputs
    {
        get
        {
            TMP_InputField[] inputs;
            inputs = new TMP_InputField[footerHeader.childCount];
            Transform child;

            for (int i = 0; i < footerHeader.childCount; i++)
            {
                child = footerHeader.GetChild(i);

                for (int j = 0; j < child.childCount; j++)
                {
                    if (child.GetChild(j).GetComponent<TMP_InputField>() != null)
                    {
                        inputs[i] = child.GetChild(j).GetComponent<TMP_InputField>();
                    }
                }
            }

            return inputs;
        }
    }
    #endregion
}
#endregion