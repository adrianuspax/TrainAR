using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Language
{
    EN = 0,
    KO = 1,
    BR = 2
};
[RequireComponent(typeof(ButtonsManager))]
public class LanguageManager : MonoBehaviour
{
    #region UNITY_INSPECTOR
    [Header(" BUTTONS ", order = 0)]
    [SerializeField] private Transform buttonsLangHeader;
    [SerializeField] private Button okButton;
    [SerializeField] private Button closeButton;

    #endregion
    #region READY_INSPECTOR
    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("LANGUAGE", order = 2)]
    [ReadOnly, SerializeField] private Language lang;
                               private static Language langStatic;
    [Space(-10, order = 0)]
    [Header("BUTTONS", order = 1)]
    [NonReorderable][ReadOnly, SerializeField] private Button[] langButton;
    [NonReorderable][ReadOnly, SerializeField] private bool[] langPressed;

    [Header("Scripts", order = 0)]
    [ReadOnly, SerializeField] private ButtonsManager Buttons;
    #endregion
    #region STATIC_STRINGS
    public static string[] pageQuestion;
    public static string nextButton;
    public static string page1PlaceHolder;
    public static string[] buttonGoal;
    public static string[] levelPhrase;
    public static string[] buttonFocus;
    public static string[] blockAreaName;
    public static string valueDefaultButtonsEdit;
    public static string[] footerTitle;
    public static string footerPlaceholder;
    public static string[] valueUnits;
    public static string saveButton;
    public static string cancelButton;
    #endregion
    #region ENGLISH
    private readonly static string[] pageQuestionEN = new string[]
    {
        "What is your name?", "What is your gender?", "What is your goal?", "What's your fitness level?", "What is your focus?", "More about you!"
    };
    private readonly static string nextButtonEN = 
        "Next";
    private readonly static string page1PlaceHolderEN = 
        "Enter your name here...";
    private readonly static string[] buttonGoalEN = new string[]
    {
        "Lose weight", "More muscle", "Keep shape"
    };
    private readonly static string[] levelPhraseEN = new string[]
    {
        "I'm sedentary!", "I'm walker!", "I'm runner!", "I'm athletic!", "I'm body builder!"
    };
    private readonly static string[] buttonFocusEN = new string[]
    {
        "Abdomen", "Chest", "Arms", "Legs"
    };
    private readonly static string[] blockAreaNameEN = new string[]
    {
        "Age", "Current weight", "Weight goal", "Height"
    };
    private readonly static string valueDfaultButtonsEditEN = 
        "edit";
    private readonly static string[] footerTitleEN = new string[]
    {
        "Enter your age", "Enter your current weight", "Enter your desired weight", "Enter your height"
    };
    private readonly static string footerPlaceholderEN = 
        "value";
    private readonly static string[] footerUnitEN = new string[]
    {
        " Years", " kg", " kg", " cm"
    };
    private readonly static string saveButtonEN = 
        "Save";
    private readonly static string cancelButtonEN = 
        "Cancel";
    #endregion
    #region KOREAN
    private readonly static string[] pageQuestionKO = new string[]
    {
        "이름을 입력하세요", "성별을 입력하세요", "목적을 선택하세요", "체력수준  선택", "집중적 운동 부위", "추가 정보 입력"
    };
    private readonly static string nextButtonKO =
        "다음";
    private readonly static string page1PlaceHolderKO =
        "여기에 입력하세요";
    private readonly static string[] buttonGoalKO = new string[]
    {
        "체중감량", "근력강화", "몸매가꾸기"
    };
    private readonly static string[] levelPhraseKO = new string[]
    {
        "운동거의 안함", "필수적인 걷기운동만", "러닝과 체조정도 운동만", "운동량이 충분히 많음", "전문적인 운동선수 급"
    };
    private readonly static string[] buttonFocusKO = new string[]
    {
        "복근", "가슴부위", "팔 부위", "다리부위"
    };
    private readonly static string[] blockAreaNameKO = new string[]
    {
        "나이", "현재 체중", "목표체중", "신장"
    };
    private readonly static string valueDfaultButtonsEditKO =
        "수정";
    private readonly static string[] footerTitleKO = new string[]
    {
        "나이를 입력하세요", "현재 체중 입력", "현재 체중 입력", "본인 키 입력"
    };
    private readonly static string footerPlaceholderKO =
        "값";
    private readonly static string[] footerUnitKO = new string[]
    {
        " 세", " kg", " kg", " cm"
    };
    private readonly static string saveButtonKO =
        "저장";
    private readonly static string cancelButtonKO =
        "취소";
    #endregion
    #region PORTUGUESE_BRAZILIAN
    private readonly static string[] pageQuestionBR = new string[]
    {
        "Qual é o seu nome?", "Qual é o seu sexo?", "Qual é o seu objetivo?", "Qual o seu nível físico?", "Qual o seu foco?", "Mais sobre você:"
    };
    private readonly static string nextButtonBR =
        "Próximo";
    private readonly static string page1PlaceHolderBR =
        "Insira o seu nome aqui...";
    private readonly static string[] buttonGoalBR = new string[]
    {
        "Perder peso", "Ter músculos", "Manter a forma"
    };
    private readonly static string[] levelPhraseBR = new string[]
    {
        "Estou sedentário", "Eu caminho", "Eu corro", "Sou atético", "Sou Body Builder"
    };
    private readonly static string[] buttonFocusBR = new string[]
    {
        "Abdomen", "Peito", "Braços", "Pernas"
    };
    private readonly static string[] blockAreaNameBR = new string[]
    {
        "Idade", "Peso atual", "Peso desejado", "Altura"
    };
    private readonly static string valueDfaultButtonsEditBR = 
        "edit";
    private readonly static string[] footerTitleBR = new string[]
    {
        "Insira a sua idade", "Insira o seu peso atual", "Insira o peso desejado", "Insira a sua altura"
    };
    private readonly static string footerPlaceholderBR = 
        "valor";
    private readonly static string[] footerUnitBR = new string[]
    {
        " anos", " kg", " kg", " cm"
    };
    private readonly static string saveButtonBR = 
        "Salvar";
    private readonly static string cancelButtonBR = 
        "Cancelar";
    #endregion

    private void Awake()
    {
        Buttons = this.GetComponent<ButtonsManager>();

        (langButton, langPressed) = Buttons.Assignment(buttonsLangHeader, LangButtonsBehaviour);
        okButton.onClick.AddListener(OkFunction);
        closeButton.onClick.AddListener(ButtonsManager.CloseApplication);
    }

    private void Start()
    {
        LangButtonsBehaviour(DataManagement.Language);
    }

    public void LangButtonsBehaviour(int index)
    {
        langPressed[index] = !langPressed[index];

        Image ring;
        Shadow shadow;

        for (int i = 0; i < langButton.Length; i++)
        {
            if (i == index && langPressed[index])
            {
                ring = langButton[index].transform.GetChild(1).GetComponent<Image>();
                shadow = langButton[index].transform.GetChild(1).GetComponent<Shadow>();
                langButton[index].image.color = Color.white;
                ring.color = PalleteColors.Main;
                shadow.effectDistance = new Vector2(1, -1);
            }
            else
            {
                ring = langButton[i].transform.GetChild(1).GetComponent<Image>();
                shadow = langButton[i].transform.GetChild(1).GetComponent<Shadow>();
                langButton[i].image.color = PalleteColors.BlueGrey(3);
                ring.color = PalleteColors.BlueGray_WhiteBlue;
                shadow.effectDistance = new Vector2(7, -1);
                langPressed[i] = false;
            }
        }

        Language = (Language)index;
        lang = langStatic;
    }

    public void OkFunction()
    {
        DataManagement.Language = (int)Language;
        LoadLang(DataManagement.Language);
        MyScenes.LoadUserData();
    }

    public static void LoadLang(int index)
    {
        Language = (Language)index;

        switch (Language)
        {
            case Language.EN:
                pageQuestion = pageQuestionEN;
                nextButton = nextButtonEN;
                page1PlaceHolder = page1PlaceHolderEN;
                buttonGoal = buttonGoalEN;
                levelPhrase = levelPhraseEN;
                buttonFocus = buttonFocusEN;
                blockAreaName = blockAreaNameEN;
                valueDefaultButtonsEdit = valueDfaultButtonsEditEN;
                footerTitle = footerTitleEN;
                footerPlaceholder = footerPlaceholderEN;
                valueUnits = footerUnitEN;
                saveButton = saveButtonEN;
                cancelButton = cancelButtonEN;
                break;
            case Language.KO:
                pageQuestion = pageQuestionKO;
                nextButton = nextButtonKO;
                page1PlaceHolder = page1PlaceHolderKO;
                buttonGoal = buttonGoalKO;
                levelPhrase = levelPhraseKO;
                buttonFocus = buttonFocusKO;
                blockAreaName = blockAreaNameKO;
                valueDefaultButtonsEdit = valueDfaultButtonsEditKO;
                footerTitle = footerTitleKO;
                footerPlaceholder = footerPlaceholderKO;
                valueUnits = footerUnitKO;
                saveButton = saveButtonKO;
                cancelButton = cancelButtonKO;
                break;
            case Language.BR:
                pageQuestion = pageQuestionBR;
                nextButton = nextButtonBR;
                page1PlaceHolder = page1PlaceHolderBR;
                buttonGoal = buttonGoalBR;
                levelPhrase = levelPhraseBR;
                buttonFocus = buttonFocusBR;
                blockAreaName = blockAreaNameBR;
                valueDefaultButtonsEdit = valueDfaultButtonsEditBR;
                footerTitle = footerTitleBR;
                footerPlaceholder = footerPlaceholderBR;
                valueUnits = footerUnitBR;
                saveButton = saveButtonBR;
                cancelButton = cancelButtonBR;
                break;
        }
    }

    public static Language Language
    {
        get
        {
            return langStatic;
        }

        private set
        {
            langStatic = value;
        }
    }
}