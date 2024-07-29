using ASP;
using ASP.Extensions;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CounterAndTimerUI : MonoBehaviour
{
    [Header(Header.READONLY, order = 0)]
    [Space(-10, order = 1)]
    [Header(Header.components, order = 2)]
    [SerializeField, ReadOnly] private Animator animator;
    [Header(Header.UI, order = 0)]
    [SerializeField, ReadOnly] private Button button;
    [SerializeField, ReadOnly] private Image ringLoad;
    [SerializeField, ReadOnly] private TextMeshProUGUI countTMP;
    [SerializeField, ReadOnly] private TextMeshProUGUI timerTMP;
    [Space(-10, order = 0)]
    [Header(Header.variables, order = 1)]
    [SerializeField, ReadOnly] private bool isStarted;

    public const string PARAMETER = "isRunning";
    /// Awake is called when an enabled script instance is being loaded.
    private void Awake()
    {
        ComponentsAssignment();
    }
    /// <summary>
    /// Assignment of components and variables
    /// </summary>
    [ContextMenu("Components Assignment")]
    private void ComponentsAssignment()
    {
        this.GetComponentIfNull(ref animator, 0);
        animator.GetComponentInChildrenIfNull(ref button);
        button.GetComponentIfNull(ref ringLoad, 0);
        ringLoad.GetComponentIfNull(ref countTMP, 0);
        this.GetComponentIfNull(ref timerTMP, 0, 0, 1);

        ASP.SceneManagement.SceneManager.SaveCurrentScene();
    }

    public void StartAnimation()
    {
        animator.SetBool(PARAMETER, true);
        StartCoroutine(_delayDisableAnimation());
        isStarted = true;

        IEnumerator _delayDisableAnimation()
        {
            float length = animator.GetAnimationClipLength("loadSysStart") + 0.1f;
            yield return new WaitForSeconds(length);
            animator.enabled = false;
        }
    }

    public void EndAnimation()
    {
        if (isStarted)
        {
            animator.enabled = true;
            animator.SetBool(PARAMETER, false);
            isStarted = false;
        }
    }

    public void SetValueCount(int count)
    {
        countTMP.text = count + "ₓ";
    }

    public void SetTextTimer(string text)
    {
        timerTMP.text = text;
    }

    public void AddListnerInButton(UnityAction call)
    {
        button.onClick.AddListener(call);
    }

    public Image RingLoad => ringLoad;

    public bool HasStarted => isStarted;

    public TextMeshProUGUI CountTMP => countTMP;

    public TextMeshProUGUI TimerTMP => timerTMP;
}
