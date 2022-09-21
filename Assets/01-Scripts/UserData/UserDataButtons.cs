using System.Collections;
using System.Collections.Generic;
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
    [Header("Page Two Buttons", order = 2)]
    [SerializeField] private Button maleButton;
    [SerializeField] private Button femaleButton;
    [SerializeField] private Color[] genreColorPressed;
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
    [SerializeField, ReadOnly] private Button[] genreButtonByIndex;
    [SerializeField, ReadOnly] private bool[] genreIsPressed;
    [SerializeField, ReadOnly] private bool isMale;
    [SerializeField, ReadOnly] private Color genreColorUnpressed;
    [Space(-10, order = 0)]
    [Header("Scripts", order = 1)]
    [SerializeField, ReadOnly] private ButtonsManager Buttons;
    [SerializeField, ReadOnly] private PagesAndFields PagesAndFields;
    [SerializeField, ReadOnly] private AnimationsManager Animations;
    [SerializeField, ReadOnly] private DataManagement Data;

    private const int MALE = 0;
    private const int FEMALE = 1;

    private void Awake()
    {
        genreButtonByIndex = new Button[] { maleButton, femaleButton };
        genreIsPressed = new bool[] { false, false };
        genreColorUnpressed = genreButtonByIndex[0].image.color;

        closeButton.onClick.AddListener(() => CloseApplication());
        nextButton.onClick.AddListener(() => NextButtonFunction());
        saveButton.onClick.AddListener(() => SaveButtonFunction());
        cancelButton.onClick.AddListener(() => CancelButtonFunction());
        editAge.onClick.AddListener(() => CallFooterField(0));
        editCurrentWeight.onClick.AddListener(() => CallFooterField(1));
        editWeightGoal.onClick.AddListener(() => CallFooterField(2));
        editHeight.onClick.AddListener(() => CallFooterField(3));
        genreButtonByIndex[MALE].onClick.AddListener(() => GenreButtonBehaviour(MALE));
        genreButtonByIndex[FEMALE].onClick.AddListener(() => GenreButtonBehaviour(FEMALE));

        Buttons = this.GetComponent<ButtonsManager>();
        PagesAndFields = this.GetComponent<PagesAndFields>();
        Animations = this.GetComponent<AnimationsManager>();
        Data = this.GetComponent<DataManagement>();
    }
    #region CLOSE_BUTTON
    public void CloseApplication()
    {
#if UNITY_EDITOR
        Debug.Log("CloseApplication() called!");
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    #endregion
    #region PAGE2
    public void GenreButtonBehaviour(int index)
    {
        genreIsPressed[index] = !genreIsPressed[index];

        Image icon;

        for (int i = 0; i < 2; i++)
        {
            if (i == index && genreIsPressed[index])
            {
                icon = genreButtonByIndex[index].transform.GetChild(0).GetComponent<Image>();
                genreButtonByIndex[index].image.color = genreColorPressed[index];
                icon.color = genreColorUnpressed;
            }
            else
            {
                icon = genreButtonByIndex[i].transform.GetChild(0).GetComponent<Image>();
                genreButtonByIndex[i].image.color = genreColorUnpressed;
                icon.color = genreColorPressed[i];
                genreIsPressed[i] = false;
            }
        }

        isMale = (index == 0);
        NextButtonInteraction = genreIsPressed[index];
    }

    public bool GenreButtonSelected => isMale;
    #endregion
    #region NEXT_BUTTON
    public void NextButtonFunction()
    {
        if (PagesAndFields.CurrentPage >= PagesAndFields.TotalPages)
        {
            Debug.Log("Finished");
            return;
        }
        else
        {
            Data.PagesSavePreferences(PagesAndFields.CurrentPage);
            PagesAndFields.PageActivation(PagesAndFields.CurrentPage + 1);
            NextButtonInteraction = false;
        }
    }

    public bool NextButtonInteraction
    {
        get => nextButton.IsInteractable();
        set => Buttons.Interaction(value, nextButton);
    }
    #endregion
    #region FOOTER
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
        PagesAndFields.FooterActivation(index + 1);
    }
    #endregion
    
}