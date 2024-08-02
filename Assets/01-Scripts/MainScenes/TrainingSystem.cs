using ASP;
using ASP.Extensions;
using System.Collections;
using UnityEngine;

namespace TreinAR.MainScenes
{
    public class TrainingSystem : MonoBehaviour
    {
        [Header(Header.READONLY, order = 0)]
        [Space(-10, order = 1)]
        [Header(Header.variables, order = 2)]
        [SerializeField, ReadOnly] private bool isFinishedCoroutine;
        [SerializeField, ReadOnly] private bool isStartedSeqiance;

        [Header(Header.scripts, order = 1)]
        [SerializeField, ReadOnly] private CounterAndTimerUI counterAndTimer;
        [SerializeField, ReadOnly] private Coach coachAnimations;
        [SerializeField, ReadOnly] private ButtonsMain buttonsMain;
        [SerializeField, ReadOnly] private DropDownMenu dropDownMenu;

        private void Awake()
        {
            ComponentsAssignment();
        }
        /// This function is called when the object becomes enabled and active.
        private void OnEnable()
        {
            counterAndTimer.AddListnerInButton(Skip);
        }

        private void Update()
        {
            LoadBehaviourDDM();

            if (coachAnimations.CurrentAnimation.isWorkout)
            {
                string textTimer = Timer.MMSS(isStartedSeqiance ? Count.down : Count.up);
                counterAndTimer.SetTextTimer(textTimer);
            }
        }
        /// <summary>
        /// Assignment of components and variables
        /// </summary>
        [ContextMenu("Components Assignment")]
        private void ComponentsAssignment()
        {
            if (counterAndTimer.IsNull())
                counterAndTimer = FindAnyObjectByType<CounterAndTimerUI>();
            if (coachAnimations.IsNull())
                coachAnimations = FindAnyObjectByType<Coach>();
            if (buttonsMain.IsNull())
                buttonsMain = FindAnyObjectByType<ButtonsMain>();
            if (dropDownMenu.IsNull())
                dropDownMenu = FindAnyObjectByType<DropDownMenu>();

            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }

        public void LoadBehaviourDDM()
        {
            if (coachAnimations.HasTriggered)
            {
                if (counterAndTimer.HasStarted)
                {
                    if (coachAnimations.CurrentAnimation.isEnter)
                    {
                        counterAndTimer.CountTMP.SetLocalizationString("Ready", "StringTableMain");
                        counterAndTimer.RingLoad.fillAmount = coachAnimations.CurrentAnimation.normalizedTime;
                    }
                    else if (coachAnimations.CurrentAnimation.isWorkout)
                    {
                        counterAndTimer.SetValueCount(coachAnimations.CurrentAnimation.loopCount);
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
                    counterAndTimer.CountTMP.SetLocalizationString("Wait", "StringTableMain");
                    counterAndTimer.RingLoad.fillAmount = coachAnimations.CurrentAnimation.normalizedReverseTime;
                }
                else
                {
                    if (coachAnimations.CurrentAnimation.isOut)
                    {
                        counterAndTimer.CountTMP.SetLocalizationString("Ready", "StringTableMain");
                        counterAndTimer.RingLoad.fillAmount = coachAnimations.CurrentAnimation.normalizedTime;
                    }
                    else if (coachAnimations.CurrentAnimation.isIdle && counterAndTimer.HasStarted)
                    {
                        counterAndTimer.EndAnimation();
                        Timer.Stop();
                        counterAndTimer.SetTextTimer("00:00");
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
            isStartedSeqiance = false;
            Timer.Stop();
            counterAndTimer.SetTextTimer("00:00");
            counterAndTimer.EndAnimation();
        }

        public void WorkoutSequence()
        {
            StartCoroutine(_loadBehaviourPanelCoach());

            IEnumerator _loadBehaviourPanelCoach()
            {
                counterAndTimer.StartAnimation();

                buttonsMain.PanelCoachInteraction = false;
                dropDownMenu.Interactable = false;
                buttonsMain.ButtonSetCoachInteraction = false;

                AnimationInLoop(Coach.Trigger.Pistol, 20); //20
                isStartedSeqiance = true;
                yield return new WaitWhile(() => !isFinishedCoroutine);
                AnimationInLoop(Coach.Trigger.Squat, 15); //15
                yield return new WaitWhile(() => !isFinishedCoroutine);
                AnimationInLoop(Coach.Trigger.Burpee, 30); //30
                yield return new WaitWhile(() => !isFinishedCoroutine);
                isStartedSeqiance = false;
                buttonsMain.PanelCoachInteraction = true;
                dropDownMenu.Interactable = true;
                buttonsMain.ButtonSetCoachInteraction = true;
                counterAndTimer.SetTextTimer("00:00");
                counterAndTimer.EndAnimation();
            }
        }

        private void AnimationInLoop(Coach.Trigger animation, int amount)
        {
            isFinishedCoroutine = false;
            StartCoroutine(Cor_AnimationInLoop(animation, amount));
        }

        private IEnumerator Cor_AnimationInLoop(Coach.Trigger animation, int amount)
        {
            counterAndTimer.CountTMP.SetLocalizationString("Ready", "StringTableMain");
            yield return new WaitWhile(() => !coachAnimations.CurrentAnimation.isIdle);

            coachAnimations.StartAnimation(animation);

            yield return new WaitWhile(() => coachAnimations.NextAnimation.willBeEnter);

            while (coachAnimations.CurrentAnimation.isEnter)
            {
                counterAndTimer.RingLoad.fillAmount = coachAnimations.CurrentAnimation.normalizedTime;
                counterAndTimer.SetValueCount(amount);
                yield return null;
            }

            counterAndTimer.SetValueCount(amount);
            counterAndTimer.RingLoad.fillAmount = 1;

            yield return new WaitWhile(() => !coachAnimations.CurrentAnimation.isWorkout);
            Timer.Start(amount * coachAnimations.CurrentAnimation.length);

            while (coachAnimations.CurrentAnimation.normalizedTime < amount - 1)
            {
                counterAndTimer.SetValueCount(amount - coachAnimations.CurrentAnimation.loopCount);
                counterAndTimer.RingLoad.fillAmount = (amount - coachAnimations.CurrentAnimation.normalizedTime) / amount;
                yield return null;
            }

            coachAnimations.StartIdleAnimation();
            counterAndTimer.SetValueCount(1);

            while (coachAnimations.CurrentAnimation.isWorkout)
            {
                counterAndTimer.SetValueCount(amount - coachAnimations.CurrentAnimation.loopCount);
                counterAndTimer.RingLoad.fillAmount = (amount - coachAnimations.CurrentAnimation.normalizedTime) / amount;
                yield return null;
            }

            counterAndTimer.CountTMP.SetLocalizationString("Ready", "StringTableMain");
            counterAndTimer.RingLoad.fillAmount = 0;
            Timer.Stop();
            yield return new WaitWhile(() => !coachAnimations.CurrentAnimation.isIdle);
            isFinishedCoroutine = true;
        }
    }
}

