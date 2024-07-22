using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeCoach : MonoBehaviour
{
    [SerializeField] private Button lupaBotton;
    [SerializeField] private Button legButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button plusButton;
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private AnimationClip coachToMuscles;
    [SerializeField] private AnimationClip musclesToCoach;
    [SerializeField] private Sprite plus;
    [SerializeField] private Sprite minus;
    [SerializeField] private Animation legAnimation;

    private bool lupaIsPressed;
    private bool plusIsPressed;

    private FloatMenu floatMenu;
    private DropDownMenu dropDownMenu;

    private void Awake()
    {
        floatMenu = this.GetComponent<FloatMenu>();
        dropDownMenu = this.GetComponent<DropDownMenu>();

        legButton.onClick.AddListener(() => ButtonLeg());
        closeButton.onClick.AddListener(() => ButtonClose());
        lupaBotton.onClick.AddListener(() => ButtonLupa());
        plusButton.onClick.AddListener(() => ButtonPlus());
    }

    private void Update()
    {
        if (lupaIsPressed)
        {
            plusButton.transform.parent.LookAt(Camera.main.transform);
        }
    }

    private void Start()
    {
        lupaBotton.gameObject.SetActive(false);
        plusButton.gameObject.SetActive(false);
    }

    public void ButtonLeg()
    {
        lupaBotton.gameObject.SetActive(true);
    }

    public void ButtonClose()
    {
        lupaBotton.gameObject.SetActive(false);
    }

    public void ButtonPlus()
    {
        plusIsPressed = !plusIsPressed;
        plusButton.image.sprite = plusIsPressed ? minus : plus;
        legAnimation.Play(plusIsPressed ? "expand" : "join");
    }

    public void ButtonLupa()
    {
        lupaIsPressed = !lupaIsPressed;

        dropDownMenu.ButtonCloseDropDown();
        floatMenu.SetBoolFloatAnimators(!lupaIsPressed);
        dropDownMenu.Interactable = !lupaIsPressed;
        fadeAnimator.Play(lupaIsPressed ? coachToMuscles.name : musclesToCoach.name);

        lupaBotton.gameObject.SetActive(lupaIsPressed);
        plusButton.gameObject.SetActive(lupaIsPressed);
    }
}
