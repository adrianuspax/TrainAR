using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(AnimationsManager))]
public class SignIn : MonoBehaviour
{
    [Header("BUTTONS", order = 0)]
    [Space(-10, order = 1)]
    [Header("Canvas Buttons", order = 2)]
    [SerializeField] private Button signInButton;
    [SerializeField] private Button signOnButton;
    [Space(-10, order = 0)]
    [Header("Input", order = 1)]
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [Space(-10, order = 0)]
    [Header("TMP", order = 1)]
    [SerializeField] private TextMeshProUGUI idTMP;
    [SerializeField] private TextMeshProUGUI passwordTMP;
    [SerializeField] private TextMeshProUGUI warningTMP;

    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("DATA INPUTED", order = 2)]
    [SerializeField, ReadOnly] private string username;
    [SerializeField, ReadOnly] private string password;
    [SerializeField, ReadOnly] private bool signed;

    [Header("Variables", order = 0)]
    [SerializeField, ReadOnly] private bool usernameIsEmpty;
    [SerializeField, ReadOnly] private bool passwordIsEmpty;

    [Header("Scripts", order = 0)]
    [SerializeField, ReadOnly] private AnimationsManager Animations;

    private void Awake()
    {
        Animations = this.GetComponent<AnimationsManager>();
        signInButton.onClick.AddListener(() => SignInFunction());
        signOnButton.onClick.AddListener(() => SignOnFunction());

        usernameInput.onSelect.AddListener(OnSelectUsername);
        usernameInput.onDeselect.AddListener(OnDeselectUsername);
        usernameInput.onValueChanged.AddListener(OnValueChangedUsername);
        usernameInput.onEndEdit.AddListener(OnEndEditUsername);

        passwordInput.onSelect.AddListener(OnSelectPassword);
        passwordInput.onDeselect.AddListener(OnDeselectPassword);
        passwordInput.onValueChanged.AddListener(OnValueChangedPassword);
        passwordInput.onEndEdit.AddListener(OnEndEditPassword);

        PlaceHolderUsername = LanguageManager.placeHolderID;
        PlaceHolderPassword = LanguageManager.placeHolderPassword;
        IDString = LanguageManager.id;
        PasswordString = LanguageManager.password;
        SignInButtonsString = LanguageManager.signIn;
        SignOnButtonsString = LanguageManager.signOn;
    }

    private void Start()
    {
        Animations.Transition.EndClip();
        warningTMP.text = string.Empty;
        usernameIsEmpty = true;
        passwordIsEmpty = true;
        signed = AccountData.Signed;
        Debug.Log(PlayerPrefs.HasKey(AccountData.USERNAME) ? "username = " + PlayerPrefs.GetString(AccountData.USERNAME) : "username = EMPTY");
        Debug.Log(PlayerPrefs.HasKey(AccountData.PASSWORD) ? "password = " + PlayerPrefs.GetString(AccountData.PASSWORD) : "password = EMPTY");
    }

    public void SignInFunction()
    {
        bool validationData;
             validationData = (username == AccountData.Username && password == AccountData.Password);

        if (AccountData.HasAccount && validationData)
        {
            AccountData.Signed = true;

            switch (ScenesManagement.PreviousScene)
            {
                case ScenesManagement.SignOn: ScenesManagement.LoadUserData();
                    break;
                default: ScenesManagement.LoadTrainAR();
                    break;
            }
        }
        else
        {
            WarningString = (usernameIsEmpty || passwordIsEmpty) ?
                LanguageManager.fieldEmptyWarning : LanguageManager.incorrectWarning;
        }
    }

    public void SignOnFunction()
    { ScenesManagement.LoadSignOn(); }
    #region USERNAME
    public void OnSelectUsername(string username)
    {
        WarningString = string.Empty;
        PlaceHolderUsername = string.Empty;
    }

    public void OnDeselectUsername(string username)
    {
        usernameIsEmpty = (username == string.Empty);
        PlaceHolderUsername = LanguageManager.placeHolderID;
    }

    public void OnValueChangedUsername(string username)
    { usernameIsEmpty = (username == string.Empty); }

    public void OnEndEditUsername(string username)
    { this.username = username; }

    public string PlaceHolderUsername { set => usernameInput.placeholder.GetComponent<TextMeshProUGUI>().text = value; }
    #endregion
    #region PASSWORD
    public void OnSelectPassword(string password)
    {
        WarningString = string.Empty;
        PlaceHolderPassword = string.Empty;
    }

    public void OnDeselectPassword(string password)
    {
        passwordIsEmpty = (password == string.Empty);
        PlaceHolderPassword = LanguageManager.placeHolderPassword;
    }

    public void OnValueChangedPassword(string password)
    { passwordIsEmpty = (password == string.Empty); }

    public void OnEndEditPassword(string password)
    { this.password = password; }

    public string PlaceHolderPassword
    { set => passwordInput.placeholder.GetComponent<TextMeshProUGUI>().text = value; }
    #endregion

    public string IDString
    { set => idTMP.text = value; }

    public string PasswordString
    { set => passwordTMP.text = value; }

    public string SignInButtonsString
    { set => signInButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value; }

    public string SignOnButtonsString
    { set => signOnButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value; }

    public string WarningString
    { set => warningTMP.text = value; }
}
