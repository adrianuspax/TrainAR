using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

public class CoatchAnimations : MonoBehaviour
{
    [Header("ANIMATIONS", order = 0)]
    [Space(-10f, order = 1)]
    [Header("Animators", order = 2)]
    [SerializeField] private Animator coachControl;
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
    [SerializeField] private float idleBlendFloat;
    [Range(0f, 1f, order = 4)]
    [SerializeField] private float breathingFloat;
    [Space(-10f, order = 0)]
    [Header("Triggered", order = 1)]
    [SerializeField] private Trigger triggered;

    private const string IDLEBLEND = "IdleBlend";
    private const int BREATHING = 1;
    private AnimationClip[] clipByIndex;

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

    private void Start()
    {
        idleBlendFloat = 0.3f;
        breathingFloat = 0f;
    }

    public void IdleBlend(float blend)
    {
        coachControl.SetFloat(IDLEBLEND, blend);
        idleBlendFloat = blend;
    }

    public void Breathing(float weight)
    {
        coachControl.SetLayerWeight(BREATHING, weight);
        breathingFloat = weight;
    }

    public void StartAnimation(Trigger trigger)
    {
        coachControl.SetTrigger(trigger.ToString());
        triggered = trigger;
    }

    public void StartAnimation(int index)
    {
        coachControl.SetTrigger(((Trigger)index).ToString());
        triggered = GetTrigger(index);
    }

    public float GetClipTime(int index)
    {
        return clipByIndex[index].length;
    }

    public bool GetClipLooping(int index)
    {
        return clipByIndex[index].isLooping;
    }

    public Trigger GetTrigger(int index)
    {
        return (Trigger)index;
    }

    public string GetName(int index)
    {
        return GetTrigger(index).ToString();
    }

    public int TotalCount
    {
        get
        {
            return System.Enum.GetValues(typeof(Trigger)).Length;
        }
    }
}