using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ASP;
using Debug = ASP.Debug;

public class Loading : MonoBehaviour
{
    [Header("Animations", order = 0)]
    [SerializeField] private Animator loadingSysAnimator;
    [SerializeField] private AnimationClip loadingSysStartClip;
    [SerializeField] private AnimationClip loadingSysEndClip;
    [Header("UI", order = 0)]
    [SerializeField] private Image circleLoad;
    [SerializeField] private TextMeshProUGUI countTMP;
    [SerializeField] private TextMeshProUGUI timerTMP;
    [SerializeField] private Button skipButton;

    [Header("Read Only", order = 0)]
    [Space(-10, order = 1)]
    [Header("Variables", order = 2)]
    [SerializeField, ReadOnly] private bool loadingIsStart;

    private void Start()
    {
        loadingIsStart = false;
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

    public void SetCount(int count)
    {
        countTMP.text = count + "ₓ";
    }

    public void SetCount(string text)
    {
        countTMP.text = text;
    }

    public float FillAmount
    {
        get => circleLoad.fillAmount;
        set => circleLoad.fillAmount = value;
    }

    public bool HasStarted
    {
        get => loadingIsStart;
        private set => loadingIsStart = value;
    }

    public Button Skip => skipButton;

    public string Timer
    {
        set => timerTMP.text = value;
    }
}
