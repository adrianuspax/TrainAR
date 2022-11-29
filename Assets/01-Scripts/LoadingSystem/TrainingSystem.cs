using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingSystem : MonoBehaviour
{
    [Header(" =========================== READY ONLY =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("Scripts", order = 2)]
    [SerializeField, ReadOnly] private Loading Loading;
    [SerializeField, ReadOnly] private AnimationsManager Animations;
    [SerializeField, ReadOnly] private ButtonsManager Buttons;
    [SerializeField, ReadOnly] private bool coroutineFinished;


    private void Awake()
    {
        Loading = this.GetComponent<Loading>();
        Animations = this.GetComponent<AnimationsManager>();
        Buttons = this.GetComponent<ButtonsManager>();

        Loading.Skip.onClick.AddListener(() => Skip());
    }

    private void Update()
    {
        LoadBehaviourDDM();

        if (Animations.Coach.HasTriggered && Animations.Coach.CurrentAnimation.isWorkout)
        {
            //Loading.Timer = Timer.CountMinutsAndSeconds(Count.Up);
        }

        Loading.Timer = Timer.CountMinutsAndSeconds(Count.Down);
    }

    public void LoadBehaviourDDM()
    {
        if (Animations.Coach.HasTriggered)
        {
            if (Loading.HasStarted)
            {
                if (Animations.Coach.CurrentAnimation.isEnter)
                {
                    Loading.SetCount("준비");
                    Loading.FillAmount = Animations.Coach.CurrentAnimation.normalizedTime;
                }
                else if (Animations.Coach.CurrentAnimation.isWorkout)
                {
                    Loading.SetCount(Animations.Coach.CurrentAnimation.loopCount);
                    Loading.FillAmount = 1f;
                    //Debug.Log
                }
            }
            else
            {
                Loading.StartAnimation();
                Timer.StartCount(0);
            }
        }
        else
        {
            if (Animations.Coach.CurrentAnimation.isWorkout)
            {
                Loading.SetCount("대기");
                Loading.FillAmount = Animations.Coach.CurrentAnimation.normalizedReverseTime;
            }
            else
            {
                if (Animations.Coach.CurrentAnimation.isOut)
                {
                    Loading.SetCount("준비");
                    Loading.FillAmount = Animations.Coach.CurrentAnimation.normalizedTime;
                }
                else  if (Animations.Coach.CurrentAnimation.isIdle && Loading.HasStarted)
                {
                    Loading.EndAnimation();
                    Timer.StopCount();
                }
            }
        }
    }

    public void Skip()
    {
        StopAllCoroutines();
        Animations.Coach.StartIdleAnimation();
        Buttons.CanvasMain.PanelCoachInteraction = true;
        Buttons.DropDownMenu.DDMInterecation = true;
        Buttons.CanvasMain.ButtonSetCoachInteraction = true;
        Timer.StopCount();
        Loading.EndAnimation();
    }

    public void LoadBehaviourPanelCoach()
    {
        StartCoroutine(Cor_LoadBehaviourPanelCoach());
    }

    private void AnimationInLoop(Trigger animation, int amount)
    {
        coroutineFinished = false;
        StartCoroutine(Cor_AnimationInLoop(animation, amount));
    }

    private IEnumerator Cor_AnimationInLoop(Trigger animation, int amount)
    {
        Loading.SetCount("준비");

        yield return new WaitWhile(() => !Animations.Coach.CurrentAnimation.isIdle);

        Animations.Coach.StartAnimation(animation);

        yield return new WaitWhile(() => Animations.Coach.NextAnimation.willBeEnter);

        while (Animations.Coach.CurrentAnimation.isEnter)
        {
            Loading.FillAmount = Animations.Coach.CurrentAnimation.normalizedTime;
            Loading.SetCount(amount);
            yield return null;
        }

        Loading.SetCount(amount);
        Loading.FillAmount = 1;

        yield return new WaitWhile(() => !Animations.Coach.CurrentAnimation.isWorkout);
        Timer.StartCount(amount * Animations.Coach.CurrentAnimation.length);

        while (Animations.Coach.CurrentAnimation.normalizedTime < amount - 1)
        {
            Loading.SetCount(amount - Animations.Coach.CurrentAnimation.loopCount);
            Loading.FillAmount = (amount - Animations.Coach.CurrentAnimation.normalizedTime) / amount;
            yield return null;
        }

        Animations.Coach.StartIdleAnimation();
        Loading.SetCount(1);

        while (Animations.Coach.CurrentAnimation.isWorkout)
        {
            Loading.SetCount(amount - Animations.Coach.CurrentAnimation.loopCount);
            Loading.FillAmount = (amount - Animations.Coach.CurrentAnimation.normalizedTime) / amount;
            yield return null;
        }

        Loading.SetCount("준비");
        Loading.FillAmount = 0;
        Timer.StopCount();
        yield return new WaitWhile(() => !Animations.Coach.CurrentAnimation.isIdle);
        coroutineFinished = true;
    }

    private IEnumerator Cor_LoadBehaviourPanelCoach()
    {
        Loading.StartAnimation();

        Buttons.CanvasMain.PanelCoachInteraction = false;
        Buttons.DropDownMenu.DDMInterecation = false;
        Buttons.CanvasMain.ButtonSetCoachInteraction = false;

        AnimationInLoop(Trigger.Pistol, 20); //20
        yield return new WaitWhile(() => !coroutineFinished);
        AnimationInLoop(Trigger.Squat, 15); //15
        yield return new WaitWhile(() => !coroutineFinished);
        AnimationInLoop(Trigger.Burpee, 30); //30
        yield return new WaitWhile(() => !coroutineFinished);

        Buttons.CanvasMain.PanelCoachInteraction = true;
        Buttons.DropDownMenu.DDMInterecation = true;
        Buttons.CanvasMain.ButtonSetCoachInteraction = true;

        Loading.EndAnimation();
    }
}
