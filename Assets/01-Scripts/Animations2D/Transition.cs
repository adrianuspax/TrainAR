using ASP;
using ASP.Extensions;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [Header(Header.READONLY, order = 0)]
    [Space(-10, order = 1)]
    [Header(Header.animators, order = 2)]
    [SerializeField, ReadOnly] private Animator animator;

    [Serializable]
    public struct AnimationControl
    {
        public string name;

        public AnimationControl(string name)
        {
            this.name = name;
        }

        public static (AnimationControl start,
                       AnimationControl idle,
                       AnimationControl end,
                       AnimationControl @null)
        State => (new("TransitionStart"),
                  new("TransitionIdle"),
                  new("TransitionEnd"),
                  new("TransitionNull"));

        public static AnimationControl Parameter => new("isStarts");
    }

    private void Awake()
    {
        ComponentsAssignment();
    }
    /// This function is called when the object becomes enabled and active.
    private void OnEnable()
    {
        ScenesManagement.coroutineDelegate = DelegateDelayToCallScenes;
    }
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    private void Start()
    {
        LanguageManager.LoadLang(AccountData.Language);
    }
    /// <summary>
    /// Assignment of components and variables
    /// </summary>
    [ContextMenu("Components Assignment")]
    private void ComponentsAssignment()
    {
        if (animator.IsNull())
            this.GetComponentFrom(out animator, 0);
    }

    public void StartClip()
    {
        animator.SetBool(AnimationControl.Parameter.name, true);
    }

    public void StartClip(float delay)
    {
        StartCoroutine(DelayClip(delay, () => animator.SetBool(AnimationControl.Parameter.name, true)));
    }

    public void EndClip()
    {
        animator.SetBool(AnimationControl.Parameter.name, false);
    }

    public void EndClip(float delay)
    {
        StartCoroutine(DelayClip(delay, () => animator.SetBool(AnimationControl.Parameter.name, false)));
    }

    private IEnumerator DelayClip(float time, UnityAction action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }

    private void DelegateDelayToCallScenes(string scene)
    {
        ScenesManagement.PreviousScene = SceneManager.GetActiveScene().name;
        StartCoroutine(_delay(scene));

        IEnumerator _delay(string scene)
        {
            StartClip();
            yield return new WaitForSeconds(animator.GetAnimationClipLength(AnimationControl.State.start.name));
            SceneManager.LoadScene(scene);
        }
    }
}
