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
    [Header("Page Six Edit Buttons", order = 2)]
    [SerializeField] private Button editAge;
    [SerializeField] private Button editCurrentWeight;
    [SerializeField] private Button editWeightGoal;
    [SerializeField] private Button editHeight;

    [Space(-10, order = 1)]
    [Header("Footer Buttons", order = 2)]
    [SerializeField] private Button saveButton;
    [SerializeField] private Button cancelButton;

    [Header("READY ONLY", order = 0)]
    [Space(-10, order = 1)]
    [Header("Variables", order = 2)]
    [SerializeField, ReadOnly] private int currentPage;
    [SerializeField, ReadOnly] private int currentFooter;
    //[SerializeField, ReadOnly] private Button[] editButtonByIndex;

    [Space(-10, order = 0)]
    [Header("Scripts", order = 1)]
    [SerializeField, ReadOnly] private ButtonsManager Buttons;
    [SerializeField, ReadOnly] private PagesAndFields PagesAndFields;
    [SerializeField, ReadOnly] private AnimationsManager Animations;

    private void Awake()
    {
        closeButton.onClick.AddListener(() => CloseApplication());
        nextButton.onClick.AddListener(() => NextButtonFunction());
        saveButton.onClick.AddListener(() => SaveButtonFunction());
        cancelButton.onClick.AddListener(() => CancelButtonFunction());
        editAge.onClick.AddListener(() => CallFooterField(0));
        editCurrentWeight.onClick.AddListener(() => CallFooterField(1));
        editWeightGoal.onClick.AddListener(() => CallFooterField(2));
        editHeight.onClick.AddListener(() => CallFooterField(3));

        Buttons = this.GetComponent<ButtonsManager>();
        PagesAndFields = this.GetComponent<PagesAndFields>();
        Animations = this.GetComponent<AnimationsManager>();

        currentPage = PagesAndFields.PAGE1;
        currentFooter = PagesAndFields.FOOTER1;
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
        if (currentPage >= PagesAndFields.TotalPages)
        {
            Debug.Log("Fora Bolsonaro!");
            return;
        }
        else
        {
            currentPage++;
            PagesAndFields.PageActivation(currentPage);
        }
    }

    public void SaveButtonFunction()
    {
        Animations.UserData.PlayFooterSaveClip();
    }

    public void CancelButtonFunction()
    {
        Animations.UserData.PlayFooterCancelClip();
    }

    public void CallFooterField(int index)
    {
        currentFooter = index + 1;
        PagesAndFields.FooterActivation(currentFooter);
    }
}