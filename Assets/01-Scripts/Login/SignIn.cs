using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ASP;
using Debug = ASP.Debug;
using PlayerPrefs = UnityEngine.PlayerPrefs;

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
    [Space(-10, order = 1)]
    [Header("fix behaviour strings", order = 2)]
    [SerializeField, ReadOnly] private string oldEditTextUsername;
    [SerializeField, ReadOnly] private string editTextUsername;
    [SerializeField, ReadOnly] private string oldEditTextPassword;
    [SerializeField, ReadOnly] private string editTextPassword;

    [Header("Variables", order = 0)]
    [SerializeField, ReadOnly] private bool keepOldTextUsername;
    [SerializeField, ReadOnly] private bool keepOldTextPassword;
    [SerializeField, ReadOnly] private bool signed;

    [Header("Scripts", order = 0)]
    [SerializeField, ReadOnly] private AnimationsManager Animations;

    private void Awake()
    {
        Animations = this.GetComponent<AnimationsManager>();
        signInButton.onClick.AddListener(() => SignInFunction());
        signOnButton.onClick.AddListener(() => SignOnFunction());
        //username
        usernameInput.onSelect.AddListener(OnSelectUsername);
        usernameInput.onDeselect.AddListener(OnDeselectUsername);
        usernameInput.onValueChanged.AddListener(OnValueChangedUsername);
        usernameInput.onEndEdit.AddListener(OnEndEditUsername);
        usernameInput.onTouchScreenKeyboardStatusChanged.AddListener(ReportChangeStatusUsername);
        //pssword
        passwordInput.onSelect.AddListener(OnSelectPassword);
        passwordInput.onDeselect.AddListener(OnDeselectPassword);
        passwordInput.onValueChanged.AddListener(OnValueChangedPassword);
        passwordInput.onEndEdit.AddListener(OnEndEditPassword);
        passwordInput.onTouchScreenKeyboardStatusChanged.AddListener(ReportChangeStatusPassword);

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
        signed = AccountData.Signed;

        Debug.Log(PlayerPrefs.HasKey(Key.username) ? "username = " + PlayerPrefs.GetString(Key.username) : "username = EMPTY");
        Debug.Log(PlayerPrefs.HasKey(Key.password) ? "password = " + PlayerPrefs.GetString(Key.password) : "password = EMPTY");
    }

    public void SignInFunction()
    {
        bool validationData;
             validationData = (username == AccountData.Username && password == AccountData.Password);

        if (AccountData.HasAccount && validationData)
        {
            AccountData.Signed = true;

            if (ScenesManagement.GetPreviousSceneSaved().Equals(ScenesManagement.signOn))
                ScenesManagement.LoadUserData();
            else
                ScenesManagement.LoadTrainAR();
        }
        else
        {
            WarningString = (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(passwordInput.text)) ?
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
        PlaceHolderUsername = usernameInput.isFocused ? string.Empty : LanguageManager.placeHolderID;
    }

    public void OnValueChangedUsername(string username)
    {
        PlaceHolderUsername = usernameInput.isFocused ? string.Empty : LanguageManager.placeHolderID;

        oldEditTextUsername = editTextUsername;
        editTextUsername = username;
    }

    public void OnEndEditUsername(string username)
    {
        this.username = username;
        PlaceHolderUsername = usernameInput.isFocused ? string.Empty : LanguageManager.placeHolderID;

        if (keepOldTextUsername && !string.IsNullOrEmpty(oldEditTextUsername))
        {
            editTextUsername = oldEditTextUsername;
            usernameInput.text = oldEditTextUsername;
            this.username = oldEditTextUsername;
            keepOldTextUsername = false;
        }

        if (usernameInput.wasCanceled)
        {
            PlaceHolderUsername = string.Empty;
        }
    }

    public void ReportChangeStatusUsername(TouchScreenKeyboard.Status newStatus)
    {
        if (newStatus == TouchScreenKeyboard.Status.Canceled)
        { keepOldTextUsername = true; }
    }

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
        PlaceHolderPassword = passwordInput.isFocused ? string.Empty : LanguageManager.placeHolderPassword;
    }

    public void OnValueChangedPassword(string password)
    {
        PlaceHolderPassword = passwordInput.isFocused ? string.Empty : LanguageManager.placeHolderPassword;

        oldEditTextPassword = editTextPassword;
        editTextPassword = password;
    }

    public void OnEndEditPassword(string password)
    {
        this.password = password;
        PlaceHolderPassword = passwordInput.isFocused ? string.Empty : LanguageManager.placeHolderPassword;

        if (keepOldTextPassword && !string.IsNullOrEmpty(oldEditTextPassword))
        {
            editTextPassword = oldEditTextPassword;
            passwordInput.text = oldEditTextPassword;
            this.password = oldEditTextPassword;
            keepOldTextPassword = false;
        }

        if (passwordInput.wasCanceled)
        {
            PlaceHolderPassword = string.Empty;
        }
    }

    public void ReportChangeStatusPassword(TouchScreenKeyboard.Status newStatus)
    {
        if (newStatus == TouchScreenKeyboard.Status.Canceled)
        { keepOldTextPassword = true; }
    }

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
