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
    private Trigger trigered;

    private const string IDLEBLEND = "IdleBlend";
    private const int BREATHING = 1;

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
        trigered = trigger;
    }
}
