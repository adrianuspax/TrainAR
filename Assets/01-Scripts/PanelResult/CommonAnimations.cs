using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Result
{
    notBad = 0,
    goodJob = 1,
    veryGood = 2,
    great = 3,
    excellent = 4
};

public class CommonAnimations : MonoBehaviour
{
    [Header("FX Animations -----------------------------------------------------------------------------------", order = 0)]
    [Space(-10, order = 1)]
    [Header("Start", order = 2)]
    [SerializeField] private AnimationClip only321Clip;
    [SerializeField] private AnimationClip onlyGoClip;
    [SerializeField] private AnimationClip countdownNullClip;
    [SerializeField] private AnimationClip readyClip;
    [SerializeField] private AnimationClip startClip;
    [Space(-10, order = 0)]
    [Header("Finish", order = 1)]
    [SerializeField] private AnimationClip youMadeItClip;
    [SerializeField] private AnimationClip tryAgainClip;
    [SerializeField] private AnimationClip popupClip;
    [SerializeField] private AnimationClip timeOverClip;
    [SerializeField] private AnimationClip dinamicResultClip;
    [SerializeField] private AnimationClip bubblePlusCilp;
    [Space(-10, order = 0)]
    [Header("AUDIO CLIPS", order = 1)]
    [SerializeField] private AudioClip notBad;
    [SerializeField] private AudioClip goodJob;
    [SerializeField] private AudioClip veryGood;
    [SerializeField] private AudioClip great;
    [SerializeField] private AudioClip excellent;
    [Space(-10, order = 0)]
    [Header("FX Animations", order = 1)]
    [SerializeField] private AnimationClip thunderClip;
    [SerializeField] private AnimationClip footFXClip;
    [SerializeField] private AnimationClip tailFXClip;
    [Space(-10, order = 0)]
    [Header("TakeOff", order = 1)]
    [SerializeField] private AnimationClip takeoffClip;
    [SerializeField] private AnimationClip targetSqrStartClip;
    [SerializeField] private AnimationClip targetSqrFinishClip;
    [Header("Guides -----------------------------------------------------------------------------------", order = 0)]
    [Space(-10, order = 1)]
    [Header("Guide1", order = 2)]
    [SerializeField] private AnimationClip startGuide1;
    [SerializeField] private AnimationClip nullGuide1;
    [Space(-10, order = 0)]
    [Header("Guide2", order = 1)]
    [SerializeField] private AnimationClip startGuide2;
    [SerializeField] private AnimationClip nullGuide2;
    [Header("Panel Result -----------------------------------------------------------------------------------", order = 0)]
    [Space(-10, order = 1)]
    [Header("FadeIn", order = 2)]
    [SerializeField] private AnimationClip panelResultFadeIn;
    [Space(-10, order = 1)]
    [Header("Pyramid Level", order = 2)]
    [SerializeField] private AnimationClip pyramidLevelStart;

    private Animator countdown;
    private Animator ready;
    private Animator start;
    private Animator youMadeIt;
    private Animator tryAgain;
    private Animator timeOver;
    private Animator popupResult;
    private Animator dinamicResult;
    private Animator bubblePlus;
    private Animator thunder;
    private Animator footFX;
    private Animator tailFX;
    private Animator takeoff;
    private Animator targetSquare;
    private Animator guide1;
    private Animator guide2;
    private Animator panelResult;
    private Animator pyramidLevel;
    private AudioSource audienceDisappointed;
    private AudioSource audienceApplause;
    private AudioSource reGuide;
    private AudioSource sndGuide2;
    private AudioSource resultVoice;
    private TextMeshProUGUI popupResultTMP;
    private TextMeshProUGUI dinamicResultTMP;

    private const string COUNTDOWN = "aniCountdown";
    private const string READY = "aniReady";
    private const string START = "aniStart";
    private const string YOUMADEIT = "aniYouMadeIt";
    private const string TRYAGAIN = "aniTryAgain";
    private const string TIMEOVER = "aniTimeOver";
    private const string POPUPRESULT = "aniPopUpResult";
    private const string DINAMICRESULT = "aniDinamicResult";
    private const string BUBBLEPLUS = "aniBubblePlus";
    private const string THUNDER = "aniThunder";
    private const string FOOTFX = "aniFootFX";
    private const string TAILFX = "aniTailFX";
    private const string TAKEOFF = "aniTakeOff";
    private const string TARGETSQUARE = "aniTargetSquare";
    private const string GUIDE1 = "aniGuide1";
    private const string GUIDE2 = "aniGuide2";
    private const string PANELRESULT = "aniPanelResult";
    private const string DISAPPOINTED = "sndDisappointed";
    private const string APPLAUSE = "sndApplause";
    private const string REGUIDE = "sndReGuide";
    private const string SOUNDGUIDE2 = "sndGuide2";
    private const string RESULTVOICE = "resultVoice";
    private const string POPUPTMP = "popupResultTMP";
    private const string DINAMICTMP = "dinamicResultTMP";
    private const string PYRAMIDLEVEL = "pyramidLevel";

    private void Awake()
    {
        if (OnTheScene(COUNTDOWN)) { countdown = Animator(COUNTDOWN); }
        if (OnTheScene(READY)) { ready = Animator(READY); }
        if (OnTheScene(START)) { start = Animator(START); }
        if (OnTheScene(YOUMADEIT)) { youMadeIt = Animator(YOUMADEIT); }
        if (OnTheScene(TRYAGAIN)) { tryAgain = Animator(TRYAGAIN); }
        if (OnTheScene(TIMEOVER)) { timeOver = Animator(TIMEOVER); }
        if (OnTheScene(POPUPRESULT)) { popupResult = Animator(POPUPRESULT); }
        if (OnTheScene(DINAMICRESULT)) { dinamicResult = Animator(DINAMICRESULT); }
        if (OnTheScene(BUBBLEPLUS)) { bubblePlus = Animator(BUBBLEPLUS); }
        if (OnTheScene(THUNDER)) { thunder = Animator(THUNDER); }
        if (OnTheScene(FOOTFX)) { footFX = Animator(FOOTFX); }
        if (OnTheScene(TAILFX)) { tailFX = Animator(TAILFX); }
        if (OnTheScene(TAKEOFF)) { takeoff = Animator(TAKEOFF); }
        if (OnTheScene(TARGETSQUARE)) { targetSquare = Animator(TARGETSQUARE); }
        if (OnTheScene(GUIDE1)) { guide1 = Animator(GUIDE1); }
        if (OnTheScene(GUIDE2)) { guide2 = Animator(GUIDE2); }
        if (OnTheScene(PANELRESULT)) { panelResult = Animator(PANELRESULT); }
        if (OnTheScene(PYRAMIDLEVEL)) { pyramidLevel = Animator(PYRAMIDLEVEL); }
        if (OnTheScene(DISAPPOINTED)) { audienceDisappointed = Audio(DISAPPOINTED); }
        if (OnTheScene(APPLAUSE)) { audienceApplause = Audio(APPLAUSE); }
        if (OnTheScene(REGUIDE)) { reGuide = Audio(REGUIDE); }
        if (OnTheScene(SOUNDGUIDE2)) { sndGuide2 = Audio(SOUNDGUIDE2); }
        if (OnTheScene(RESULTVOICE)) { resultVoice = Audio(RESULTVOICE); }
        if (OnTheScene(POPUPTMP)) { popupResultTMP = TMP(POPUPTMP); }
        if (OnTheScene(DINAMICTMP)) { dinamicResultTMP = TMP(DINAMICTMP); }
    }

    private Animator Animator(string tag) => GameObject.FindGameObjectWithTag(tag).GetComponent<Animator>();
    private AudioSource Audio(string tag) => GameObject.FindGameObjectWithTag(tag).GetComponent<AudioSource>();
    private TextMeshProUGUI TMP(string tag) => GameObject.FindGameObjectWithTag(tag).GetComponent<TextMeshProUGUI>();
    private bool OnTheScene(string tag) => GameObject.FindWithTag(tag) != null;

    #region COUNTDOWN
    public void InitialCount()
    {
        countdown.Play(only321Clip.name);
    }

    public void InitialCount(float delay)
    {
        StartCoroutine(Cor_InitialCount(delay));
    }

    public void Go()
    {
        countdown.Play(onlyGoClip.name);
    }

    public void Go(float delay)
    {
        StartCoroutine(Cor_Go(delay));
    }

    public void CountdownNull()
    {
        countdown.Play(countdownNullClip.name);
    }

    public float InitialCountTime
    {
        get
        {
            return 5f;
        }
    }

    public float GoTime
    {
        get
        {
            return 1f;
        }
    }

    private IEnumerator Cor_InitialCount(float time)
    {
        yield return new WaitForSeconds(time);
        countdown.Play(only321Clip.name);
    }

    private IEnumerator Cor_Go(float time)
    {
        yield return new WaitForSeconds(time);
        countdown.Play(onlyGoClip.name);
    }
    #endregion
    #region READY / START

    public void Ready()
    {
        ready.Play(readyClip.name);
    }

    public void ReadyNull()
    {
        ready.Play("readyNull");
    }

    public void Ready(float delay)
    {
        StartCoroutine(Cor_Ready(delay));
    }

    public void StartAni()
    {
        start.Play(startClip.name);
    }

    public void StartAni(float delay)
    {
        StartCoroutine(Cor_Start(delay));
    }

    public float ReadyTime
    {
        get
        {
            return readyClip.length;
        }
    }

    public float StartTime
    {
        get
        {
            return startClip.length;
        }
    }

    private IEnumerator Cor_Start(float time)
    {
        yield return new WaitForSeconds(time);
        start.Play(startClip.name);
    }

    private IEnumerator Cor_Ready(float time)
    {
        yield return new WaitForSeconds(time);
        ready.Play(readyClip.name);
    }

    #endregion
    #region YOU MADE IT
    public void YouMadeIt()
    {
        youMadeIt.Play(youMadeItClip.name);
    }

    public void YouMadeIt(float delay)
    {
        StartCoroutine(Cor_YouMadeIt(delay));
    }

    public float YouMadeItTime
    {
        get
        {
            return 3f;
        }
    }

    private IEnumerator Cor_YouMadeIt(float time)
    {
        yield return new WaitForSeconds(time);
        youMadeIt.Play(youMadeItClip.name);
    }
    #endregion
    #region TRY AGAIN
    public void TryAgain()
    {
        tryAgain.Play(tryAgainClip.name);
    }

    public void TryAgain(float delay)
    {
        StartCoroutine(Cor_TryAgain(delay));
    }

    public float TryAgainTime
    {
        get
        {
            return tryAgainClip.length;
        }
    }

    IEnumerator Cor_TryAgain(float time)
    {
        yield return new WaitForSeconds(time);
        tryAgain.Play(tryAgainClip.name);
    }
    #endregion
    #region TIME OVER
    public void TimeOver()
    {
        timeOver.Play(timeOverClip.name);
    }

    public void TimeOver(float delay)
    {
        StartCoroutine(Cor_TimeOver(delay));
    }

    public float TimeOverTime
    {
        get
        {
            return 2.5f;
        }

    }

    IEnumerator Cor_TimeOver(float time)
    {
        yield return new WaitForSeconds(time);
        timeOver.Play(timeOverClip.name);
    }
    #endregion
    #region POPUP RESULT
    public void PopupResult()
    {
        popupResult.Play(popupClip.name);
    }

    public void PopupResult(string value)
    {
        PopupResultValue = value;
        popupResult.Play(popupClip.name);
    }

    public void PopupResult(string value, float delay)
    {
        StartCoroutine(Cor_PopupResult(value, delay));
    }

    public float PopupResultTime
    {
        get
        {
            return 3f;
        }
    }

    public string PopupResultValue
    {
        get
        {
            return popupResultTMP.ToString();
        }

        set
        {
            popupResultTMP.text = value;
        }
    }

    IEnumerator Cor_PopupResult(string value, float time)
    {
        yield return new WaitForSeconds(time);
        PopupResultValue = value;
        popupResult.Play(popupClip.name);
    }
    #endregion
    #region DINAMIC RESULT
    public void DinamicResult(Result result)
    {
        switch (result)
        {
            case Result.notBad:
                DinamicResultValue = "Not Bad";
                resultVoice.clip = notBad;
                break;
            case Result.goodJob:
                DinamicResultValue = "Good Job";
                resultVoice.clip = goodJob;
                break;
            case Result.veryGood:
                DinamicResultValue = "Very Good";
                resultVoice.clip = veryGood;
                break;
            case Result.great:
                DinamicResultValue = "Great";
                resultVoice.clip = great;
                break;
            case Result.excellent:
                DinamicResultValue = "Excellent";
                resultVoice.clip = excellent;
                break;
        }

        dinamicResult.Play(dinamicResultClip.name);
    }

    public void DinamicResult(Result result, float delay)
    {
        StartCoroutine(Cor_DinamicResult(result, delay));
    }

    public float DinamicResultTime
    {
        get
        {
            return dinamicResultClip.length;
        }
    }

    public string DinamicResultValue
    {
        get
        {
            return dinamicResultTMP.ToString();
        }

        set
        {
            dinamicResultTMP.text = value;
        }
    }

    IEnumerator Cor_DinamicResult(Result result, float time)
    {
        yield return new WaitForSeconds(time);

        switch (result)
        {
            case Result.notBad:
                DinamicResultValue = "Not Bad";
                resultVoice.clip = notBad;
                break;
            case Result.goodJob:
                DinamicResultValue = "Good Job";
                resultVoice.clip = goodJob;
                break;
            case Result.veryGood:
                DinamicResultValue = "Very Good";
                resultVoice.clip = veryGood;
                break;
            case Result.great:
                DinamicResultValue = "Great";
                resultVoice.clip = great;
                break;
            case Result.excellent:
                DinamicResultValue = "Excellent";
                resultVoice.clip = excellent;
                break;
        }

        yield return null;

        dinamicResult.Play(dinamicResultClip.name);
    }
    #endregion
    #region BUBBLE PLUS
    public void BubblePlus()
    {
        bubblePlus.Play(bubblePlusCilp.name);
    }

    public void BubblePlus(float delay)
    {
        StartCoroutine(Cor_BubblePlus(delay));
    }

    public float BubblePlusTime
    {
        get
        {
            return bubblePlusCilp.length;
        }
    }

    private IEnumerator Cor_BubblePlus(float time)
    {
        yield return new WaitForSeconds(time);
        bubblePlus.Play(bubblePlusCilp.name);
    }
    #endregion
    #region THUNDER
    public void Thunder()
    {
        thunder.Play(thunderClip.name);
    }

    public void Thunder(float delay)
    {
        StartCoroutine(Cor_Thunder(delay));
    }

    public float ThunderTime
    {
        get
        {
            return thunderClip.length;
        }
    }

    IEnumerator Cor_Thunder(float time)
    {
        yield return new WaitForSeconds(time);
        thunder.Play(thunderClip.name);
    }
    #endregion
    #region FOOT FX
    public void FootFX()
    {
        footFX.Play(footFXClip.name);
    }

    public void FootFX(float delay)
    {
        StartCoroutine(Cor_FootFX(delay));
    }

    public float FootFXTime
    {
        get
        {
            return footFXClip.length;
        }
    }

    IEnumerator Cor_FootFX(float time)
    {
        yield return new WaitForSeconds(time);
        footFX.Play(footFXClip.name);
    }
    #endregion
    #region TAIL FX
    public void TailFX()
    {
        tailFX.Play(tailFXClip.name);
    }

    public void TailFX(float delay)
    {
        StartCoroutine(Cor_TailFX(delay));
    }

    public float TailFXTime
    {
        get
        {
            return tailFXClip.length;
        }
    }

    IEnumerator Cor_TailFX(float time)
    {
        yield return new WaitForSeconds(time);
        tailFX.Play(tailFXClip.name);
    }
    #endregion
    #region TAKEOFF
    public void TakeOff()
    {
        takeoff.Play(takeoffClip.name);
    }

    public void TakeOff(float delay)
    {
        StartCoroutine(Cor_TakeOff(delay));
    }

    public float TakeoffTime
    {
        get
        {
            return takeoffClip.length;
        }
    }

    IEnumerator Cor_TakeOff(float time)
    {
        yield return new WaitForSeconds(time);
        takeoff.Play(takeoffClip.name);
    }
    #endregion
    #region TARGET SQUARE
    public void TargetSquareStart()
    {
        targetSquare.Play(targetSqrStartClip.name);
    }

    public void TargetSquareFinish()
    {
        targetSquare.Play(targetSqrFinishClip.name);
    }

    public void TargetSquareStart(float delay)
    {
        StartCoroutine(Cor_TargetSquareStart(delay));
    }

    public void TargetSquareFinish(float delay)
    {
        StartCoroutine(Cor_TargetSquareFinish(delay));
    }

    public float TargetSquareStartTime
    {
        get
        {
            return targetSqrStartClip.length;
        }
    }

    public float TargetSquareFinishTime
    {
        get
        {
            return targetSqrFinishClip.length;
        }
    }

    IEnumerator Cor_TargetSquareStart(float time)
    {
        yield return new WaitForSeconds(time);
        targetSquare.Play(targetSqrStartClip.name);
    }

    IEnumerator Cor_TargetSquareFinish(float time)
    {
        yield return new WaitForSeconds(time);
        targetSquare.Play(targetSqrFinishClip.name);
    }
    #endregion
    #region TAKEOFF + TARGET SQUARE
    public void TargetTakeoff()
    {
        TakeOff();
        TargetSquareStart();
    }

    public void TargetTakeoff(float delay)
    {
        TakeOff(delay);
        TargetSquareStart(delay);
    }
    #endregion
    #region GUIDE 1
    public void Guide1Null()
    {
        guide1.Play(nullGuide1.name);
    }

    public void Guide1()
    {
        guide1.Play(startGuide1.name);
    }

    public void Guide1(float delay)
    {
        StartCoroutine(Cor_Guide1(delay));
    }

    public float Guide1Time
    {
        get
        {
            return startGuide1.length;
        }
    }

    IEnumerator Cor_Guide1(float time)
    {
        yield return new WaitForSeconds(time);
        guide1.Play(startGuide1.name);
    }
    #endregion
    #region GUIDE 2
    public void Guide2Null()
    {
        guide2.Play(nullGuide2.name);
    }

    public void Guide2Null(float delay)
    {
        StartCoroutine(Cor_Guide2Null(delay));
    }

    public void Guide2()
    {
        Guide1Null();
        guide2.Play(startGuide2.name);
    }

    public void Guide2(float delay)
    {
        StartCoroutine(Cor_Guide2(delay));
    }

    public float Guide2Time
    {
        get
        {
            return sndGuide2.clip.length;
        }
    }

    IEnumerator Cor_Guide2(float time)
    {
        yield return new WaitForSeconds(time);
        guide2.Play(startGuide2.name);
    }

    IEnumerator Cor_Guide2Null(float time)
    {
        yield return new WaitForSeconds(time);
        guide1.Play(nullGuide2.name);
    }

    #endregion
    #region PANEL RESULT
    public void PanelResult(int level)
    {
        StartCoroutine(Cor_PanelResult(level));
    }

    private IEnumerator Cor_PanelResult(int level)
    {
        panelResult.Play(panelResultFadeIn.name);

        yield return new WaitForSeconds(PanelResultTime);

        pyramidLevel.Play(pyramidLevelStart.name);
        pyramidLevel.SetInteger("level", level);
    }

    public float PanelResultTime => panelResultFadeIn.length;

    #endregion
    #region SOUNDS
    public void AudienceApplause()
    {
        audienceApplause.Play();
    }

    public void AudienceDisappointed()
    {
        audienceDisappointed.Play();
    }

    public void ReGuide()
    {
        reGuide.Play();
    }

    public void AudienceApplause(float delay)
    {
        StartCoroutine(Cor_Applause(delay));
    }

    public void AudienceDisappointed(float delay)
    {
        StartCoroutine(Cor_Disappointed(delay));
    }

    public void ReGuide(float delay)
    {
        StartCoroutine(Cor_ReGuide(delay));
    }

    public float ApplauseTime
    {
        get
        {
            return audienceApplause.clip.length;
        }
    }

    public float DisappointedTime
    {
        get
        {
            return audienceDisappointed.clip.length;
        }
    }

    public float ReGuideTime
    {
        get
        {
            return reGuide.clip.length;
        }
    }

    private IEnumerator Cor_Applause(float time)
    {
        yield return new WaitForSeconds(time);
        audienceApplause.Play();
    }

    private IEnumerator Cor_Disappointed(float time)
    {
        yield return new WaitForSeconds(time);
        audienceDisappointed.Play();
    }

    private IEnumerator Cor_ReGuide(float time)
    {
        yield return new WaitForSeconds(time);
        reGuide.Play();
    }

    #endregion
}