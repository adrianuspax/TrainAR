using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    [Header("ANCHOR")]
    private Animator anchorAnimator;
    [SerializeField] private AnimationClip anchorARIdleStart;
    [SerializeField] private AnimationClip anchorARNoHitStart;

    [HideInInspector] public CoachAnimations coach;

    public const string TagAnchor = "aniAnchorAR";
    public const string ParameterAnchor = "isHit";

    private void Awake()
    {
        coach = this.GetComponent<CoachAnimations>();
        if (IsOnTheScene(TagAnchor)) { anchorAnimator = GetAnimator(TagAnchor); }
    }

    private void Start()
    {

    }

    private bool IsOnTheScene(string tag)
    {
        return GameObject.FindWithTag(tag) != null;
    }

    private Animator GetAnimator(string tag)
    {
        return GameObject.FindGameObjectWithTag(tag).GetComponent<Animator>();
    }

    #region ANCHOR_AR
    public void AnchorIsHit(bool isHit)
    {
        anchorAnimator.SetBool(ParameterAnchor, isHit);
    }
    #endregion
}