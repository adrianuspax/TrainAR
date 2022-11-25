using UnityEngine;
[RequireComponent(typeof(CoachAnimations), typeof(AnimationsManager))]
public class AnimationsMain : MonoBehaviour
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
    [SerializeField, ReadOnly] private Animator anchorAnimator;
    [SerializeField, ReadOnly] private Animator movePhoneAnimator;

    private void Awake()
    {
        if (IsOnTheScene(Tag.Anchor))
        { anchorAnimator = GetAnimator(Tag.Anchor); }
        if (IsOnTheScene(Tag.ButtonAR))
        { movePhoneAnimator = GetAnimator(Tag.ButtonAR); }
    }

    private bool IsOnTheScene(string tag) => GameObject.FindWithTag(tag) != null;
    private Animator GetAnimator(string tag) => GameObject.FindGameObjectWithTag(tag).GetComponent<Animator>();
    #region ANCHOR_AR
    public void AnchorIsHit(bool isHit) => anchorAnimator.SetBool(Parameter.AnchorHit, isHit);
    public void AnchorVisible(bool isVisible) => anchorAnimator.SetBool(Parameter.AnchorEnable, isVisible);
    #endregion
    #region BUTTON_AR
    public void MovePhoneButton(bool isPlaying)
    {
        movePhoneAnimator.enabled = isPlaying;

        if (movePhoneAnimator.enabled)
        { movePhoneAnimator.Play(movePhoneStart.name); }
    }
    #endregion
}