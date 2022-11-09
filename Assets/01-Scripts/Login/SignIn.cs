using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AnimationsManager))]
public class SignIn : MonoBehaviour
{
    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("Scripts", order = 2)]
    [SerializeField, ReadOnly] private AnimationsManager Animations;

    private void Awake()
    {
        Animations = this.GetComponent<AnimationsManager>();
    }

    private void Start()
    {
        Animations.Transition.EndClip();
    }
}
