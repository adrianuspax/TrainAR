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

    [Header("Scripts", order = 0)]
    [SerializeField, ReadOnly] private AnimationsManager Animations;
    [SerializeField, ReadOnly] private ButtonsManager Buttons;

    private void Awake()
    {
        Animations = this.GetComponent<AnimationsManager>();
        Buttons = this.GetComponent<ButtonsManager>();
    }

    private void Start()
    {
        loadingIsStart = false;
    }

    private void Update()
    {
        if (Buttons.DropDownMenu.IsPressed)
        {
            LoadBehaviourDDM();
        }
        else
        {

        }
    }

    public void StartAnimation()
    {
        loadingSysAnimator.Play(loadingSysStartClip.name);
        LoadingAnimationDisable();
        HasStarted = true;
    }

    public void EndAnimation()
    {
        if (HasStarted)
        {
            LoadingAnimationEnable();
            loadingSysAnimator.Play(loadingSysEndClip.name);
            HasStarted = false;
        }
    }

    private void LoadingAnimationDisable() => StartCoroutine(Cor_DelayDisableAnimation());
    private void LoadingAnimationEnable() => loadingSysAnimator.enabled = true;
    private IEnumerator Cor_DelayDisableAnimation()
    {
        yield return new WaitForSeconds(loadingSysStartClip.length + 0.1f);
        loadingSysAnimator.enabled = false;
    }

    public void LoadBehaviourDDM()
    {
        if (Animations.Coach.HasTriggered)
        {
            if (this.HasStarted)
            {
                if (Animations.Coach.CurrentAnimation.isEnter)
                {
                    countTMP.text = "준비";
                    circleLoad.fillAmount = Animations.Coach.CurrentAnimation.normalizedTime;
                }
                else if (Animations.Coach.CurrentAnimation.isWorkout)
                {
                    countTMP.text = Animations.Coach.CurrentAnimation.loopCount + "ₓ";
                    circleLoad.fillAmount = 1f;
                }
            }
            else
            {
                StartAnimation();
            }
        }
        else
        {
            if (Animations.Coach.CurrentAnimation.isWorkout)
            {
                countTMP.text = "대기";
                circleLoad.fillAmount = Animations.Coach.CurrentAnimation.normalizedReverseTime;
            }
            else
            {
                if (Animations.Coach.CurrentAnimation.isOut)
                {
                    countTMP.text = "준비";
                    circleLoad.fillAmount = Animations.Coach.CurrentAnimation.normalizedTime;
                }
                else if (Animations.Coach.CurrentAnimation.isIdle && this.HasStarted)
                {
                    EndAnimation();
                }
            }
        }
    }

    public void LoadBehaviourPanelCoach()
    {
        StartCoroutine(Cor_LoadBehaviourPanelCoach());
    }

    private IEnumerator Cor_LoadBehaviourPanelCoach()
    {
        StartAnimation();
        circleLoad.fillAmount = 1;
        Animations.Coach.StartAnimation(Trigger.Pistol);
        yield return new WaitWhile(() => !Animations.Coach.CurrentAnimation.isWorkout);

        while (Animations.Coach.CurrentAnimation.loopCount < 3)
        {
            if (true)
            {

            }

            countTMP.text = (3 - Animations.Coach.CurrentAnimation.loopCount) + "ₓ";
            circleLoad.fillAmount = (3 - Animations.Coach.CurrentAnimation.normalizedTime) / 3;
            yield return null;
        }

        Animations.Coach.StartIdleAnimation();
        yield return new WaitWhile(() => !Animations.Coach.CurrentAnimation.isIdle);

        Animations.Coach.StartAnimation(Trigger.Squat);
        yield return new WaitWhile(() => !Animations.Coach.CurrentAnimation.isWorkout);

        while (Animations.Coach.CurrentAnimation.loopCount < 5)
        {
            countTMP.text = (5 - Animations.Coach.CurrentAnimation.loopCount) + "ₓ";
            circleLoad.fillAmount = (5 - Animations.Coach.CurrentAnimation.normalizedTime) / 5;
            yield return null;
        }

        Animations.Coach.StartIdleAnimation();
        yield return new WaitWhile(() => !Animations.Coach.CurrentAnimation.isIdle);

        Animations.Coach.StartAnimation(Trigger.Burpee);
        yield return new WaitWhile(() => !Animations.Coach.CurrentAnimation.isWorkout);

        while (Animations.Coach.CurrentAnimation.loopCount < 2)
        {
            countTMP.text = (2 - Animations.Coach.CurrentAnimation.loopCount) + "ₓ";
            circleLoad.fillAmount = (2 - Animations.Coach.CurrentAnimation.normalizedTime) / 2;
            yield return null;
        }

        Animations.Coach.StartIdleAnimation();
        yield return new WaitWhile(() => !Animations.Coach.CurrentAnimation.isIdle);
        EndAnimation();
    }

    public bool HasStarted
    {
        get => loadingIsStart;
        private set => loadingIsStart = value;
    }
}
