using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private AugmentedReality AR;
    private DropDownMenu DropDownMenu;
    private AnimationsManager Animations;
    private ImagesManager Sprites;

    private bool coachDropped;
    private bool dropDownIsOpen;

    private void Awake()
    {
        btnTopR.onClick.AddListener(CloseApplication);
        btnBarC.onClick.AddListener(() => SetCoachInPlace(coachDropped));
        btnBarR.onClick.AddListener(() => CallDropDownMenu(dropDownIsOpen));

        AR = this.GetComponent<AugmentedReality>();
        Animations = this.GetComponent<AnimationsManager>();
        DropDownMenu = this.GetComponent<DropDownMenu>();
        Sprites = this.GetComponent<ImagesManager>();
    }

    private void Start()
    {
        
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

    public void SetCoachInPlace(bool isSet)
    {
        isSet = !isSet;

        if (isSet) { AR.SetCoachAR(); }
        if (!isSet) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }

        btnBarC.image.sprite = isSet ? Sprites.refreshAR : Sprites.setCoach;

        coachDropped = isSet;

        Debug.Log("SetCoachInPlace() called");
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
        get
        {
            return btnBarC.IsInteractable();
        }

        set
        {
            btnBarC.interactable = value;
        }
    }
}