using ASP;
using ASP.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class FadeCoach : MonoBehaviour
{
    [Header(Header.MANAGEABLE, order = 0)]
    [Space(-10, order = 1)]
    [Header(Header.variables, order = 2)]
    [Space(-10, order = 3)]
    [Header(Header.animationClips, order = 4)]
    [SerializeField] private AnimationClip coachToMuscles;
    [SerializeField] private AnimationClip musclesToCoach;
    [Space(-10, order = 0)]
    [Header(Header.sprites, order = 1)]
    [SerializeField] private Sprite plus;
    [SerializeField] private Sprite minus;

    [Header(Header.READONLY, order = 0)]
    [Space(-10, order = 1)]
    [Header(Header.components, order = 2)]
    [Space(-10, order = 3)]
    [Header(Header.buttons, order = 4)]
    [SerializeField, ReadOnly] private Button lupaBotton;
    [SerializeField, ReadOnly] private Button legButton;
    [SerializeField, ReadOnly] private Button closeButton;
    [SerializeField, ReadOnly] private Button plusButton;
    [Space(-10, order = 0)]
    [Header(Header.animators, order = 1)]
    [SerializeField, ReadOnly] private Animator fadeAnimator;
    [SerializeField, ReadOnly] private Animation legAnimation;
    [Space(-10, order = 0)]
    [Header(Header.variables, order = 1)]
    [SerializeField, ReadOnly] private bool lupaIsPressed;
    [SerializeField, ReadOnly] private bool plusIsPressed;

    [Header(Header.scripts, order = 0)]
    [SerializeField, ReadOnly] private FloatMenu floatMenu;
    [SerializeField, ReadOnly] private DropDownMenu dropDownMenu;

    private void Awake()
    {
        ComponentsAssignment();
    }
    /// This function is called when the object becomes enabled and active.
    private void OnEnable()
    {
        legButton.onClick.AddListener(() => ButtonLeg());
        closeButton.onClick.AddListener(() => ButtonClose());
        lupaBotton.onClick.AddListener(() => ButtonLupa(lupaIsPressed = !lupaIsPressed));
        plusButton.onClick.AddListener(() => ButtonPlus(plusIsPressed = !plusIsPressed));
    }

    private void Start()
    {
        lupaBotton.gameObject.SetActive(false);
        plusButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (lupaIsPressed)
            plusButton.transform.parent.LookAt(Camera.main.transform);
    }
    /// <summary>
    /// Assignment of components and variables
    /// </summary>
    [ContextMenu("Components Assignment")]
    private void ComponentsAssignment()
    {
        if (floatMenu.IsNull())
            floatMenu = GetComponent<FloatMenu>();
        if (dropDownMenu.IsNull())
            dropDownMenu = GetComponent<DropDownMenu>();
        if (lupaBotton.IsNull())
            lupaBotton = GameObject.Find("btnLegLupa").GetComponent<Button>();
        if (legButton.IsNull())
            legButton = GameObject.Find("btnFloat3").GetComponent<Button>();
        if (closeButton.IsNull())
            closeButton = GameObject.Find("btnCloseDropDown").GetComponent<Button>();
        if (plusButton.IsNull())
            plusButton = GameObject.Find("btnPlus").GetComponent<Button>();
        if (fadeAnimator.IsNull())
            fadeAnimator = GameObject.Find("aniFade").GetComponent<Animator>();
        if (legAnimation.IsNull())
            legAnimation = GameObject.Find("leg").GetComponent<Animation>();

        ASP.SceneManagement.SceneManager.SaveCurrentScene();
    }

    public void ButtonLeg()
    {
        lupaBotton.gameObject.SetActive(true);
    }

    public void ButtonClose()
    {
        lupaBotton.gameObject.SetActive(false);
    }

    public void ButtonPlus(bool isPressed)
    {
        plusButton.image.sprite = isPressed ? minus : plus;
        legAnimation.Play(isPressed ? "expand" : "join");
        isPressed.ComparativeAssignment(ref plusIsPressed);
    }

    public void ButtonLupa(bool isPressed)
    {
        dropDownMenu.ButtonCloseDropDown();
        floatMenu.SetBoolFloatAnimators(!isPressed);
        dropDownMenu.Interactable = !isPressed;
        fadeAnimator.Play(isPressed ? coachToMuscles.name : musclesToCoach.name);

        lupaBotton.gameObject.SetActive(isPressed);
        plusButton.gameObject.SetActive(isPressed);

        isPressed.ComparativeAssignment(ref lupaIsPressed);
    }
}
