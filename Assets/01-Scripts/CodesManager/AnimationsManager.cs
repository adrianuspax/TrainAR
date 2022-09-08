using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    [Header("ANCHOR", order = 0)]
    [SerializeField] private AnimationClip anchorARIdleStart;
    [SerializeField] private AnimationClip anchorARNoHitStart;
    [SerializeField] private AnimationClip anchorDisable;

    [Header("BUTTON AR", order = 0)]
    [SerializeField] private AnimationClip movePhoneStart;
    [SerializeField] private AnimationClip movePhoneNull;

    [Header("READY ONLY", order = 0)]
    [Space(-10, order = 1)]
    [Header("Animators", order = 2)]
    [ReadOnly] public CoachAnimations coach;
    [SerializeField, ReadOnly] private Animator anchorAnimator;
    [SerializeField, ReadOnly] private Animator movePhoneAnimator;

    public const string TagAnchor = "aniAnchorAR";
    public const string TagButtonAR = "aniButtonAR";
    public const string ParameterAnchorHit = "isHit";
    public const string ParameterAnchorEnable = "anchorEnable";

    private void Awake()
    {
        coach = this.GetComponent<CoachAnimations>();
        if (IsOnTheScene(TagAnchor)) { anchorAnimator = GetAnimator(TagAnchor); }
        if (IsOnTheScene(TagButtonAR)) { movePhoneAnimator = GetAnimator(TagButtonAR); }
    }

    private bool IsOnTheScene(string tag) => GameObject.FindWithTag(tag) != null;
    private Animator GetAnimator(string tag) => GameObject.FindGameObjectWithTag(tag).GetComponent<Animator>();
    #region ANCHOR_AR
    public void AnchorIsHit(bool isHit) => anchorAnimator.SetBool(ParameterAnchorHit, isHit);
    public void AnchorVisible(bool isVisible) => anchorAnimator.SetBool(ParameterAnchorEnable, isVisible);
    #endregion
    #region BUTTON_AR
    public void MovePhoneButton(bool isPlaying) => movePhoneAnimator.Play(isPlaying ? movePhoneStart.name : movePhoneNull.name);
    #endregion
}