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
    SquatBentArms = 4
}

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
    public AnimationClip airSquatClip;
    public AnimationClip airSquatBentArmsClip;
    [Space(-10f, order = 0)]
    [Header("BicepCurl", order = 1)]
    public AnimationClip bicepCurlClip;
    [Space(-10f, order = 0)]
    [Header("BoxJump", order = 1)]
    public AnimationClip boxJumpClip;
    [Space(-10f, order = 0)]
    [Header("Burpee", order = 1)]
    public AnimationClip burpeeStartClip;
    public AnimationClip burpeeClip;
    public AnimationClip burpeeEndClip;
    [Space(-10f, order = 0)]
    [Header("BicycleCrunch", order = 1)]
    public AnimationClip bicycleCrunchStartClip;
    public AnimationClip bicycleCrunchClip;
    public AnimationClip bicycleCrunchEndClip;
    [Space(-10f, order = 0)]
    [Header("CircleCrunch", order = 1)]
    public AnimationClip circleCrunchClip;
    [Space(-10f, order = 0)]
    [Header("JumpingJacks", order = 1)]
    public AnimationClip jumpingJacksStartClip;
    public AnimationClip jumpingJacksClip;
    public AnimationClip jumpingJacksEndClip;
    [Space(-10f, order = 0)]
    [Header("Pistol", order = 1)]
    public AnimationClip pistolStartClip;
    public AnimationClip pistolClip;
    public AnimationClip pistolEndClip;
    [Space(-10f, order = 0)]
    [Header("PikeWalk", order = 1)]
    public AnimationClip pikeWalkClip;
    [Space(-10f, order = 0)]
    [Header("Plank", order = 1)]
    public AnimationClip plankStartClip;
    public AnimationClip plankClip;
    public AnimationClip plankEndClip;
    [Space(-10f, order = 0)]
    [Header("PushUp", order = 1)]
    public AnimationClip pushUpStartClip;
    public AnimationClip pushUpClip;
    public AnimationClip pushUpEndClip;
    [Space(-10f, order = 0)]
    [Header("QuickSteps", order = 1)]
    public AnimationClip quickStepsClip;
    [Space(-10f, order = 0)]
    [Header("Situps", order = 1)]
    public AnimationClip situpStartClip;
    public AnimationClip situpClip;
    public AnimationClip situpEndClip;
    [Space(-10f, order = 0)]
    [Header("Idles", order = 1)]
    public AnimationClip idle0Clip;
    public AnimationClip idleClip;
    public AnimationClip pantingClip;
    public AnimationClip breathingClip;
    public AnimationClip transitionEnterClip;
    public AnimationClip transitionOutClip;
    public AnimationClip offensiveClip;
    public AnimationClip boredClip;

    [Header("READ ONLY", order = 0)]
    [Space(-10f, order = 1)]
    [Header("Blends", order = 2)]
    [Range(-1f, 1f, order = 3)]
    [SerializeField] 
    private float idleBlendFloat;
    [Range(0f, 1f, order = 4)]
    [SerializeField]
    private float breathingFloat;
    [Space(-10f, order = 0)]
    [Header("Triggered", order = 1)]
    [SerializeField]
    private Trigger triggered;

    private const string IDLEBLEND = "IdleBlend";
    private const int BREATHING = 1;
    [HideInInspector]
    public AnimationClip[] clipByIndex;

    private void Awake()
    {
        clipByIndex = new AnimationClip[System.Enum.GetValues(typeof(Trigger)).Length];
        clipByIndex[0] = idleClip;
        clipByIndex[1] = boredClip;
        clipByIndex[2] = offensiveClip;
        clipByIndex[3] = airSquatClip;
        clipByIndex[4] = airSquatBentArmsClip;
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

    public void Animation(Trigger trigger)
    {
        coachControl.SetTrigger(trigger.ToString());
        triggered = trigger;
    }
}
