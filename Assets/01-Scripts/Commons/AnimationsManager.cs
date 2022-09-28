using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AnimationsManager))]
public class AnimationsManager : MonoBehaviour
{
    [Header(" =========================== READY ONLY =========================== ", order = 0)]

    [Header("Scripts", order = 0)]
    [SerializeField, ReadOnly] private UserDataAnimations userData;
    [SerializeField, ReadOnly] private AnimationsMain main;

    private void Awake()
    {
        if (this.GetComponent<UserDataAnimations>() != null) { userData = this.GetComponent<UserDataAnimations>(); }
        if (this.GetComponent<AnimationsMain>() != null) { main = this.GetComponent<AnimationsMain>(); }
    }

    public UserDataAnimations UserData => userData;
    public AnimationsMain Main => main;
}
