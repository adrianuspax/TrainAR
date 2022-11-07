using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(ButtonsManager))]
public class Menu : MonoBehaviour
{
    [Header("BUTTONS", order = 0)]
    [Space(-10, order = 1)]
    [Header("Canvas Buttons", order = 2)]
    [SerializeField] private Button closeButton;
    [SerializeField] private Button editProfileButton;
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

    /*[Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("Scripts", order = 2)]
    [SerializeField, ReadOnly] private ButtonsManager Buttons;*/

    private void Awake()
    {
        closeButton.onClick.AddListener(() => ButtonsManager.CloseApplication());
        editProfileButton.onClick.AddListener(() => ScenesManagement.LoadUserData());
        backButton.onClick.AddListener(() => ScenesManagement.LoadTrainAR());
        languageButton.onClick.AddListener(() => ScenesManagement.LoadLanguageScene());

    }
}
