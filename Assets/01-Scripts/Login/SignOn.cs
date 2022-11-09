using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(AnimationsManager))]
public class SignOn : MonoBehaviour
{
    [Header("UI", order = 0)]
    [Space(-10, order = 1)]
    [Header("Buttons", order = 2)]
    [SerializeField] private Button okButton;
    [SerializeField] private Button returnButton;
    [Space(-10, order = 0)]
    [Header("Input", order = 1)]
    [SerializeField] private TMP_InputField newUsername;
    [SerializeField] private TMP_InputField newPassword;
    [SerializeField] private TMP_InputField repeatPassword;
    [Space(-10, order = 0)]
    [Header("Input", order = 1)]
    [SerializeField] private TextMeshProUGUI log;

    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("DATA", order = 2)]
    [SerializeField, ReadOnly] private string username;
    [SerializeField, ReadOnly] private string password;

    [Header("Variables", order = 0)]
    [SerializeField, ReadOnly] private bool usernameReleased;
    [SerializeField, ReadOnly] private bool passwordReleased;
    [SerializeField, ReadOnly] private bool usernameIsEmpty;

    [Header("Scripts", order = 0)]
    [SerializeField, ReadOnly] private AnimationsManager Animations;

    private void Awake()
    {
        Animations = this.GetComponent<AnimationsManager>();

        newUsername.onSelect.AddListener(OnSelectNewUsername);
        newUsername.onDeselect.AddListener(OnDeselectNewUsername);
        newUsername.onValueChanged.AddListener(OnValueChangedNewUsername);
        newUsername.onEndEdit.AddListener(OnEndEditNewUsername);

        newPassword.onSelect.AddListener(OnSelectNewPassword);
        newPassword.onDeselect.AddListener(OnDeselectNewPassword);
        newPassword.onValueChanged.AddListener(OnValueChangedNewPassword);
        newPassword.onEndEdit.AddListener(OnEndEditNewPassword);

        repeatPassword.onSelect.AddListener(OnSelectRepeatPassword);
        repeatPassword.onDeselect.AddListener(OnDeselectRepeatPassword);
        repeatPassword.onValueChanged.AddListener(OnValueChangedRepeatPassword);
        repeatPassword.onEndEdit.AddListener(OnEndEditRepeatPassword);

        okButton.onClick.AddListener(() => Ok());
        returnButton.onClick.AddListener(() => Return());
    }

    private void Start()
    {
        Animations.Transition.EndClip();
        usernameReleased = true;
        passwordReleased = true;
    }
    #region BUTTONS
    public void Ok()
    {
        if (usernameReleased)
        {
            AccountData.Username = username;
            log.text = "username salvo!";
        }
        else
        {
            log.text = "Escolha outro username. Este username já está em uso!";
        }
    }

    public void Return()
    {
        PlayerPrefs.DeleteKey("username");
    }
    #endregion
    #region newUsername
    public void OnSelectNewUsername(string newUsername)
    {
        usernameIsEmpty = true;
        PlaceHolderNewUsername = "";
    }

public void OnDeselectNewUsername(string newUsername)
    {
        usernameIsEmpty = (newUsername == "");
        PlaceHolderNewUsername = "New username";
    }

    public void OnValueChangedNewUsername(string newUsername)
    {
        usernameIsEmpty = (newUsername == "");
        log.text = "";
    }

    public void OnEndEditNewUsername(string newUsername)
    {
        if (AccountData.HasUsername)
        {
            log.text = (newUsername == AccountData.Username) ? "Este username já está em uso!" : usernameIsEmpty ? "" : "username válido!";
            usernameReleased = !(newUsername == AccountData.Username);
        }
        else
        {
            log.text = usernameIsEmpty ? "" : "username válido!";
            usernameReleased = true;
        }

         username = newUsername;
    }

    public string PlaceHolderNewUsername { set => newUsername.placeholder.GetComponent<TextMeshProUGUI>().text = value; }
    #endregion
    #region newPassword
    public void OnSelectNewPassword(string newPassword)
    {

    }

    public void OnDeselectNewPassword(string newPassword)
    {

    }

    public void OnValueChangedNewPassword(string newPassword)
    {

    }

    public void OnEndEditNewPassword(string newPassword)
    {

    }
    #endregion
    #region repeatPassword
    public void OnSelectRepeatPassword(string repeatPassword)
    {

    }

    public void OnDeselectRepeatPassword(string repeatPassword)
    {

    }

    public void OnValueChangedRepeatPassword(string repeatPassword)
    {

    }

    public void OnEndEditRepeatPassword(string repeatPassword)
    {

    }
    #endregion
}
