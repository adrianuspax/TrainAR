using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASP;
using Debug = ASP.Debug;

public class TrainingSystem : MonoBehaviour
{
    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("Scripts", order = 2)]
    [SerializeField, ReadOnly] private CounterAndTimerUI counterAndTimer;
    [SerializeField, ReadOnly] private bool coroutineFinished;
    [SerializeField, ReadOnly] private bool squenceStarted;
    [SerializeField, ReadOnly] private Coach coachAnimations;
    [SerializeField, ReadOnly] private ButtonsMain buttonsMain;
    [SerializeField, ReadOnly] private DropDownMenu dropDownMenu;

    private void Awake()
    {
        counterAndTimer = FindAnyObjectByType<CounterAndTimerUI>();
        coachAnimations = FindAnyObjectByType<Coach>();
        buttonsMain = FindAnyObjectByType<ButtonsMain>();
        dropDownMenu = FindAnyObjectByType<DropDownMenu>();

        counterAndTimer.AddListnerInButton(Skip);
    }

    private void Update()
    {
        LoadBehaviourDDM();
        if (coachAnimations.CurrentAnimation.isWorkout)
        { counterAndTimer.Timer = Timer.MMSS(squenceStarted ? Count.down : Count.up); }
    }

    public void LoadBehaviourDDM()
    {
        if (coachAnimations.HasTriggered)
        {
            if (counterAndTimer.HasStarted)
            {
                if (coachAnimations.CurrentAnimation.isEnter)
                {
                    counterAndTimer.SetCount("준비");
                    counterAndTimer.RingLoad.fillAmount = coachAnimations.CurrentAnimation.normalizedTime;
                }
                else if (coachAnimations.CurrentAnimation.isWorkout)
                {
                    counterAndTimer.SetCount(coachAnimations.CurrentAnimation.loopCount);
                    counterAndTimer.RingLoad.fillAmount = 1f;
                }
            }
            else
            {
                counterAndTimer.StartAnimation();
                Timer.Start(0);
            }
        }
        else
        {
            if (coachAnimations.CurrentAnimation.isWorkout)
            {
                counterAndTimer.SetCount("대기");
                counterAndTimer.RingLoad.fillAmount = coachAnimations.CurrentAnimation.normalizedReverseTime;
            }
            else
            {
                if (coachAnimations.CurrentAnimation.isOut)
                {
                    counterAndTimer.SetCount("준비");
                    counterAndTimer.RingLoad.fillAmount = coachAnimations.CurrentAnimation.normalizedTime;
                }
                else  if (coachAnimations.CurrentAnimation.isIdle && counterAndTimer.HasStarted)
                {
                    counterAndTimer.EndAnimation();
                    Timer.Stop();
                    counterAndTimer.Timer = "00:00";
                }
            }
        }
    }

    public void Skip()
    {
        StopAllCoroutines();
        coachAnimations.StartIdleAnimation();
        buttonsMain.PanelCoachInteraction = true;
        dropDownMenu.Interactable = true;
        buttonsMain.ButtonSetCoachInteraction = true;
        squenceStarted = false;
        Timer.Stop();
        counterAndTimer.Timer = "00:00";
        counterAndTimer.EndAnimation();
    }

    public void WorkoutSequence()
    {
        StartCoroutine(Cor_LoadBehaviourPanelCoach());
    }

    private void AnimationInLoop(Coach.Trigger animation, int amount)
    {
        coroutineFinished = false;
        StartCoroutine(Cor_AnimationInLoop(animation, amount));
    }

    private IEnumerator Cor_AnimationInLoop(Coach.Trigger animation, int amount)
    {
        counterAndTimer.SetCount("준비");
        yield return new WaitWhile(() => !coachAnimations.CurrentAnimation.isIdle);

        coachAnimations.StartAnimation(animation);

        yield return new WaitWhile(() => coachAnimations.NextAnimation.willBeEnter);

        while (coachAnimations.CurrentAnimation.isEnter)
        {
            counterAndTimer.RingLoad.fillAmount = coachAnimations.CurrentAnimation.normalizedTime;
            counterAndTimer.SetCount(amount);
            yield return null;
        }

        counterAndTimer.SetCount(amount);
        counterAndTimer.RingLoad.fillAmount = 1;

        yield return new WaitWhile(() => !coachAnimations.CurrentAnimation.isWorkout);
        Timer.Start(amount * coachAnimations.CurrentAnimation.length);

        while (coachAnimations.CurrentAnimation.normalizedTime < amount - 1)
        {
            counterAndTimer.SetCount(amount - coachAnimations.CurrentAnimation.loopCount);
            counterAndTimer.RingLoad.fillAmount = (amount - coachAnimations.CurrentAnimation.normalizedTime) / amount;
            yield return null;
        }

        coachAnimations.StartIdleAnimation();
        counterAndTimer.SetCount(1);

        while (coachAnimations.CurrentAnimation.isWorkout)
        {
            counterAndTimer.SetCount(amount - coachAnimations.CurrentAnimation.loopCount);
            counterAndTimer.RingLoad.fillAmount = (amount - coachAnimations.CurrentAnimation.normalizedTime) / amount;
            yield return null;
        }

        counterAndTimer.SetCount("준비");
        counterAndTimer.RingLoad.fillAmount = 0;
        Timer.Stop();
        yield return new WaitWhile(() => !coachAnimations.CurrentAnimation.isIdle);
        coroutineFinished = true;
    }

    private IEnumerator Cor_LoadBehaviourPanelCoach()
    {
        counterAndTimer.StartAnimation();

        buttonsMain.PanelCoachInteraction = false;
        dropDownMenu.Interactable = false;
        buttonsMain.ButtonSetCoachInteraction = false;

        AnimationInLoop(Coach.Trigger.Pistol, 20); //20
        squenceStarted = true;
        yield return new WaitWhile(() => !coroutineFinished);
        AnimationInLoop(Coach.Trigger.Squat, 15); //15
        yield return new WaitWhile(() => !coroutineFinished);
        AnimationInLoop(Coach.Trigger.Burpee, 30); //30
        yield return new WaitWhile(() => !coroutineFinished);
        squenceStarted = false;
        buttonsMain.PanelCoachInteraction = true;
        dropDownMenu.Interactable = true;
        buttonsMain.ButtonSetCoachInteraction = true;
        counterAndTimer.Timer = "00:00";
        counterAndTimer.EndAnimation();
        
    }
}
