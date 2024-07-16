using UnityEngine;
using ASP;
using Debug = ASP.Debug;

public class CoachAnimations : MonoBehaviour
{
    [Header("ANIMATIONS", order = 0)]
    [Space(-10f, order = 1)]
    [Header("Animators", order = 2)]
    public Animator coachControl;
    [Space(-10f, order = 0)]
    [Header("CLIPS", order = 1)]
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

    [Header("READ ONLY", order = 0)]
    [Space(-10f, order = 1)]
    [Header("Blends", order = 2)]
    [Range(-1f, 1f, order = 3)]
    [SerializeField, ReadOnly] private float breathingFloat;
    [Space(-10f, order = 0)]
    [Header("Triggered", order = 1)]
    [SerializeField, ReadOnly] private Trigger triggered;
    [Space(-10f, order = 0)]
    [Header("AnimationsClips Indexing", order = 1)]
    [SerializeField, ReadOnly] private AnimationClip[] clipByIndex;
    [Space(-10f, order = 0)]
    [Header("Booleans", order = 1)]
    [SerializeField, ReadOnly] private bool AnimationStarted;

    [Header("Animations Values", order = 0)]
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

    private void Awake()
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

    private void Update()
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
    }

    public void Breathing(float weight)
    {
        coachControl.SetFloat(Parameter.Breathing, weight);
        breathingFloat = weight;
    }

    public void StartAnimation(Trigger trigger)
    {
        coachControl.SetTrigger(trigger.ToString());
        triggered = trigger;
        HasTriggered = !(trigger == 0);
    }

    public void StartAnimation(int index)
    {
        coachControl.SetTrigger(((Trigger)index).ToString());
        triggered = GetTrigger(index);
        HasTriggered = !(index == 0);
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
    public bool HasTriggered
    {
        get => AnimationStarted;
        private set => AnimationStarted = value;
    }

    public (float normalizedTime, float normalizedReverseTime, float length, float countdown, float countup, int loopCount, bool isWorkout, bool isIdle, bool isEnter, bool isOut) CurrentAnimation =>
        (
        coachControl.GetCurrentAnimatorStateInfo(0).normalizedTime,
        (1 - (coachControl.GetCurrentAnimatorStateInfo(0).normalizedTime % 1)),
        coachControl.GetCurrentAnimatorStateInfo(0).length,
        coachControl.GetCurrentAnimatorStateInfo(0).length - (coachControl.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 * coachControl.GetCurrentAnimatorStateInfo(0).length),
        coachControl.GetCurrentAnimatorStateInfo(0).length * coachControl.GetCurrentAnimatorStateInfo(0).normalizedTime,
        (int)coachControl.GetCurrentAnimatorStateInfo(0).normalizedTime,
        coachControl.GetCurrentAnimatorStateInfo(0).IsTag(StateTag.Workout),
        coachControl.GetCurrentAnimatorStateInfo(0).IsName(State.IdleBlend),
        coachControl.GetCurrentAnimatorStateInfo(0).IsTag(StateTag.EnterWorkout),
        coachControl.GetCurrentAnimatorStateInfo(0).IsTag(StateTag.OutWorkout)
        );

    public (float normalizedTime, float normalizedReverseTime, float length, bool willBeWorkout, bool willBeIdle, bool willBeEnter, bool willBeOut) NextAnimation =>
        (
        coachControl.GetNextAnimatorStateInfo(0).normalizedTime,
        (1 - (coachControl.GetNextAnimatorStateInfo(0).normalizedTime % 1)),
        coachControl.GetNextAnimatorStateInfo(0).length,
        coachControl.GetNextAnimatorStateInfo(0).IsTag(StateTag.Workout),
        coachControl.GetNextAnimatorStateInfo(0).IsName(State.IdleBlend),
        coachControl.GetNextAnimatorStateInfo(0).IsTag(StateTag.EnterWorkout),
        coachControl.GetNextAnimatorStateInfo(0).IsTag(StateTag.OutWorkout)
        );

    public (float normalizedTime, float duration, float countdown, bool isTransition, bool toIdle) CurrentTransition =>
        (
        coachControl.GetAnimatorTransitionInfo(0).normalizedTime,
        coachControl.GetAnimatorTransitionInfo(0).duration,
        coachControl.GetAnimatorTransitionInfo(0).duration - (coachControl.GetAnimatorTransitionInfo(0).normalizedTime * coachControl.GetAnimatorTransitionInfo(0).duration),
        coachControl.GetAnimatorTransitionInfo(0).duration != 0,
        coachControl.GetAnimatorTransitionInfo(0).IsUserName(TransitionUserName.ToIdle)
        );
}