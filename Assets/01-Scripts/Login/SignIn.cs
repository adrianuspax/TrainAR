using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

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
    [SerializeField] private TextMeshProUGUI warning;

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
    }

    private void Start()
    {
        Animations.Transition.EndClip();
        warning.text = string.Empty;
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
                case ScenesManagement.SignOn:
                    ScenesManagement.LoadUserData();
                    break;
                default:
                    ScenesManagement.LoadTrainAR();
                    break;
            }
        }
        else
        {
            warning.text = (usernameIsEmpty || passwordIsEmpty) ? "Preencha os campos!" : "Incorrect: username or password!";
        }
    }

    public void SignOnFunction()
    {
        ScenesManagement.LoadSignOn();
    }
    #region USERNAME
    public void OnSelectUsername(string username)
    {
        warning.text = string.Empty;
        PlaceHolderUsername = string.Empty;
    }

    public void OnDeselectUsername(string username)
    {
        usernameIsEmpty = (username == string.Empty);
        PlaceHolderUsername = "Enter username...";
    }

    public void OnValueChangedUsername(string username)
    {
        usernameIsEmpty = (username == string.Empty);
    }

    public void OnEndEditUsername(string username)
    {
        this.username = username;
    }

    public string PlaceHolderUsername { set => usernameInput.placeholder.GetComponent<TextMeshProUGUI>().text = value; }
    #endregion
    #region PASSWORD
    public void OnSelectPassword(string password)
    {
        warning.text = string.Empty;
        PlaceHolderPassword = string.Empty;
    }

    public void OnDeselectPassword(string password)
    {
        passwordIsEmpty = (password == string.Empty);
        PlaceHolderPassword = "Enter password...";
    }

    public void OnValueChangedPassword(string password)
    {
        passwordIsEmpty = (password == string.Empty);
    }

    public void OnEndEditPassword(string password)
    {
        this.password = password;
    }

    public string PlaceHolderPassword { set => passwordInput.placeholder.GetComponent<TextMeshProUGUI>().text = value; }
    #endregion
}
