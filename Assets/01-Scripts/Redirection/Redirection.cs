using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AnimationsManager))]
public class Redirection : MonoBehaviour
{
    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    
    [Header("Scripts", order = 1)]
    [SerializeField, ReadOnly] private AnimationsManager Animations;

    private void Awake()
    {
        Animations = this.GetComponent<AnimationsManager>();
    }

    private void Start()
    {
        Animations.Transition.StartClip();
    }

    public void RedirectionScene()
    {
        if (AccountData.HasLanguage)
        {
            ScenesManagement.LoadSignIn();
        }
        else
        {
            ScenesManagement.LoadLanguage();
        }
    }
}