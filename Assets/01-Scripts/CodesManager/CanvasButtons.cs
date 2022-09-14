using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum StateButtonAR
{
    ReadyToSet = 0,
    Refresh = 1
};

public class CanvasButtons : MonoBehaviour
{
    [Header("BUTTONS", order = 0)]
    [Space(-10, order = 1)]
    [Header("Top Area", order = 2)]
    [SerializeField] private Button btnTopL;
    [SerializeField] private Button btnTopR;
    [Space(-10, order = 0)]
    [Header("Top Area", order = 1)]
    [SerializeField] private Button btnBarL;
    [SerializeField] private Button btnBarC;
    [SerializeField] private Button btnBarR;

    [Header("READY ONLY", order = 0)]
    [Space(-10, order = 1)]
    [Header("Variables", order = 2)]
    [SerializeField, ReadOnly] private StateButtonAR stateButtonAR;
    [Space(-10, order = 1)]
    [Header("Scripts", order = 2)]
    [SerializeField, ReadOnly] private AugmentedReality AR;
    [SerializeField, ReadOnly] private DropDownMenu DropDownMenu;
    [SerializeField, ReadOnly] private AnimationsManager Animations;
    [SerializeField, ReadOnly] private ImagesManager Sprites;

    private bool dropDownIsOpen;

    private void Awake()
    {
        btnTopR.onClick.AddListener(() => CloseApplication());
        btnBarC.onClick.AddListener(() => AR.PlaceCoachAR());
        btnBarR.onClick.AddListener(() => CallDropDownMenu(dropDownIsOpen));

        AR = this.GetComponent<AugmentedReality>();
        Animations = this.GetComponent<AnimationsManager>();
        DropDownMenu = this.GetComponent<DropDownMenu>();
        Sprites = this.GetComponent<ImagesManager>();
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

    public void ButtonARBehaviour(StateButtonAR state, bool isInteractable)
    {
        bool condition;

        switch (state)
        {
            case StateButtonAR.ReadyToSet:
                btnBarC.image.sprite = Sprites.setCoachAR;
                break;
            case StateButtonAR.Refresh:
                btnBarC.image.sprite = Sprites.replaceCoachAR;
                break;
        }

        condition = CurrentStateButtonAR == StateButtonAR.Refresh || isInteractable;
        ButtonSetCoachInteraction = condition && !Animations.coach.IsStarted;
        //ButtonSetCoachInteraction = Animations.coach.AnimationIsStarted;

        if (ButtonSetCoachInteraction)
        {
            btnBarC.image.rectTransform.localScale = Vector3.one;
            btnBarC.image.rectTransform.localRotation = Quaternion.Euler(Vector3.zero);
            btnBarC.image.rectTransform.anchoredPosition = Vector3.zero;
        }

        Animations.MovePhoneButton(!condition);
        stateButtonAR = state;
    }

    public void CallDropDownMenu(bool called)
    {
        called = !called;

        if (called) { DropDownMenu.ButtonOpenDropDown(); }
        if (!called) { DropDownMenu.ButtonCloseDropDown(); }
        btnBarR.image.transform.localScale = new Vector2(called ? -1 : 1, 1);

        dropDownIsOpen = called;
    }

    public bool ButtonSetCoachInteraction
    {
        get => btnBarC.IsInteractable();
        set => btnBarC.interactable = value;
    }

    public bool ButtonDDMInteraction
    {
        get => btnBarR.IsInteractable();
        set => btnBarR.interactable = value;
    }

    public StateButtonAR CurrentStateButtonAR
    {
        get => stateButtonAR;
    }
}