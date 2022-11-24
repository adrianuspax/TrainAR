using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [Header("Animations", order = 0)]
    [SerializeField] private Animator loadingSysAnimator;
    [SerializeField] private AnimationClip loadingSysStartClip;
    [SerializeField] private AnimationClip loadingSysEndClip;
    [Header("UI", order = 0)]
    [SerializeField] private Image circleLoad;
    [SerializeField] private TextMeshProUGUI countTMP;

    [Header("Read Only", order = 0)]
    [Space(-10, order = 1)]
    [Header("Variables", order = 2)]
    [SerializeField, ReadOnly] private bool loadingIsStart;
    [SerializeField, ReadOnly] private float animationTime;
    [SerializeField, ReadOnly] private int totalAmount;
    [SerializeField, ReadOnly] private float totalTime;

    [Header("Scripts", order = 0)]
    [SerializeField, ReadOnly] private AnimationsManager Animations;

    private void Awake()
    {
        Animations = this.GetComponent<AnimationsManager>();
    }

    private void Start()
    {
        loadingIsStart = false;
    }

    public void StartLoadind(bool start)
    {
        loadingSysAnimator.Play(start ? loadingSysStartClip.name : loadingSysEndClip.name);
        loadingIsStart = start;
    }

    public void LoadingCountdown(int amount, Trigger animation)
    {
        totalAmount = amount;
        animationTime = Animations.Coach.GetClipTime(animation);
        totalTime = totalAmount * animationTime;
            
        StartLoadind(true);
        StartCoroutine(Cor_Countdown(totalTime));
    }

    public void LoadingCountup(Trigger animation)
    {
        animationTime = Animations.Coach.GetClipTime(animation);
        StartCoroutine(Cor_Countup());
    }

    private IEnumerator Cor_Countdown(float t)
    {
        yield return new WaitForSeconds(loadingSysStartClip.length + 0.1f);
        loadingSysAnimator.enabled = false;
        circleLoad.fillAmount = 1f;
        countTMP.text = totalAmount.ToString();

        while (t > 0f)
        {
            t -= Time.deltaTime;
            circleLoad.fillAmount = t / totalTime;
            countTMP.text = Mathf.RoundToInt((t / animationTime)).ToString() + "x";
            yield return null;
        }

        yield return new WaitForSeconds(0.75f);
        loadingSysAnimator.enabled = true;
        StartLoadind(false);
    }

    private IEnumerator Cor_Countup()
    {
        yield return new WaitForSeconds(loadingSysStartClip.length + 0.1f);
        loadingSysAnimator.enabled = false;
        circleLoad.fillAmount = 1f;
        countTMP.text = "0x";

        float time = 0.0001f;

        while(time > 0)
        {
            if (Animations.Coach.IsStarted)
            {
                time += Time.deltaTime;
                countTMP.text = Mathf.RoundToInt((time / animationTime)).ToString() + "x";
            }
            else
            {
                time = 0;
            }
            
        }

        yield return new WaitForSeconds(0.75f); //colocar o tempo de transição para idle
        loadingSysAnimator.enabled = true;
        StartLoadind(false);
    }



    public void Test()
    {
        LoadingCountdown(5, Trigger.JumpingJacks);
    }
}
