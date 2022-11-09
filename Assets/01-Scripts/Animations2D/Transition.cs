using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("Animators", order = 2)]
    [SerializeField, ReadOnly] private Animator transitionAnimation;
    [Space(-10, order = 0)]
    [Header("Animations Clip", order = 1)]
    [SerializeField] AnimationClip transitionStartClip;
    [SerializeField] AnimationClip transitionEndClip;

    private void Awake()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
        ScenesManagement.coroutineDelegate = DelegateDelayToCallScenes;
        transitionAnimation = this.transform.GetChild(0).GetComponent<Animator>();
    }

    public void StartClip()
    {
        transitionAnimation.Play(transitionStartClip.name);
    }

    public void StartClip(float delay)
    {
        StartCoroutine(Cor_DelayStartClip(delay));
    }

    private IEnumerator Cor_DelayStartClip(float time)
    {
        yield return new WaitForSeconds(time);
        StartClip();
    }

    public void EndClip()
    {
        transitionAnimation.Play(transitionEndClip.name);
    }

    public void EndClip(float delay)
    {
        StartCoroutine(Cor_DelayEndClip(delay));
    }

    public void CompleteClip()
    {
        StartClip();
        EndClip(StartClipTime);
    }

    private IEnumerator Cor_DelayEndClip(float time)
    {
        yield return new WaitForSeconds(time);
        EndClip();
    }

    private void DelegateDelayToCallScenes(string scene)
    {
        StartCoroutine(Cor_Delay(scene));
    }

    private IEnumerator Cor_Delay(string scene)
    {
        StartClip();
        yield return new WaitForSeconds(StartClipTime);
        SceneManager.LoadScene(scene);
    }

    public float StartClipTime => transitionStartClip.length;
    public float EndCliptime => transitionEndClip.length;
}
