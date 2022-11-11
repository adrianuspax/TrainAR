using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(AnimationsManager), typeof(ButtonsManager))]
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
    [SerializeField] private TextMeshProUGUI warning;

    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("DATA INPUTED", order = 2)]
    [SerializeField, ReadOnly] private string username;
    [SerializeField, ReadOnly] private string newPassword;
    [SerializeField, ReadOnly] private string repeatPassword;

    [Header("Variables", order = 0)]
    [SerializeField, ReadOnly] private bool usernameReleased;
    [SerializeField, ReadOnly] private bool passwordReleased;
    [SerializeField, ReadOnly] private bool buttonOkRealeased;
    [SerializeField, ReadOnly] private bool usernameIsEmpty;
    [SerializeField, ReadOnly] private bool newPasswordIsEmpty;
    [SerializeField, ReadOnly] private bool repeatPasswordIsEmpty;

    [Header("Scripts", order = 0)]
    [SerializeField, ReadOnly] private AnimationsManager Animations;
    [SerializeField, ReadOnly] private ButtonsManager Buttons;

    private void Awake()
    {
        Animations = this.GetComponent<AnimationsManager>();
        Buttons = this.GetComponent<ButtonsManager>();

        newUsernameInput.onSelect.AddListener(OnSelectNewUsername);
        newUsernameInput.onDeselect.AddListener(OnDeselectNewUsername);
        newUsernameInput.onValueChanged.AddListener(OnValueChangedNewUsername);
        newUsernameInput.onEndEdit.AddListener(OnEndEditNewUsername);

        newPasswordInput.onSelect.AddListener(OnSelectNewPassword);
        newPasswordInput.onDeselect.AddListener(OnDeselectNewPassword);
        newPasswordInput.onValueChanged.AddListener(OnValueChangedNewPassword);
        newPasswordInput.onEndEdit.AddListener(OnEndEditNewPassword);

        repeatPasswordInput.onSelect.AddListener(OnSelectRepeatPassword);
        repeatPasswordInput.onDeselect.AddListener(OnDeselectRepeatPassword);
        repeatPasswordInput.onValueChanged.AddListener(OnValueChangedRepeatPassword);
        repeatPasswordInput.onEndEdit.AddListener(OnEndEditRepeatPassword);

        okButton.onClick.AddListener(() => Ok());
        returnButton.onClick.AddListener(() => Return());
    }

    private void Start()
    {
        Animations.Transition.EndClip();
        warning.text = string.Empty;
        newPasswordIsEmpty = true;
        repeatPasswordIsEmpty = true;
        Buttons.Interaction(false, okButton);

        Debug.Log(PlayerPrefs.HasKey(AccountData.USERNAME) ? "username = " + PlayerPrefs.GetString(AccountData.USERNAME) : "username = EMPTY");
        Debug.Log(PlayerPrefs.HasKey(AccountData.PASSWORD) ? "password = " + PlayerPrefs.GetString(AccountData.PASSWORD) : "password = EMPTY");
    }
    #region BUTTONS
    public void Ok()
    {
        if (buttonOkRealeased)
        {
            AccountData.Username = username;
            AccountData.Password = newPassword;

            switch (ScenesManagement.PreviousScene)
            {
                case ScenesManagement.Menu:
                    ScenesManagement.LoadMenu();
                    break;
                default:
                    ScenesManagement.LoadSignIn();
                    break;
            }
        }
    }

    public void Return()
    {
        switch (ScenesManagement.PreviousScene)
        {
            case ScenesManagement.Menu:
                ScenesManagement.LoadMenu();
                break;
            default:
                ScenesManagement.LoadSignIn();
                break;
        }
    }
    #endregion
    #region newUsername
    public void OnSelectNewUsername(string newUsername)
    {
        PlaceHolderNewUsername = string.Empty;
    }

public void OnDeselectNewUsername(string newUsername)
    {
        usernameIsEmpty = (newUsername == string.Empty);
        PlaceHolderNewUsername = "New username";
    }

    public void OnValueChangedNewUsername(string newUsername)
    {
        usernameIsEmpty = (newUsername == string.Empty);
        warning.text = string.Empty;
    }

    public void OnEndEditNewUsername(string newUsername)
    {
        if (AccountData.HasUsername)
        {
            if (newUsername == AccountData.Username)
            {
                warning.text = "Este username já está em uso!";
            }
            else if (usernameIsEmpty)
            {
                warning.text = string.Empty;
            }

            usernameReleased = !(newUsername == AccountData.Username);
        }
        else if (usernameIsEmpty)
        {
            warning.text = string.Empty;
        }
        else
        {
            usernameReleased = true;
        }

        username = newUsername;
        ValidationPasswords();
        ValidationRealease();
    }

    public string PlaceHolderNewUsername { set => newUsernameInput.placeholder.GetComponent<TextMeshProUGUI>().text = value; }
    #endregion
    #region newPassword
    public void OnSelectNewPassword(string newPassword)
    {
        PlaceHolderNewPassword = string.Empty;
    }

    public void OnDeselectNewPassword(string newPassword)
    {
        newPasswordIsEmpty = (newPassword == string.Empty);
        PlaceHolderNewPassword = "New password";
    }

    public void OnValueChangedNewPassword(string newPassword)
    {
        newPasswordIsEmpty = (newPassword == string.Empty);
    }

    public void OnEndEditNewPassword(string newPassword)
    {
        this.newPassword = newPassword;
        ValidationPasswords();
        ValidationRealease();
    }

    public string PlaceHolderNewPassword { set => newPasswordInput.placeholder.GetComponent<TextMeshProUGUI>().text = value; }
    #endregion
    #region repeatPassword
    public void OnSelectRepeatPassword(string repeatPassword)
    {
        PlaceHolderRepeatPassword = string.Empty;
    }

    public void OnDeselectRepeatPassword(string repeatPassword)
    {
        repeatPasswordIsEmpty = (repeatPassword == string.Empty);
        PlaceHolderRepeatPassword = "Repeat password";
    }

    public void OnValueChangedRepeatPassword(string repeatPassword)
    {
        repeatPasswordIsEmpty = (repeatPassword == string.Empty);
    }

    public void OnEndEditRepeatPassword(string repeatPassword)
    {
        this.repeatPassword = repeatPassword;
        ValidationPasswords();
        ValidationRealease();
    }

    private void ValidationPasswords()
    {
        if (usernameReleased)
        {
            warning.text = (newPasswordIsEmpty && repeatPasswordIsEmpty) || (newPassword == repeatPassword) ? string.Empty : "As senhas não coincidem!";
        }
    }

    private void ValidationRealease()
    {
        passwordReleased = (newPassword == repeatPassword) && (!newPasswordIsEmpty && !repeatPasswordIsEmpty);
        buttonOkRealeased = usernameReleased && passwordReleased;
        Buttons.Interaction(buttonOkRealeased, okButton);
    }

    public string PlaceHolderRepeatPassword { set => repeatPasswordInput.placeholder.GetComponent<TextMeshProUGUI>().text = value; }
    #endregion
}