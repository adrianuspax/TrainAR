using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(AnimationsManager), typeof(ButtonsManager))]
public class Menu : MonoBehaviour
{
    [Header("BUTTONS", order = 0)]
    [Space(-10, order = 1)]
    [Header("Canvas Buttons", order = 2)]
    [SerializeField] private Button closeButton;
    [SerializeField] private Button updateProfileButton;
    [SerializeField] private Button editAccountButton;
    [SerializeField] private Button languageButton;
    [SerializeField] private Button deleteAccountButton;
    [SerializeField] private Button signOutButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button okButton;
    [Space(-10, order = 0)]
    [Header("UI", order = 1)]
    [Space(-10, order = 2)]
    [Header("Text Mesh Pro", order = 3)]
    [SerializeField] private TextMeshProUGUI usernameTMP;

    [Header(" =========================== READY ONLY =========================== ", order = 0)]

    [Header("Scripts", order = 0)]
    [SerializeField, ReadOnly] private AnimationsManager Animations;

    private void Awake()
    {
        Animations = this.GetComponent<AnimationsManager>();
        closeButton.onClick.AddListener(() => ButtonsManager.CloseApplication());
        updateProfileButton.onClick.AddListener(() => UpdateProfile());
        languageButton.onClick.AddListener(() => Language());
        deleteAccountButton.onClick.AddListener(() => DeleteAccount());
        signOutButton.onClick.AddListener(() => SignOut());
        backButton.onClick.AddListener(() => Back());
    }

    private void Start()
    {
        Animations.Transition.EndClip();
    }

    public void UpdateProfile()
    {
        ScenesManagement.LoadUserData();
    }

    public void Language()
    {
        ScenesManagement.LoadLanguage();
    }

    public void DeleteAccount()
    {
        
    }

    public void SignOut()
    {

    }

    public void Back()
    {
        ScenesManagement.LoadTrainAR();
    }

    public void Ok()
    {
        ScenesManagement.LoadRedirection();
    }
}
