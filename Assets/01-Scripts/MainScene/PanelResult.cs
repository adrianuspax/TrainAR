using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelResult : MonoBehaviour
{
    [Header("Animation", order = 0)]
    [SerializeField] private Animator panelResultAnimator;
    [SerializeField] private AnimationClip panelResultFadeInClip;
    [SerializeField] private AnimationClip panelResultFadeOutClip;
}
