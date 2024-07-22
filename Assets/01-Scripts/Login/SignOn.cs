using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ASP;
using Debug = ASP.Debug;
using PlayerPrefs = ASP.PlayerPrefs;

[RequireComponent( typeof(ButtonsManager))]
public class SignOn : MonoBehaviour
{
    [Header("UI", order = 0)]
    [Space(-10, order = 1)]
    [Header("Buttons", order = 2)]
    [SerializeField] private Button okButton;
    [SerializeField] private Button returnButton;
    [Space(-10, order = 0)]
    [Header("Input", order = 1)]
    [SerializeField] private TMP_InputField newUsernameInput;
    [SerializeField] private TMP_InputField newPasswordInput;
    [SerializeField] private TMP_InputField repeatPasswordInput;
    [Space(-10, order = 0)]
    [Header("TMP", order = 1)]
    [SerializeField] private TextMeshProUGUI enterNewUsernameTMP;
    [SerializeField] private TextMeshProUGUI enterNewPasswordTMP;
    [SerializeField] private TextMeshProUGUI repeatNewPasswordTMP;
    [SerializeField] private TextMeshProUGUI warningTMP;

    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("DATA INPUTED", order = 2)]
    [SerializeField, ReadOnly] private string username;
    [SerializeField, ReadOnly] private string newPassword;
    [SerializeField, ReadOnly] private string repeatPassword;
    [Space(-10, order = 1)]
    [Header("fix behaviour strings", order = 2)]
    [SerializeField, ReadOnly] private string oldEditTextNewUsername;
    [SerializeField, ReadOnly] private string editTextNewUsername;
    [SerializeField, ReadOnly] private string oldEditTextNewPassword;
    [SerializeField, ReadOnly] private string editTextNewPassword;
    [SerializeField, ReadOnly] private string oldEditTextRepeatPassword;
    [SerializeField, ReadOnly] private string editTextRepeatPassword;

    [Header("Variables", order = 0)]
    [SerializeField, ReadOnly] private bool usernameReleased;
    [SerializeField, ReadOnly] private bool passwordReleased;
    [SerializeField, ReadOnly] private bool buttonOkRealeased;
    [SerializeField, ReadOnly] private bool keepOldTextNewUsername;
    [SerializeField, ReadOnly] private bool keepOldTextNewPassword;
    [SerializeField, ReadOnly] private bool keepOldTextRepeatPassword;

    [Header("Scripts", order = 0)]
    [SerializeField, ReadOnly] private ButtonsManager Buttons;
    [SerializeField, ReadOnly] private Transition transition;

    private void Awake()
    {
        Buttons = this.GetComponent<ButtonsManager>();
        transition = GameObject.FindGameObjectWithTag(Tag.Transition).GetComponent<Transition>();

        newUsernameInput.onSelect.AddListener(OnSelectNewUsername);
        newUsernameInput.onDeselect.AddListener(OnDeselectNewUsername);
        newUsernameInput.onValueChanged.AddListener(OnValueChangedNewUsername);
        newUsernameInput.onEndEdit.AddListener(OnEndEditNewUsername);
        newUsernameInput.onTouchScreenKeyboardStatusChanged.AddListener(ReportChangeStatusNewUsername);

        newPasswordInput.onSelect.AddListener(OnSelectNewPassword);
        newPasswordInput.onDeselect.AddListener(OnDeselectNewPassword);
        newPasswordInput.onValueChanged.AddListener(OnValueChangedNewPassword);
        newPasswordInput.onEndEdit.AddListener(OnEndEditNewPassword);
        newPasswordInput.onTouchScreenKeyboardStatusChanged.AddListener(ReportChangeStatusNewPassword);

        repeatPasswordInput.onSelect.AddListener(OnSelectRepeatPassword);
        repeatPasswordInput.onDeselect.AddListener(OnDeselectRepeatPassword);
        repeatPasswordInput.onValueChanged.AddListener(OnValueChangedRepeatPassword);
        repeatPasswordInput.onEndEdit.AddListener(OnEndEditRepeatPassword);
        repeatPasswordInput.onTouchScreenKeyboardStatusChanged.AddListener(ReportChangeStatusRepeatPassword);

        okButton.onClick.AddListener(() => Ok());
        returnButton.onClick.AddListener(() => Return());

        enterNewUsernameTMP.text = LanguageManager.newID;
        enterNewPasswordTMP.text = LanguageManager.newPassword;
        repeatNewPasswordTMP.text = LanguageManager.repeatPassword;
        PlaceHolderNewUsername = LanguageManager.placeHolderNewID;
        PlaceHolderNewPassword = LanguageManager.placeHolderNewPassword;
        PlaceHolderRepeatPassword = LanguageManager.placeHolderRepeatPassword;
        OkButtonString = LanguageManager.okButtonString;
    }

    private void Start()
    {
        transition.EndClip();
        warningTMP.text = string.Empty;
        Buttons.Interaction(false, okButton);

        //Debug.Log(PlayerPrefs.HasKey(Key.username) ? "username = " + PlayerPrefs.GetString(Key.username) : "username = EMPTY");
        //Debug.Log(PlayerPrefs.HasKey(Key.password) ? "password = " + PlayerPrefs.GetString(Key.password) : "password = EMPTY");
    }
    #region BUTTONS
    public void Ok()
    {
        if (buttonOkRealeased)
        {
            AccountData.Username = username;
            AccountData.Password = newPassword;

            if (TrainARSceneManager.GetPreviousSceneSaved() == TrainARSceneManager.menu)
                TrainARSceneManager.LoadMenu();
            else
                TrainARSceneManager.LoadSignIn();
        }
    }

    public void Return()
    {
        if (buttonOkRealeased)
        {
            AccountData.Username = username;
            AccountData.Password = newPassword;

            if (TrainARSceneManager.GetPreviousSceneSaved() == TrainARSceneManager.menu)
                TrainARSceneManager.LoadMenu();
            else
                TrainARSceneManager.LoadSignIn();
        }
    }
    #endregion
    #region newUsername
    public void OnSelectNewUsername(string newUsername)
    {
        PlaceHolderNewUsername = string.Empty;
        warningTMP.text = string.Empty;
    }

    public void OnDeselectNewUsername(string newUsername)
    {
        PlaceHolderNewUsername = LanguageManager.placeHolderNewID;
    }

    public void OnValueChangedNewUsername(string newUsername)
    {
        warningTMP.text = string.Empty;
        oldEditTextNewUsername = editTextNewUsername;
        editTextNewUsername = newUsername;
    }

    public void OnEndEditNewUsername(string newUsername)
    {
        if (keepOldTextNewUsername && !string.IsNullOrEmpty(oldEditTextNewUsername))
        {
            editTextNewUsername = oldEditTextNewUsername;
            newUsernameInput.text = oldEditTextNewUsername;
            this.username = oldEditTextNewUsername;
            keepOldTextNewUsername = false;
        }

        if (AccountData.HasUsername)
        {
            if (newUsername == AccountData.Username)
            { warningTMP.text = LanguageManager.alreadyIDWarning; }
            else if (string.IsNullOrEmpty(newUsernameInput.text))
            { warningTMP.text = string.Empty; }

            usernameReleased = !(newUsername == AccountData.Username);
        }
        else if (string.IsNullOrEmpty(newUsernameInput.text))
        { warningTMP.text = string.Empty; }
        else
        { usernameReleased = true; }

        username = newUsername;
        ValidationPasswords();
        ValidationRealease();
    }

    public void ReportChangeStatusNewUsername(TouchScreenKeyboard.Status newStatus)
    {
        if (newStatus == TouchScreenKeyboard.Status.Canceled)
        { keepOldTextNewUsername = true; }
    }

    public string PlaceHolderNewUsername
    { set => newUsernameInput.placeholder.GetComponent<TextMeshProUGUI>().text = value; }
    #endregion
    #region newPassword
    public void OnSelectNewPassword(string newPassword)
    {
        PlaceHolderNewPassword = string.Empty;
    }

    public void OnDeselectNewPassword(string newPassword)
    {
        PlaceHolderNewPassword = LanguageManager.placeHolderNewPassword;
    }

    public void OnValueChangedNewPassword(string newPassword)
    {
        oldEditTextNewPassword = editTextNewPassword;
        editTextNewPassword = newPassword;
    }

    public void OnEndEditNewPassword(string newPassword)
    {
        this.newPassword = newPassword;

        if (keepOldTextNewPassword && !string.IsNullOrEmpty(oldEditTextNewPassword))
        {
            editTextNewPassword = oldEditTextNewPassword;
            newPasswordInput.text = oldEditTextNewPassword;
            this.newPassword = oldEditTextNewPassword;
            keepOldTextNewPassword = false;
        }

        ValidationPasswords();
        ValidationRealease();
    }

    public void ReportChangeStatusNewPassword(TouchScreenKeyboard.Status newStatus)
    {
        if (newStatus == TouchScreenKeyboard.Status.Canceled)
        { keepOldTextNewPassword = true; }
    }

    public string PlaceHolderNewPassword
    { set => newPasswordInput.placeholder.GetComponent<TextMeshProUGUI>().text = value; }
    #endregion
    #region repeatPassword
    public void OnSelectRepeatPassword(string repeatPassword)
    {
        PlaceHolderRepeatPassword = string.Empty;
    }

    public void OnDeselectRepeatPassword(string repeatPassword)
    {
        PlaceHolderRepeatPassword = LanguageManager.placeHolderRepeatPassword;
    }

    public void OnValueChangedRepeatPassword(string repeatPassword)
    {
        oldEditTextRepeatPassword = editTextRepeatPassword;
        editTextRepeatPassword = repeatPassword;
    }

    public void OnEndEditRepeatPassword(string repeatPassword)
    {
        this.repeatPassword = repeatPassword;

        if (keepOldTextRepeatPassword && !string.IsNullOrEmpty(oldEditTextRepeatPassword))
        {
            editTextRepeatPassword = oldEditTextRepeatPassword;
            repeatPasswordInput.text = oldEditTextRepeatPassword;
            this.repeatPassword = oldEditTextRepeatPassword;
            keepOldTextRepeatPassword = false;
        }

        ValidationPasswords();
        ValidationRealease();
    }

    public void ReportChangeStatusRepeatPassword(TouchScreenKeyboard.Status newStatus)
    {
        if (newStatus == TouchScreenKeyboard.Status.Canceled)
        { keepOldTextRepeatPassword = true; }
    }

    private void ValidationPasswords()
    {
        if (usernameReleased)
        {
            warningTMP.text =
                (string.IsNullOrEmpty(newPasswordInput.text) && string.IsNullOrEmpty(repeatPasswordInput.text)) || (newPassword == repeatPassword) ? string.Empty : LanguageManager.passwordsDontMatch;
        }
    }

    private void ValidationRealease()
    {
        passwordReleased = (newPassword == repeatPassword) &&
            (!string.IsNullOrEmpty(newPasswordInput.text) && !string.IsNullOrEmpty(repeatPasswordInput.text));

        buttonOkRealeased = usernameReleased && passwordReleased;
        Buttons.Interaction(buttonOkRealeased, okButton);
    }

    public string PlaceHolderRepeatPassword
    { set => repeatPasswordInput.placeholder.GetComponent<TextMeshProUGUI>().text = value; }
    #endregion

    public string OkButtonString
    { set => okButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value; }
}