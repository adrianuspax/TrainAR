using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AnimationsManager))]
public class FloatMenu : MonoBehaviour
{
    [Header("BUTTONS", order = 0)]
    [Space(-10, order = 1)]
    [Header("Header", order = 2)]
    [SerializeField] private Transform buttonsFloatHeader;

    [Header("READY ONLY", order = 0)]
    [Space(-10, order = 1)]
    [Header("Variables", order = 2)]
    [NonReorderable][SerializeField, ReadOnly] private Button[] buttonFloat;
    [NonReorderable][SerializeField, ReadOnly] private Transform[] buttonFloatPivot;
    [NonReorderable][SerializeField, ReadOnly] private Animator[] buttonFloatAnimator;

    private const string animationParameter = "floatIsOpen";

    private void Awake()
    {
        buttonFloatAnimator = AssignAnimators(FloatHeader);
        buttonFloatPivot = AssignPivots(AnimatorFloat);
        buttonFloat = AssignButtons(PivotFloat);
    }

    public int TotalFloats => FloatHeader.childCount;
    public Transform FloatHeader => buttonsFloatHeader;

    private Animator[] AssignAnimators(Transform header)
    {
        Animator[] animator;
        animator = new Animator[TotalFloats];

        for (int i = 0; i < TotalFloats; i++)
        {
            animator[i] = header.GetChild(i).GetChild(0).GetComponent<Animator>();
        }

        return animator;
    }
    public Animator[] AnimatorFloat => buttonFloatAnimator;

    private Transform[] AssignPivots(Animator[] animator)
    {
        Transform[] pivot;
        pivot = new Transform[TotalFloats];

        for (int i = 0; i < TotalFloats; i++)
        {
            pivot[i] = animator[i].transform.GetChild(0).transform;
        }

        return pivot;
    }
    public Transform[] PivotFloat => buttonFloatPivot;

    private Button[] AssignButtons(Transform[] pivot)
    {
        Button[] button;
        button = new Button[TotalFloats];

        for (int i = 0; i < TotalFloats; i++)
        {
            button[i] = pivot[i].GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>();
        }

        return button;
    }
    public Button[] ButtonFloat => buttonFloat;

    public void LookingAtCam(bool isLooking)
    {
        if (isLooking)
        {
            foreach (Transform pivots in PivotFloat)
            {
                pivots.LookAt(Camera.main.transform);
            }
        }
    }

    public bool FloatsIsOpen
    {
        get
        {
            int index;
            index = Random.Range(0, TotalFloats);
            return AnimatorFloat[index].GetBool(animationParameter);
        }

        set
        {
            foreach (Animator animators in AnimatorFloat)
            {
                animators.SetBool(animationParameter, value);
            }
        }
    }
}