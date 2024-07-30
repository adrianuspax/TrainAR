using ASP;
using ASP.Extensions;
using UnityEngine;

public class Coach : MonoBehaviour
{
    public enum Trigger
    {
        Idle = 0,
        Bored = 1,
        Offensive = 2,
        Squat = 3,
        SquatBentArms = 4,
        BicepCurl = 5,
        BoxJump = 6,
        Burpee = 7,
        BicycleCrunch = 8,
        CircleCrunch = 9,
        JumpingJacks = 10,
        Pistol = 11,
        Plank = 12,
        PikeWalk = 13,
        PushUp = 14,
        QuickSteps = 15,
        Situps = 16
    };

    [Header(Header.MANAGEABLE, order = 0)]
    [Space(-10, order = 1)]
    [Header(Header.animationClips, order = 2)]
    [Space(-10f, order = 2)]
    [Header("AirSquat", order = 3)]
    [SerializeField] private AnimationClip airSquatClip;
    [SerializeField] private AnimationClip airSquatBentArmsClip;
    [Space(-10f, order = 0)]
    [Header("BicepCurl", order = 1)]
    [SerializeField] private AnimationClip bicepCurlClip;
    [Space(-10f, order = 0)]
    [Header("BoxJump", order = 1)]
    [SerializeField] private AnimationClip boxJumpClip;
    [Space(-10f, order = 0)]
    [Header("Burpee", order = 1)]
    [SerializeField] private AnimationClip burpeeStartClip;
    [SerializeField] private AnimationClip burpeeClip;
    [SerializeField] private AnimationClip burpeeEndClip;
    [Space(-10f, order = 0)]
    [Header("BicycleCrunch", order = 1)]
    [SerializeField] private AnimationClip bicycleCrunchStartClip;
    [SerializeField] private AnimationClip bicycleCrunchClip;
    [SerializeField] private AnimationClip bicycleCrunchEndClip;
    [Space(-10f, order = 0)]
    [Header("CircleCrunch", order = 1)]
    [SerializeField] private AnimationClip circleCrunchClip;
    [Space(-10f, order = 0)]
    [Header("JumpingJacks", order = 1)]
    [SerializeField] private AnimationClip jumpingJacksStartClip;
    [SerializeField] private AnimationClip jumpingJacksClip;
    [SerializeField] private AnimationClip jumpingJacksEndClip;
    [Space(-10f, order = 0)]
    [Header("Pistol", order = 1)]
    [SerializeField] private AnimationClip pistolStartClip;
    [SerializeField] private AnimationClip pistolClip;
    [SerializeField] private AnimationClip pistolEndClip;
    [Space(-10f, order = 0)]
    [Header("PikeWalk", order = 1)]
    [SerializeField] private AnimationClip pikeWalkClip;
    [Space(-10f, order = 0)]
    [Header("Plank", order = 1)]
    [SerializeField] private AnimationClip plankStartClip;
    [SerializeField] private AnimationClip plankClip;
    [SerializeField] private AnimationClip plankEndClip;
    [Space(-10f, order = 0)]
    [Header("PushUp", order = 1)]
    [SerializeField] private AnimationClip pushUpStartClip;
    [SerializeField] private AnimationClip pushUpClip;
    [SerializeField] private AnimationClip pushUpEndClip;
    [Space(-10f, order = 0)]
    [Header("QuickSteps", order = 1)]
    [SerializeField] private AnimationClip quickStepsClip;
    [Space(-10f, order = 0)]
    [Header("Situps", order = 1)]
    [SerializeField] private AnimationClip situpStartClip;
    [SerializeField] private AnimationClip situpClip;
    [SerializeField] private AnimationClip situpEndClip;
    [Space(-10f, order = 0)]
    [Header("Idles", order = 1)]
    [SerializeField] private AnimationClip idle0Clip;
    [SerializeField] private AnimationClip idleClip;
    [SerializeField] private AnimationClip pantingClip;
    [SerializeField] private AnimationClip breathingClip;
    [SerializeField] private AnimationClip transitionEnterClip;
    [SerializeField] private AnimationClip transitionOutClip;
    [SerializeField] private AnimationClip offensiveClip;
    [SerializeField] private AnimationClip boredClip;

    [Header(Header.READONLY, order = 0)]
    [Space(-10f, order = 1)]
    [Header(Header.components, order = 2)]
    [SerializeField, ReadOnly] private Animator animator;
    [Header("Blends", order = 2)]
    [SerializeField, Range(-1f, 1f), ReadOnly] private float breathingFloat;
    [Space(-10f, order = 0)]
    [Header("Triggered", order = 1)]
    [SerializeField, ReadOnly] private Trigger triggered;
    [Space(-10f, order = 0)]
    [Header("AnimationsClips Indexing", order = 1)]
    [SerializeField, ReadOnly] private AnimationClip[] clipByIndex;
    [Space(-10f, order = 0)]
    [Header("Booleans", order = 1)]
    [SerializeField, ReadOnly] private bool isAnimationStarted;
# if UNITY_EDITOR
    [Header("Animations Values", order = 0)]
#pragma warning disable IDE0052
    [SerializeField, ReadOnly] private string animationNormalizedTime;
    [SerializeField, ReadOnly] private string animationLength;
    [SerializeField, ReadOnly] private string animationCountdown;
    [SerializeField, ReadOnly] private string animatonCountup;
    [SerializeField, ReadOnly] private string animationLoopCount;

    [Space(-10f, order = 0)]
    [Header("Transitions Values", order = 1)]
    [SerializeField, ReadOnly] private string transitionNormalizedTime;
    [SerializeField, ReadOnly] private string transitionDuration;
    [SerializeField, ReadOnly] private string transitionCountdown;

    [Header("States And Transitions", order = 0)]
    [SerializeField, ReadOnly] private bool isIdle;
    [SerializeField, ReadOnly] private bool enterWorkout;
    [SerializeField, ReadOnly] private bool isWorkout;
    [SerializeField, ReadOnly] private bool outWorkout;
    [SerializeField, ReadOnly] private bool isTransition;
    [SerializeField, ReadOnly] private bool toIdle;
#pragma warning restore IDE0052
    private void InformationsLog()
    {
        animationNormalizedTime = CurrentAnimation.normalizedTime.ToString();
        animationLength = CurrentAnimation.length.ToString();
        animationCountdown = CurrentAnimation.countdown.ToString();
        animatonCountup = CurrentAnimation.countup.ToString();
        animationLoopCount = CurrentAnimation.loopCount.ToString();
        transitionNormalizedTime = CurrentTransition.normalizedTime.ToString();
        transitionDuration = CurrentTransition.duration.ToString();
        transitionCountdown = CurrentTransition.countdown.ToString();
        isIdle = CurrentAnimation.isIdle;
        isTransition = CurrentTransition.isTransition;
        toIdle = CurrentTransition.toIdle;
        enterWorkout = CurrentAnimation.isEnter;
        isWorkout = CurrentAnimation.isWorkout;
        outWorkout = CurrentAnimation.isOut;
#else
        return;
#endif
    }
    /// Awake is called when an enabled script instance is being loaded.
    private void Awake()
    {
        ComponentsAssignment();
    }

    private void Update()
    {
        InformationsLog();
    }
    /// <summary>
    /// Assignment of components and variables
    /// </summary>
    [ContextMenu("Components Assignment")]
    private void ComponentsAssignment()
    {
         this.GetComponentIfNull(ref animator);

        if (clipByIndex.IsNullOrEmpty())
        {
            clipByIndex = new AnimationClip[TotalCount];
            clipByIndex[(int)Trigger.Idle] = idleClip;
            clipByIndex[(int)Trigger.Bored] = boredClip;
            clipByIndex[(int)Trigger.Offensive] = offensiveClip;
            clipByIndex[(int)Trigger.Squat] = airSquatClip;
            clipByIndex[(int)Trigger.SquatBentArms] = airSquatBentArmsClip;
            clipByIndex[(int)Trigger.BicepCurl] = bicepCurlClip;
            clipByIndex[(int)Trigger.BoxJump] = boxJumpClip;
            clipByIndex[(int)Trigger.Burpee] = burpeeClip;
            clipByIndex[(int)Trigger.BicycleCrunch] = bicycleCrunchClip;
            clipByIndex[(int)Trigger.CircleCrunch] = circleCrunchClip;
            clipByIndex[(int)Trigger.JumpingJacks] = jumpingJacksClip;
            clipByIndex[(int)Trigger.Pistol] = pistolClip;
            clipByIndex[(int)Trigger.Plank] = plankClip;
            clipByIndex[(int)Trigger.PikeWalk] = pikeWalkClip;
            clipByIndex[(int)Trigger.PushUp] = pushUpClip;
            clipByIndex[(int)Trigger.QuickSteps] = quickStepsClip;
            clipByIndex[(int)Trigger.Situps] = situpClip;
        }

        ASP.SceneManagement.SceneManager.SaveCurrentScene();
    }

    public void Breathing(float weight)
    {
        animator.SetFloat(breathing, weight);
        weight.ComparativeAssignment(ref breathingFloat);
    }

    public void StartAnimation(Trigger trigger)
    {
        animator.SetTrigger(trigger.ToString());
        isAnimationStarted = !(trigger == 0);
        trigger.ComparativeAssignment(ref triggered);
    }

    public void StartAnimation(int index)
    {
        animator.SetTrigger(((Trigger)index).ToString());
        isAnimationStarted = !(index == 0);
        GetTrigger(index).ComparativeAssignment(ref triggered);
    }

    public void StartIdleAnimation()
    {
        StartAnimation(Trigger.Idle);
    }

    public float GetClipTime(int index) => clipByIndex[index].length;
    public float GetClipTime(Trigger trigger) => clipByIndex[(int)trigger].length;
    public bool GetClipLooping(int index) => clipByIndex[index].isLooping;
    public Trigger GetTrigger(int index) => (Trigger)index;
    public string GetName(int index) => GetTrigger(index).ToString();
    public int TotalCount => System.Enum.GetValues(typeof(Trigger)).Length;
    public bool HasTriggered => isAnimationStarted;
    public Animator Animator => animator;

    public const string TransitionUserName = "ToIdle";
    public const string StateIdleBlend = "IdleBlend";
    public const string breathing = "BreathingBlend";

    public (string startWorkout,
            string inWorkout,
            string finishWorkout)
    StateTag
    {
        get => ("enterWorkout",
                "workout",
                "outWorkout");
    }

    public (float normalizedTime,
            float normalizedReverseTime,
            float length, float countdown,
            float countup, int loopCount,
             bool isWorkout,
             bool isIdle,
             bool isEnter,
             bool isOut)
    CurrentAnimation
    {
        get
        {
            AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            float normalizedTime = animatorStateInfo.normalizedTime;
            float length = animatorStateInfo.length;

            return (normalizedTime,
               1 - (normalizedTime % 1),
                    length,
                    length - (normalizedTime % 1 * length),
                    length * normalizedTime,
               (int)normalizedTime,
                    animatorStateInfo.IsTag(StateTag.inWorkout),
                    animatorStateInfo.IsName(StateIdleBlend),
                    animatorStateInfo.IsTag(StateTag.startWorkout),
                    animatorStateInfo.IsTag(StateTag.finishWorkout));
        }
    }

    public (float normalizedTime,
            float normalizedReverseTime,
            float length,
             bool willBeWorkout,
             bool willBeIdle,
             bool willBeEnter,
             bool willBeOut)
    NextAnimation
    {
        get
        {
            AnimatorStateInfo animatorStateInfo = animator.GetNextAnimatorStateInfo(0);
            float normalizedTime = animatorStateInfo.normalizedTime;
            float length = animatorStateInfo.length;

            return (normalizedTime,
               1 - (normalizedTime % 1),
                    length,
                    animatorStateInfo.IsTag(StateTag.inWorkout),
                    animatorStateInfo.IsName(StateIdleBlend),
                    animatorStateInfo.IsTag(StateTag.startWorkout),
                    animatorStateInfo.IsTag(StateTag.finishWorkout));
        }
    }

    public (float normalizedTime,
            float duration,
            float countdown,
            bool isTransition,
            bool toIdle)
    CurrentTransition
    {
        get
        {
            AnimatorTransitionInfo animatorTransitionInfo = animator.GetAnimatorTransitionInfo(0);
            float normalizedTime = animatorTransitionInfo.normalizedTime;
            float duration = animatorTransitionInfo.duration;

            return (normalizedTime,
                    duration,
                    duration - (normalizedTime * duration),
                    duration != 0,
                    animatorTransitionInfo.IsUserName(TransitionUserName));
        }
    }
}