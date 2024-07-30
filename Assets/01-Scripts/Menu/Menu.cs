using ASP;
using ASP.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header(Header.READONLY, order = 0)]
    [Space(-10, order = 1)]
    [Header(Header.components, order = 2)]
    [SerializeField, ReadOnly] private TextMeshProUGUI usernameTMP;
    [Space(-10, order = 3)]
    [Header(Header.buttons, order = 4)]
    [SerializeField, NonReorderable, ReadOnly] private Button[] buttons;

    [Header(Header.scripts, order = 0)]
    [SerializeField, ReadOnly] private QuestionPanel questionPanel;
    [SerializeField, ReadOnly] private Transition transition;

    public const int CLOSE = 0;
    public const int UPDATEPROFILE = 1;
    public const int EDITACCOUNT = 2;
    public const int CHANGELANGUAGE = 3;
    public const int DELETEACCOUNT = 4;
    public const int SIGNOUT = 5;
    public const int LEFT = 6;

    private void Awake()
    {
        ComponentsAssignment();
    }
    /// This function is called when the object becomes enabled and active.
    private void OnEnable()
    {
        buttons[CLOSE].onClick.AddListener(ButtonsManager.CloseApplication);
        buttons[UPDATEPROFILE].onClick.AddListener(UpdateProfile);
        buttons[EDITACCOUNT].onClick.AddListener(EditAccount);
        buttons[CHANGELANGUAGE].onClick.AddListener(Language);
        buttons[DELETEACCOUNT].onClick.AddListener(questionPanel.OpenPanel);
        buttons[SIGNOUT].onClick.AddListener(SignOut);
        buttons[LEFT].onClick.AddListener(Back);
    }

    private void Start()
    {
        usernameTMP.text = AccountData.Username;
        transition.EndClip();
    }
    /// <summary>
    /// Assignment of components and variables
    /// </summary>
    [ContextMenu("Components Assignment")]
    private void ComponentsAssignment()
    {
        if (transition.IsNull())
            transition = GameObject.FindGameObjectWithTag(Tag.Transition).GetComponent<Transition>();
        if (questionPanel.IsNull())
            questionPanel = FindAnyObjectByType<QuestionPanel>();
        this.GetComponentIfNull(ref usernameTMP, 1, 0, 1);
        this.GetComponentsInAllChildrenIfNull(ref buttons);
        ASP.SceneManagement.SceneManager.SaveCurrentScene();
    }

    public void UpdateProfile() => TrainARSceneManager.LoadUserData();

    public void EditAccount() => TrainARSceneManager.LoadSignOn();

    public void Language() => TrainARSceneManager.LoadLanguage();

    public void SignOut()
    {
        AccountData.Signed = false;
        TrainARSceneManager.LoadSignIn();
    }

    public void Back() => TrainARSceneManager.LoadTrainAR();
}
