using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum StateButtonAR
{
    ReadyToSet = 0,
    Uninteractable = 1,
    Refresh = 2
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

    //private bool coachSeted;
    private bool dropDownIsOpen;

    private void Awake()
    {
        btnTopR.onClick.AddListener(() => CloseApplication());
        btnBarC.onClick.AddListener(() => AR.SetCoachAR());
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

    /*public void SetCoachInPlace(bool set)
    {
        set = !set;

        if (set) { AR.SetCoachAR(); }
        else { ButtonARBehaviour(StateButtonAR.Refresh); }
        
        //Animations.AnchorEnable(false);
        
        //if (!isSet) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }

        //btnBarC.image.sprite = isSet ? Sprites.refreshAR : Sprites.setCoach;

        coachSeted = set;
    }*/

    public void ButtonARBehaviour(StateButtonAR state)
    {
        switch (state)
        {
            case StateButtonAR.ReadyToSet:
                Animations.AnchorVisible(true);
                Animations.MovePhoneButton(false);
                btnBarC.image.sprite = Sprites.setCoachAR;
                ButtonSetCoachIntercatable = true;
                break;
            case StateButtonAR.Uninteractable:
                Animations.AnchorVisible(true);
                Animations.MovePhoneButton(true);
                ButtonSetCoachIntercatable = false;
                break;
            case StateButtonAR.Refresh:
                Animations.AnchorVisible(false);
                Animations.MovePhoneButton(false);
                btnBarC.image.sprite = Sprites.replaceCoachAR;
                ButtonSetCoachIntercatable = true;
                break;
            default:
                Animations.AnchorVisible(true);
                Animations.MovePhoneButton(false);
                btnBarC.image.sprite = Sprites.setCoachAR;
                ButtonSetCoachIntercatable = true;
                break;
        }

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

    public bool ButtonSetCoachIntercatable
    {
        get => btnBarC.IsInteractable();
        set => btnBarC.interactable = value;
    }
}