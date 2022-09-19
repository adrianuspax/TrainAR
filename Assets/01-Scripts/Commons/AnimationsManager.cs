using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    [Header("READY ONLY", order = 0)]
    [Space(-10, order = 1)]
    [Header("Scripts", order = 2)]
    [SerializeField, ReadOnly] private UserDataAnimations userData;
    [SerializeField, ReadOnly] private AnimationsMain main;

    private void Awake()
    {
        if (this.GetComponent<UserDataAnimations>() != null)
        { userData = this.GetComponent<UserDataAnimations>(); }
        if (this.GetComponent<AnimationsMain>() != null)
        { main = this.GetComponent<AnimationsMain>(); }
    }

    public UserDataAnimations UserData => userData;
    public AnimationsMain Main => main;
}
