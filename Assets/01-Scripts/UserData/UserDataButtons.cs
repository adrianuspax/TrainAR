using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UserDataButtons : MonoBehaviour
{
    [Header("BUTTONS", order = 0)]
    [Space(-10, order = 1)]
    [Header("Canvas Buttons", order = 2)]
    [SerializeField] private Button nextButton;
    [SerializeField] private Button closeButton;
    [Space(-10, order = 1)]
    [Header("Footer Buttons", order = 2)]
    [SerializeField] private Button saveButton;
    [SerializeField] private Button cancelButton;

    [Header("READY ONLY", order = 0)]
    [Space(-10, order = 1)]
    [Header("Variables", order = 2)]
    [SerializeField, ReadOnly] private int currentPage;
    [SerializeField, ReadOnly] private int currentField;
    [Space(-10, order = 0)]
    [Header("Scripts", order = 1)]
    [SerializeField, ReadOnly] private ButtonsManager Buttons;
    [SerializeField, ReadOnly] private PagesAndFields UserDataManager;
    [SerializeField, ReadOnly] private UserDataAnimations UserDataAnimations;

    private void Awake()
    {
        closeButton.onClick.AddListener(() => CloseApplication());
        nextButton.onClick.AddListener(() => NextButtonFunction());
        saveButton.onClick.AddListener(() => SaveButtonFunction());
        cancelButton.onClick.AddListener(() => CancelButtonFunction());

        Buttons = this.GetComponent<ButtonsManager>();
        UserDataManager = this.GetComponent<PagesAndFields>();
        UserDataAnimations = this.GetComponent<UserDataAnimations>();

        currentPage = PagesAndFields.PAGE1;
        currentField = PagesAndFields.FIELD1;
    }

    public void CloseApplication()
    {
#if UNITY_EDITOR
        Debug.Log("CloseApplication() called!");
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void NextButtonFunction()
    {
        if (currentPage >= UserDataManager.TotalPages)
        {
            Debug.Log("Fora Bolsonaro!");
            return;
        }
        else
        {
            currentPage++;
            UserDataManager.PageActivation(currentPage);
        }
    }

    public void SaveButtonFunction()
    {

    }

    public void CancelButtonFunction()
    {

    }
}