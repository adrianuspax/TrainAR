using ASP;
using ASP.Extensions;
using TMPro;
using TreinAR;
using TreinAR.UserData.Pages;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsMain : MonoBehaviour
{
    [Header(Header.READONLY, order = 0)]
    [Space(-10, order = 1)]
    [Header(Header.components, order = 2)]
    [SerializeField, ReadOnly] private Button buttonClose;
    [SerializeField, ReadOnly] private Button buttonNext;
    [SerializeField, ReadOnly] private Button buttonBack;
    [SerializeField, ReadOnly] private TextMeshProUGUI nextTMP;
    [Header(Header.scripts, order = 0)]
    [SerializeField, ReadOnly] private PagesControl pagesControl;
    /// Awake is called when an enabled script instance is being loaded.
    private void Awake()
    {
        ComponentsAssignment();
    }
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    private void Start()
    {
        SetNextButtonInteractable(false);
    }
    /// This function is called when the object becomes enabled and active.
    private void OnEnable()
    {
        buttonClose.onClick.AddListener(Close);
        buttonNext.onClick.AddListener(Next);
        buttonBack.onClick.AddListener(Back);
    }
    /// <summary>
    /// Assignment of components and variables
    /// </summary>
    [ContextMenu("Components Assignment")]
    private void ComponentsAssignment()
    {
        this.GetComponentIfNull(ref buttonClose, 0, 0);
        this.GetComponentIfNull(ref buttonNext, 1, 0);
        this.GetComponentIfNull(ref buttonBack, 1, 1);
        buttonNext.GetComponentInChildrenIfNull(ref nextTMP);
        if (pagesControl.IsNull())
            pagesControl = FindAnyObjectByType<PagesControl>();

        ASP.SceneManagement.SceneManager.SaveCurrentScene();
    }

    public void Close()
    {
        ButtonsManager.CloseApplication();
    }

    public void Next()
    {
        bool isNext = pagesControl.SetActiveNextPage();
        SetNextButtonInteractable(false);

        if (isNext)
            return;

        pagesControl.SaveData();

        if (TrainARSceneManager.GetPreviousSceneSaved() == TrainARSceneManager.menu)
            TrainARSceneManager.LoadMenu();
        else
            TrainARSceneManager.LoadTrainAR();
    }

    public void Back()
    {
        TrainARSceneManager.LoadMenu();
    }

    public void SetNextButtonInteractable(bool value)
    {
        if (buttonNext.IsInteractable() == value)
            return;

        buttonNext.interactable = value;
        nextTMP.SetAlpha(value ? 1f : 0.5f);
    }
}
