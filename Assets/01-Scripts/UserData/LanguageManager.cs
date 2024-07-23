using UnityEngine;
using UnityEngine.UI;
using ASP;
using Debug = ASP.Debug;

public class LanguageManager : MonoBehaviour
{
    public enum Lang
    {
        EN = 0,
        KO = 1,
        BR = 2
    };

    [Header(" BUTTONS ", order = 0)]
    [SerializeField] private Transform buttonsLangHeader;
    [SerializeField] private Button okButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button returnButton;

    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("LANGUAGE", order = 2)]
    [ReadOnly, SerializeField] private Lang lang;
                               private static Lang langStatic;
    [Space(-10, order = 0)]
    [Header("BUTTONS", order = 1)]
    [NonReorderable][ReadOnly, SerializeField] private Button[] langButton;
    [NonReorderable][ReadOnly, SerializeField] private bool[] langPressed;

    [Header("Scripts", order = 0)]
    [SerializeField, ReadOnly] private Transition transition;

    ///pages and fields
    public static string[] pageQuestion;
    public static string nextButton;
    public static string page1PlaceHolder;
    public static string[] goalPhrase;
    public static string[] levelPhrase;
    public static string[] buttonFocus;
    public static string[] blockAreaName;
    public static string valueDefaultButtonsEdit;
    public static string[] footerTitle;
    public static string footerPlaceholder;
    public static string[] valueUnits;
    public static string saveButton;
    public static string cancelButton;
    ///Login
    public static string id;
    public static string password;
    public static string placeHolderID;
    public static string placeHolderPassword;
    public static string signIn;
    public static string signOn;
    public static string fieldEmptyWarning;
    public static string incorrectWarning;
    ///edit
    public static string newID;
    public static string newPassword;
    public static string repeatPassword;
    public static string placeHolderNewID;
    public static string placeHolderNewPassword;
    public static string placeHolderRepeatPassword;
    public static string alreadyIDWarning;
    public static string passwordsDontMatch;
    public static string okButtonString;
    ///menu
    public static string signedInAs;
    public static string updateProfile;
    public static string editAccount;
    public static string language;
    public static string deleteAccount;
    public static string signOut;
    public static string areYouSure;
    public static string yes;
    public static string nop;
    ///default
    public static string _name;
    public static string age;
    public static string weight;
    public static string height;
    public static string gender;
    public static string level;
    public static string male;
    public static string female;
    public static string[] statusBMI;
    public static string yourData;
    public static string calculation;
    public static string target;
    public static string bodyFat;
    public static string totalFat;
    public static string goal;
    public static string toLoseFat;
    public static string toBurnCal;

    #region ENGLISH
    private readonly static string nameEN = 
        "Name";
    private readonly static string ageEN =
        "Age";
    private readonly static string weightEN =
        "Weight";
    private readonly static string heightEN =
        "Height";
    private readonly static string genderEN =
        "Gender";
    private readonly static string levelEN =
        "Level";
    private readonly static string maleEN =
        "Male";
    private readonly static string femaleEN =
        "Female";
    private readonly static string[] statusBMIEN = new string[]
    { "Lean", "Normal", "Overweight", "Fat" };
    private readonly static string yourDataEN =
        "Your Data";
    private readonly static string calculationEN =
        "Calculation";
    private readonly static string targetEN =
        "Target";
    private readonly static string bodyFatEN =
        "Body Fat";
    private readonly static string totalFatEN =
        "Total Fat";
    private readonly static string goalEN =
        "Goal";
    private readonly static string toLoseFatEN =
        "To Lose Fat";
    private readonly static string toBurnCalEN =
        "To Burn Calories";
    /* ======================================================================================================================================= */
    private readonly static string idEN =
        "ID";
    private readonly static string passwordEN =
        "Password";
    private readonly static string placeHolderIDEN =
        "Enter username";
    private readonly static string placeHolderPasswordEN =
        "Enter password";
    private readonly static string signInEN =
        "Sign in";
    private readonly static string signOnEN =
        "Sign up";
    private readonly static string fieldEmptyWarningEN =
        "There are empty fields!";
    private readonly static string incorrectWarningEN =
        "Wrong username or password!";
    /* ======================================================================================================================================= */
    private readonly static string newIDEN =
        "Enter new username:";
    private readonly static string newPasswordEN =
        "Enter new password:";
    private readonly static string repeatPasswordEN =
        "Repeat new password:";
    private readonly static string placeHolderNewIDEN =
        "New username";
    private readonly static string placeHolderNewPasswordEN =
        "New password";
    private readonly static string placeHolderRepeatPasswordEN =
        "Repeat password";
    private readonly static string alreadyIDWarningEN =
        "This username is already used!";
    private readonly static string passwordsDontMatchEN =
        "Passwords don't match!";
    private readonly static string okButtonEN =
        "OK";
    /* ======================================================================================================================================= */
    private readonly static string signedInAsEN = 
        "Signed in as";
    private readonly static string updateProfileEN = 
        "Update Profile";
    private readonly static string editAccountEN = 
        "Edit Account";
    private readonly static string languageEN = 
        "Language";
    private readonly static string deleteAccountEN = 
        "Delete Account";
    private readonly static string signOutEN = 
        "Sign out";
    private readonly static string areYouSureEN =
        "Are you sure you want to cancel the account?";
    private readonly static string yesEN = 
        "Yes";
    private readonly static string nopEN = 
        "No";
    /* ======================================================================================================================================= */
    private readonly static string[] pageQuestionEN = new string[]
    { "What is your name?", "What is your gender?", "What is your goal?", "What'sec your fitness level?", "What is your focus?", "More about you!" };
    private readonly static string nextButtonEN = 
        "Next";
    private readonly static string page1PlaceHolderEN = 
        "Enter your name here...";
    private readonly static string[] buttonGoalEN = new string[]
    { "Lose weight", "More muscle", "Keep shape" };
    private readonly static string[] levelPhraseEN = new string[]
    { "Sedentary!", "Walker!", "Rrunner!", "Athletic!", "Body builder!" };
    private readonly static string[] buttonFocusEN = new string[]
    { "Abdomen", "Chest", "Arms", "Legs" };
    private readonly static string[] blockAreaNameEN = new string[]
    { "Age", "Current weight", "Weight goal", "Height" };
    private readonly static string valueDfaultButtonsEditEN = 
        "edit";
    private readonly static string[] footerTitleEN = new string[]
    { "Enter your age", "Enter your current weight", "Enter your desired weight", "Enter your height" };
    private readonly static string footerPlaceholderEN = 
        "value";
    private readonly static string[] footerUnitEN = new string[]
    { " Years", " kg", " kg", " cm" };
    private readonly static string saveButtonEN = 
        "Save";
    private readonly static string cancelButtonEN = 
        "Cancel";
    #endregion
    #region KOREAN
    private readonly static string nameKO =
        "이름";
    private readonly static string ageKO =
        "나이";
    private readonly static string weightKO =
        "몸무게";
    private readonly static string heightKO =
        "신장";
    private readonly static string genderKO =
        "성별";
    private readonly static string levelKO =
        "레벨";
    private readonly static string maleKO =
        "남성";
    private readonly static string femaleKO =
        "여성";
    private readonly static string[] statusBMIKO = new string[]
    { "기대다", "정상", "초과 중량", "지방" };
    private readonly static string yourDataKO =
        "귀하의 데이터";
    private readonly static string calculationKO =
        "계산";
    private readonly static string targetKO =
        "목적";
    private readonly static string bodyFatKO =
        "체지방";
    private readonly static string totalFatKO =
        "지방";
    private readonly static string goalKO =
        "목표";
    private readonly static string toLoseFatKO =
        "지방을 잃으려면";
    private readonly static string toBurnCalKO =
        "칼로리를 태우려면";
    /* ======================================================================================================================================= */
    private readonly static string idKO =
        "아이디";
    private readonly static string passwordKO =
        "패스워드";
    private readonly static string placeHolderIDKO =
        "사용자 이름";
    private readonly static string placeHolderPasswordKO =
        "비밀번호 입력";
    private readonly static string signInKO =
        "로그인";
    private readonly static string signOnKO =
        "회원가입";
    private readonly static string fieldEmptyWarningKO =
        "입력된 정보가 없습니다";
    private readonly static string incorrectWarningKO =
        "잘못된 유저 정보거나 비밀번호입니다";
    /* ======================================================================================================================================= */
    private readonly static string newIDKO =
        "새로운 이름을 넣으세요:";
    private readonly static string newPasswordKO =
        "새로운 비밀번호를 넣으세요:";
    private readonly static string repeatPasswordKO =
        "다시한번더 비밀번호를 입력하세요:";
    private readonly static string placeHolderNewIDKO =
        "새로운 사용자명";
    private readonly static string placeHolderNewPasswordKO =
        "새 비밀번호";
    private readonly static string placeHolderRepeatPasswordKO =
        "비밀번호 다시 입력";
    private readonly static string alreadyIDWarningKO =
        "현재 사용자명은 이미 존재합니다!";
    private readonly static string passwordsDontMatchKO =
        "비밀번호가 일치하지 않습니다";
    private readonly static string okButtonKO =
        "OK";
    /* ======================================================================================================================================= */
    private readonly static string signedInAsKO =
        "회원가입";
    private readonly static string updateProfileKO =
        "프로필 업데이트";
    private readonly static string editAccountKO =
        "계정 수정";
    private readonly static string languageKO =
        "언어";
    private readonly static string deleteAccountKO =
        "계정 삭제";
    private readonly static string signOutKO =
        "로그 아웃";
    private readonly static string areYouSureKO =
        "계정을 정말 삭제하겠습니까?";
    private readonly static string yesKO =
        "예";
    private readonly static string nopKO =
        "아니오";
    /* ======================================================================================================================================= */
    private readonly static string[] pageQuestionKO = new string[]
    { "이름을 입력하세요", "성별을 입력하세요", "목적을 선택하세요", "체력수준  선택", "집중적 운동 부위", "추가 정보 입력" };
    private readonly static string nextButtonKO =
        "다음";
    private readonly static string page1PlaceHolderKO =
        "여기에 입력하세요";
    private readonly static string[] buttonGoalKO = new string[]
    { "체중감량", "근력강화", "몸매가꾸기" };
    private readonly static string[] levelPhraseKO = new string[]
    { "운동거의 안함", "필수적인 걷기운동만", "러닝과 체조정도 운동만", "운동량이 충분히 많음", "전문적인 운동선수 급" };
    private readonly static string[] buttonFocusKO = new string[]
    { "복근", "가슴부위", "팔 부위", "다리부위" };
    private readonly static string[] blockAreaNameKO = new string[]
    { "나이", "현재 체중", "목표체중", "신장" };
    private readonly static string valueDfaultButtonsEditKO =
        "수정";
    private readonly static string[] footerTitleKO = new string[]
    { "나이를 입력하세요", "현재 체중 입력", "현재 체중 입력", "본인 키 입력" };
    private readonly static string footerPlaceholderKO =
        "값";
    private readonly static string[] footerUnitKO = new string[]
    { " 세", " kg", " kg", " cm" };
    private readonly static string saveButtonKO =
        "저장";
    private readonly static string cancelButtonKO =
        "취소";
    #endregion
    #region PORTUGUESE_BRAZILIAN
    private readonly static string nameBR =
        "Nome";
    private readonly static string ageBR =
        "Idade";
    private readonly static string weightBR =
        "Peso";
    private readonly static string heightBR =
        "Altura";
    private readonly static string genderBR =
        "Gênero";
    private readonly static string levelBR =
        "Nível";
    private readonly static string maleBR =
        "Masculino";
    private readonly static string femaleBR =
        "Feminino";
    private readonly static string[] statusBMIBR = new string[]
    { "Magro", "Normal", "Sobrepeso", "Gordo" };
    private readonly static string yourDataBR =
        "Seus Dados";
    private readonly static string calculationBR =
        "Cálculos";
    private readonly static string targetBR =
        "Meta";
    private readonly static string bodyFatBR =
        "Gordura Corporal";
    private readonly static string totalFatBR =
        "Gordura total";
    private readonly static string goalBR =
        "Objetivo";
    private readonly static string toLoseFatBR =
        "Gordura a Perder";
    private readonly static string toBurnCalBR =
        "Calorias a queimar";
    /* ======================================================================================================================================= */
    private readonly static string idBR =
        "Usuário";
    private readonly static string passwordBR =
        "Senha";
    private readonly static string placeHolderIDBR =
        "Nome de usuário";
    private readonly static string placeHolderPasswordBR =
        "Sua senha";
    private readonly static string signInBR =
        "Entrar";
    private readonly static string signOnBR =
        "Inscreva-se";
    private readonly static string fieldEmptyWarningBR =
        "Preencha campos vazios!";
    private readonly static string incorrectWarningBR =
        "Nome de usuário ou senha incorretos!";
    /* ======================================================================================================================================= */
    private readonly static string newIDBR =
        "Novo nome de usuário:";
    private readonly static string newPasswordBR =
        "Insira a nova senha:";
    private readonly static string repeatPasswordBR =
        "Repita a nova senha:";
    private readonly static string placeHolderNewIDBR =
        "Novo usuário";
    private readonly static string placeHolderNewPasswordBR =
        "Nova senha";
    private readonly static string placeHolderRepeatPasswordBR =
        "Repita a senha";
    private readonly static string alreadyIDWarningBR =
        "Este nome de usuário está indisponível!";
    private readonly static string passwordsDontMatchBR =
        "As senhas não coincidem!";
    private readonly static string okButtonBR =
        "OK";
    /* ======================================================================================================================================= */
    private readonly static string signedInAsBR =
        "Registrado como";
    private readonly static string updateProfileBR =
        "Editar Perfil";
    private readonly static string editAccountBR =
        "Editar Conta";
    private readonly static string languageBR =
        "Idioma";
    private readonly static string deleteAccountBR =
        "Excluir conta";
    private readonly static string signOutBR =
        "Sair";
    private readonly static string areYouSureBR =
        "Tem certeza que deseja excluir a sua conta?";
    private readonly static string yesBR =
        "Sim";
    private readonly static string nopBR =
        "Não";
    /* ======================================================================================================================================= */
    private readonly static string[] pageQuestionBR = new string[]
    { "Qual é o seu nome?", "Qual é o seu sexo?", "Qual é o seu objetivo?", "Qual o seu nível físico?", "Qual o seu foco?", "Mais sobre você:" };
    private readonly static string nextButtonBR =
        "Próximo";
    private readonly static string page1PlaceHolderBR =
        "Insira o seu nome aqui...";
    private readonly static string[] buttonGoalBR = new string[]
    { "Perder peso", "Ter músculos", "Manter a forma" };
    private readonly static string[] levelPhraseBR = new string[]
    { "Sedentário", "Caminhante", "Corredor", "Atleta", "Musculoso" };
    private readonly static string[] buttonFocusBR = new string[]
    { "Abdomen", "Peito", "Braços", "Pernas" };
    private readonly static string[] blockAreaNameBR = new string[]
    { "Idade", "Peso atual", "Peso desejado", "Altura" };
    private readonly static string valueDfaultButtonsEditBR = 
        "edit";
    private readonly static string[] footerTitleBR = new string[]
    { "Insira a sua idade", "Insira o seu peso atual", "Insira o peso desejado", "Insira a sua altura" };
    private readonly static string footerPlaceholderBR = 
        "valor";
    private readonly static string[] footerUnitBR = new string[]
    { " anos", " kg", " kg", " cm" };
    private readonly static string saveButtonBR = 
        "Salvar";
    private readonly static string cancelButtonBR = 
        "Cancelar";
    #endregion

    private void Awake()
    {
        transition = GameObject.FindGameObjectWithTag(Tag.Transition).GetComponent<Transition>();

        (langButton, langPressed) = ButtonsManager.Assignment(buttonsLangHeader, LangButtonsBehaviour);
        okButton.onClick.AddListener(OkFunction);
        closeButton.onClick.AddListener(ButtonsManager.CloseApplication);
        returnButton.onClick.AddListener(() => TrainARSceneManager.LoadMenu());
    }

    private void Start()
    {
        transition.EndClip();
        LangButtonsBehaviour(AccountData.Language);
        returnButton.gameObject.SetActive(TrainARSceneManager.GetPreviousSceneSaved() == TrainARSceneManager.menu);
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

        Language = (Lang)index;
        lang = langStatic;
    }

    public void OkFunction()
    {
        AccountData.Language = (int)Language;
        LoadLang(AccountData.Language);

        if (TrainARSceneManager.GetPreviousSceneSaved() == TrainARSceneManager.menu)
            TrainARSceneManager.LoadMenu();
        else
            TrainARSceneManager.LoadSignIn();
    }

    public static void LoadLang(int index)
    {
        Language = (Lang)index;

        switch (Language)
        {
            case Lang.EN:
               _name = nameEN;
                age = ageEN;
                weight = weightEN;
                height = heightEN;
                gender = genderEN;
                level = levelEN;
                male = maleEN;
                female = femaleEN;
                statusBMI = statusBMIEN;
                yourData = yourDataEN;
                calculation = calculationEN;
                target = targetEN;
                bodyFat = bodyFatEN;
                totalFat = totalFatEN;
                goal = goalEN;
                toLoseFat = toLoseFatEN;
                toBurnCal = toBurnCalEN;

                id = idEN;
                password = passwordEN;
                placeHolderID = placeHolderIDEN;
                placeHolderPassword = placeHolderPasswordEN;
                signIn = signInEN;
                signOn = signOnEN;
                fieldEmptyWarning = fieldEmptyWarningEN;
                incorrectWarning = incorrectWarningEN;

                newID = newIDEN;
                newPassword = newPasswordEN;
                repeatPassword = repeatPasswordEN;
                placeHolderNewID = placeHolderNewIDEN;
                placeHolderNewPassword = placeHolderNewPasswordEN;
                placeHolderRepeatPassword = placeHolderRepeatPasswordEN;
                alreadyIDWarning = alreadyIDWarningEN;
                passwordsDontMatch = passwordsDontMatchEN;
                okButtonString = okButtonEN;

                signedInAs = signedInAsEN;
                updateProfile = updateProfileEN;
                editAccount = editAccountEN;
                language = languageEN;
                deleteAccount = deleteAccountEN;
                signOut = signOutEN;
                areYouSure = areYouSureEN;
                yes = yesEN;
                nop = nopEN;

                pageQuestion = pageQuestionEN;
                nextButton = nextButtonEN;
                page1PlaceHolder = page1PlaceHolderEN;
                goalPhrase = buttonGoalEN;
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
            case Lang.KO:
                _name = nameKO;
                age = ageKO;
                weight = weightKO;
                height = heightKO;
                gender = genderKO;
                level = levelKO;
                male = maleKO;
                female = femaleKO;
                statusBMI = statusBMIKO;
                yourData = yourDataKO;
                calculation = calculationKO;
                target = targetKO;
                bodyFat = bodyFatKO;
                totalFat = totalFatKO;
                goal = goalKO;
                toLoseFat = toLoseFatKO;
                toBurnCal = toBurnCalKO;

                id = idKO;
                password = passwordKO;
                placeHolderID = placeHolderIDKO;
                placeHolderPassword = placeHolderPasswordKO;
                signIn = signInKO;
                signOn = signOnKO;
                fieldEmptyWarning = fieldEmptyWarningKO;
                incorrectWarning = incorrectWarningKO;

                newID = newIDKO;
                newPassword = newPasswordKO;
                repeatPassword = repeatPasswordKO;
                placeHolderNewID = placeHolderNewIDKO;
                placeHolderNewPassword = placeHolderNewPasswordKO;
                placeHolderRepeatPassword = placeHolderRepeatPasswordKO;
                alreadyIDWarning = alreadyIDWarningKO;
                passwordsDontMatch = passwordsDontMatchKO;
                okButtonString = okButtonKO;

                signedInAs = signedInAsKO;
                updateProfile = updateProfileKO;
                editAccount = editAccountKO;
                language = languageKO;
                deleteAccount = deleteAccountKO;
                signOut = signOutKO;
                areYouSure = areYouSureKO;
                yes = yesKO;
                nop = nopKO;

                pageQuestion = pageQuestionKO;
                nextButton = nextButtonKO;
                page1PlaceHolder = page1PlaceHolderKO;
                goalPhrase = buttonGoalKO;
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
            case Lang.BR:
                _name = nameBR;
                age = ageBR;
                weight = weightBR;
                height = heightBR;
                gender = genderBR;
                level = levelBR;
                male = maleBR;
                female = femaleBR;
                statusBMI = statusBMIBR;
                yourData = yourDataBR;
                calculation = calculationBR;
                target = targetBR;
                bodyFat = bodyFatBR;
                totalFat = totalFatBR;
                goal = goalBR;
                toLoseFat = toLoseFatBR;
                toBurnCal = toBurnCalBR;

                id = idBR;
                password = passwordBR;
                placeHolderID = placeHolderIDBR;
                placeHolderPassword = placeHolderPasswordBR;
                signIn = signInBR;
                signOn = signOnBR;
                fieldEmptyWarning = fieldEmptyWarningBR;
                incorrectWarning = incorrectWarningBR;

                newID = newIDBR;
                newPassword = newPasswordBR;
                repeatPassword = repeatPasswordBR;
                placeHolderNewID = placeHolderNewIDBR;
                placeHolderNewPassword = placeHolderNewPasswordBR;
                placeHolderRepeatPassword = placeHolderRepeatPasswordBR;
                alreadyIDWarning = alreadyIDWarningBR;
                passwordsDontMatch = passwordsDontMatchBR;
                okButtonString = okButtonBR;

                signedInAs = signedInAsBR;
                updateProfile = updateProfileBR;
                editAccount = editAccountBR;
                language = languageBR;
                deleteAccount = deleteAccountBR;
                signOut = signOutBR;
                areYouSure = areYouSureBR;
                yes = yesBR;
                nop = nopBR;

                pageQuestion = pageQuestionBR;
                nextButton = nextButtonBR;
                page1PlaceHolder = page1PlaceHolderBR;
                goalPhrase = buttonGoalBR;
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

    public static Lang Language
    {
        get => langStatic;
        private set => langStatic = value;
    }
}