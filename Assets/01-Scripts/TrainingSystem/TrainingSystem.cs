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
    [SerializeField, ReadOnly] private Loading Loading;
    [SerializeField, ReadOnly] private bool coroutineFinished;
    [SerializeField, ReadOnly] private bool squenceStarted;
    [SerializeField, ReadOnly] private CoachAnimations coachAnimations;
    [SerializeField, ReadOnly] private ButtonsMain buttonsMain;
    [SerializeField, ReadOnly] private DropDownMenu dropDownMenu;

    private void Awake()
    {
        Loading = this.GetComponent<Loading>();
        coachAnimations = this.GetComponent<CoachAnimations>();
        buttonsMain = this.GetComponent<ButtonsMain>();
        dropDownMenu = this.GetComponent<DropDownMenu>();

        Loading.Skip.onClick.AddListener(() => Skip());
    }

    private void Update()
    {
        LoadBehaviourDDM();
        if (coachAnimations.CurrentAnimation.isWorkout)
        { Loading.Timer = Timer.MMSS(squenceStarted ? Count.down : Count.up); }
    }

    public void LoadBehaviourDDM()
    {
        if (coachAnimations.HasTriggered)
        {
            if (Loading.HasStarted)
            {
                if (coachAnimations.CurrentAnimation.isEnter)
                {
                    Loading.SetCount("준비");
                    Loading.FillAmount = coachAnimations.CurrentAnimation.normalizedTime;
                }
                else if (coachAnimations.CurrentAnimation.isWorkout)
                {
                    Loading.SetCount(coachAnimations.CurrentAnimation.loopCount);
                    Loading.FillAmount = 1f;
                }
            }
            else
            {
                Loading.StartAnimation();
                Timer.Start(0);
            }
        }
        else
        {
            if (coachAnimations.CurrentAnimation.isWorkout)
            {
                Loading.SetCount("대기");
                Loading.FillAmount = coachAnimations.CurrentAnimation.normalizedReverseTime;
            }
            else
            {
                if (coachAnimations.CurrentAnimation.isOut)
                {
                    Loading.SetCount("준비");
                    Loading.FillAmount = coachAnimations.CurrentAnimation.normalizedTime;
                }
                else  if (coachAnimations.CurrentAnimation.isIdle && Loading.HasStarted)
                {
                    Loading.EndAnimation();
                    Timer.Stop();
                    Loading.Timer = "00:00";
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
        Loading.Timer = "00:00";
        Loading.EndAnimation();
    }

    public void WorkoutSequence()
    {
        StartCoroutine(Cor_LoadBehaviourPanelCoach());
    }

    private void AnimationInLoop(CoachAnimations.Trigger animation, int amount)
    {
        coroutineFinished = false;
        StartCoroutine(Cor_AnimationInLoop(animation, amount));
    }

    private IEnumerator Cor_AnimationInLoop(CoachAnimations.Trigger animation, int amount)
    {
        Loading.SetCount("준비");
        yield return new WaitWhile(() => !coachAnimations.CurrentAnimation.isIdle);

        coachAnimations.StartAnimation(animation);

        yield return new WaitWhile(() => coachAnimations.NextAnimation.willBeEnter);

        while (coachAnimations.CurrentAnimation.isEnter)
        {
            Loading.FillAmount = coachAnimations.CurrentAnimation.normalizedTime;
            Loading.SetCount(amount);
            yield return null;
        }

        Loading.SetCount(amount);
        Loading.FillAmount = 1;

        yield return new WaitWhile(() => !coachAnimations.CurrentAnimation.isWorkout);
        Timer.Start(amount * coachAnimations.CurrentAnimation.length);

        while (coachAnimations.CurrentAnimation.normalizedTime < amount - 1)
        {
            Loading.SetCount(amount - coachAnimations.CurrentAnimation.loopCount);
            Loading.FillAmount = (amount - coachAnimations.CurrentAnimation.normalizedTime) / amount;
            yield return null;
        }

        coachAnimations.StartIdleAnimation();
        Loading.SetCount(1);

        while (coachAnimations.CurrentAnimation.isWorkout)
        {
            Loading.SetCount(amount - coachAnimations.CurrentAnimation.loopCount);
            Loading.FillAmount = (amount - coachAnimations.CurrentAnimation.normalizedTime) / amount;
            yield return null;
        }

        Loading.SetCount("준비");
        Loading.FillAmount = 0;
        Timer.Stop();
        yield return new WaitWhile(() => !coachAnimations.CurrentAnimation.isIdle);
        coroutineFinished = true;
    }

    private IEnumerator Cor_LoadBehaviourPanelCoach()
    {
        Loading.StartAnimation();

        buttonsMain.PanelCoachInteraction = false;
        dropDownMenu.Interactable = false;
        buttonsMain.ButtonSetCoachInteraction = false;

        AnimationInLoop(CoachAnimations.Trigger.Pistol, 20); //20
        squenceStarted = true;
        yield return new WaitWhile(() => !coroutineFinished);
        AnimationInLoop(CoachAnimations.Trigger.Squat, 15); //15
        yield return new WaitWhile(() => !coroutineFinished);
        AnimationInLoop(CoachAnimations.Trigger.Burpee, 30); //30
        yield return new WaitWhile(() => !coroutineFinished);
        squenceStarted = false;
        buttonsMain.PanelCoachInteraction = true;
        dropDownMenu.Interactable = true;
        buttonsMain.ButtonSetCoachInteraction = true;
        Loading.Timer = "00:00";
        Loading.EndAnimation();
        
    }
}
