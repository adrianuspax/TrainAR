using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private TMP_InputField loginInput;
    [SerializeField] private TMP_InputField passwordInput;

    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("Data", order = 2)]
    [SerializeField, ReadOnly] private string username;
    [SerializeField, ReadOnly] private string password;
    [Header("Scripts", order = 0)]
    [SerializeField, ReadOnly] private AnimationsManager Animations;

    private void Awake()
    {
        Animations = this.GetComponent<AnimationsManager>();
        signInButton.onClick.AddListener(() => SignInFunction());
        signOnButton.onClick.AddListener(() => SignOnFunction());

        loginInput.onSelect.AddListener(OnSelectUsername);
        loginInput.onDeselect.AddListener(OnDeselectUsername);
        loginInput.onValueChanged.AddListener(OnValueChangedUsername);
        loginInput.onEndEdit.AddListener(OnEndEditUsername);

        passwordInput.onSelect.AddListener(OnSelectPassword);
        passwordInput.onDeselect.AddListener(OnDeselectPassword);
        passwordInput.onValueChanged.AddListener(OnValueChangedPassword);
        passwordInput.onEndEdit.AddListener(OnEndEditPassword);
    }

    private void Start()
    {
        Animations.Transition.EndClip();
    }

    public void SignInFunction()
    {
        /*bool validated;
             validated = AccountData.Password == 

        if ()
        {

        }*/
    }

    public void SignOnFunction()
    {
        ScenesManagement.LoadSignOn();
    }

    public void OnSelectUsername(string username)
    {

    }

    public void OnDeselectUsername(string username)
    {

    }

    public void OnValueChangedUsername(string username)
    {
        
    }

    public void OnEndEditUsername(string username)
    {
        
    }

    public void OnSelectPassword(string password)
    {

    }

    public void OnDeselectPassword(string password)
    {

    }

    public void OnValueChangedPassword(string password)
    {

    }

    public void OnEndEditPassword(string password)
    {
        
    }
}
