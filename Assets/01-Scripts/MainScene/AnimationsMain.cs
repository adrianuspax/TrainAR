using ASP;
using ASP.Extensions;
using UnityEngine;
[RequireComponent(typeof(CoachAnimations))]
public class AnimationsMain : MonoBehaviour
{
    [Header(Header.MANAGEABLE, order = 0)]
    [SerializeField] private AnimationClip movePhoneStart;

    [Header(Header.READONLY, order = 0)]
    [Space(-10, order = 1)]
    [Header(Header.animators, order = 2)]
    [SerializeField, ReadOnly] private Animator anchorAnimator;
    [SerializeField, ReadOnly] private Animator movePhoneAnimator;
    /// Reset to default values.
    private void Reset()
    {
        ComponentsAssignment();
    }

    private void Awake()
    {
        ComponentsAssignment();
    }
    /// <summary>
    /// Assignment of components and variables
    /// </summary>
    [ContextMenu("Components Assignment")]
    private void ComponentsAssignment()
    {
        if (anchorAnimator.IsNull() && _isOnTheScene(Tag.Anchor))
            anchorAnimator = _getAnimator(Tag.Anchor);
        if (movePhoneAnimator.IsNull() && _isOnTheScene(Tag.ButtonAR))
            movePhoneAnimator = _getAnimator(Tag.ButtonAR);

        ASP.SceneManagement.SceneManager.SaveCurrentScene();

        Animator _getAnimator(string tag) => GameObject.FindGameObjectWithTag(tag).GetComponent<Animator>();
        bool _isOnTheScene(string tag) => GameObject.FindWithTag(tag) != null;
    }

    public void AnchorIsHit(bool isHit) => anchorAnimator.SetBool(Parameter.AnchorHit, isHit);
    public void AnchorVisible(bool isVisible) => anchorAnimator.SetBool(Parameter.AnchorEnable, isVisible);
    public void MovePhoneButton(bool isPlaying)
    {
        movePhoneAnimator.enabled = isPlaying;

        if (movePhoneAnimator.enabled)
            movePhoneAnimator.Play(movePhoneStart.name);
    }
}